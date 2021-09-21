using System;

namespace Project.Service.RequestResponse
{
    public class AddAsignmentRequest :BaseRequest
    {
        public Guid Project_Id { get; set; }
        public Guid Assignment_Id { get; set; }
        public string AssignmentDescription { get; set; }
        public string AssignmentAbbreviation { get; set; }
    }
}