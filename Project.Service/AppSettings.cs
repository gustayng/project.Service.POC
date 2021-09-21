using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Project.Service
{
    public class AppSettings : IAppSettings
    {
        public string[] AllowedOrigins { get; set; }
        public string SERVICE_BUS_CONNECTIONSTRING { get; set; }
        public string AssignmentUpdatedTopicName { get; set; }
        public string dbConnectionString { get; set; }
        public string APPINSIGHTS_CONNECTIONSTRING { get; set; }
    }
}
