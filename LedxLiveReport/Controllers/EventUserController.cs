using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LedxLiveReport.Models;


using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LedxLiveReport.Controllers
{
    public class EventUserController : Controller
    {
        private readonly ReportDataContext reportDataContext;
        public EventUserController(ReportDataContext reportDataContext)
        {
            this.reportDataContext = reportDataContext;
        }

        // GET: /<controller>/
        public async Task<IActionResult> Index(int? pageNumber)
        {
            //@Model.Where(p => p.Status == "Free").Count()


            ViewData["TotalCount"] = reportDataContext.VwEventUsers.Count();
            ViewData["FreeCount"] = reportDataContext.VwEventUsers.Where(p => p.Status == "Free").Count();
            ViewData["PaidCount"] = reportDataContext.VwEventUsers.Where(p => p.Status == "Paid").Count();

            int pageSize = 15;

            return View(PaginatedList<VwEventUser>.CreateAsync(await reportDataContext.VwEventUsers.OrderByDescending(t => t.CreatedDate).ToListAsync(), pageNumber ?? 1, pageSize));

           // var users = await reportDataContext.VwEventUsers.OrderByDescending(t => t.Id)
           /// .Take(25).ToListAsync();

           // return View(users);
        }

        public async Task<IActionResult> PaidUsers(int? pageNumber, bool paidUser)
        {
            //@Model.Where(p => p.Status == "Free").Count()


            ViewData["TotalCount"] = reportDataContext.VwEventUsers.Count();
            ViewData["FreeCount"] = reportDataContext.VwEventUsers.Where(p => p.Status == "Free").Count();
            ViewData["PaidCount"] = reportDataContext.VwEventUsers.Where(p => p.Status == "Paid").Count();

            int pageSize = 15;

            if (paidUser == true)
            {

                return View(PaginatedList<VwEventUser>.CreateAsync(await reportDataContext.VwEventUsers.Where(p => p.Status == "Paid").OrderByDescending(t => t.CreatedDate).ToListAsync(), pageNumber ?? 1, pageSize));
            }
            else
            {
                return View(PaginatedList<VwEventUser>.CreateAsync(await reportDataContext.VwEventUsers.OrderByDescending(t => t.CreatedDate).ToListAsync(), pageNumber ?? 1, pageSize));
            }
            // var users = await reportDataContext.VwEventUsers.OrderByDescending(t => t.Id)
            /// .Take(25).ToListAsync();

            // return View(users);
        }


    }
}

