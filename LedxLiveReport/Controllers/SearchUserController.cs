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
    public class SearchUserController : Controller
    {
        private readonly ReportDataContext reportDataContext;
        public SearchUserController(ReportDataContext reportDataContext)
        {
            this.reportDataContext = reportDataContext;
        }
        // GET: /<controller>/
        //public async Task<IActionResult> Index()
        //{
        //    string userEmail = "avinash.mahajan.ind@gmail.com";
        //    var users = await reportDataContext.RegisteredUsers.FromSql($"sp_GetUserIdByMail {userEmail}").ToListAsync();

        //    return View(users);
        //}

        public async Task<IActionResult> Index()
        {
           string userEmail = "avinash.mahajan@ledx.law";
            var users = await reportDataContext.RegisteredUsers.FromSql($"sp_GetUserIdByMail {userEmail}").ToListAsync();

            return View(users);
        }

        [HttpPost]
        public async Task<IActionResult> Index(string UserMail)
        {
            // string userEmail = "avinash.mahajan@ledx.law";
            var users = await reportDataContext.RegisteredUsers.FromSql($"sp_GetUserIdByMail {UserMail}").ToListAsync();

            return View(users);
        }

        //window.location = '@Url.Action("CreatePerson", "Person", new { Enc = "id" })
    }
}

