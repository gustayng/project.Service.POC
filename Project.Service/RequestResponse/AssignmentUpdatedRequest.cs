using System;

namespace Project.Service.RequestResponse
{
    public class AssignmentUpdatedRequest : BaseRequest
    {
        public Guid Assignment_Id { get; set; }
        public string Description { get; set; }
        public string Abbreviation { get; set; }
    }
}