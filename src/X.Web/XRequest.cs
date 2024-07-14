using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace X.Web;

/// <summary>
/// 
/// </summary>
[PublicAPI]
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
    
    public static string GetParamValue(HttpRequest request, string paramName)
    {
        // Check query string parameters
        if (request.Query.ContainsKey(paramName))
        {
            return request.Query[paramName].ToString();
        }

        // Check form data
        if (request.HasFormContentType && request.Form.ContainsKey(paramName))
        {
            return request.Form[paramName].ToString();
        }

        // Parameter not found
        return null;
    }
        
    protected string GetValueFromRequest(string name)
    {
        var requestParam = GetParamValue(Request, name);

        if (!string.IsNullOrEmpty(requestParam))
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
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="url"></param>
    /// <param name="method"></param>
    /// <param name="form"></param>
    /// <param name="proxy"></param>
    /// <returns></returns>
    public static WebRequest CreateWebRequest(string url, string method, IEnumerable<KeyValuePair<string, string>> form, IWebProxy proxy = null)
    {
        var list = form as KeyValuePair<string, string>[] ?? form.ToArray();

        var count = list.Count();
        var sb = new StringBuilder();

        for (var i = 0; i < count; i++)
        {
            var item = list.ElementAt(i);
            sb.AppendFormat("{0}={1}", item.Key, item.Value);

            if (i + 1 < count)
            {
                sb.Append("&");
            }
        }

        return CreateWebRequest(url, method, sb.ToString(), proxy);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="url"></param>
    /// <param name="method"></param>
    /// <param name="data"></param>
    /// <param name="proxy"></param>
    /// <returns></returns>
    public static WebRequest CreateWebRequest(string url, string method, string data, IWebProxy proxy = null)
    {
        var request = WebRequest.Create(url);
        request.Method = method;
        request.Proxy = proxy ?? request.Proxy;
        
        if (!String.IsNullOrEmpty(data))
        {
            if (method.Equals("POST"))
            {
                var bytes = Encoding.UTF8.GetBytes(data);

                // Set the ContentType property of the WebRequest.
                request.ContentType = "application/x-www-form-urlencoded";

                // Set the ContentLength property of the WebRequest.
                request.ContentLength = bytes.Length;

                // Get the request stream.
                var stream = request.GetRequestStream();

                // Write the data to the request stream.
                stream.Write(bytes, 0, bytes.Length);

                // Close the Stream object.
                stream.Close();
            }

            if (method.Equals("GET"))
            {
                url = $"{url}?{data}";
                request = WebRequest.Create(url);
                request.Proxy = proxy ?? request.Proxy;
            }
        }

        return request;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public static string GetWebRequestResponse(WebRequest request)
    {
        // Get the original response.
        var response = request.GetResponse();

        var status = ((HttpWebResponse)response).StatusDescription;

        // Get the stream containing all content returned by the requested server.
        var stream = response.GetResponseStream();

        // Open the stream using a StreamReader for easy access.
        if (stream == null)
        {
            return String.Empty;
        }

        var reader = new StreamReader(stream);

        // Read the content fully up to the end.
        var data = reader.ReadToEnd();

        // Clean up the streams.
        reader.Close();
        stream.Close();
        response.Close();

        return data;
    }
}