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
                        //Ϊ����Ӧ�����ò����򿪵���� TCP ������,Ĭ������£������������������ (NULL)
                        //serverOptions.Limits.MaxConcurrentConnections = 100;
                        //�����Ѵ� HTTP �� HTTPS ��������һ��Э�飨���磬Websocket ���󣩵����ӣ���һ�����������ơ� ���������󣬲������ MaxConcurrentConnections ����
                        //serverOptions.Limits.MaxConcurrentUpgradedConnections = 100;
                        //��ȡ�����ñ��ֻ״̬��ʱ�� Ĭ��ֵΪ 2 ���ӡ�
                        serverOptions.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(10);
                        //serverOptions.Limits.RequestHeadersTimeout = TimeSpan.FromMinutes(1);
                    });
                });
    }
}
