using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Auctions.Models
{
    public class Auction
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string About { get; set; }
        public DateTime Begindate { get; set; }
        public DateTime Enddate { get; set; }
        public decimal Startprice { get; set; }
        public decimal Currentprice { get; set; }
        public decimal Minprice { get; set; }
                public int UserId { get; set; }
    }
}