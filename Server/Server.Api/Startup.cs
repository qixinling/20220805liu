using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Server.Models.DataBaseModels;
using Server.Utils.Configuration_Utils;
using Server.Models;
using System;
using System.IO;
using Server.Logs;
using Server.Quartz;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Server.Utils.WebSocket_Utils;
using System.Globalization;
using Server.Api.Method;
using Server.Bonus.Utils;
using Server.Api.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace Server.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

#if DEBUG
            // 添加Swagger
            services.AddSwaggerGen(c =>
            {
                //v1 这个后面也要统一大小写
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
                var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);
                var xmlPath = Path.Combine(basePath, "Server.Api.xml");//SwaggerDemo.xml这个在解决方案生成的时候xml生成文档
                c.IncludeXmlComments(xmlPath);
            });
#endif         

            services.AddControllers().AddNewtonsoftJson(options =>
            {
                //把NewtonsoftJson默认设置添加到所有API控制器

                //忽略循环引用
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

                //日期格式化
                options.SerializerSettings.DateFormatHandling = DateFormatHandling.MicrosoftDateFormat;
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";

                //空值处理
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            });

            //修改JsonConvert默认设置
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                //循环引用处理
                Formatting = Formatting.Indented,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,

                //日期类型默认格式化处理
                DateFormatHandling = DateFormatHandling.MicrosoftDateFormat,
                DateFormatString = "yyyy-MM-dd HH:mm:ss",

                //空值处理
                NullValueHandling = NullValueHandling.Ignore
            };

            string server;
#if DEBUG
            server = ConfigUtils.Configuration["AppSettings:server_debug"];
#else
            server = ConfigUtils.Configuration["AppSettings:server"];
#endif

            string connStr = string.Format("server={0};user id={1};password={2};database={3};allow zero datetime = true;character set=utf8"
                    , server
                    , ConfigUtils.Configuration["AppSettings:user"]
                    , ConfigUtils.Configuration["AppSettings:pwd"]
                    , ConfigUtils.Configuration["AppSettings:database"]
                );

            services.AddDbContext<DbConnect>(options => options.UseMySql(connStr, ServerVersion.Parse("5.7.34-mysql")), ServiceLifetime.Transient);

            //配置跨域处理，允许所有来源：
            services.AddCors(options => options.AddPolicy("AllowedHosts", p => p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

            //获取客户端信息
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //获取注入的服务
            DbConnectUtils.ServiceProvider = services.BuildServiceProvider();

            //注册房间服务,在项目运行期间保存房间用户
            services.AddRoomService();

            //注册Result
            services.AddTransient(options => new Result());

            #region redis注入
            //redis缓存
            var section = Configuration.GetSection("Redis:Default");
            //连接字符串
            string _connectionString = section.GetSection("Connection").Value;
            //实例名称
            string _instanceName = section.GetSection("InstanceName").Value;
            //默认数据库 
            int _defaultDB = int.Parse(section.GetSection("DefaultDB").Value ?? "0");
            services.AddSingleton(new RedisUtils(_connectionString, _instanceName, _defaultDB));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime appLifetime)
        {
            //解决Body接收不到参数
            app.Use(next => new RequestDelegate(
                  async context =>
                  {
                      context.Request.EnableBuffering();
                      await next(context);
                  }
              ));

            //访问html服务
            app.UseStaticFiles();
            //开启index.html
            app.UseFileServer();

            appLifetime.ApplicationStarted.Register(() =>
            {
                NLogHelper._.Info("系统启动");
            });
            appLifetime.ApplicationStopping.Register(() =>
            {
                Console.WriteLine("系统正在关闭...");
            });
            appLifetime.ApplicationStopped.Register(() =>
            {
                NLogHelper._.Info("系统关闭");
            });

            //检测到访问时,判断appsettings.json中Update是否为"0",不为"0"时自尽,由守护进程复活
            app.Use(async (context, next) =>
            {
                if (ConfigUtils.Configuration["Update"].Equals("0"))
                {
                    await next();
                }
                else
                {
                    try
                    {
                        string contentPath = env.ContentRootPath + @"/"; ;   //项目根目录

                        var filePath = contentPath + "appsettings.json";

                        JObject jsonObject;
                        using (StreamReader file = new StreamReader(filePath))
                        using (JsonTextReader reader = new JsonTextReader(file))
                        {
                            jsonObject = (JObject)JToken.ReadFrom(reader);
                            jsonObject["Update"] = "0";
                        }

                        using (var writer = new StreamWriter(filePath))
                        using (JsonTextWriter jsonwriter = new JsonTextWriter(writer))
                        {
                            jsonObject.WriteTo(jsonwriter);
                        }

                        //自尽
                        appLifetime.StopApplication();
                    }
                    catch (Exception ex)
                    {
                        NLogHelper._.Error("appsettings.json修改错误", ex);
                        await next();
                    }
                }
            });

            //解决使用反向代理时无法获取IP的问题
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

#if DEBUG
            // 添加Swagger有关中间件
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Demo v1");
            });
#endif

            app.UseRouting();

            app.UseCors("AllowedHosts");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //设置全局日期格式,解决linux环境下显示AM/PM的问题
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("") { DateTimeFormat = { ShortDatePattern = "yyyy-MM-dd", FullDateTimePattern = "yyyy-MM-dd HH:mm:ss", LongTimePattern = "HH:mm:ss" } };

            //启动任务调度器
            QuartzHelper.Run();

            YejiUtils ym = new YejiUtils();
            ym.AddXyzZonge(0);

            ThreadPool.SetMinThreads(300, 300);
        }
    }
}
