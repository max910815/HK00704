using HK_Product.Data;
using HK_Product.Services;
using HK_project.Interface;
using HK_project.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HK_project.Controllers
{
    public class UserController : Controller
    {
        private readonly HKContext _ctx;
        private readonly IHashService _hashService;
        private readonly AccountServices _accountServices;
        private readonly ILogger<MemberController> _logger;

        public UserController(HKContext ctx, AccountServices accountServices, IHashService hashService, ILogger<MemberController> logger)
        {
            _ctx = ctx;
            _accountServices = accountServices;
            _hashService = hashService;
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Chooseapp()
        {
            var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var member =await _ctx.Users.FirstOrDefaultAsync(m => m.UserId == userid);
            var applications = _ctx.Application.Where(a=>a.ApplicationId!=null).ToList();

            ViewBag.Applist = applications;
            return View();

        }
        [HttpPost]
        public IActionResult Chooseapp(string applicationId, string applicationname, string parameter)
        {
            TempData["Userchooseappid"] = applicationId;
            TempData["Userchoosename"] = applicationname;
            TempData["Userchooseparameter"] = parameter;

            return RedirectToAction("Qa","User");

        }
        public IActionResult Qa()
        {
            var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var chat = _ctx.Chats.Where(c => c.UserId == userid).OrderByDescending(c=>c.ChatTime);
            var chatmaxid = chat.OrderByDescending(c => c.ChatId).FirstOrDefault();
            var Qahistory = _ctx.QAHistory.Where(q => q.ChatId == chatmaxid.ChatId).ToList();
            var chatlist = chat.ToList();
            ViewBag.Userchatlist = chatlist;
            ViewBag.ChatQaHistory = Qahistory;
            if (TempData["Userchoosename"] != null)
            {
                var choosename = TempData["Userchoosename"].ToString();
                HttpContext.Session.SetString("Userchoosename", choosename);
            }
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Creatchat()
        {
            var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var chatmaxid = _ctx.Chats.OrderByDescending(c => c.ChatId).FirstOrDefault();
            int newId = int.Parse(chatmaxid.UserId.Substring(1)) + 1;
            string newchatId = "C" + newId.ToString().PadLeft(4, '0');
            Chat chat = new Chat()
            {
                ChatId = newchatId,
                UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                ChatTime = DateTime.Now,
                ChatName = newchatId
            };
            _ctx.Chats.Add(chat);
            await _ctx.SaveChangesAsync();
            return Json(new { status = "success", message = "Chat created successfully." });
        }

        public IActionResult ChatHistory(string chatid)
        {
            var chatHistory = _ctx.QAHistory.Where(c => c.ChatId == chatid).ToList();
            return View(chatHistory);
        }

        [HttpPost]
        public IActionResult GetChatHistory(string id)
        {
            var chatHistory = _ctx.QAHistory.Where(q => q.ChatId == id).ToList();
            return Json(chatHistory);
        }

    }
}
