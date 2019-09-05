using System;
using System.Collections.Generic;

namespace DAL.Entities
{
    public partial class Events
    {
        public int EventId { get; set; }
        public string EventType { get; set; }
        public string TargetAudience { get; set; }
        public int? ResponsiblePerson { get; set; }
        public string Status { get; set; }
        public string Feeback { get; set; }

        public virtual Customers ResponsiblePersonNavigation { get; set; }
    }
}
