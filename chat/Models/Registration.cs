using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace chat.Models
{
    public class Registration
    {
        public string userName { get; set; }
        public string phoneNumber { get; set; }
        public string job { get; set; }
        public string jobIntroduction { get; set; }
        public byte[] img { get; set; }
    }
}