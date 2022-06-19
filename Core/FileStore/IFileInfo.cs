using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.FileStore
{
    public interface IFileInfo
    {
        string GetFilePathWithName();
        string GetInformation();
    }

    public class NoFIleInfo : IFileInfo
    {
        public string GetFilePathWithName()
        {
            return "No File Found";
        }
        public string GetInformation()
        {
            return "No File Found";
        }
    }

    public class LocalFileInfo : IFileInfo
    {
        public string Name { get; set; }
        public string FullName { get; set; }
        public long Length { get; set; }

        public string GetFilePathWithName()
        {
            return FullName;
        }

        public string GetInformation()
        {
            return $"File name: {Name}, Full file name: {FullName}, Length:{Length}";
        }
    }
}
