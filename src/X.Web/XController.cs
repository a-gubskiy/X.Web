using System;
using System.Globalization;
using System.Threading;
using System.Web.Mvc;
using System.Web.Routing;

namespace X.Web
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class XController : Controller, IWebPage
    {
        /// <summary>
        /// 
        /// </summary>
        public virtual string Title
        {
            get { return ViewBag.Title; }
            set { ViewBag.Title = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual string Keywords
        {
            get { return ViewBag.Keywords; }
            set { ViewBag.Keywords = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual string Description
        {
            get { return ViewBag.Description; }
            set { ViewBag.Description = value; }
        }

        public CultureInfo CurrentCulture { get; protected set; }

        protected override IAsyncResult BeginExecute(RequestContext requestContext, AsyncCallback callback, object state)
        {
            DetectCulture();

            return base.BeginExecute(requestContext, callback, state);
        }

        /// <summary>
        /// 
        /// </summary>
        protected virtual void DetectCulture()
        {
            CurrentCulture = Thread.CurrentThread.CurrentCulture;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual ActionResult NotFound()
        {
            return HttpNotFound();
        }
    }
}
