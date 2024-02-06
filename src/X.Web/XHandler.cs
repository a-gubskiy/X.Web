using System.Web;
using System.Web.Routing;
using System.Web.SessionState;

namespace X.Web;

public abstract class XHandler : IHttpHandler, IRouteHandler, IRequiresSessionState
{
    public virtual IHttpHandler GetHttpHandler(RequestContext requestContext)
    {
        return this;
    }

    public virtual bool IsReusable
    {
        get { return true; }
    }

    protected HttpContext Context { get; private set; }
    protected XRequest XRequest { get; private set; }

    public virtual void ProcessRequest(HttpContext context)
    {
        Context = context;
        XRequest = new XRequest(context.Request, null);
    }
}