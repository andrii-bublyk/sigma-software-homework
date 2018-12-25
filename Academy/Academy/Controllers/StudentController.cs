using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Academy.Controllers
{
    public class StudentController : Controller
    {
        [Authorize(Roles = "admin")]
        public IActionResult Students()
        {
            return View();
        }
    }
}