using System;
using System.Configuration;
using System.Linq;
using System.Security.Authentication;
using System.Web;
using System.Web.SessionState;
using Jayrock.Json;
using Inqwise.AdsCaptcha.Common;

namespace Admin.Handlers
{
    public abstract class BaseHandler
    {
        public string GetGeneralErrorString(string description)
        {
            return GetErrorJson(AdsCaptchaErrors.GeneralError, description).ToString();
        }

        public JsonObject GetMethodNotFoundJson(string methodName)
        {
            return GetErrorJson(AdsCaptchaErrors.MethodNotFound, methodName);
        }

        internal static JsonObject GetErrorJson(AdsCaptchaErrors errorCode, string details = null)
        {
            var errJo = new JsonObject();
            errJo.Put("result", errorCode);
            errJo.Put("description", details);
            return errJo;
        }

        internal static JsonObject GetJsonOk()
        {
            var jo = new JsonObject();
            jo.Put("result", "success");
            return jo;
        }

        protected bool ExactOne(params object[] args)
        {
            bool flag = false;
            foreach (var o in args)
            {
                if (null != o)
                {
                    if (flag)
                    {
                        flag = false;
                        break;
                    }
                    flag = true;
                }
            }

            return flag;
        }
    }

    public abstract class BaseHandler<TOutput> : BaseHandler, IHttpHandler, IRequiresSessionState
    {
        protected const string LIST_PARAM_NAME = "list";
        protected static readonly string API_URL = ConfigurationManager.AppSettings["API"];

        private static readonly NLog.Logger Log = NLog.LogManager.GetCurrentClassLogger();

        public void ProcessRequest(HttpContext context)
        {
            Callback = context.Request["callback"];
            ContextType = typeof (TOutput) is JsonObject ? "application/json" : "text/plain";
            object output;
            string requestQuery = null;
            try
            {
                if (context.Session["AdminId"] == null)
                {
                    throw new AuthenticationException("Not logged in");
                }

                context.Response.ContentType = "application/json";
                requestQuery = context.Request["rq"];
                var requestJson = Jayrock.Json.Conversion.JsonConvert.Import<JsonObject>(requestQuery);
                var method = requestJson.First<JsonMember>();

                output = Process(method.Name, (JsonObject)method.Value);
                context.Response.ContentType = ContextType;
            }
            catch (Exception ex)
            {
                Log.ErrorException(
                    string.Format("ProcessRequest: Unexpected result occured"), ex);
                output = GetGeneralErrorString(ex.ToString());
                context.Response.ContentType = "application/json";
            }

            output = (null == Callback ? output : String.Concat(Callback, "(", output, ")"));

            context.Response.Output.Write(output);
        }

        public string ContextType { get; protected set; }

        public string Callback { get; protected set; }

        protected abstract TOutput Process(string methodName, JsonObject args);

        public bool IsReusable
        {
            get { return false; }
        }
    }
}