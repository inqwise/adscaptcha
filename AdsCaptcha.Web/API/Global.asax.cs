using System;
using System.Web;
using Inqwise.AdsCaptcha.Model;
using Inqwise.AdsCaptcha.Cache;
using Inqwise.AdsCaptcha.BLL;
using System.Web.Routing;

public partial class Global : System.Web.HttpApplication
{
    protected void RegisterRouters(RouteCollection routes)
    {

    }

    protected void Application_Start(object sender, EventArgs e)
    {


        CacheCoreManager.GetInstanse().Init();
        CacheCoreManager.GetInstanse().LoadCache(ApplicationConfiguration.CacheTypes.Decode);
        CacheCoreManager.GetInstanse().LoadCache(ApplicationConfiguration.CacheTypes.Country);
        CacheCoreManager.GetInstanse().LoadCache(ApplicationConfiguration.CacheTypes.Language);
        CacheCoreManager.GetInstanse().LoadCache(ApplicationConfiguration.CacheTypes.Category);
        CacheCoreManager.GetInstanse().LoadCache(ApplicationConfiguration.CacheTypes.Theme);
        CacheCoreManager.GetInstanse().LoadCache(ApplicationConfiguration.CacheTypes.Dimension);


    }

    protected void Session_Start(object sender, EventArgs e)
    {

    }

    protected void Application_BeginRequest(object sender, EventArgs e)
    {
    }

    protected void Application_AuthenticateRequest(object sender, EventArgs e)
    {
    }

    protected void Application_Error(object sender, EventArgs e)
    {
        HttpContext ctx = HttpContext.Current;
        Exception exception = ctx.Server.GetLastError();

        string errorInfo = "An unhandled error occured at " + ctx.Request.Url.ToString() + " (Referrer: " + ctx.Request.UrlReferrer + ")";

        NLogManager.logger.FatalException(errorInfo, exception);
    }

    protected void Session_End(object sender, EventArgs e)
    {
    }

    protected void Application_End(object sender, EventArgs e)
    {
    }
}