using System;
using System.Collections.Generic;

namespace Project.Service.DTO
{
    public class ProjectDTO : BaseClassDTO
    {
        public Guid Project_Id { get; set; }
        public Guid ProjectType_Id { get; set; }
        public string Project_Name { get; set; }

        public ProjectDTO()
        {
        }
    }
}