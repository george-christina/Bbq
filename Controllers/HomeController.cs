using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Bbq.Models;
using Microsoft.EntityFrameworkCore;

namespace Bbq.Controllers
{
    public class HomeController : Controller
    {
        private MyContext dbContext;
        public HomeController(MyContext context)
        {
            dbContext = context;
        }
        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("register")]
        public IActionResult Register(User register)
        {
            if (ModelState.IsValid)
            {
                if (dbContext.Users.Any(u => u.Email == register.Email))
                {
                    ModelState.AddModelError("Email", "Email already in use");
                    return View("Index");
                }
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                register.Password = Hasher.HashPassword(register, register.Password);
                dbContext.Users.Add(register);
                dbContext.SaveChanges();
                HttpContext.Session.SetInt32("UserId", register.UserId);
                return RedirectToAction("Success");
            }
            else
            {
                return View("Index");
            }
        }
        [HttpPost("login")]
        public IActionResult Login(LoginUser login)
        {
            if (ModelState.IsValid)
            {
                var userDb = dbContext.Users.FirstOrDefault(u => u.Email == login.LoginEmail);
                var hasher = new PasswordHasher<LoginUser>();
                var result = hasher.VerifyHashedPassword(login, userDb.Password, login.LoginPassword);
                if (userDb == null || result == 0)
                {
                    Console.WriteLine("Made it in if");
                    ModelState.AddModelError("LoginEmail", "Invalid Email/Password");
                    return View("Index");
                }
                Console.WriteLine("Made it past");
                HttpContext.Session.SetInt32("UserId", userDb.UserId);
                return RedirectToAction("Success");
            }
            else
            {
                return View("Index");
            }
        }

        public User loggedInUser()
        {
            int? loggedId = HttpContext.Session.GetInt32("UserId");
            User logged = dbContext.Users.FirstOrDefault(u => u.UserId == loggedId);
            return logged;
        }

        [HttpGet("success")]
        public IActionResult Success()
        {
            Console.WriteLine(loggedInUser());
            if (loggedInUser() == null)
            {
                Console.WriteLine("success failed");
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.User = loggedInUser();
                List<BbqEvent> AllBbqEvents = dbContext.BbqEvents
                    .Include(b => b.Planner)
                    .Include(b => b.GuestList)
                    .ThenInclude(r => r.Guest)
                    .ToList();
                return View(AllBbqEvents);
            }
        }

        [HttpGet("newBbq")]
        public IActionResult NewBbq()
        {
            ViewBag.User = loggedInUser();
            return View();
        }

        [HttpPost("createBbq")]
        public IActionResult CreateBbq(BbqEvent newBbqEvent)
        {
            if(ModelState.IsValid)
            {
                dbContext.Add(newBbqEvent);
                dbContext.SaveChanges();
                return RedirectToAction("ShowBbq",new {BbqEventId = newBbqEvent.BbqEventId});
            } else {
                ViewBag.AllBbqEvents = dbContext.BbqEvents
                    .ToList();
                return View("NewBbq");
            }
        }

        [HttpGet("showBbq/{BbqEventId}")]
        public IActionResult ShowBbq(int BbqEventId)
        {
            if (loggedInUser() == null)
            {
                Console.WriteLine("success failed");
                return RedirectToAction("Index");
            } else {
                ViewBag.show = dbContext.BbqEvents
                    .Include(b => b.Planner)
                    .Include(b => b.GuestList)
                    .ThenInclude(r => r.Guest)
                    .FirstOrDefault(b => b.BbqEventId == BbqEventId);
                ViewBag.User = loggedInUser();
                int PartyTotal =  0;
                foreach(var r in ViewBag.show.GuestList)
                {
                    PartyTotal += r.TotalGuests+1;
                }
                ViewBag.PartyTotal = PartyTotal;
                ViewBag.User = loggedInUser();
                return View();
            }
        }

        [HttpPost("rsvp")]
        public IActionResult UpdateRsvp(Rsvp newRsvp)
        {
            dbContext.Add(newRsvp);
            dbContext.SaveChanges();
            var BbqEventId = newRsvp.BbqEventId;
            // ViewBag.User = loggedInUser();
            return RedirectToAction("Success");
        }

        [HttpGet("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        
    }
}
