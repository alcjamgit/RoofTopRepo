using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoofTop.Core.Entities
{
    public class RealEstateAd
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public int City_Id { get; set; }
        public int RoomCount { get; set; }
        public int BathCount { get; set; }

        public string CreatedBy { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }

        #region Foreign Keys

        [ForeignKey("City_Id")]
        public virtual City City { get; set; }
        
        #endregion Foreign Keys

        #region Releated Collections

        public virtual ICollection<Image> Images { get; set; }
        
        #endregion
    }
}
