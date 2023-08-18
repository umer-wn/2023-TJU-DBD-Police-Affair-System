using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using System.Text;

using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Data.Common;
using System.Reflection.PortableExecutable;
using System.Text;

//前后端进行数据交流的数据结构
public class citizenInfo
{
    public string IDNum { get; set; }
    public string citizenName { get; set; }
    public string gender { get; set; }
    public string fatherID { get; set; }
    public string motherID { get; set; }
}

public class inputCitizenInfo
{
    public string IDNum { get; set; }
    public string citizenName { get; set; }
    public string gender { get; set; }
}

[ApiController]
[Route("api/citizenInfo")]
public class citizenInfoController : ControllerBase
{
    private OracleConnection _connection;

    public citizenInfoController(OracleConnection connection)
    {
        _connection = connection;
    }

    [HttpPost]
    public IActionResult HandleEndpoint(inputCitizenInfo inputInfo) //接收前端的数据
    {
        List<citizenInfo> citizens = new List<citizenInfo>();
        Console.WriteLine($"查询数据为:{inputInfo.IDNum}\t{inputInfo.citizenName}\t{inputInfo.gender}");
        try
        {
            _connection.Open();
            // 执行 SQL 查询
            using (var command = _connection.CreateCommand())
            {
                command.Connection = _connection;
                //先写SQL语句的主体
                command.CommandText = "SELECT * FROM CITIZEN WHERE 1 = 1";
                //下面的whereClause可以接到SQL语句的后面，实现SQL语句的动态变化
                StringBuilder whereClause = new StringBuilder();

                if (!string.IsNullOrEmpty(inputInfo.IDNum))
                {
                    //下面的SQL表示筛选出包含inputInfo.IDNum的结果
                    whereClause.Append(" AND ID_NUM LIKE '%' || :IDNum || '%'");
                    command.Parameters.Add(":IDNum", OracleDbType.Varchar2).Value = inputInfo.IDNum;
                }
                if (!string.IsNullOrEmpty(inputInfo.citizenName))
                {
                    whereClause.Append(" AND CITIZEN_NAME LIKE '%' || :citizenName || '%'");
                    command.Parameters.Add(":citizenName", OracleDbType.Varchar2).Value = inputInfo.citizenName;
                }
                if (inputInfo.gender != "全部")
                {
                    whereClause.Append(" AND GENDER LIKE '%' || :gender || '%'");
                    command.Parameters.Add(":gender", OracleDbType.Char).Value = inputInfo.gender;
                }

                //将后续SQL语句合并到主语句中
                if (whereClause.Length > 0)
                {
                    command.CommandText += whereClause.ToString();
                }

                //在控制台输出一下查询语句，方便检错
                Console.WriteLine($"查询数据SQL为:{command.CommandText}");
                //下面进行查询，查询结果放在reader里
                using (OracleDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        citizenInfo citizen = new citizenInfo
                        {
                            IDNum = reader.GetString(reader.GetOrdinal("ID_NUM")),
                            citizenName = reader.GetString(reader.GetOrdinal("CITIZEN_NAME")),
                            gender = reader.GetString(reader.GetOrdinal("GENDER")),
                            fatherID = reader.GetString(reader.GetOrdinal("FATHER_ID")),
                            motherID = reader.GetString(reader.GetOrdinal("MOTHER_ID"))
                        };
                        citizens.Add(citizen);
                    }

                    _connection.Close();
                    return Ok(citizens);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"查询数据时发生错误:{ex.Message}");
            return StatusCode(499, $"查询数据时发生错误: {ex.Message}");
        }
        finally
        {
            _connection.Close();
        }
    }
}