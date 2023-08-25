using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Oracle.ManagedDataAccess.Client;
using System.Text;
using web.Helpers;

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

                    SerialnumHelper.FormSeqTest_Load();
                    // 配置JWT
                    services.AddAuthentication(options =>
                    {
                        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    }).AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters()
                        {
                            ValidateIssuer = true,
                            ValidIssuer = "PoliceApp",
                            ValidateAudience = true,
                            ValidAudience = "PoliceAppUser",
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                                    "123cdefefefeasd14a5445411sds65d4asw65f4e")),
                            ValidateLifetime = true,
                            ClockSkew = TimeSpan.FromMinutes(10),
                            RequireExpirationTime = true,
                        };
                    });
                    services.AddHttpContextAccessor();
                    services.AddSwaggerGen(options =>
                    {
                        options.SwaggerDoc("v1", new OpenApiInfo
                        {
                            Version = "v1",
                            Title = "API标题",
                            Description = "API描述"
                        });
                    });
                    services.AddSwaggerGen(c =>
                    {
                        //添加Jwt验证设置,添加请求头信息
                        c.AddSecurityRequirement(new OpenApiSecurityRequirement
                        {
                            {
                                new OpenApiSecurityScheme
                                {
                                    Reference = new OpenApiReference
                                    {
                                        Id = "Bearer",
                                        Type = ReferenceType.SecurityScheme
                                    }
                                },
                                new List<string>()
                            }
                        });

                        //放置接口Auth授权按钮
                        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                        {
                            Description = "Value Bearer {token}",
                            Name = "Authorization",//jwt默认的参数名称
                            In = ParameterLocation.Header,//jwt默认存放Authorization信息的位置(请求头中)
                            Type = SecuritySchemeType.ApiKey
                        });
                    });

                })
                .Configure(app =>
                {
                    app.UseRouting();

                    // 添加 CORS 中间件
                    app.UseCors();

                    app.UseAuthentication();
                    app.UseAuthorization();


                    app.UseSwagger();
                    app.UseSwaggerUI(c =>
                    {
                        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Web1");
                    });

                    app.UseEndpoints(endpoints =>
                    {
                        endpoints.MapControllers();
                    });
                });
            });
}
