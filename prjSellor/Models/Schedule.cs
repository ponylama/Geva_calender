using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace prjSellor.Models
{
    public class Schedule
    {
        [Key, Column(Order = 0)]
        public string userName { get; set; }
        [Key, Column(Order = 1)]
        public DateTime startDate { get; set; }

        public DateTime endDate { get; set; }

        
    }
}
