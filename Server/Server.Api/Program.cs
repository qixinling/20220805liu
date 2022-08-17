using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Server.Utils.Configuration_Utils;
using System;

namespace Server.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    string Url = "http://*:" + ConfigUtils.Configuration["port"];
                    webBuilder.UseUrls(Url).UseStartup<Startup>()
                    .ConfigureKestrel((context, serverOptions) =>
                    {
                        //为整个应用设置并发打开的最大 TCP 连接数,默认情况下，最大连接数不受限制 (NULL)
                        //serverOptions.Limits.MaxConcurrentConnections = 100;
                        //对于已从 HTTP 或 HTTPS 升级到另一个协议（例如，Websocket 请求）的连接，有一个单独的限制。 连接升级后，不会计入 MaxConcurrentConnections 限制
                        //serverOptions.Limits.MaxConcurrentUpgradedConnections = 100;
                        //获取或设置保持活动状态超时。 默认值为 2 分钟。
                        serverOptions.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(10);
                        //serverOptions.Limits.RequestHeadersTimeout = TimeSpan.FromMinutes(1);
                    });
                });
    }
}
