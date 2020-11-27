using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Nop.Web.Framework.Mvc.Routing;

namespace Nop.Web.Api.Infrastructure
{
    public class ApiUrlRouteProvider : IRouteProvider
    {
        public void RegisterRoutes(IEndpointRouteBuilder routeBuilder)
        {
            //and default one
            //routeBuilder.MapRoute("Default", "{controller}/{action}/{id?}");

            // Controller Only
            routeBuilder.MapControllerRoute(
                name: "ControllerOnly",
                pattern: "api/{controller}"
            );

            // Controller with ID
            routeBuilder.MapControllerRoute(
                name: "ControllerAndId",
                pattern: "api/{controller}/{id}",
                defaults: null,
                constraints: new { id = @"^\d+$" } // Only integers
            );

            // Controller with actions
            routeBuilder.MapControllerRoute(
                name: "ControllerAndAction",
                pattern: "api/{controller}/{action}"
            );

            // Controller with actions
            routeBuilder.MapControllerRoute(
                name: "ControllerAndActionAndId",
                pattern: "api/{controller}/{action}/{id}",
                defaults: null,
                constraints: new { id = @"^\d+$" } // Only integers
            );


        }

        #region Properties

        /// <summary>
        /// Gets a priority of route provider
        /// </summary>
        public int Priority
        {
            //it should be the last route. we do not set it to -int.MaxValue so it could be overridden (if required)
            get { return -1000000; }
        }

        #endregion
    }
}
