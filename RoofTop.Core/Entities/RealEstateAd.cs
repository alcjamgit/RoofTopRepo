using RoofTop.Core.DomainServices;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoofTop.Core.Entities
{
    public class RealEstateAd: IAuditable
    {
        public Guid Id { get; set; }
        [Required, StringLength(128)]
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string HtmlContent { get; set; }

        public int City_Id { get; set; }
        public int? RoomCount { get; set; }
        public int? BathCount { get; set; }

        #region IAuditable Implementation
        [StringLength(128)]
        public string CreatedBy { get; set; }
        public DateTime CreateDate { get; set; }
        [StringLength(128)]
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        #endregion IAuditable Implementation

        #region Foreign Keys

        [ForeignKey("City_Id")]
        public virtual City City { get; set; }
        
        #endregion Foreign Keys

        #region Releated Collections

        public virtual ICollection<Image> Images { get; set; }
        
        #endregion
    }
}
