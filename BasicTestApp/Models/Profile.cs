using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BasicTestApp.Models
{
    public class ModelProfile
    {
        [Key]
        public int ID { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }       
        public DateTime DOB{ get; set; }
        public string UserID { get; set; }
  
    }
}