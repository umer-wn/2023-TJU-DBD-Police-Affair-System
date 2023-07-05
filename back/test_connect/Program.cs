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
                    // ��ȡ���ݿ������ַ���
                    string connectionString = hostContext.Configuration.GetConnectionString("OracleConnection");

                    // ע��Oracle���Ӷ�������ע������
                    services.AddScoped<OracleConnection>(_ => new OracleConnection(connectionString));

                    // ��ӿ�����
                    services.AddControllers();

                    // ���CORS����
                    services.AddCors(options =>
                    {
                        options.AddPolicy("AllowCors", builder =>
                        {
                            builder.AllowAnyOrigin()
                                .AllowAnyMethod()
                                .AllowAnyHeader();
                        });
                    });
                })
                .Configure(app =>
                {
                    app.UseRouting();

                    // ����CORS�м��
                    app.UseCors("AllowCors");

                    app.UseEndpoints(endpoints =>
                    {
                        endpoints.MapControllers();
                    });
                });
            });
}
