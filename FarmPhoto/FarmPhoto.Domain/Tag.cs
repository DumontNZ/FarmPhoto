using System;

namespace FarmPhoto.Domain
{
    public class Tag
    {
        public int TagId { get; set; }
        public string Description { get; set; }
        public int PhotoId { get; set; }
        public DateTime CreatedOnDateUtc { get; set; }
        public DateTime DeletedOnDateUtc { get; set; }
    }
}
