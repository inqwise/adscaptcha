using System;
using System.Configuration;
using System.Web;
using Jayrock.Json;
using Inqwise.AdsCaptcha.Common;

namespace Inqwise.AdsCaptchaDemo.Handlers
{
    public abstract class BaseHandler<TOutput> : IHttpHandler
    {
        protected const string LIST_PARAM_NAME = "list";
        protected const string CAPTCHA_ID_QS_KEY = "CaptchaId";
        protected const string PUBLIC_KEY_QS_KEY = "PublicKey";
        protected const string CAPTCHA_ID_JO_KEY = "captchaId";
        protected const string PUBLIC_KEY_JO_KEY = "publicKey";
        protected const string CHALLANGE_JO_KEY = "challenge";
        protected const string WIDTH_JO_KEY = "width";
        protected const string HEIGHT_JO_KEY = "height";
        protected const string COUNT_OF_FRAMES_JO_KEY = "count";
        protected static readonly string API_URL = ConfigurationManager.AppSettings["API"];

        private static readonly NLog.Logger Log = NLog.LogManager.GetCurrentClassLogger();

        public void ProcessRequest(HttpContext context)
        {
            Callback = context.Request["callback"];
            ContextType = typeof(TOutput) is JsonObject ? "application/json" : "text/plain";
            object output;
            try
            {
                output = Process(context);
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

        public string ContextType
        {
            get; protected set;
        }

        public string Callback { get; protected set; }

        protected abstract TOutput Process(HttpContext context);

        public bool IsReusable
        {
            get { return false; }
        }

        public string GetGeneralErrorString(string description)
        {
            return GetErrorJson(AdsCaptchaErrors.GeneralError, description).ToString();
        }

        public JsonObject GetMethodNotFoundJson(string methodName)
        {
            return GetErrorJson(AdsCaptchaErrors.MethodNotFound, methodName);
        }

        protected JsonObject GetErrorJson(AdsCaptchaErrors errorCode, string details = null)
        {
            var errJo = new JsonObject();
            errJo.Put("result", errorCode);
            errJo.Put("description", details);
            return errJo;
        }

        protected JsonObject GetJsonOk()
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
}