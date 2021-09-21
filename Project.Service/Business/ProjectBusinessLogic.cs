using Microsoft.ApplicationInsights;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Project.Service.RequestResponse;
using Project.Service.DTO;
using System.Net.Http.Headers;
using Cart.Common.Services;
using Project.Service;
using Microsoft.ApplicationInsights.DataContracts;

namespace Project.Business
{
    public interface IProjectBusinessLogic
    {
        Task<List<ProjectDTO>> GetProjects();
        Task<ProjectDTO> UpdateProject(UpdateProjectRequest assignment);
        Task<ProjectDTO> CreateProject(CreateProjectRequest request);
        Task<ProjectAssignmentDTO> AddAssignment(AddAsignmentRequest request);
        Task<ProjectAndAssignmentDTO> GetProject(GetProjectRequest request);
        Task<AssignmentUpdatedResponse> AssignmentUpdated(AssignmentUpdatedRequest request);
    }

    public class ProjectBusinessLogic : IProjectBusinessLogic
    {
        TelemetryClient _applicationInsights;
        ProjectContext _context;
        IAppSettings _appSettings;
        IServiceBus _serviceBus;

        public ProjectBusinessLogic(ProjectContext context, TelemetryClient applicationInsights, IAppSettings appSettings, IServiceBus serviceBus)  
        {
            _context = context;
            _applicationInsights = applicationInsights;
            _appSettings = appSettings;
            _serviceBus = serviceBus;
        }

        public async Task<List<ProjectDTO>> GetProjects()
        {
            var response = new List<ProjectDTO>();

            try
            {
                var currentAssignments = _context.Projects.ToList();

                response = currentAssignments.Select(p => new ProjectDTO()
                {
                    Project_Id = p.Project_Id,
                    ProjectType_Id = p.ProjectType_Id,
                    Project_Name = p.Project_Name,
                    Status = p.Status,
                    CreatedBy = p.CreatedBy,
                    UpdatedBy = p.UpdatedBy,
                    CreatedDateTime = p.CreatedDateTime,
                    UpdatedDateTime = p.UpdatedDateTime,
                    StatusName = p.StatusName
                }).ToList();
            }
            catch (Exception ex)
            {
                //TODO: 
            }

            return response;
        }

        public async Task<ProjectAssignmentDTO> AddAssignment(AddAsignmentRequest request)
        {
            var response = new ProjectAssignmentDTO();

            var existingProject = _context.Projects.Where(p => p.Project_Id == request.Project_Id).FirstOrDefault();

            var existingAssignment = _context.Assignments.Where(p => p.Assignment_Id == request.Assignment_Id).FirstOrDefault();

            try
            {
                if (existingAssignment == null)
                {
                    // Create assignment
                    existingAssignment = new Service.Assignment()
                    {
                        Assignment_Id = request.Assignment_Id,
                        Abbreviation = request.AssignmentAbbreviation,
                        Description = request.AssignmentDescription,
                        CreatedBy = request.UserId,
                        UpdatedBy = request.UserId,
                        CreatedDateTime = DateTime.Now,
                        UpdatedDateTime = DateTime.Now,
                        Status = 10,
                        StatusName = "Active"
                    };
                    _context.Assignments.Add(existingAssignment);

                    _context.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                response.ErrorMessage = ex.Message;
            }
            
            if (existingProject != null)
            {
                // Assignment already connected?
                try
                {
                    if (!existingProject.Assignments.Any(p => p.Assignment_Id == request.Assignment_Id))
                    {
                        existingProject.Assignments.Add(existingAssignment);
                    }

                    response.AssignmentDescription = existingAssignment.Description;
                    response.Assignment_Id = existingAssignment.Assignment_Id;
                    response.Project_Id = existingProject.Project_Id;
                    response.ProjectName = existingProject.Project_Name;

                    _context.SaveChanges();

                }
                catch (Exception ex)
                {
                    response.ErrorMessage = ex.Message;
                }
            }
            else
            {
                response.ErrorMessage = string.Format("Project: {0} does not exists.", request.Project_Id.ToString());
            }

            // Take the just updated assignment and put on Service Bus Topc for other services to handle.
            //var serviceBusMessage = JsonSerializer.Serialize(project);
            //await _serviceBus.SendMessageToTopicAsync("", _appSettings.SERVICE_BUS_CONNECTIONSTRING, _appSettings.AssignmentUpdatedTopicName, serviceBusMessage);

            return response;
        }

        public async Task<ProjectDTO> UpdateProject(UpdateProjectRequest project)
        {
            var response = new ProjectDTO();

            var existingProject = _context.Projects.Where(p => p.Project_Id == project.Project_Id).FirstOrDefault();

            if (existingProject != null)
            {
                existingProject.ProjectType_Id = project.ProjectType_Id;
                existingProject.Project_Name = project.ProjectName;
                existingProject.UpdatedBy = "Someone";
                existingProject.UpdatedDateTime = DateTime.Now;

                response.UpdatedBy = existingProject.UpdatedBy;
                response.UpdatedDateTime = existingProject.UpdatedDateTime;
                response.Project_Name = existingProject.Project_Name;
                response.ProjectType_Id = existingProject.ProjectType_Id;

                _context.SaveChanges();
            }

            // Take the just updated assignment and put on Service Bus Topc for other services to handle.
            //var serviceBusMessage = JsonSerializer.Serialize(project);
            //await _serviceBus.SendMessageToTopicAsync("", _appSettings.SERVICE_BUS_CONNECTIONSTRING, _appSettings.AssignmentUpdatedTopicName, serviceBusMessage);

            return response;
        }

        public async Task<ProjectDTO> CreateProject(CreateProjectRequest request)
        {
            var response = new ProjectDTO();

            var id = Guid.NewGuid();
            var currentDateTime = DateTime.Now;

            _context.Projects.Add(new Service.Project()
            {
                Project_Id = id,
                ProjectType_Id = request.ProjectType_Id,
                Project_Name = request.ProjectName,
                Status = 10,
                StatusName = "Active",
                CreatedBy = request.UserId,
                UpdatedBy = request.UserId,
                CreatedDateTime = currentDateTime,
                UpdatedDateTime = currentDateTime
            });

            response.CreatedBy = request.UserId;
            response.CreatedDateTime = currentDateTime;

            _context.SaveChanges();

            return response;
        }

        public async Task<ProjectAndAssignmentDTO> GetProject(GetProjectRequest request)
        {
            var response = new ProjectAndAssignmentDTO();

            var project = _context.Projects.Where(p => p.Project_Id == request.Project_Id).FirstOrDefault();

            if (project != null)
            {
                response.Project.Project_Id = project.Project_Id;
                response.Project.ProjectType_Id = project.ProjectType_Id;
                response.Project.Project_Name = project.Project_Name;
                response.Project.Status = project.Status;
                response.Project.StatusName = project.StatusName;
                response.Project.UpdatedBy = project.UpdatedBy;
                response.Project.CreatedBy = project.CreatedBy;
                response.Project.CreatedDateTime = project.CreatedDateTime;
                response.Project.UpdatedDateTime = project.UpdatedDateTime;
            }

            return response;
        }

        public async Task<AssignmentUpdatedResponse> AssignmentUpdated(AssignmentUpdatedRequest request)
        {
            var response = new AssignmentUpdatedResponse();
            response.CorrelationId = request.CorrelationId;

            var requestJson = JsonSerializer.Serialize(request);
            var dependencyTelematric = new DependencyTelemetry();
            dependencyTelematric.Name = $"Update assignment in Project AssignmentUpdated()";
            dependencyTelematric.Type = "AssignmentUpdated Project.Service";
            dependencyTelematric.Data = requestJson;

            using var operation = _applicationInsights.StartOperation<DependencyTelemetry>(dependencyTelematric);

            var existingAssignment = _context.Assignments.Where(p => p.Assignment_Id == request.Assignment_Id).FirstOrDefault();

            try
            {
                if (existingAssignment != null)
                {
                    existingAssignment.Description = request.Description;
                    existingAssignment.Abbreviation = request.Abbreviation;
                    existingAssignment.UpdatedBy = request.UserId;
                    existingAssignment.UpdatedDateTime = DateTime.Now;

                    _context.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                response.ErrorMessage = ex.Message;
            }

            _applicationInsights.StopOperation(operation);

            return response;
        }
    }

}
