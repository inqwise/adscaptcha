<%@ WebHandler Language="C#" Class="Validate" %>

using System;
using System.Web;

public class Validate : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        string request = context.Request.QueryString["r"];
        string answer = context.Request.QueryString["a"];
        
        /*
        Request cacherequest = (Request)CacheManager.Instance.GetCachedItem(request);
        try
        {
            if (Math.Abs(cacherequest.Challenge - Convert.ToInt32(answer)) < 3)
            {
                context.Response.Write("1");
            }
            else
            {
                context.Response.Write("0");
            }
        }
        catch
        {
             context.Response.Write("0");
        }


        if (null != cacherequest)
        {
            CacheManager.Instance.RemoveCachedItem(cacherequest.ID); 
        }
         */
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}