##X.Web


Library for extend ASP.NET functionality.
- Metadata class
- XController class
- XRequest class
- XPage class
- XMasterPage class
- XWebRequest

X.Web is a part of X-Framework library.

You can install X.Web from NuGet - http://nuget.org/packages/xweb/


####usage X.Web.Security.CredentialsMembershipProvider

Membership provider that allow load user information from web.config file.

Inspired by http://leastprivilege.com/2005/07/27/asp-net-membershipprovider-for-web-config/

#####Example

    <authentication mode="Forms">
      <forms name="auth" loginUrl="~/System/Login.aspx" protection="All" path="/" timeout="30">
        <credentials passwordFormat="Clear">
          <user name="u1" password="p1"/>
          <user name="u2" password="p2"/>
          <user name="u3" password="p3"/>
        </credentials>
      </forms>
    </authentication>


    <membership defaultProvider="CredentialsMembershipProvider" userIsOnlineTimeWindow="15">
      <providers>
        <clear />
        <add name="CredentialsMembershipProvider" type="X.Web.Security.CredentialsMembershipProvider" />
      </providers>
    </membership>


[![Bitdeli Badge](https://d2weczhvl823v0.cloudfront.net/ernado-x/x.web/trend.png)](https://bitdeli.com/free "Bitdeli Badge")

