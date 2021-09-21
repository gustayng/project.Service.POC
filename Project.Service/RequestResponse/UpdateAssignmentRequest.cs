using System;

namespace Project.Service.RequestResponse
{
    public class UpdateAssignmentRequest : RequestBase
    {
        public UpdateAssignmentRequest()
        {
        }

        public Guid Assignment_Id { get; set; }
        public string Description { get; set; }
        public string Abbreviation { get; set; }
    }
}