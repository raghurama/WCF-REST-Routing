using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using Microsoft.Practices.Unity;
using PMTools.Services.InputValidation;
using Microsoft.Http;
using Microsoft.Http.Headers;
using System.Runtime.Serialization.Json;

namespace PMTools.Services
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
            Logger = ServiceBootStrapper.GetContainer().Resolve<ICitiLogger>();
        }

        [Description("Save Project PL.")]
        [WebGet(UriTemplate = "SaveProjectPL/projectId={projectId}&perd={perd}&pl={pl}&costType={costType}&fpc={fpc}&insertDelete={insertDelete}&userID={userID}")]
        public int? SaveProjectPL(string projectId, string perd, string pl, string costType, string fpc, string insertDelete, string userID)
        {
            ...
            ...
            var saveProjectTransaction = ProjectRepo.SaveProjectPL(plItem);

            if (saveProjectTransaction.Status == ResultStatus.Error)
            {
                Logger.Error.Log(string.Format(
                        "InternalServerError.\n Request: SaveProjectPL(projectId: {0}, perd: {1}, pl: {2}, costType: {3}, fpc: {4}, insertDelete: {5}, userID: {6}).\n TransactionMessage: {7}",
                        projectId, perd, pl, costType, fpc, insertDelete, userID, saveProjectTransaction.Message));
                throw new WebFaultException(HttpStatusCode.InternalServerError);
            }

            return saveProjectTransaction.Data;
        }

        [Description("Get Project Summaries based on Filter Criteria.")]
        [WebGet(UriTemplate = "Summaries/{employeeId}/?drilldown={drilldown}&roleId={roleId}&year={year}&statusIds={statusIds}&bow={bowFlag}")]
        public IEnumerable<ProjectSummary> GetProjectSummaries(string employeeId, bool drilldown, string roleId, 
                                                               string year, string statusIds, bool bowFlag)
        {

           ...
           ...

            //TODO: Artificially limited due to web service not supporting massive record sets. Work in progress
            return projectSummariesTransaction.Data.Take(5000);
        }


        [Description("Gets a list of project using query on project id, project name and project manager.")]
        [WebGet(UriTemplate = "Search/?projectId={projectId}&projectName={projectName}&projectManager={projectManager}")]
        public IEnumerable<ProjectSearchList> GetProjects(string projectId, string projectName, 
            string projectManager)
        {            

            var GetProjectsTransaction = ProjectRepo.ProjectSearchList(projectId, projectName,
                projectManager);

            ...
            ...
            return GetProjectsTransaction.Data;
        }

        [Description("Gets a list of FPC Codes for a Project PL")]
        [WebGet(UriTemplate = "ProjectFPCs/{projectId}/{projectPl}/{projectYear}")]
        public IEnumerable<ProjectPL> GetProjectFPCs(string projectId, string projectPl, string projectYear)
        {
            var GetProjectsTransaction = ProjectRepo.ProjectFPCs(int.Parse(projectId), projectPl, projectYear);
            IEnumerable<ProjectPL> plList = GetProjectsTransaction.Data.ToList();
            return plList;
        }

        [Description("Get SOW Details for SOW ID & Year.")]
        [WebGet(UriTemplate = "SOWDetails/{SowID}/{year}")]
        public SOWDetails GetSOWDetails(string SowID, string year)
        {
            var sowDetailsTransaction = ProjectRepo.GetSOWDetails(SowID, year);

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
