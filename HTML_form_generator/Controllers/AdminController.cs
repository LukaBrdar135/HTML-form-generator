using HTML_form_generator.Models;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HTML_form_generator.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        ApplicationDbContext _context;

        public AdminController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult allStyles()
        {
            List<Style> allStyles = _context.Styles.ToList();
            allStyles.Remove(allStyles.FirstOrDefault(s => s.id == 1));
            return View(allStyles);
        }//AllSyles()


        public ActionResult newStyle()
        {
            Style style = new Style();
            return View(style);
        }//newStyle()

        [HttpPost]
        public ActionResult newStyle(Style style)
        {
            if(ModelState.IsValid)
            {
                if(style.Code==null || style.Code.Trim() == "")
                {
                    ViewBag.Error = "All fields must be filled.";
                    return View(style);
                }
                _context.Styles.Add(style);
                _context.SaveChanges();

                return RedirectToAction("allStyles");
            }

            TempData["success"] = "Successfully added new style";

            return RedirectToAction("allStyles");
        }//newStyle()

        public ActionResult editStyle(int id)
        {
            Style style = _context.Styles.FirstOrDefault(s => s.id == id);
            return View(style);
        }//StyleDetails()

        [HttpPost]
        public ActionResult editStyle(Style style)
        {
            if (ModelState.IsValid)
            {
                if (style.Code == null || style.Code.Trim() == "")
                {
                    ViewBag.Error = "All fields must be filled.";
                    return View(style);
                }
                _context.Entry(style).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();
                ViewBag.Success = "Successfully edited style";
                return View();
            }
            ViewBag.Error = "Error";
            return View();
        }//editStyle()

        public ActionResult deleteStyle(int id)
        {
            Style style = _context.Styles.FirstOrDefault(s => s.id == id);
            if (style != null)
            {
                List<PreMadeForm> preMadeForms = _context.PreMadeForms.Where(p => p.StyleId == id).ToList();
                foreach (PreMadeForm p in preMadeForms)
                {
                    p.StyleId = 1;
                }
                _context.Styles.Remove(style);
                _context.SaveChanges();

                TempData["success"] = "Successfully deleted style";
            }

            return RedirectToAction("allStyles");
        }//deleteStyle()

        public ActionResult preMadeForms()
        {
            List<PreMadeForm> preMadeForms = _context.PreMadeForms.Include(p => p.Style).ToList();
            foreach(PreMadeForm p in preMadeForms)
            {
                p.Code += "\n\n<style>\n" + p.Style.Code+"\n</style>";
            }
            ViewBag.Styles = _context.Styles.ToList();
            return View("preMadeFormsAdmin",preMadeForms);
        }//preMadeForms()
        

        public ActionResult newPremadeForm()
        {
            ViewBag.Styles = _context.Styles.ToList();
            return View();
        }//newPremadeForm()

        public ActionResult deletePreMadeForm(int id)
        {
            PreMadeForm form =_context.PreMadeForms.FirstOrDefault(f => f.id == id);
            if(form != null)
            {
                _context.PreMadeForms.Remove(form);
                _context.SaveChanges();
            }

            TempData["success"] = "Successfully deleted pre-made form";

            return RedirectToAction("preMadeForms");
        }//deletePreMadeForm()

        [HttpPost]
        public ActionResult changePremadeFormStyle()
        {
            int formID = Convert.ToInt32(Request["formId"]);
            PreMadeForm form = _context.PreMadeForms.FirstOrDefault(f => f.id == formID);
            form.StyleId = Convert.ToInt32(Request["style"]);

            _context.Entry(form).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();

            TempData["success"] = "Successfully changed style";

            return RedirectToAction("preMadeForms");
        }//changePremadeFormStyle()

        public ActionResult allUsers()
        {
            var allUsers = _context.Users.Where(u=>u.Email != "admin@app.com").ToList();

            return View(allUsers);
        }//allUsers()

        
        public ActionResult editUser(string id)
        {
            ApplicationUser user = _context.Users.FirstOrDefault(u => u.Id == id);

            EditAccountViewModel model = new EditAccountViewModel();
            model.Email = user.Email;
            model.FirstName = user.FirstName;
            model.LastName = user.LastName;

            ViewBag.userId = user.Id;

            return View(model);
        }//editUser()

        [HttpPost]
        public ActionResult editUser(EditAccountViewModel model)
        {
            if (ModelState.IsValid)
            {
                string userId = Request.Form["userId"];
                ApplicationUser user = _context.Users.FirstOrDefault(u => u.Id == userId);

                user.Email = model.Email;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.UserName = model.Email;

                _context.Entry(user).State = System.Data.Entity.EntityState.Modified;

                _context.SaveChanges();

                TempData["success"] = "Successfully edited user";

                return RedirectToAction("allUsers");
            }
            return View(model);           
        }//editUser()

        
        public ActionResult deleteUser(string id)
        {
            ApplicationUser user = _context.Users.FirstOrDefault(u => u.Id == id);

            List<CodeHistory> codeHistories = _context.CodeHistories.Where(u => u.UserId == id).ToList();
            _context.CodeHistories.RemoveRange(codeHistories);
     
            _context.Users.Remove(user);
            _context.SaveChanges();

            TempData["success"] = "Successfully deleted user";

            return RedirectToAction("allUsers");
        }//deleteUser()
    }
}