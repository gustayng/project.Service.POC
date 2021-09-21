using Cart.Common.Services;
using Project.Business;
using Microsoft.ApplicationInsights;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Project.Service;

namespace Project.Service_Test
{
    [TestClass]
    public class ProjectTest
    {
        [TestMethod]
        public void SeedDataTest()
        {
            using (var context = new ProjectContext("Data Source =.; Initial Catalog = Project.Service; persist security info = True; Integrated Security = SSPI;"))
            {
                if (!context.ProjectTypes.Any(p => p.ProjectType_Name == "Delivery Project"))
                {

                    context.ProjectTypes.Add(new ProjectType()
                    {
                        ProjectType_Id = Guid.NewGuid(),
                        ProjectType_Name = "Delivery Project",
                        CreatedBy = "SEED",
                        Status = 10,
                        UpdatedBy = "SEED",
                        CreatedDateTime = DateTime.Now,
                        UpdatedDateTime = DateTime.Now,
                        StatusName = "Active"
                    });

                    context.ProjectTypes.Add(new ProjectType()
                    {
                        ProjectType_Id = Guid.NewGuid(),
                        ProjectType_Name = "HFB Ordering Project",
                        CreatedBy = "SEED",
                        Status = 10,
                        UpdatedBy = "SEED",
                        CreatedDateTime = DateTime.Now,
                        UpdatedDateTime = DateTime.Now,
                        StatusName = "Active"
                    });

                    //    //context.SaveChanges();

                    //    //context.Assignments.Add(new CartService.Assignment()
                    //    //{
                    //    //    Assignment_Id = Guid.NewGuid(),
                    //    //    AssignmentGroup_Id = assignmentGroupId1,
                    //    //    Abbreveation = "MPP",
                    //    //    Description = "Main Product Picture",
                    //    //    CreatedBy = "SEED",
                    //    //    Status = 10,
                    //    //    UpdatedBy = "SEED",
                    //    //    CreatedDateTime = DateTime.Now,
                    //    //    UpdatedDateTime = DateTime.Now,
                    //    //    StatusName = "Active"
                    //    //});

                    //    //context.Assignments.Add(new CartService.Assignment()
                    //    //{
                    //    //    Assignment_Id = Guid.NewGuid(),
                    //    //    AssignmentGroup_Id = assignmentGroupId1,
                    //    //    Abbreveation = "CPP",
                    //    //    Description = "Context Product Picture",
                    //    //    CreatedBy = "SEED",
                    //    //    Status = 10,
                    //    //    UpdatedBy = "SEED",
                    //    //    CreatedDateTime = DateTime.Now,
                    //    //    UpdatedDateTime = DateTime.Now,
                    //    //    StatusName = "Active"
                    //    //});

                    //    //context.Assignments.Add(new CartService.Assignment()
                    //    //{
                    //    //    Assignment_Id = Guid.NewGuid(),
                    //    //    AssignmentGroup_Id = assignmentGroupId1,
                    //    //    Abbreveation = "FPP",
                    //    //    Description = "Functional Product Picture",
                    //    //    CreatedBy = "SEED",
                    //    //    Status = 10,
                    //    //    UpdatedBy = "SEED",
                    //    //    CreatedDateTime = DateTime.Now,
                    //    //    UpdatedDateTime = DateTime.Now,
                    //    //    StatusName = "Active"
                    //    //});

                    context.SaveChanges();
                }


            }
        }

     

    }
}
