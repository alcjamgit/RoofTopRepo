using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoofTop.Core.Domain
{
    public class RealEstateAd
    {
        public Guid Id { get; set; }
        public string Title { get; set; }

        [ForeignKey("City")]
        public virtual int City_Id { get; set; }
        public int RoomCount { get; set; }
        public int BathCount { get; set; }

        public string CreatedBy { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        
        

        public virtual City City { get; set; }
        

    }
}
