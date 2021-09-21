using System;
using System.Collections.Generic;

#nullable disable

namespace Project.Service
{
    public partial class Assignment : EntityBase
    {
        public Assignment()
        {
            Projects = new HashSet<Project>();
        }

        public Guid Assignment_Id { get; set; }
        public string Description { get; set; }
        public string Abbreviation { get; set; }

        public ICollection<Project> Projects { get; set; }

    }
}
