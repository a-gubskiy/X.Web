using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace X.Web;

[PublicAPI]
public abstract class XHandler : IRouteHandler
{
    public virtual bool IsReusable => true;

    protected HttpContext Context { get; set; }
    
    protected XRequest XRequest { get; set; }

    public virtual void ProcessRequest(HttpContext context)
    {
        Context = context;
        XRequest = new XRequest(context.Request, null);
    }

    public abstract RequestDelegate GetRequestHandler(HttpContext httpContext, RouteData routeData);
}