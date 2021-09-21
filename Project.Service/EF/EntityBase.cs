using System;
using System.Collections.Generic;

#nullable disable

namespace Project.Service
{
    public partial class EntityBase
    {
        public EntityBase()
        {

        }

        public short Status { get; set; }
        public string StatusName { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime UpdatedDateTime { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }


    }
}
