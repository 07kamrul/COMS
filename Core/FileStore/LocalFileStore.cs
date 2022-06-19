using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.FileStore
{
    public class LocalFileStore : IFileStore
    {
        public bool CheckIfFileExists(string fullPathWithName)
        {
            throw new NotImplementedException();
        }

        public void CopyFile(string sourcePathWithName, string destinationPathWithName)
        {
            throw new NotImplementedException();
        }

        public void DeleteFile(IFileInfo fileInfo)
        {
            throw new NotImplementedException();
        }

        public void ReadFile(IFileInfo sourceFileInfo, Stream destinationFileStream)
        {
            throw new NotImplementedException();
        }

        public Stream ReadFile(string filePathWithName)
        {
            throw new NotImplementedException();
        }

        public byte[] ReadFileData(string fullPathWithName)
        {
            throw new NotImplementedException();
        }

        public void WriteFile(IFileInfo destinationFileInfo, Stream sourceFileStream)
        {
            throw new NotImplementedException();
        }

        public void WriteFile(IFileInfo destinationFileInfo, string sourceFilePathWithName)
        {
            throw new NotImplementedException();
        }
    }
}
