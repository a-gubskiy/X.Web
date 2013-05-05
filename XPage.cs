using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace X.Web
{
    /// <summary>
    /// Deprecated code
    /// </summary>
    public abstract class XPage : Page
    {
        /// <summary>
        /// Advanced request with safe parameter gettin functions
        /// </summary>
        public XRequest XRequest { get; private set; }

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            XRequest = new XRequest(Request, this.RouteData);

            if (Request.UserAgent != null && Request.UserAgent.IndexOf("AppleWebKit", StringComparison.CurrentCultureIgnoreCase) > -1)
            {
                this.ClientTarget = "uplevel";
            }
        }

        /// <summary>
        /// Enable page cache
        /// </summary>
        /// <param name="duration">Duraion in seconds</param>
        /// <param name="parameters">Url parameters</param>
        protected void EnableFullPageCache(int duration, IEnumerable<string> parameters)
        {
            Response.Cache.SetExpires(DateTime.Now.AddSeconds(duration));
            Response.Cache.SetCacheability(HttpCacheability.Public);
            Response.Cache.SetValidUntilExpires(true);

            foreach (var p in parameters)
            {
                Response.Cache.VaryByParams[p] = true;
            }
        }

        /// <summary>
        /// Set page title
        /// </summary>
        /// <param name="title">Page title</param>
        [Obsolete("SetTitle is deprecated.")]
        protected abstract void SetTitle(string title);

        #region Canonical Url

        private bool _canonicalUrlAdded = false;

        [Obsolete("SetCanonicalUrl is deprecated.")]
        protected virtual void SetCanonicalUrl(string canonicalUrl)
        {
            if (String.IsNullOrEmpty(canonicalUrl))
            {
                throw new Exception("Empty canonical url");
            }

            canonicalUrl = canonicalUrl.ToLower();

            if (!String.IsNullOrEmpty(canonicalUrl))
            {
                var requestUrl = Request.Url.ToString();

                if (requestUrl != canonicalUrl && requestUrl != canonicalUrl + "default.aspx" /*&& !requestUrl.Contains("localhost")*/)
                {
                    Response.Redirect(canonicalUrl);
                }
            }

            if (!_canonicalUrlAdded)
            {
                var link = new HtmlLink();
                link.Attributes["rel"] = "canonical";
                link.Href = canonicalUrl;

                Header.Controls.Add(link);
                _canonicalUrlAdded = true;
            }
            else
            {
                throw new Exception("Canonical url already added");
            }
        }

        #endregion

        #region Culture (deprecated)

        private const string SessionLanguageKey = "session_language";

        /// <summary>
        /// Url argument - lng, default - null
        /// </summary>
        [Obsolete("CurrentCulture is deprecated.")]
        public virtual CultureInfo CurrentCulture
        {
            get { return XPage.GetCurrentCulture(this, this.DefaultCulture, this.AllowedCultures, true); }
        }

        /// <summary>
        /// By default - all cultures are allowed
        /// </summary>
        [Obsolete("AllowedCultures is deprecated.")]
        public virtual IEnumerable<CultureInfo> AllowedCultures
        {
            get { return new List<CultureInfo> { DefaultCulture }; }
        }

        /// <summary>
        /// Default page culture info
        /// </summary>
        [Obsolete("DefaultCulture is deprecated.")]
        protected abstract CultureInfo DefaultCulture { get; }

        /// <summary>
        /// Return page culture info
        /// </summary>
        /// <param name="page"></param>
        /// <param name="defaultPageCulture"></param>
        /// <param name="allowedCultures"></param>
        /// <param name="useSessionState"></param>
        /// <returns></returns>
        [Obsolete("GetCurrentCulture is deprecated.")]
        public static CultureInfo GetCurrentCulture(XPage page, CultureInfo defaultPageCulture, IEnumerable<CultureInfo> allowedCultures, bool useSessionState)
        {
            var cultureCode = page.XRequest.GetParameter("lng", String.Empty).Trim().ToLower();

            CultureInfo cultureInfo = null;

            try
            {
                if (!String.IsNullOrEmpty(cultureCode))
                {
                    cultureInfo = new CultureInfo(cultureCode);
                }
            }
            catch
            {
                cultureInfo = null;
            }


            if (useSessionState)
            {
                if (cultureInfo != null && !allowedCultures.Contains(cultureInfo))
                {
                    cultureInfo = defaultPageCulture;
                }

                if (cultureInfo != null)
                {
                    page.Session[SessionLanguageKey] = cultureInfo;
                }

                if (page.Session[SessionLanguageKey] == null)
                {
                    page.Session[SessionLanguageKey] = defaultPageCulture;
                }

                return (CultureInfo)page.Session[SessionLanguageKey];
            }

            if (cultureInfo == null || !allowedCultures.Contains(cultureInfo))
            {
                cultureInfo = defaultPageCulture;
            }

            return cultureInfo;
        }

        #endregion

        /// <summary>
        /// Get page master page
        /// </summary>
        public XMasterPage XMaster
        {
            get { return (Master is XMasterPage) ? (Master as XMasterPage) : null; }
        }
    }
}
