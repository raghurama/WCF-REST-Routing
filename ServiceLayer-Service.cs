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
    public class <class name>
    {
        private IProjectsRepository ProjectRepo { get; set; }
        
        public ProjectsService()
        {
            ProjectRepo = ServiceBootStrapper.GetContainer().Resolve<IProjectsRepository>();
            
        }


        [Description("")]
        [WebGet(UriTemplate = "Search/?projectId={p1}&....")]
        public IEnumerable<ProjectSearchList> GetProjects(string p1, string p2, 
            string p3)
        {            

            var object = ProjectRepo.ProjectSearchList(...);

            ...
            ...
            return object.Data;
        }

        [Description("")]
        [WebGet(UriTemplate = "<name>/{p1}/{p2}")]
        public SOWDetails Get(string p1, string p2)
        {
            var object = //call data repository class here

            if (sowDetailsTransaction.Status == ResultStatus.Error)
            {
                Logger.Error.Log(.......);
                throw new WebFaultException(HttpStatusCode.InternalServerError);
                //throw new System.Exception(sowDetailsTransaction.Message);  // get exact error
            }
            return object.Data;
        }

    }
}
