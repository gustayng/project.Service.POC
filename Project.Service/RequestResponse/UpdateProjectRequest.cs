using System;

namespace Project.Service.RequestResponse
{
    public class UpdateProjectRequest :RequestBase
    {
        public UpdateProjectRequest()
        {
        }

        public Guid Project_Id { get; set; }
        public Guid ProjectType_Id { get; set; }
        public string ProjectName { get; set; }
    }
}