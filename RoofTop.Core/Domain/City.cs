using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoofTop.Core.Domain
{
    public class City
    {
        public int Id { get; set; }
        [StringLength(100), Display(Name = "City Name")]
        public string Name { get; set; }
        public string Region { get; set; }
    }
}
