using HTML_form_generator.Models;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections.Specialized;
using System.Text;
using Microsoft.AspNet.Identity;

namespace HTML_form_generator.Controllers
{
    public class GeneratorController : Controller
    {
        ApplicationDbContext _context;

        public GeneratorController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult preMadeForms()
        {
            List<PreMadeForm> preMadeForms = _context.PreMadeForms.Include(p => p.Style).ToList();
            foreach (PreMadeForm p in preMadeForms)
            {
                p.Code += "\n\n<style>\n" + p.Style.Code + "\n</style>";
            }

            ViewBag.Categories = _context.PreMadeForms.Select(s => s.Category).Distinct().ToList();

            return View(preMadeForms);
        }//preMadeForms()

        public ActionResult preMadeFormCode(int id)
        {
            PreMadeForm form = _context.PreMadeForms.Include(p => p.Style).FirstOrDefault(f => f.id == id);
            if(form == null)
            {
                return View("Error");
            }
            if(form.Style.id != 1)
            {
                form.Code += "\n\n<style>\n" + form.Style.Code + "\n</style>"; 
            }

            return View(form);
        }//preMadeFormCode()

        [Authorize]
        public ActionResult customForm()
        {
            ViewBag.Styles = _context.Styles.ToList();
            return View();
        }//customForm()


        [HttpPost]
        public ActionResult customFormPost()
        {
            NameValueCollection nvc = Request.Form;

            StringBuilder code = new StringBuilder();
            
            if (!string.IsNullOrEmpty(nvc["numberOfSelects"]))
            {
                bool bootstrap = false;
                if (Validation.validate(nvc))
                {
                    if (nvc["cb"] != null)
                    {
                        code.Append(generateWithBootstrap(nvc));
                        bootstrap = true;
                    }
                    else
                    {
                        //generate code without bootstrap
                        code.Append(generateNoBootstrap(nvc));
                    }
                }
                else
                {
                    //validation error
                    return View("Error");
                }
                
                code.Append("</form>");


                string selectedStyle = nvc["styleSelect"].ToString();
                Style style;
                if (selectedStyle == "/")
                {
                    style = _context.Styles.FirstOrDefault(s => s.Name == "None");
                }
                else
                {
                    style = _context.Styles.FirstOrDefault(s => s.Code == selectedStyle);
                }

                ApplicationUser user = getUser();
                if (user.Email == "admin@app.com")
                {
                    PreMadeForm form = new PreMadeForm()
                    {
                        Name = nvc["formName"],
                        Category = nvc["formCategory"],
                        withBootstrap = bootstrap,
                        Code = code.ToString(),
                        StyleId = style.id
                    };

                    if (nvc["free"] != null)
                    {
                        form.Free = true;
                    }
                    else
                    {
                        form.Free = false;
                    }

                    _context.PreMadeForms.Add(form);
                    _context.SaveChanges();
                }
                

                if(selectedStyle != "/")
                {
                    code.Append("\n\n<style>");
                    code.Append("\n" + style.Code);
                    code.Append("\n</style>");
                }

                if (user.Email != "admin@app.com")
                {
                    CodeHistory codeHistory = new CodeHistory()
                    {
                        UserId = user.Id,
                        Code = code.ToString(),
                        Date = DateTime.Now
                    };

                    _context.CodeHistories.Add(codeHistory);
                    _context.SaveChanges();
                }

                ViewBag.Code = code;

                //success page
                return View();
            }
            else
            {
                //error
                return View("Error");
            }
        }//customFormPost()
       

        #region ReusableMethods
        public ApplicationUser getUser()
        {
            var appUser = System.Web.HttpContext.Current.User.Identity.GetUserName();
            ApplicationUser user = _context.Users.FirstOrDefault(u => u.Email == appUser);
            return user;
        }//getUser()

        public StringBuilder generateWithBootstrap(NameValueCollection nvc)
        {
            StringBuilder code = new StringBuilder();
            code.Append("<form id=\"form\">\n");
            int numberOfSelects = Convert.ToInt32(nvc["numberOfSelects"]);
            for (int i = 0; i < numberOfSelects; i++)
            {
                if (nvc["select" + i] != "radio" && nvc["select" + i] != "checkbox" && nvc["select" + i] != "range")
                {
                    code.Append(GeneratedCode.simpleField(nvc["select" + i], nvc["input" + i]));
                }
                else if (nvc["select" + i] == "radio")
                {
                    int radioNumber = Convert.ToInt32(nvc["radioNumber" + i]);

                    code.Append("\t<div class=\"form-group\">\n");
                    code.Append("\t\t<label class=\"control-label\">" + nvc["radioGroupName" + i] + "</label>\n");

                    for (int j = 0; j < radioNumber; j++)
                    {
                        List<string> radioInputs = new List<string>();
                        radioInputs.Add(nvc["radio" + i + "input" + j]);
                        if (j == 0)
                        {
                            code.Append(GeneratedCode.radioFieldsChecked(radioInputs, i));
                        }
                        else
                        {
                            code.Append(GeneratedCode.radioFields(radioInputs, i));
                        }
                    }
                    code.Append("\t</div>\n");
                }
                else if (nvc["select" + i] == "checkbox")
                {
                    code.Append("\t<div class=\"form-group\">\n");
                    code.Append("\t\t<label class=\"control-label\">" + nvc["cboxGroupName" + i] + "</label>\n");
                    int cboxNumber = Convert.ToInt32(nvc["cboxNumber" + i]);
                    for (int j = 0; j < cboxNumber; j++)
                    {
                        List<string> cboxInputs = new List<string>();
                        cboxInputs.Add(nvc["cbox" + i + "input" + j]);
                        code.Append(GeneratedCode.cboxFields(nvc["cboxGroupName" + i], cboxInputs, i));
                    }
                    code.Append("\t</div>\n");
                }
                else if (nvc["select" + i] == "range")
                {
                    code.Append(GeneratedCode.rangeField(nvc["rangeName" + i], nvc["rangeMin" + i], nvc["rangeMax" + i]));
                }
            }
            code.Append("\t<input id=\"submitBtn\" type=\"submit\" value=\"" + nvc["submitButton"].Replace("\"", "") + "\" class=\"btn\" />\n");
            return code;
        }//generateWithBootstrap()


        public StringBuilder generateNoBootstrap(NameValueCollection nvc)
        {
            StringBuilder code = new StringBuilder();
            code.Append("<form id=\"form\">\n");
            int numberOfSelects = Convert.ToInt32(nvc["numberOfSelects"]);
            for (int i = 0; i < numberOfSelects; i++)
            {
                if (nvc["select" + i] != "radio" && nvc["select" + i] != "checkbox" && nvc["select" + i] != "range")
                {
                    code.Append(GeneratedCode.simpleFieldNoBS(nvc["select" + i], nvc["input" + i]));
                }
                else if (nvc["select" + i] == "radio")
                {
                    int radioNumber = Convert.ToInt32(nvc["radioNumber" + i]);
                    
                    code.Append("\t<label>" + nvc["radioGroupName" + i] + "</label><br>\n");

                    for (int j = 0; j < radioNumber; j++)
                    {
                        List<string> radioInputs = new List<string>();
                        radioInputs.Add(nvc["radio" + i + "input" + j]);
                        if (j == 0)
                        {
                            code.Append(GeneratedCode.radioFieldsNoBSChecked(radioInputs, i));
                        }
                        else
                        {
                            code.Append(GeneratedCode.radioFieldsNoBS(radioInputs, i));
                        }
                    }
                }
                else if (nvc["select" + i] == "checkbox")
                {
                    code.Append("\t<label>" + nvc["cboxGroupName" + i] + "</label><br>\n");
                    int cboxNumber = Convert.ToInt32(nvc["cboxNumber" + i]);
                    for (int j = 0; j < cboxNumber; j++)
                    {
                        List<string> cboxInputs = new List<string>();
                        cboxInputs.Add(nvc["cbox" + i + "input" + j]);
                        code.Append(GeneratedCode.cboxFieldsNoBS(nvc["cboxGroupName" + i], cboxInputs, i));
                    }
                }
                else if (nvc["select" + i] == "range")
                {
                    code.Append(GeneratedCode.rangeFieldNoBS(nvc["rangeName" + i], nvc["rangeMin" + i], nvc["rangeMax" + i]));
                }
            }
            code.Append("\t<input id=\"submitBtn\" type=\"submit\" value=\"" + nvc["submitButton"].Replace("\"", "") + "\" />\n");
            return code;
        }//generateNoBootstrap()
        #endregion
    }
}