<%@ Application Language="C#" %>

<script runat="server">

    private static readonly NLog.Logger Log = NLog.LogManager.GetCurrentClassLogger();
    
    void RegisterRouters(System.Web.Routing.RouteCollection routes)
    {
        routes.MapPageRoute("landing-pages-demos", "lp/{demo}", "~/LandingDemo/LandingPage.aspx");
    }

    void Application_Start(object sender, EventArgs e) 
    {
        RegisterRouters(System.Web.Routing.RouteTable.Routes);

        /*
        using (AdsCaptcha_DemoModel.AdsCaptcha_ImagesEntities ent = new AdsCaptcha_DemoModel.AdsCaptcha_ImagesEntities())
        {
            int[] images = (from m in ent.T_IMAGES
                            where !m.IsDeleted
                            select m.ImageID).ToArray();

            string key = string.Empty;
            for (int i = 0; i < images.Length; i++)
            {
                key = "Image_" + images[i].ToString();
                
                if (CacheManager.Instance.GetCachedItem(key) == null)
                {
                    int imgid = images[i];
                    var defs = (from d in ent.T_IMAGES_DEFORMATIONS
                                where d.ImageID == imgid
                              select d).ToList();

                    List<ImageDeformation> cachedefs = new List<ImageDeformation>();

                    foreach (var d in defs)
                    {
                        ImageDeformation def = new ImageDeformation();
                        def.DeformationID = d.DeformationID;
                        def.ImageStream = d.ImageStream;
                        cachedefs.Add(def);
                    }

                    CacheManager.Instance.AddToCache(key, cachedefs, TimeSpan.FromDays(300));
                }
            }
        }
         */

    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  Code that runs on application shutdown

    }
        
    void Application_Error(object sender, EventArgs e) 
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
            Log.FatalException(errorInfo, exception);
        }
    }

    void Session_Start(object sender, EventArgs e) 
    {
        // Code that runs when a new session is started

    }

    void Session_End(object sender, EventArgs e) 
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }
       
</script>
