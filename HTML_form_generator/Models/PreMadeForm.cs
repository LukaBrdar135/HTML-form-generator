using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HTML_form_generator.Models
{
    public class PreMadeForm
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public bool withBootstrap { get; set; }
        public string Code { get; set; }
        public bool Free { get; set; }
        public Style Style { get; set; }
        public int StyleId { get; set; }
    }
}