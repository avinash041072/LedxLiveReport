using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LedxLiveReport.Models;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LedxLiveReport.Controllers
{
    public class RegisteredUserController : Controller
    {
        private readonly ReportDataContext reportDataContext;
        public RegisteredUserController(ReportDataContext reportDataContext)
        {
            this.reportDataContext = reportDataContext;
        }
        // GET: /<controller>/
        public async Task<IActionResult> Index(int? pageNumber)
        {
            int pageSize = 15;

            return View(PaginatedList<VwRegisteredUsersLive>.CreateAsync(await reportDataContext.VwRegisteredUsersLives.OrderByDescending(t => t.UserId).Take(100).ToListAsync(), pageNumber ?? 1, pageSize));

           
        }
    }
}

