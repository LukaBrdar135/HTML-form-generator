using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HTML_form_generator.Models
{
    public class CodeHistory
    {
        public int id { get; set; }
        public DateTime Date { get; set; }
        public string Code { get; set; }
        public ApplicationUser User { get; set; }
        public string UserId { get; set; }
    }
}