using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;


namespace RoofTop.Infrastructure.BLL.ApplicationServices
{
    public class UserFileService
    {
        private readonly HttpPostedFileBase _httpPostedFileBase;
        private readonly HttpServerUtilityBase _server;
        private readonly string _userId;

        public UserFileService(HttpPostedFileBase fileBase, HttpServerUtilityBase server, string userId)
        {
            _httpPostedFileBase = fileBase;
            _server = server;
            _userId = userId;
        }


        /// <summary>
        /// Upload the image to the server in the user's image directory
        /// </summary>
        public string UploadImage()
        {
            //Save Ad Image to the server and add to database
            var fileNameNoExten = Guid.NewGuid().ToString();
            var fileExt = Path.GetExtension(_httpPostedFileBase.FileName);
            var relativePath = string.Format( "~/Content/UserFiles/{0}", _userId );
            var absPath1 = _server.MapPath(relativePath);
            CreateUserDirectory(absPath1);
            var absolutePath = Path.Combine( _server.MapPath(relativePath) , fileNameNoExten + fileExt );
            _httpPostedFileBase.SaveAs(absolutePath);
            
            return fileNameNoExten + fileExt;
        }

        /// <summary>
        /// Upload the image to the server in the user's image directory
        /// </summary>
        public string UploadImage(Stream reader, string fileName, int fileSize)
        {
            //TODO: Write an uploader without dependency on HttpPostedFileBase
            //http://stackoverflow.com/questions/18539000/how-to-save-to-disk-httppostedfilebase-object-as-a-file


            //Save Ad Image to the server and add to database
            var fileNameNoExten = Guid.NewGuid().ToString();
            var fileExt = Path.GetExtension(fileName);
            var relativePath = string.Format("~/Content/UserFiles/{0}", _userId);
            var absPath1 = _server.MapPath(relativePath);
            CreateUserDirectory(absPath1);
            var absolutePath = Path.Combine(_server.MapPath(relativePath), fileNameNoExten + fileExt);

            const int BUFFER_SIZE = 65536; // 65536 = 64 Kilobytes
            byte[] buffer = new byte[BUFFER_SIZE];
            using (FileStream fs = System.IO.File.Create(absolutePath)) 
            {
                int read = -1;
                int pos = 0;
                do
                {
                    int len;
                    if (fileSize < pos + BUFFER_SIZE)
                    {
                        len = fileSize - pos;
                    }
                    else
                    {
                        len = BUFFER_SIZE;
                    }
                    read = reader.Read(buffer, 0, len);
                    fs.Write(buffer, 0, len);
                    pos += read;
                } while (read > 0);
            }

            return absolutePath;
        }
        /// <summary>
        /// Creates a user directory for its ad images if it doesn't exist yet
        /// </summary>
        private void CreateUserDirectory(string userAbsoluteDirectory)
        {
            if (!Directory.Exists(userAbsoluteDirectory))
            {
                Directory.CreateDirectory(userAbsoluteDirectory);
            }
        }

    }
}
