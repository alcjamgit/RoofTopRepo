using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Util;
using RoofTop.Core.ApplicationServices;


namespace RoofTop.Infrastructure.BLL.ApplicationServices
{
    public class AmazonS3FileService: IFileService
    {
        private ICurrentUserService _currentUserService;
        const int BUFFER_SIZE = 65536; // 65536 = 64 Kilobytes
        string AMAZON_S3_BUCKET_NAME = "";

        public AmazonS3FileService(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }

        /// <summary>
        /// Upload a file in Amazon s3
        /// http://stackoverflow.com/questions/22084987/how-to-send-httppostedfilebase-to-s3-via-aws-sdk
        /// </summary>
        /// <param name="inputStream">Stream input</param>
        /// <param name="destinationPath">Absolute destination path</param>
        /// <param name="fileName">Filename of input including extension</param>
        /// <param name="fileSize">File size in bytes</param>
        /// <returns></returns>
        public virtual string UploadFile(Stream inputStream, string destinationPath, string fileName, int fileSize)
        {

            using (IAmazonS3 s3Client = new AmazonS3Client(RegionEndpoint.USWest2)) 
            { 
                //we don't need to create the folder inside the bucket because the concept of folder in amazon is a bit different
                //http://stackoverflow.com/questions/7039771/upload-a-file-to-s3-buckets-folder-using-asp-net-sdk
                var fileNameWithFolder = _currentUserService.UserID + "/" + fileName;
                // Setup request for putting an object in S3. 
                PutObjectRequest request = new PutObjectRequest 
                { 
                    BucketName = AMAZON_S3_BUCKET_NAME,
                    Key = fileNameWithFolder, 
                    ContentBody = "Sample Content"
                }; 
  
                // Make service call and get back the response. 
                PutObjectResponse response = s3Client.PutObject(request); 
            }

            return "file name here";
        }


    }
}
