using System;
using System.Collections.Generic;

#nullable disable

namespace Project.Service
{
    public partial class ProjectType : EntityBase
    {
        public ProjectType()
        {
        }

        public Guid ProjectType_Id { get; set; }
        public string ProjectType_Name { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
    }
}
