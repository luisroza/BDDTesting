using Microsoft.Extensions.Configuration;
using System.IO;

namespace WebStore.BDD.Tests.Config
{
    public class ConfigurationHelper
    {
        private readonly IConfiguration _config;

        public ConfigurationHelper()
        {
            _config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
        }

        public string WebDrivers => $"{_config.GetSection("WebDrivers").Value}";
        public string ProductUrl => $"{DomainUrl}{_config.GetSection("ProductUrl").Value}";
        public string DisplayUrl => $"{DomainUrl}{_config.GetSection("DisplayUrl").Value}";
        public string RegisterUrl => $"{DomainUrl}{_config.GetSection("RegisterUrl").Value}";
        public string LoginUrl => $"{DomainUrl}{_config.GetSection("LoginUrl").Value}";
        public string CartUrl => $"{DomainUrl}{_config.GetSection("CartUrl").Value}";
        public string DomainUrl => $"{_config.GetSection("DomainUrl").Value}";
        public string FolderParh => Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory()));
        public string FolderPicture => $"{FolderParh}{_config.GetSection("FolderPicture").Value}";
        public string MAX_QUANTITY_ALLOWED => $"{_config.GetSection("MAX_QUANTITY_ALLOWED").Value}";
    }
}
