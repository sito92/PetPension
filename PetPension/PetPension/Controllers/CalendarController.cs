using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLogic.IBussinessLogic;
using Database.Models;
using DayPilot.Web.Mvc;
using DayPilot.Web.Mvc.Enums;
using DayPilot.Web.Mvc.Events.Calendar;

namespace PetPension.Controllers
{
    public class CalendarController : Controller
    {
        public IReserverationTimesBL reserverationTimesBl;
        public CalendarController(IReserverationTimesBL _reserverationTimesBl)
        {
            reserverationTimesBl = _reserverationTimesBl;
        }
        public ActionResult Backend(DateTime starTime,DateTime endTime)
        {
            return new Dpc(reserverationTimesBl,starTime,endTime).CallBack(this);
            
        }

        class Dpc : DayPilotCalendar
        {
            public IReserverationTimesBL reserverationTimesBl;
            private DateTime starTime, endTime;
            public Dpc(IReserverationTimesBL _reserverationTimesBl, DateTime _starTime, DateTime _endTime)
            {
                reserverationTimesBl = _reserverationTimesBl;
                starTime = _starTime;
                endTime = _endTime;
            }
            protected override void OnInit(InitArgs e)
            {
                UpdateWithMessage("Welcome!", CallBackUpdateType.Full);
            }
           
            protected override void OnFinish()
            {
                if (UpdateType == CallBackUpdateType.None)
                {
                    return;
                }

                DataIdField = "Id";
                DataStartField = "From";
                DataEndField = "To";
                DataTextField = "RoomId";
                DataAllDayField = "AllDay";
                var reservationTimes = reserverationTimesBl.GetClosestfreeTimes(1, starTime, endTime);


                Events = reservationTimes.AsEnumerable();
            }

        }
    }
}