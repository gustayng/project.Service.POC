using Project.Service.DTO;
using System.Collections.Generic;

namespace Project.Service.RequestResponse
{
    public class ProjectAndAssignmentDTO
    {
        public ProjectDTO Project { get; set; }
        public List<AssignmentDTO> Assignments { get; set; }

        public ProjectAndAssignmentDTO()
        {
            this.Project = new ProjectDTO();
            this.Assignments = new List<AssignmentDTO>();
        }
    }
}