using System;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;

namespace X.Web;

/// <summary>
/// Metadata for website
/// </summary>
[Serializable]
[PublicAPI]
public class Metadata
{
    public string Title { get; set; }

    public string DefaultDescription { get; set; }

    public string DefaultKeywords { get; set; }

    public string WebsiteUrl { get; set; }

    public string FacebookLogo { get; set; }

    public string FacebookApplicationId { get; set; }

    public string SupportEmail { get; set; }
}