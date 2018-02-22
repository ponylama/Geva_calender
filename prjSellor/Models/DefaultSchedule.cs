using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace prjSellor.Models
{
    public class DefaultSchedule
    {
        [Key]
        public string day { get; set; }
        public int timeSlace { get; set; }
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }

    }
}