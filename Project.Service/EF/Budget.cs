using System;
using System.Collections.Generic;

#nullable disable

namespace Project.Service
{
    public partial class AssignmentCommunicationArea
    {
        public AssignmentCommunicationArea()
        {
        }

        public Guid AssignmentCommunicationArea_Id { get; set; }
        public string AssignmentCommunicationArea_Name { get; set; }
        public short Status { get; set; }
        public string StatusName { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime UpdatedDateTime { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }

    }
}
