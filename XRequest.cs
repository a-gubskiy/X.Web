using System;
using System.ComponentModel;
using System.Web;
using System.Web.Routing;

namespace X.Web
{
    public class XRequest
    {
        public XRequest(HttpRequest request, RouteData routeData)
        {
            Request = request;
            RouteData = routeData;
        }

        public HttpRequest Request { get; set; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public RouteData RouteData { get; set; }

        #region GetParameter(...)
        
        public string GetParameter(string name, string defaultValue)
        {
            var p = GetValueFromRequest(name);

            if (String.IsNullOrEmpty(p))
            {
                return defaultValue;
            }

            return p;
        }

        public double GetParameter(string name, double defaultValue)
        {
            var p = GetValueFromRequest(name);

            if (!String.IsNullOrEmpty(p))
            {
                double result;

                if (double.TryParse(p, out result))
                {
                    return result;
                }
            }

            return defaultValue;
        }

        public int GetParameter(string name, int defaultValue)
        {
            var p = GetValueFromRequest(name);

            if (!String.IsNullOrEmpty(p))
            {
                int result;

                if (int.TryParse(p, out result))
                {
                    return result;
                }
            }

            return defaultValue;
        }

        public long GetParameter(string name, long defaultValue)
        {
            var p = GetValueFromRequest(name);

            if (!String.IsNullOrEmpty(p))
            {
                long result;

                if (long.TryParse(p, out result))
                {
                    return result;
                }
            }

            return defaultValue;
        }

        public bool GetParameter(string name, bool defaultValue)
        {
            var p = GetValueFromRequest(name);

            if (String.IsNullOrEmpty(p))
            {
                return defaultValue;
            }

            if (p == "1" || p.ToLower() == "true" || p.ToLower() == "yes")
            {
                return true;
            }

            return false;
        }
        
        private string GetValueFromRequest(string name)
        {
            var requestParam = Request.Params[name];

            if (!String.IsNullOrEmpty(requestParam))
            {
                return requestParam;
            }

            if (RouteData != null)
            {
                var routeDataValue = RouteData.Values[name];

                if (routeDataValue != null)
                {
                    return routeDataValue.ToString();
                }
            }

            return String.Empty;
        }

        #endregion
    }
}
