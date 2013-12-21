﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace X.Web
{
    public class XWebRequest
    {
        public WebRequest Request { get; private set; }

        public string Status { get; set; }

        public XWebRequest(string url, string method = "GET")
            : this(url, method, String.Empty)
        {
        }

        public XWebRequest(string url, string method, string data, IWebProxy proxy = null)
        {
            Request = CreateRequest(url, method, data, proxy);
        }

        public XWebRequest(string url, string method, IEnumerable<KeyValuePair<string, string>> form, IWebProxy proxy = null)
        {
            Request = CreateRequest(url, method, form, proxy);
        }

        public static WebRequest CreateRequest(string url, string method, IEnumerable<KeyValuePair<string, string>> form, IWebProxy proxy = null)
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

            return CreateRequest(url, method, sb.ToString(), proxy);
        }

        public static WebRequest CreateRequest(string url, string method, string data, IWebProxy proxy = null)
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
                    url = String.Format("{0}?{1}", url, data);
                    request = WebRequest.Create(url);
                    request.Proxy = proxy ?? request.Proxy;
                }
            }

            return request;
        }

        public string GetResponse()
        {
            // Get the original response.
            var response = Request.GetResponse();

            Status = ((HttpWebResponse)response).StatusDescription;

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
}