using HTML_form_generator.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace HTML_form_generator.Controllers
{
    [Authorize]
    public class CodeHistoryController : Controller
    {
        ApplicationDbContext _context;

        public CodeHistoryController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            ApplicationUser user = getUser();
            List<CodeHistory> codeHistory = _context.CodeHistories.Where(c=>c.UserId == user.Id).ToList();
            return View(codeHistory);
        }//index()

        public ActionResult viewCodeFromHistory(int id)
        {
            CodeHistory codeHistory = _context.CodeHistories.FirstOrDefault(c => c.id == id);
            ApplicationUser user = getUser();
            if(codeHistory==null || codeHistory.UserId != user.Id)
            {
                return View("Error");
            }
            else
            {
                return View(codeHistory);
            }
        }//viewCodeFromHistory()

        public ActionResult deleteOneCodeFromHistory(int id)
        {
            CodeHistory codeHistory = _context.CodeHistories.FirstOrDefault(c => c.id == id);
            ApplicationUser user = getUser();
            if (codeHistory.UserId != user.Id)
            {
                return View("Error");
            }
            else
            {
                _context.CodeHistories.Remove(codeHistory);
                _context.SaveChanges();

                TempData["success"] = "Successfully deleted code from history";

                return RedirectToAction("Index");
            }
        }//deleteOneCodeFromHistory()

        public ActionResult deleteCodeHistory()
        {
            ApplicationUser user = getUser();
            List<CodeHistory> codeHistory = _context.CodeHistories.Where(c => c.UserId == user.Id).ToList();
            _context.CodeHistories.RemoveRange(codeHistory);
            _context.SaveChanges();

            TempData["success"] = "Successfully deleted history";

            return RedirectToAction("Index");
        }//deleteCodeHistory()

        #region Reusable Methods
        public ApplicationUser getUser()
        {
            var appUser = System.Web.HttpContext.Current.User.Identity.GetUserName();
            ApplicationUser user = _context.Users.FirstOrDefault(u => u.Email == appUser);
            return user;
        }//getUser()
        #endregion
    }
}