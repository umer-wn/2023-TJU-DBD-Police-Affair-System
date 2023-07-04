using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Oracle.ManagedDataAccess.Client;

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
                webBuilder.UseUrls("http://localhost:7078");
                webBuilder.ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                })
                .ConfigureServices((hostContext, services) =>
                {
                    // 获取数据库连接字符串
                    string connectionString = hostContext.Configuration.GetConnectionString("OracleConnection");

                    // 注册Oracle连接对象到依赖注入容器
                    services.AddScoped<OracleConnection>(_ => new OracleConnection(connectionString));

                    // 添加控制器
                    services.AddControllers();

                    // 添加 CORS 配置
                    services.AddCors(options =>
                    {
                        options.AddDefaultPolicy(builder =>
                        {
                            builder.WithOrigins("http://localhost:8080") // 允许的前端应用地址
                                .AllowAnyMethod()
                                .AllowAnyHeader();
                        });
                    });
                })
                .Configure(app =>
                {
                    app.UseRouting();

                    // 添加 CORS 中间件
                    app.UseCors();

                    app.UseEndpoints(endpoints =>
                    {
                        endpoints.MapControllers();
                    });
                });
            });
}
