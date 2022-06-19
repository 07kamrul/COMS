using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.FileStore
{
    public class FTPFileStore : IFileStore
    {
        private string _serverIP;
        private int _port;
        private string _username;
        private string _password;

        public FTPFileStore(FTPSettings settings)
        {
            _serverIP = settings.ServerIP;
            _port = settings.Port;
            _username = settings.UserName;
            _password = settings.Password;
        }

        public FTPFileStore(string server, int port, string userName, string password)
        {
            _serverIP = server;
            _port = port;
            _username = userName;
            _password = password;
        }

        public bool CheckIfFileExists(string fullPathWithName)
        {
            using (SftpClient sftp = new SftpClient(_serverIP, _port, _username, _password))
            {
                return sftp.Exists(fullPathWithName);
            }
        }

        public void CopyFile(string sourcePathWithName, string destinationPathWithName)
        {
            using(SftpClient sftp = new SftpClient(_serverIP, _port, _username, _password))
            {
                var inFile = sftp.Get(sourcePathWithName);
                inFile.MoveTo(destinationPathWithName);
            }
        }

        public void DeleteFile(IFileInfo fileInfo)
        {
            using (SftpClient sftp = new SftpClient(_serverIP, _port, _username, _password))
            {
                sftp.Delete(fileInfo.GetFilePathWithName());
            }
        }

        public void ReadFile(IFileInfo sourceFileInfo, Stream destinationFileStream)
        {
            using (SftpClient sftp = new SftpClient(_serverIP, _port, _username, _password))
            {
                sftp.DownloadFile(sourceFileInfo.GetFilePathWithName(), destinationFileStream);
            }
        }

        public Stream ReadFile(string filePathWithName)
        {
            using (SftpClient sftp = new SftpClient(_serverIP, _port, _username, _password))
            {
                using(Stream stream = new MemoryStream())
                {
                    sftp.DownloadFile(filePathWithName, stream);
                    return stream;
                }
            }
        }

        public byte[] ReadFileData(string fullPathWithName)
        {
            using (SftpClient sftp = new SftpClient(_serverIP, _port, _username, _password))
            {
                using (Stream stream = new MemoryStream())
                {
                    return sftp.ReadAllBytes(fullPathWithName);
                }
            }
        }

        public void WriteFile(IFileInfo destinationFileInfo, Stream sourceFileStream)
        {
            using (SftpClient sftp = new SftpClient(_serverIP, _port, _username, _password))
            {
                sftp.UploadFile(sourceFileStream, destinationFileInfo.GetFilePathWithName());
            }
        }

        public void WriteFile(IFileInfo destinationFileInfo, string sourceFilePathWithName)
        {
            using (SftpClient sftp = new SftpClient(_serverIP, _port, _username, _password))
            {
                using(FileStream fs = new FileStream(destinationFileInfo.GetFilePathWithName(), FileMode.Open))
                {
                    sftp.UploadFile(fs, destinationFileInfo.GetFilePathWithName());
                }
            }
        }
    }

    public class FTPSettings
    {
        public string ServerIP { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
