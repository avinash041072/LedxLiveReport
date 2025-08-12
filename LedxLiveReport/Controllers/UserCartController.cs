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
    public class UserCartController : Controller
    {
        private readonly ReportDataContext reportDataContext;
        public UserCartController(ReportDataContext reportDataContext)
        {
            this.reportDataContext = reportDataContext;
        }
        // GET: /<controller>/
        public async Task<IActionResult> Index(int? pageNumber)
        {
            int pageSize = 15;

            return View(PaginatedList<VwUserCart>.CreateAsync(await reportDataContext.VwUserCarts.OrderByDescending(t => t.Id).Take(100).ToListAsync(), pageNumber ?? 1, pageSize));
            //var userCarts= await reportDataContext.VwUserCarts.OrderByDescending(t => t.Id)
            // .Take(25).ToListAsync();

           // return View(userCarts);
        }



    }
}

