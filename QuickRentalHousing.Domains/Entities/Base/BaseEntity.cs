using System;

namespace QuickRentalHousing.Domains.Entities.Base
{
    public class BaseEntity
    {
        public bool IsActive { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedTime { get; set; }
        public Guid UpdatedBy { get; set; }
        public DateTime UpdatedTime { get; set; }
    }
}
