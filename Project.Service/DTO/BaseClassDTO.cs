using System;
using System.Collections.Generic;

namespace Project.Service.DTO
{
    public class BaseClassDTO
    {
        public short Status { get; set; }
        public string StatusName { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedDateTime { get; set; }
        public DateTime CreatedDateTime { get; set; }

        public BaseClassDTO()
        {
        }
    }
}