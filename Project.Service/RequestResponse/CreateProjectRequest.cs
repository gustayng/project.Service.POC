using System;

namespace Project.Service.RequestResponse
{
    public class CreateProjectRequest : RequestBase
    {
        public CreateProjectRequest()
        {
        }

        public Guid ProjectType_Id { get; set; }
        public string ProjectName { get; set; }


    }
}