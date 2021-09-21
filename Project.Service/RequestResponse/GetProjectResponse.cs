using Project.Service.DTO;
using System.Collections.Generic;

namespace Project.Service.RequestResponse
{
    public class GetProjectResponse
    {
        public List<ProjectTypeDTO> Projects { get; set; }
        public GetProjectResponse()
        {
            this.Projects = new List<ProjectTypeDTO>();
        }
    }
}