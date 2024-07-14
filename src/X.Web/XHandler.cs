using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace X.Web;

public abstract class XHandler : IRouteHandler
{
    public virtual bool IsReusable => true;

    protected HttpContext Context { get; private set; }
    
    protected XRequest XRequest { get; private set; }

    public virtual void ProcessRequest(HttpContext context)
    {
        Context = context;
        XRequest = new XRequest(context.Request, null);
    }

    public abstract RequestDelegate GetRequestHandler(HttpContext httpContext, RouteData routeData);
}