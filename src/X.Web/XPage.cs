using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace X.Web;

/// <summary>
/// Deprecated code
/// </summary>
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
    /// Url argument - lng, default - null
    /// </summary>
    public virtual CultureInfo CurrentCulture
    {
        get { return XPage.GetCurrentCulture(this, this.DefaultCulture, this.AllowedCultures, true); }
    }

    private const string SessionLanguageKey = "session_language";

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
        
        
        if (cultureInfo == null || !allowedCultures.Contains(cultureInfo))
        {
            cultureInfo = defaultPageCulture;
        }

        return cultureInfo;
    }
}