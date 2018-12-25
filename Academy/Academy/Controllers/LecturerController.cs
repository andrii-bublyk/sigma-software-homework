using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Academy.Controllers
{
    public class LecturerController : Controller
    {
        public IActionResult Lecturers()
        {
            return View();
        }
    }
}