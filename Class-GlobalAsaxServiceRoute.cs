using System;
using System.Collections;
using System.ServiceModel.Activation;
using System.Web.Routing;

namespace <mynamespace>
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            //
            ConfigureHttpWebServices();
            
        }

        private static void ConfigureHttpWebServices()
        {
            RouteTable.Routes.Add(new ServiceRoute("service 1", new WebServiceHostFactory(), typeof(ProjectsService)));
            RouteTable.Routes.Add(new ServiceRoute("service 2", new WebServiceHostFactory(), typeof(ResourcesService)));
            ...
            ...
            
        }

    }
}
