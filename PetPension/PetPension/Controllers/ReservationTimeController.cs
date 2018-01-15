using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLogic.IBussinessLogic;
using PetPension.Models;

namespace PetPension.Controllers
{
    public class ReservationTimeController : Controller
    {
        private IReserverationTimesBL reserverationTimesBL;
        public ReservationTimeController(IReserverationTimesBL _reservationTimesBL)
        {
            reserverationTimesBL = _reservationTimesBL;
        }

        public ActionResult Index()
        {
            return View("Index");
        }
        public ActionResult ReseveInTimeFrame(DateTime startDate,DateTime endTime)
        {
            var model = new NewReservationViewModel()
            {
                MaxStarTime = startDate,
                MaxEndTime = endTime
            };
            return View(model);
        }
        public ActionResult GetReservationTimes(int id)
        {
         
            return View();
        }

        public ActionResult ReserveTime(NewReservationViewModel model)
        {
            var reservationResult = reserverationTimesBL.ReserveTime(1, model.StarTime, (model.EndTime - model.StarTime).Days);

            if (reservationResult != null && reservationResult.Any())
            {
                return View("FreeTimes",model);
            }
            else
            {
                return View("ReservationSuccess");
            }

        }
    }
}