using System;

namespace X.Web
{
    public interface IWebPage
    {
        String Title { get; set; }
        String Keywords { get; set; }
        String Description { get; set; }
    }
}
