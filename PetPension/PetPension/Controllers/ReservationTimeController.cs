using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLogic.IBussinessLogic;

namespace PetPension.Controllers
{
    public class ReservationTimeController : Controller
    {
        private IReserverationTimesBL reserverationTimesBL;
        public ReservationTimeController(IReserverationTimesBL _reservationTimesBL)
        {
            reserverationTimesBL = _reservationTimesBL;
        }

        public ActionResult GetReservationTimes(int id)
        {
            var rt = reserverationTimesBL.GetClosestReservetionTimes(id, DateTime.Now, 1);
            return View(rt);
        }
    }
}