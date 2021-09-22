using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Project.Service.DTO;
using Cart.Common.Services;
using Project.Service.RequestResponse;
using Project.Business;
using Microsoft.AspNetCore.Mvc;
using Project.Service;
using System.Net;

namespace Cloudstarter.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProjectController : ControllerBase
    {
        IAppSettings _appSettings;
        IProjectBusinessLogic _projectBusiness;

        public ProjectController(IAppSettings appSettings, IProjectBusinessLogic projectBusiness)
        {
            _appSettings = appSettings;
            _projectBusiness = projectBusiness;
        }

        [Route("/GetProjects")]
        [HttpGet]
        public async Task<List<ProjectDTO>> GetProjects()
        {
            var response = await _projectBusiness.GetProjects();
            return response;
        }

        [Route("/GetProject")]
        [HttpPost]
        public async Task<ProjectAndAssignmentDTO> GetProject(GetProjectRequest request)
        {
            var response = await _projectBusiness.GetProject(request);
            return response;
        }

        [Route("/CreateProject")]
        [HttpPost]
        public async Task<ProjectDTO> UpdateProject(CreateProjectRequest request)
        {
            var response = await _projectBusiness.CreateProject(request);
            return response;
        }

        [Route("/UpdateProject")]
        [HttpPost]
        public async Task<ProjectDTO> UpdateProject(UpdateProjectRequest request)
        {
            var response = await _projectBusiness.UpdateProject(request);
            return response;
        }

        [Route("/AddAssignment")]
        [HttpPost]
        public async Task<ProjectAssignmentDTO> AddAssignment(AddAsignmentRequest request)
        {
            var response = await _projectBusiness.AddAssignment(request);
            return response;
        }

        [Route("/AssignmentUpdated")]
        [HttpPost]
        public async Task<AssignmentUpdatedResponse> AssignmentUpdated(AssignmentUpdatedRequest request)
        {
            var response = await _projectBusiness.AssignmentUpdated(request);
            return response;
        }

        [Route("/Settings")]
        [HttpGet]
        public async Task<IAppSettings> Settings()
        {
            return _appSettings;
        }

        [Route("/Ygge")]
        [HttpGet]
        public async Task<string> Ygge()
        {
            return "This is the YYYYYYYYYYYYYYYY";
        }

        /// <summary>
        /// This method will check a url to see that it does not return server or protocol errors
        /// </summary>
        /// <param name="url">The path to check</param>
        /// <returns></returns>
        [HttpGet]
        [Route("/UrlIsValid")]
        public string UrlIsValid(string url)
        {
            try
            {
                HttpWebRequest request = HttpWebRequest.Create(url) as HttpWebRequest;
                request.Timeout = 5000; //set the timeout to 5 seconds to keep the user from waiting too long for the page to load
                request.Method = "HEAD"; //Get only the header information -- no need to download any content

                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    int statusCode = (int)response.StatusCode;
                    if (statusCode >= 100 && statusCode < 400) //Good requests
                    {
                        return "OK";
                    }
                    else if (statusCode >= 500 && statusCode <= 510) //Server Errors
                    {
                        //log.Warn(String.Format("The remote server has thrown an internal error. Url is not valid: {0}", url));
                        return String.Format("The remote server has thrown an internal error. Url is not valid: {0}", url);
                    }
                }
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError) //400 errors
                {
                    return ex.Message;
                }
                else
                {
                    return String.Format("Unhandled status [{0}] returned for url: {1}", ex.Status, url);
                }
            }
            catch (Exception ex)
            {
                return String.Format("Could not test url {0}.", url);
            }
            return "ERROR";
        }

    }
}