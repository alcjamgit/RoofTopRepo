﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace RoofTop.Web.Models
{
    public class CreateAdViewModel
    {
        [Required()] [StringLength(128)]
        public string Title { get; set; }
        public decimal Price { get; set; }
        [DisplayName("Description"), AllowHtml]
        public string HtmlContent { get; set; }
        [DisplayName("City"), Required]
        public virtual int City_Id { get; set; }
        [Range(0, int.MaxValue, ErrorMessage  ="Must be greater than zero")]
        public int? RoomCount { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Must be greater than zero")]
        public int? BathCount { get; set; }

        //This will serve as the dropdown (but wont be persisted)
        public IEnumerable<SelectListItem> Cities { get; set; }
        public HttpPostedFileBase PostedImage { get; set; }
    }
    public class DisplayAdViewModel
    {
        public string Title { get; set; }
        public virtual int Address { get; set; }
    }

    public class DetailsAdViewModel
    {
        public string Title { get; set; }
        public decimal Price { get; set; }
        [DisplayName("Description"), AllowHtml]
        public string HtmlContent { get; set; }
        [DisplayName("Rooms")]
        public int? RoomCount { get; set; }
        [DisplayName("Baths")]
        public int? BathCount { get; set; }
        [DisplayName("City")]
        public string CityName { get; set; }
        public string ImageUrl { get; set; }

    }

}
