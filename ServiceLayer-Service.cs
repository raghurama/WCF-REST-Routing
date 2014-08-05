using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using Microsoft.Practices.Unity;

using Microsoft.Http;
using Microsoft.Http.Headers;
using System.Runtime.Serialization.Json;

namespace <mynamespace>
{
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class ProjectsService
    {
        private IProjectsRepository ProjectRepo { get; set; }
        
        public ProjectsService()
        {
            ProjectRepo = ServiceBootStrapper.GetContainer().Resolve<IProjectsRepository>();
            
        }


        [Description("")]
        [WebGet(UriTemplate = "Search/?projectId={p1}&projectName={p2}&projectManager={p3}")]
        public IEnumerable<ProjectSearchList> GetProjects(string p1, string p2, 
            string p3)
        {            

            var GetProjectsTransaction = ProjectRepo.ProjectSearchList(projectId, projectName,
                projectManager);

            ...
            ...
            return GetProjectsTransaction.Data;
        }

        [Description("Get SOW Details for SOW ID & Year.")]
        [WebGet(UriTemplate = "<name>/{p1}/{p2}")]
        public SOWDetails Get(string p1, string p2)
        {
            var sowDetailsTransaction = //call data repository class here

            if (sowDetailsTransaction.Status == ResultStatus.Error)
            {
                Logger.Error.Log(string.Format(
                    "Service Error.\n Request: GetSOWDetails(SOW ID: {0}).\n TransactionMessage: {1}",
                    SowID, sowDetailsTransaction.Message));
                throw new WebFaultException(HttpStatusCode.InternalServerError);
                //throw new System.Exception(sowDetailsTransaction.Message);  // get exact error
            }
            return sowDetailsTransaction.Data;
        }

    }
}
