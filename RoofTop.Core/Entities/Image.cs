using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoofTop.Core.Entities
{
    public class Image
    {
        public int Id { get; set; }
        [StringLength(128)]
        public string FileName { get; set; }
        [StringLength(128)]
        public string Caption { get; set; }
        public Guid RealEstateAd_Id { get; set; }

        [ForeignKey("RealEstateAd_Id")]
        public virtual RealEstateAd RealEstateAd { get; set; }
    }
}
