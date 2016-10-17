using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Blog.Models
{
    public class Map
    {
        public int ID { get; set; }
        public string Address { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
    }
}