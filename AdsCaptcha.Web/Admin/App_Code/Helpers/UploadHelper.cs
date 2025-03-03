using System;
using System.IO;
using System.Web;
using Inqwise.AdsCaptcha.Common;
using Inqwise.AdsCaptcha.Common.Data;

namespace Admin.Helpers
{
    public class UploadHelper
    {
        public string FileNamePrefix { get; private set; }
        public string UploadDir { get; set; }
        public int MaxFileSize { get; set; }
        public int MinFileSize { get; set; }
        public bool DiscardAbortedUploads { get; set; }

        public UploadHelper()
        {
            PropertiesInit(null);
        }

        public UploadHelper(string uploadDir)
        {
            PropertiesInit(uploadDir);
        }

        private void PropertiesInit(string uploadDir)
        {
            this.FileNamePrefix = string.Format("{0:x2}", DateTime.Now.Ticks);
            this.UploadDir = uploadDir;
            this.MaxFileSize = 10124000;
            this.MinFileSize = 1;
            this.DiscardAbortedUploads = true;
        }

        public bool Validate(HttpPostedFile uploadedFile, NewImageArgs file)
        {
            if (file.HasError)
            {
                return false;
            }

            if (String.IsNullOrEmpty(file.Name))
            {
                file.Error = AdsCaptchaErrors.MissingFileName;
                return false;
            }
            if (file.Name.IndexOfAny(Path.GetInvalidFileNameChars()) != -1)
            {
                file.Error = AdsCaptchaErrors.InvalidFileName;
                return false;
            }

            /*if (!Regex.IsMatch(file.Name, this.AcceptFileTypes, RegexOptions.Multiline | RegexOptions.IgnoreCase))
            {
                file.Error = "acceptFileTypes";
                return false;
            }*/

            if (this.MaxFileSize > 0 && (file.Size > this.MaxFileSize))
            {
                file.Error = AdsCaptchaErrors.MaxFileSize;
                return false;
            }

            if (this.MinFileSize > 1 && (file.Size < this.MinFileSize))
            {
                file.Error = AdsCaptchaErrors.MinFileSize;
                return false;
            }

            return true;
        }

        public NewImageArgs FileUploadHandle(HttpPostedFile uploadedFile, NewImageArgs file)
        {
            if (Validate(uploadedFile, file))
            {
                long actualFileSize = uploadedFile.InputStream.Length;
                file.AbsoluteFilePath = this.UploadDir + FileNamePrefix + file.Name.StripNonAsciiChars();
                bool appendFile = !this.DiscardAbortedUploads && File.Exists(file.AbsoluteFilePath)
                                    || file.Size > actualFileSize;


                // multipart/formdata uploads (POST method uploads)
                if (appendFile)
                {
                    using (FileStream fs = File.Open(file.AbsoluteFilePath, FileMode.Append))
                    {
                        uploadedFile.InputStream.CopyTo(fs);
                        fs.Flush();
                    }
                }
                else
                {
                    using (FileStream fs = File.OpenWrite(file.AbsoluteFilePath))
                    {
                        uploadedFile.InputStream.CopyTo(fs);
                        fs.Flush();
                    }
                }

                /*
                if (file.Size == new FileInfo(filePath).Length)
                {
                    //Validate again for chunked files.
                    if (Validate(uploadedFile, file, error, index))
                    {
                        //if (this.orient_images)
                        //{
                        //    //orient_image(file_path);
                        //}
                        //Create different versions
                        file.url = this.upload_url + HttpUtility.UrlEncode(file.Name);
                    }
                }
                else
                {
                    if (!appendFile && this.DiscardAbortedUploads)
                    {
                        FileInfo.Delete(filePath);
                        file.Error = "abort";
                    }

                }*/

                if (file.Size > actualFileSize && !appendFile && this.DiscardAbortedUploads)
                {
                    File.Delete(file.AbsoluteFilePath);
                    file.Error = AdsCaptchaErrors.Abort;
                }
            }

            return file;
        }
    }
}