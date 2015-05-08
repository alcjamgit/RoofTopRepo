using System;

namespace RoofTop.Core.DomainServices
{
    public interface IAuditable
    {
        string CreatedBy { get; set; }
        DateTime CreateDate { get; set; }
        string ModifiedBy { get; set; }
        DateTime ModifiedDate { get; set; }
    }
}
