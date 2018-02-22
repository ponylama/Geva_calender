using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace prjSellor.Models
{
    public class Breaks
    {
        [Key, Column(Order = 0)]
        public string day { get; set; }
        [Key, Column(Order = 1)]
        public DateTime startTime { get; set; }
        [Key, Column(Order = 2)]
        public DateTime endTime { get; set; }
    }
}
