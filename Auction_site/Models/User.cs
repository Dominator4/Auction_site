using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Auction_site.Models
{
    public class User
    {
        public int Id { get; set; }
        public String Firstname { get; set; }
        public String Lastname { get; set; }
        public string Login { get; set; }
        public string Pass { get; set; }
        public string Permissions { get; set; }
    }
}