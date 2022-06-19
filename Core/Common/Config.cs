using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Common
{
    public class Config
    {
        private IConfiguration _config;
        public Config(IConfiguration config)
        {
            _config = config;
        }

        public Jwt Jwt => new Jwt
        {
            Key = _config["Jwt:Key"],
            TokenLifespan = _config["Jwt:TokenLifespan"],
            RememberMeTokenLifespan = _config["Jwt:RememberMeTokenLifespan"]
        };

        public ConnectionStrings ConnectionStrings => new ConnectionStrings
        {
            DBConnection = _config["ConnectionStrings:DBConnection"]
        };

        public LogSettings LogSettings => new LogSettings
        {
            LogLevel = _config["LogSettings:LogLevel"],
            LogPath = _config["LogSettings:LogPath"]
        };

        public LocalFileStoreSettings LocalFileStore => new LocalFileStoreSettings
        {
            Path = _config["LocalFileStore:Path"]
        };
    }

    public class Jwt
    {
        public string Key { get; set; }
        public string TokenLifespan { get; set; }
        public string RememberMeTokenLifespan { get; set; }
    }
    public class ConnectionStrings
    {
        public string DBConnection { get; set; }
    }
    public class LogSettings
    {
        public string LogLevel { get; set; }
        public string LogPath { get; set; }
    }
    public class LocalFileStoreSettings
    {
        public string Path { get; set; }
    }

}
