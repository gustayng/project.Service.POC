using System;
using System.Collections.Generic;

namespace Project.Service.DTO
{
    public class AssignmentDTO : BaseClassDTO
    {
        public Guid Assignment_Id { get; set; }
        public string Description { get; set; }
        public string Abbreviation { get; set; }
        public Guid AssignmentGroup_Id { get; set; }

        public AssignmentDTO()
        {
        }
    }
}