using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace X.Web;

/// <summary>
/// Deprecated code
/// </summary>
[PublicAPI]
public abstract class XPage : Page, IWebPage
{
    public virtual string Title { get; set; }
    
    public virtual string Keywords { get; set; }
    
    public virtual string Description { get; set; }

    /// <summary>
    /// Advanced request with safe parameter gettin functions
    /// </summary>
    public XRequest XRequest { get; private set; }

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
        
        if (cultureInfo == null || !allowedCultures.Contains(cultureInfo))
        {
            cultureInfo = defaultPageCulture;
        }

        return cultureInfo;
    }
}