using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace Server.Utils.Configuration_Utils
{
    public class ConfigUtils
    {
        public static IConfiguration Configuration { get; set; }

        /// <summary>
        /// 读取appsettings.json节点
        /// </summary>
        static ConfigUtils()
        {
            //ReloadOnChange = true 当appsettings.json被修改时重新加载
            Configuration = new ConfigurationBuilder()
                .Add(new JsonConfigurationSource
                {
                    Path = "appsettings.json",
                    ReloadOnChange = true
                }).Build();
        }
    }
}
