using Hangfire.Annotations;
using Hangfire.Dashboard;

namespace MongoApi.Utilts
{
    public class CustomAuthorizeFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize([NotNull] DashboardContext context)
        {
            var httpcontext = context.GetHttpContext();
            return httpcontext.User.Identity.IsAuthenticated;
        }
    }
}
