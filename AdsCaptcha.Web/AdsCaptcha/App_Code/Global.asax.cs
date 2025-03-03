using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using Inqwise.AdsCaptcha.BLL;
using Inqwise.AdsCaptcha.Cache;
using Inqwise.AdsCaptcha.Model;

public class Global : System.Web.HttpApplication
{
    protected void Application_Start(object sender, EventArgs e)
    {
        CacheCoreManager.GetInstanse().Init();
        CacheCoreManager.GetInstanse().LoadCache(CacheTypes.Decode);
        CacheCoreManager.GetInstanse().LoadCache(CacheTypes.Country);
        CacheCoreManager.GetInstanse().LoadCache(CacheTypes.Language);
        CacheCoreManager.GetInstanse().LoadCache(CacheTypes.Category);
        CacheCoreManager.GetInstanse().LoadCache(CacheTypes.Theme);
        CacheCoreManager.GetInstanse().LoadCache(CacheTypes.Dimension);
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
        if (null != exception.InnerException && exception is HttpException)
        {
            exception = exception.InnerException;
        }

        if (exception is HttpException)
        {
            // Page not Found
        }
        else
        {
            string errorInfo = "An unhandled error occured at " + ctx.Request.Url + " (Referrer: " + ctx.Request.UrlReferrer + ")";
            NLogManager.logger.FatalException(errorInfo, exception);
        }
    }

    protected void Session_End(object sender, EventArgs e)
    {

    }

    protected void Application_End(object sender, EventArgs e)
    {
    }
}