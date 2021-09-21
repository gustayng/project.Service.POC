using System;

namespace Project.Service.RequestResponse
{
    public class ProjectAssignmentDTO : ResponseBase
    {
        public Guid Project_Id { get; set; }
        public Guid Assignment_Id { get; set; }
        public string ProjectName { get; set; }
        public string AssignmentDescription { get; set; }
    }
}