using BusinessLogic.IBussinessLogic;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using Database.IRepository;
using Database.Models;

namespace BusinessLogic.BusinessLogic
{
    public class ReserverationTimesBL : IReserverationTimesBL
    {
        private IReservationTimesRepository reservationTimesRepo;
        private IRoomsRepostitory roomsRepo;
        private IReservationRepository reservationRepo;
        public ReserverationTimesBL(IReservationTimesRepository _reservationTimes,IRoomsRepostitory _roomsRepo, IReservationRepository _reservationRepo)
        {
            reservationTimesRepo = _reservationTimes;
            reservationRepo = _reservationRepo;
            roomsRepo = _roomsRepo;
        }

        public List<FreeTime> ReserveTime(int roomType, DateTime startDate, int duration)
        {
            int freeRoomId;
            var isFree = CheckIfRoomIsFree(roomType, startDate, duration,out freeRoomId);

            if (isFree)
            {

                var newreservation = new Reservation()
                {
                    CreatedTime = DateTime.Now,
                    RoomId = freeRoomId,
                    ReservationTime = new ReservationTime()
                    {
                        From = startDate,
                        To = startDate.AddDays(duration),
                    }
                };
                reservationRepo.AddReservation(newreservation);
                return null;
            }
            else
            {
                var delta = GetSearchDeltaDays();
                var times = GetFreeTimes(roomType, startDate, duration, delta);
                return times;
            }
            

        }     

        public bool CheckIfRoomIsFree(int roomType, DateTime startDate, int duration,out int roomId)
        {
            var times = GetFreeTimes(roomType, startDate, duration,0);
            roomId = 0;
            var endDate = startDate.AddDays(duration);
            if (times.Any(x => x.From <= startDate && x.To >= endDate))
            {
                roomId = times.First().RoomId;
                return true;
            }

            return false;

        }

        public List<FreeTime> GetClosestfreeTimes(int roomType, DateTime startDate, DateTime endDate)
        {
            var delta = GetSearchDeltaDays();
            var times = GetFreeTimes(roomType, startDate, (endDate - startDate).Days, delta);
            return times;
        }
        private List<FreeTime> GetFreeTimes(int roomType, DateTime startDate, int duration,int delta)
        {
            var reservationTimes = reservationTimesRepo.GetReservationTimesForRoomType(roomType);

           

            var startFreeTime = startDate.AddDays(-delta);
            var endFreeTime = startDate.AddDays(duration).AddDays(delta);

            var reservationTimesIntimeFrame = reservationTimes.Where(x =>
                (x.From >= startFreeTime || x.To >= startFreeTime) && (x.To <= endFreeTime || x.From <= endFreeTime)).ToList();

            List<FreeTime> result = new List<FreeTime>();
            var rooms = reservationTimesIntimeFrame.Select(x => x.Reservation.RoomId).Distinct().ToList();
            var freeRooms = GetFreeRooms(roomType, rooms);

            foreach (var freeRoom in freeRooms)
            {
                result.Add(new FreeTime()
                {
                    RoomId = freeRoom,
                    RoomTypeId = roomType,
                    From = startDate,
                    To = startDate.AddDays(duration)
                });
            }

           
            
            foreach (var room in rooms)
            {
              

                var resPerRoom = reservationTimesIntimeFrame.Where(x => x.Reservation.RoomId == room).ToList();

                AddFirstFreeTime(result, resPerRoom, startFreeTime, roomType);
                AddLastFirstFreeTime(result, resPerRoom, endFreeTime, roomType);

                for (int i = 0; i < resPerRoom.Count(); i++)
                {
                    if (i != resPerRoom.Count() - 1)
                    {


                        var currentReservation = resPerRoom[i];
                        var nextReservation = resPerRoom[i + 1];

                        if ((nextReservation.From - currentReservation.To) >= TimeSpan.FromDays(1))
                        {
                            result.Add(new FreeTime()
                            {
                                RoomId = currentReservation.Reservation.RoomId,
                                RoomTypeId = roomType,
                                From = currentReservation.To,
                                To = nextReservation.From
                            });
                        }
                    }
                }

            }

            return MergeFreeTimes(result);
        }

        private void AddFirstFreeTime(List<FreeTime> freeTimes, List<ReservationTime> reservationTimesIntimeFrame, DateTime startFreeTime, int roomType)
        {
            if (reservationTimesIntimeFrame.Any())
            {
                var firstReservation = reservationTimesIntimeFrame.FirstOrDefault();
                if (firstReservation.From > startFreeTime)
                {
                    freeTimes.Add(new FreeTime()
                    {
                        RoomId = firstReservation.Reservation.RoomId,
                        RoomTypeId = roomType,
                        From = startFreeTime,
                        To = firstReservation.From
                    });
                }
            }
        }
        private void AddLastFirstFreeTime(List<FreeTime> freeTimes, List<ReservationTime> reservationTimesIntimeFrame, DateTime endFreeTime, int roomType)
        {
            if (reservationTimesIntimeFrame.Any())
            {
                var lastReservation = reservationTimesIntimeFrame.LastOrDefault();
                if (lastReservation.To < endFreeTime)
                {
                    freeTimes.Add(new FreeTime()
                    {
                        RoomId = lastReservation.Reservation.RoomId,
                        RoomTypeId = roomType,
                        From = lastReservation.To,
                        To = endFreeTime
                    });
                }
            }
        }
        
        private List<FreeTime> MergeFreeTimes(List<FreeTime> freeTimes)
        {
            var result = new List<FreeTime>();
            var p1merge = new List<FreeTime>();
            var ordered = freeTimes.OrderByDescending(x => x.To - x.From);

            foreach (var orderedFT in ordered)
            {
                var next = ordered.FirstOrDefault(x => x.From == orderedFT.To);
                if (next != null)
                {
                    p1merge.Add(new FreeTime()
                    {
                        From = orderedFT.From,
                        To = next.To,
                        RoomTypeId = orderedFT.RoomTypeId,
                        RoomId = orderedFT.RoomId                       
                    });
                }
                else
                {
                    p1merge.Add(new FreeTime()
                    {
                        From = orderedFT.From,
                        To = orderedFT.To,
                        RoomTypeId = orderedFT.RoomTypeId,
                        RoomId = orderedFT.RoomId
                    });
                }
            }
            foreach (var ft in p1merge.OrderByDescending(x => x.To - x.From))
            {
                var insideFt = p1merge.Where(x => x.From >= ft.From && x.To <= ft.To);
                var insideOf = result.FirstOrDefault(x => x.From <= ft.From && x.To >= ft.To);
                if (insideOf == null)
                {


                    foreach (var ift in insideFt)
                    {
                        var actual = result.FirstOrDefault(x => x.To == ft.To && x.From == ft.From);
                        if (actual == null)
                        {
                            result.Add(new FreeTime()
                            {
                                From = ft.From,
                                To = ft.To,
                                RoomTypeId = ft.RoomTypeId,
                                RoomId = ft.RoomId
                            });
                        }

                    }
                }
            }
            return result;
        }

        private List<int> GetFreeRooms(int roomType,List<int> roomsWithReservation)
        {
            var roomsPerType = roomsRepo.GetRoomsPerType(roomType);
            if (roomsPerType != null && roomsPerType.Any())
            {
                var roomsIds = roomsPerType.Select(x => x.Id);
                return roomsIds.Where(x=>!roomsWithReservation.Contains(x)).ToList();
            }
            return new List<int>();
        }
        private int GetSearchDeltaDays()
        {
            var delta = ConfigurationManager.AppSettings["SearchDeltaDays"];
            return Convert.ToInt32(delta);
        }
    }
}
