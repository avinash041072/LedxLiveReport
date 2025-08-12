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
    public class UserActivityController : Controller
    {
        private readonly ReportDataContext reportDataContext;
        


        public UserActivityController(ReportDataContext reportDataContext)
        {
            this.reportDataContext = reportDataContext;
        }
        // GET: /<controller>/


        public async Task<IActionResult> Index(int? pageNumber)
        {
            int pageSize = 15;

            return View(PaginatedList<VwUserActivityLiveToday>.CreateAsync(await reportDataContext.VwUserActivityLiveTodays.OrderByDescending(t => t.Id).Take(100).ToListAsync(), pageNumber ?? 1, pageSize));


        }
    }
}

