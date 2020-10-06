using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace HTML_form_generator.Models
{
    public class GeneratedCode
    {
        #region With Bootstrap
        public static StringBuilder simpleField(string type, string name)
        {
            StringBuilder code = new StringBuilder();
            code.Append("\t<div class=\"form-group\">\n");
            code.Append("\t\t<label class=\"control-label\">" + name + "</label>\n");
            code.Append("\t\t<input class=\"form-control\" type=\"" + type + "\" name=\"" + name.Replace("\"","") + "\"/>\n" +
                "\t</div>\n");
            return code;
        }//simpleField()

        public static StringBuilder radioFields(List<string> radioInputs, int number)
        {
            StringBuilder code = new StringBuilder();
            foreach (string input in radioInputs)
            {
                code.Append("\t\t<div class=\"form-check\">\n");
                code.Append("\t\t\t<input type=\"radio\" name=\"radio" + number + "\" class=\"form-check-input\" value=\"" + input.Replace("\"", "") + "\">\n");
                code.Append("\t\t\t<label class=\"form-check-label\">" + input + "</label>\n");
                code.Append("\t\t</div>\n");
            }
            return code;
        }//radioFields()

        public static StringBuilder cboxFields(string groupName, List<string> cboxInputs, int number)
        {
            StringBuilder code = new StringBuilder();
            foreach (string input in cboxInputs)
            {
                string name = input.Replace("\"", "");
                code.Append("\t\t<div class=\"form-check\">\n");
                code.Append("\t\t\t<input type=\"checkbox\" name=\"" + name + "\" class=\"form-check-input\" value=\"" + name + "\">\n");
                code.Append("\t\t\t<label class=\"form-check-label\">" + input + "</label>\n");
                code.Append("\t\t</div>\n");
            }
            return code;
        }//cboxFields()

        public static StringBuilder rangeField(string name, string min, string max)
        {
            StringBuilder code = new StringBuilder();
            code.Append("\t<div class=\"form-group\">\n");
            code.Append("\t\t<label class=\"control-label\">" + name + "</label>\n");
            code.Append("\t\t<input class=\"form-control\" type=\"range\" name=\"" + name.Replace("\"", "") + "\" min=\"" + min + "\" max=\"" + max + "\">\n");
            code.Append("\t</div>\n");
            return code;
        }//rangeField()

        public static StringBuilder radioFieldsChecked(List<string> radioInputs, int number)
        {
            StringBuilder code = new StringBuilder();
            foreach (string input in radioInputs)
            {
                code.Append("\t\t<div class=\"form-check\">\n");
                code.Append("\t\t\t<input type=\"radio\" checked name=\"radio" + number + "\" class=\"form-check-input\" value=\"" + input.Replace("\"", "") + "\">\n");
                code.Append("\t\t\t<label class=\"form-check-label\">" + input + "</label>\n");
                code.Append("\t\t</div>\n");
            }
            return code;
        }//radioFieldsChecked()
        #endregion

        #region Without Bootstrap
        public static StringBuilder simpleFieldNoBS(string type, string name)
        {
            StringBuilder code = new StringBuilder();
            code.Append("\t<label>" + name + "</label><br>\n");
            code.Append("\t<input type=\"" + type + "\" name=\"" + name.Replace("\"", "") + "\"/><br>\n");
            return code;
        }//simpleFieldNoBS()

        public static StringBuilder radioFieldsNoBS(List<string> radioInputs, int number)
        {
            StringBuilder code = new StringBuilder();
            foreach (string input in radioInputs)
            {
                code.Append("\t<input type=\"radio\" name=\"radio" + number + "\" value=\"" + input.Replace("\"", "") + "\">\n");
                code.Append("\t<label>" + input + "</label><br>\n");
            }
            return code;
        }//radioFieldsNoBS()

        public static StringBuilder cboxFieldsNoBS(string groupName, List<string> cboxInputs, int number)
        {
            StringBuilder code = new StringBuilder();
            foreach (string input in cboxInputs)
            {
                string name = input.Replace("\"", "");
                code.Append("\t<input type=\"checkbox\" name=\"" + name + "\" value=\"" + name + "\">\n");
                code.Append("\t<label>" + input + "</label><br>\n");
            }
            return code;
        }//cboxFieldsNoBS()

        public static StringBuilder rangeFieldNoBS(string name, string min, string max)
        {
            StringBuilder code = new StringBuilder();
            code.Append("\t<label>" + name + "</label><br>\n");
            code.Append("\t<input type=\"range\" name=\"" + name.Replace("\"", "") + "\" min=\"" + min + "\" max=\"" + max + "\"><br>\n");
            return code;
        }//rangeFieldNoBS()

        public static StringBuilder radioFieldsNoBSChecked(List<string> radioInputs, int number)
        {
            StringBuilder code = new StringBuilder();
            foreach (string input in radioInputs)
            {
                code.Append("\t<input type=\"radio\" checked name=\"radio" + number + "\" value=\"" + input.Replace("\"", "") + "\">\n");
                code.Append("\t<label>" + input + "</label><br>\n");
            }
            return code;
        }//radioFieldsNoBSChecked()
        #endregion
    }
}