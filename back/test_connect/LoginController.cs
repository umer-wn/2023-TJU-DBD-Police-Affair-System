using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using web1.Helpers;

public class LoginRequest
{
    public string Username { get; set; }
    public string Password { get; set; }
}

namespace LoginController
{
    [ApiController]
    [Route("api/login")]


    public class LoginController : ControllerBase
    {
        private readonly OracleConnection _connection;


        public LoginController(OracleConnection connection)
        {
            _connection = connection;

        }
        [HttpPost]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            // 进行登录逻辑处理
            // 从 request 对象中获取账号和密码

            // 有一个名为 CheckLogin 的方法来验证登录信息
            if (CheckLogin(request.Username, request.Password))
            {

                _connection.Open();
                string authority;
                using (OracleCommand command = _connection.CreateCommand())
                {
                    command.CommandText = "SELECT AUTHORITY FROM POLICE_ACCOUNT WHERE POLICE_NUMBER = :PoliceNumber";
                    command.Parameters.Add(new OracleParameter("PoliceNumber", request.Username));
                    authority = command.ExecuteScalar()?.ToString() ?? string.Empty;
                }
                _connection.Close();
                return Ok(new { success = true, message = "登录成功", token = JwtHelper.CreateToken(request.Username, authority, 600) });
            }
            else
            {
                return Ok(new { success = false, error = "登录失败", message = "登录失败" });
            }
        }


        private bool CheckLogin(string username, string password)
        {

            _connection.Open();

            // 创建命令对象
            using (var command = _connection.CreateCommand())
            {
                // 设置 SQL 查询语句
                command.CommandText = "SELECT COUNT(*) FROM POLICE_ACCOUNT WHERE POLICE_NUMBER = :username AND POLICE_KEY = :password";

                // 添加参数
                command.Parameters.Add(":username", OracleDbType.Varchar2).Value = username;
                command.Parameters.Add(":password", OracleDbType.Varchar2).Value = password;

                // 执行查询
                int count = Convert.ToInt32(command.ExecuteScalar());

                _connection.Close();
                // 检查是否存在匹配的记录
                return count > 0;
            }

        }
    }
}
