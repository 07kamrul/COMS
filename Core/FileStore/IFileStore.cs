using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.FileStore
{
    public interface IFileStore
    {
        void ReadFile(IFileInfo sourceFileInfo, Stream destinationFileStream);
        Stream ReadFile(string filePathWithName);
        void WriteFile(IFileInfo destinationFileInfo, Stream sourceFileStream);
        void WriteFile(IFileInfo destinationFileInfo, string sourceFilePathWithName);
        void CopyFile(string sourcePathWithName, string destinationPathWithName);
        bool CheckIfFileExists(string fullPathWithName);
        byte[] ReadFileData(string fullPathWithName);
        void DeleteFile(IFileInfo fileInfo);
    }
}
