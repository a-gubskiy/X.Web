using System;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace X.Web
{
    [Serializable]
    public class Metadata
    {
        public String Title { get; set; }

        public String DefaultDescription { get; set; }
        public String DefaultKeywords { get; set; }

        public String WebsiteUrl { get; set; }

        public String FileStorageUrl { get; set; }
        public String FileStorageConnectionString { get; set; }

        public String BlobContainerName { get; set; }


        public String FacebookLogo { get; set; }
        public String FacebookApplicationId { get; set; }
        public String FacebookApplicationSecret { get; set; }

        public String SmtpHost { get; set; }
        public int SmtpPort { get; set; }
        public String SmtpUserName { get; set; }
        public String SmtpPassword { get; set; }

        public String MailFrom { get; set; }

        public bool IsDebuggingEnabled
        {
            get { return HttpContext.Current.IsDebuggingEnabled; }
        }

        public Metadata()
        {
            SmtpPort = 25;
        }

        private static Metadata _current;

        public static Metadata Current
        {
            get
            {
                if (_current == null)
                {
                    try
                    {
                        var configurationFileLocation = HttpContext.Current.Server.MapPath("~/web.config");
                        _current = Load(configurationFileLocation);
                    }
                    catch
                    {
                        return null;
                    }
                }

                return _current;
            }
        }

        public static Metadata Load(string configurationFileLocation)
        {
            var configuration = XDocument.Load(configurationFileLocation);

            var metaData = new Metadata
                {
                    Title = GetField(configuration, "Title", String.Empty),
                    WebsiteUrl = GetField(configuration, "WebsiteUrl", String.Empty),
                    DefaultDescription = GetField(configuration, "DefaultDescription", String.Empty),
                    DefaultKeywords = GetField(configuration, "DefaultKeywords", String.Empty),
                    FileStorageConnectionString = GetField(configuration, "FileStorageConnectionString", String.Empty),
                    FileStorageUrl = GetField(configuration, "FileStorageUrl", String.Empty),
                    BlobContainerName = GetField(configuration, "BlobContainerName", String.Empty),
                    FacebookLogo = GetField(configuration, "FacebookLogo", String.Empty),
                    FacebookApplicationId = GetField(configuration, "FacebookApplicationId", String.Empty),
                    FacebookApplicationSecret = GetField(configuration, "FacebookApplicationSecret", String.Empty),
                    SmtpHost = GetField(configuration, "SmtpHost", String.Empty),
                    MailFrom = GetField(configuration, "MailFrom", String.Empty),
                    SmtpUserName = GetField(configuration, "SmtpUserName", String.Empty),
                    SmtpPassword = GetField(configuration, "SmtpPassword", String.Empty),
                    SmtpPort = GetField(configuration, "SmtpPort", 25)

                };


            return metaData;
        }

        #region GetField

        private static int GetField(XContainer configuration, string fieldName, int defaultValue)
        {
            int result;
            return int.TryParse(GetField(configuration, fieldName, defaultValue.ToString()), out result) ? result : defaultValue;
        }

        private static string GetField(XContainer document, string fieldName, string defaultValue)
        {
            try
            {
                var configuration = document.Elements("configuration").First();
                var appSettings = configuration.Elements("appSettings").First();
                return appSettings.Elements().FirstOrDefault(n => n.Attribute("key").Value == fieldName).Attribute("value").Value;
            }
            catch { }

            return defaultValue;
        }

        #endregion
    }
}


