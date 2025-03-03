using System;
using System.Configuration;
using System.IO;
using System.Web;
using Admin.Helpers;
using Jayrock.Json;
using Inqwise.AdsCaptcha.Common;
using Inqwise.AdsCaptcha.Common.Data;
using Inqwise.AdsCaptcha.Managers;

namespace Admin.Handlers
{
    public class ImportHandler : BaseHandler, IHttpHandler
    {
        private static readonly NLog.Logger Log = NLog.LogManager.GetCurrentClassLogger();

        private HttpRequest _request;
        private HttpResponse _response;
        private HttpServerUtility _server;
        static readonly string TempFolderPath = ConfigurationManager.AppSettings["TempUploadFolder"];

        public void ProcessRequest(HttpContext context)
        {
            _request = context.Request;
            _response = context.Response;
            _server = context.Server;


            _response.Clear();
            _response.AddHeader("Pragma", "no-cache");
            _response.AddHeader("Cache-Control", "no-store, no-cache, must-revalidate");
            _response.AddHeader("Content-Disposition", "inline; filename=\"files.json\"");
            _response.AddHeader("X-Content-Type-Options", "nosniff");
            _response.AddHeader("Access-Control-Allow-Origin", "*");
            _response.AddHeader("Access-Control-Allow-Methods", "POST");
            _response.AddHeader("Access-Control-Allow-Headers", "X-FileInfo-Name, X-FileInfo-Type, X-FileInfo-Size");

            var fileInfo = new NewImageArgs { AllowedContentTypes = ImagesManager.AllowedContextTypes.Value, AllowedDimansions = ImagesManager.AllowedDimensions.Value };
            AdsCaptchaOperationResult result = null;
            Object output = null;
            try
            {
                int imageTypeId;
                if (!int.TryParse(_request["imageTypeId"], out imageTypeId) || !Enum.IsDefined(typeof(ImageType), imageTypeId))
                {
                    imageTypeId = (int)ImageType.Random;
                }

                UploadFile(fileInfo);

                long? imageId = null;
                if (fileInfo.HasError)
                {
                    result = AdsCaptchaOperationResult.ToError(fileInfo.Error.Value);
                }
               
                if(null == result)
                {
                    fileInfo.ImageType = (ImageType)imageTypeId;
                    AdsCaptchaOperationResult<long> addResult = ImagesManager.Add(fileInfo);
                    if (addResult.HasError)
                    {
                        fileInfo.Error = addResult.Error;
                    }
                    else
                    {
                        imageId = addResult.Value;
                    }
                }

                if (null == result)
                {
                    _response.Clear();
                    _response.AddHeader("Vary", "Accept");

                    var jo = new JsonObject();

                    if (fileInfo.HasError)
                    {
                        jo.Put("error", fileInfo.Error);
                    }
                    else
                    {
                        jo.Put("width", fileInfo.Width);
                        jo.Put("height", fileInfo.Height);
                        jo.Put("imageId", imageId);
                    }

                    string redirect = null;
                    if (_request["redirect"] != null)
                    {
                        redirect = _request["Redirect"];
                    }
                    if (redirect != null)
                    {
                        _response.AddHeader("Location,", String.Format(redirect, _server.UrlEncode(jo.ToString())));
                        _response.End();
                    }

                    output = jo;
                }
                else
                {
                    output = result.ToJson();
                }

                _response.Write(output);
            }
            catch (Exception ex)
            {
                Log.ErrorException("ImportHandler: Unexpected error occured", ex);
                _response.Write(GetGeneralErrorString(ex.ToString()));
            }

            if (_request.ServerVariables["HTTP_ACCEPT"] != null && _request.ServerVariables["HTTP_ACCEPT"].IndexOf("application/json") >= 0)
            {
                _response.AddHeader("Content-type", "application/json");
            }
            else
            {
                _response.AddHeader("Content-type", "text/plain");
            }
            _response.End();
        }

        public bool IsReusable {
            get { return false; }
        }

        private void UploadFile(NewImageArgs fileInfo)
        {
            var uploadHelper = new UploadHelper(TempFolderPath);
            HttpFileCollection upload = _request.Files;

            for (int i = 0; i < upload.Count; i++)
            {
                HttpPostedFile file = upload[i];
                //fileInfo.type = (Path.GetExtension(file.FileName) ?? String.Empty).ToLower();
                fileInfo.Name = Path.GetFileName(file.FileName);
                fileInfo.Size = file.InputStream.Length;

                //fileInfo.ResourceType = ;
                if (_request.Headers["X-FileInfo-Size"] != null)
                {
                    fileInfo.Size = long.Parse(_request.Headers["X-FileInfo-Size"]);
                }

                uploadHelper.FileUploadHandle(file, fileInfo);
            }
        }

    }
}