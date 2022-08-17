using Microsoft.Extensions.DependencyInjection;

namespace Server.Utils.WebSocket_Utils
{
    public static class RoomService
    {
        //注册服务
        public static IServiceCollection AddRoomService(this IServiceCollection service)
        {
            //通过AddSingleton注册，会在第一次被请求时创建实例，之后的每个请求都会使用这个实例
            return service.AddSingleton(Options => new Rooms());
        }
    }
}
