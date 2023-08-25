using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using System.Security.Claims;
using web.DTO_group2;

[ApiController]
[Authorize] // 添加Authorize特性，表示需要进行身份验证
public class TestController_all : ControllerBase
{
    private readonly OracleConnection _connection;

    public TestController_all(OracleConnection connection)
    {
        _connection = connection;
    }

    [HttpGet("api/test")]
    public ActionResult<IEnumerable<Police>> GetStudents()
    {
        var policemen = new List<Police>();
        var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
        if (userRole == "0")
        {
            try
            {
                _connection.Open();
                string sql = "SELECT * FROM POLICEMEN";
                OracleCommand command = new OracleCommand(sql, _connection);

                using (OracleDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var policeman = new Police
                        {
                            police_number = reader.GetString(reader.GetOrdinal("POLICE_NUMBER")),
                            police_name = reader.GetString(reader.GetOrdinal("POLICE_NAME")),
                            ID_number = reader.GetString(reader.GetOrdinal("ID_NUMBER")),
                            gender = reader.GetString(reader.GetOrdinal("GENDER"))
                        };

                        policemen.Add(policeman);
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"查询数据时发生错误: {ex.Message}");
            }
            finally
            {
                _connection.Close();
            }

            return Ok(policemen);

        }
        else
            return Unauthorized();
    }

    [HttpPost("api/test")]
    public ActionResult<string> ProcessInput(Request request)
    {
        try
        {
            string response = request.input + " hello_world";
            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"处理输入时发生错误: {ex.Message}");
        }
    }
}
