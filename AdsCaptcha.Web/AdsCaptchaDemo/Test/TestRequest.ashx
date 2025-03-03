<%@ WebHandler Language="C#" Class="TestRequest" %>

using System;
using System.Web;

public class TestRequest : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        try
        {
            string ip = System.Web.HttpContext.Current.Request.UserHostAddress;
            string headers = string.Empty;

            System.Collections.Specialized.NameValueCollection headersnames = System.Web.HttpContext.Current.Request.Headers;
            for (int i = 0; i < headersnames.Count; i++)
            {
                string key = headersnames.GetKey(i);
                string value = headersnames.Get(i);
                headers += value + ": " + value + "; ";
            }

            context.Response.Write("IP - " + ip + ", Headers - " + headers);
        }
        catch (Exception exc)
        {
            context.Response.Write(exc.ToString());
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}