using System;
using System.Collections.Generic;

#nullable disable

namespace Project.Service
{
    public partial class Project : EntityBase
    {
        public Project()
        {
            Assignments = new HashSet<Assignment>();
        }

        public Guid Project_Id { get; set; }
        public Guid ProjectType_Id { get; set; }
        public string Project_Name { get; set; }

        public ICollection<Assignment> Assignments { get; set; }

        public ProjectType ProjectType { get; set; }
    }
}
