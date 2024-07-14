using System;
using System.Globalization;
using System.Threading;
using Microsoft.AspNetCore.Mvc;

namespace X.Web;

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

    public CultureInfo CurrentCulture { get; protected set; }

    /// <summary>
    /// 
    /// </summary>
    protected virtual void DetectCulture()
    {
        CurrentCulture = Thread.CurrentThread.CurrentCulture;
    }
}