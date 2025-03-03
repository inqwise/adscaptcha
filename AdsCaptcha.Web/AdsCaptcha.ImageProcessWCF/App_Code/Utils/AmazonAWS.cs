using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.SimpleDB;

namespace Inqwise.AdsCaptcha.ImageProcessWCF.Utils
{
    public class TrustAllCertificatePolicy : System.Net.ICertificatePolicy
    {
        public bool CheckValidationResult(ServicePoint sp, X509Certificate cert, WebRequest req, int problem)
        {
            return true;
        }
    }

    public static class AmazonAWS
    {
        private static string accessKeyID = "";
        private static string secretAccessKeyID = "";
        private static string bucketName = "";
        private static AmazonS3 storage = null;
        private static AmazonSimpleDB db = null;

        static AmazonAWS()
        {
            accessKeyID = ConfigurationManager.AppSettings["AWSAccessKey"];
            secretAccessKeyID = ConfigurationManager.AppSettings["AWSSecretKey"];
            bucketName = ConfigurationManager.AppSettings["AWSBucketName"];

            ServicePointManager.CertificatePolicy = new TrustAllCertificatePolicy();
        }

        public static bool Upload(Stream s, string filename, int width, int height, string contentType)
        {
            using (storage = Amazon.AWSClientFactory.CreateAmazonS3Client(accessKeyID, secretAccessKeyID))
            {
                try
                {
                    PutObjectRequest request = new PutObjectRequest();
                    request.WithMetaData("width", width.ToString())
                           .WithMetaData("height", height.ToString());
                    request.BucketName = bucketName;
                    request.Key = filename;
                    request.ContentType = contentType;
                    request.InputStream = s;
                    request.StorageClass = (ConfigurationManager.AppSettings["AWSReducedRedundancy"] == "true"
                                                ? S3StorageClass.ReducedRedundancy
                                                : S3StorageClass.Standard);
                    request.CannedACL = S3CannedACL.PublicRead;

                    S3Response response = storage.PutObject(request);
                    response.Dispose();

                    return true;
                }
                catch (AmazonS3Exception amazonS3Exception)
                {
                    if (amazonS3Exception.ErrorCode != null &&
                        (amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId") ||
                         amazonS3Exception.ErrorCode.Equals("InvalidSecurity")))
                    {
                        throw new Exception(
                            "Please check the provided AWS Credentials. If you haven't signed up for Amazon S3, please visit http://aws.amazon.com/s3");
                    }
                    else
                    {
                        throw amazonS3Exception;
                    }
                }
            }
        }

        public static bool Delete(string filename)
        {
            using (storage = Amazon.AWSClientFactory.CreateAmazonS3Client(accessKeyID, secretAccessKeyID))
            {
                try
                {
                    /*
                        PutObjectRequest request = new PutObjectRequest();
                        S3Response response = storage.PutObject(request);
                        response.Dispose();
                        */
                    return true;
                }
                catch (AmazonS3Exception amazonS3Exception)
                {
                    if (amazonS3Exception.ErrorCode != null &&
                        (amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId") ||
                         amazonS3Exception.ErrorCode.Equals("InvalidSecurity")))
                    {
                        throw new Exception(
                            "Please check the provided AWS Credentials. If you haven't signed up for Amazon S3, please visit http://aws.amazon.com/s3");
                    }
                    else
                    {
                        throw amazonS3Exception;
                    }
                }
            }
        }
    }
}