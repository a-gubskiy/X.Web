using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;

namespace X.Web;

/// <summary>
/// Web page controller
/// </summary>
[PublicAPI]
public abstract class XController : Controller, IWebPage
{
    /// <summary>
    /// 
    /// </summary>
    public virtual string Title
    {
        get => ViewBag.Title;
        set => ViewBag.Title = value;
    }

    /// <summary>
    /// 
    /// </summary>
    public virtual string Keywords
    {
        get => ViewBag.Keywords;
        set => ViewBag.Keywords = value;
    }

    /// <summary>
    /// 
    /// </summary>
    public virtual string Description
    {
        get => ViewBag.Description;
        set => ViewBag.Description = value;
    }
}