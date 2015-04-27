using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace RoofTop.Web.Models
{
    public class CreateAdViewModel
    {
        public string Title { get; set; }
        public virtual int City_Id { get; set; }
        public int RoomCount { get; set; }
        public int BathCount { get; set; }
        [HiddenInput]
        public DateTime Created { get { return DateTime.Now; } }
        [HiddenInput]
        public DateTime Modified { get { return DateTime.Now; } }
        //This will serve as the dropdown (but wont be persisted)
        public IEnumerable<SelectListItem> Cities { get; set; }
    }
    public class DetailsAdViewModel
    {
        public string Title { get; set; }
        public virtual int CityName { get; set; }
    }

}
