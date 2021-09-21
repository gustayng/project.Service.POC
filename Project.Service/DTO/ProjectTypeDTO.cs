using System;
using System.Collections.Generic;

namespace Project.Service.DTO
{
    public class ProjectTypeDTO : BaseClassDTO
    {
        public Guid ProjectType_Id { get; set; }
        public string ProjectType_Name { get; set; }

        public ProjectTypeDTO()
        {
        }
    }
}