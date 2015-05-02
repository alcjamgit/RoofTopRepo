using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using RoofTop.Core.ApplicationServices;


namespace RoofTop.Infrastructure.BLL.ApplicationServices
{
    public class UserFileService: IFileService
    {
        const int BUFFER_SIZE = 65536; // 65536 = 64 Kilobytes

        /// <summary>
        /// Upload a file in the specified folder
        /// </summary>
        /// <param name="inputStream">Stream input</param>
        /// <param name="destinationPath">Absolute destination path</param>
        /// <param name="fileName">Filename of input including extension</param>
        /// <param name="fileSize">File size in bytes</param>
        /// <returns></returns>
        public virtual string UploadFile(Stream inputStream, string destinationPath, string fileName, int fileSize)
        {
            //Build absolute file path
            var outputFileName = Guid.NewGuid().ToString();
            var fileExt = Path.GetExtension(fileName);
            CreateUserDirectory(destinationPath);
            var absolutePath = Path.Combine(destinationPath, outputFileName + fileExt);

            byte[] buffer = new byte[BUFFER_SIZE];

            //Read the stream from the input file and copies it to a new ouput file
            using (FileStream fileStream = System.IO.File.Create(absolutePath)) 
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
                    read = inputStream.Read(buffer, 0, len);
                    fileStream.Write(buffer, 0, len);
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
