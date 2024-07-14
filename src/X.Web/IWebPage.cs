using JetBrains.Annotations;

namespace X.Web;

[PublicAPI]
public interface IWebPage
{
    string Title { get; set; }
    
    string Keywords { get; set; }
    
    string Description { get; set; }
}