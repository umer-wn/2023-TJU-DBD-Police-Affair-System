using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Data.Common;
using System.Reflection.PortableExecutable;
using System.Text;



[ApiController]
[Route("/")]
public class PolicemenInfoControllerZYH : ControllerBase
{
    private OracleConnection _connection;

    public PolicemenInfoControllerZYH(OracleConnection connection)
    {
        _connection = connection;
    }

    [HttpPost("api/policemenInfo")]
    public IActionResult HandleEndpoint(inputPolicemenInfoZYH inputInfo) //接收前端的数据
    {
        List<PolicemenInfoZYH> policemen = new List<PolicemenInfoZYH>();
        Console.WriteLine($"查询数据为:{inputInfo.policemenNumber}\t{inputInfo.policemenName}\t{inputInfo.policemenStatus}\t{inputInfo.policemenPosition}");
        try
        {
            _connection.Open();
            // 执行 SQL 查询
            using (var command = _connection.CreateCommand())
            {
                command.Connection = _connection;
                //先写SQL语句的主体
                command.CommandText = "SELECT POLICE_NUMBER,POLICE_NAME,ID_NUMBER,BIRTHDAY,GENDER," +
                    "NATION,PHONE_NUMBER,STATUS,POSITION,SALARY FROM POLICEMEN WHERE 1 = 1";
                //下面的whereClause可以接到SQL语句的后面，实现SQL语句的动态变化
                StringBuilder whereClause = new StringBuilder();

                if (!string.IsNullOrEmpty(inputInfo.policemenNumber))
                {
                    //下面的SQL表示筛选出包含inputInfo.policemenNumber的结果
                    whereClause.Append(" AND POLICE_NUMBER LIKE '%' || :policemenNumber || '%'");
                    command.Parameters.Add(":policemenNumber", OracleDbType.Varchar2).Value = inputInfo.policemenNumber;
                }
                if (!string.IsNullOrEmpty(inputInfo.policemenName))
                {
                    whereClause.Append(" AND POLICE_NAME LIKE '%' || :policemenName || '%'");
                    command.Parameters.Add(":policemenName", OracleDbType.Varchar2).Value = inputInfo.policemenName;
                }
                if (inputInfo.policemenStatus != "全部")
                {
                    whereClause.Append(" AND STATUS LIKE '%' || :policemenStatus || '%'");
                    command.Parameters.Add(":policemenStatus", OracleDbType.Varchar2).Value = inputInfo.policemenStatus;
                }
                if (inputInfo.policemenPosition != "全部")
                {
                    whereClause.Append(" AND POSITION LIKE '%' || :policemenPosition || '%'");
                    command.Parameters.Add(":policemenPosition", OracleDbType.Varchar2).Value = inputInfo.policemenPosition;
                }

                if (whereClause.Length > 0)
                {
                    command.CommandText += whereClause.ToString();
                }

                Console.WriteLine($"查询数据SQL为:{command.CommandText}");
                using (OracleDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        PolicemenInfoZYH policeman = new PolicemenInfoZYH
                        {
                            policemenNumber = reader.GetString(reader.GetOrdinal("POLICE_NUMBER")),
                            policemenName = reader.GetString(reader.GetOrdinal("POLICE_NAME")),
                            IDNumber = reader.GetString(reader.GetOrdinal("ID_NUMBER")),
                            birthday = reader.GetString(reader.GetOrdinal("BIRTHDAY")),
                            gender = reader.GetString(reader.GetOrdinal("GENDER")),
                            nation = reader.GetString(reader.GetOrdinal("NATION")),
                            phoneNumber = reader.GetString(reader.GetOrdinal("PHONE_NUMBER")),
                            policemenStatus = reader.GetString(reader.GetOrdinal("STATUS")),
                            policemenPosition = reader.GetString(reader.GetOrdinal("POSITION")),
                        };
                        policemen.Add(policeman);
                    }

                    _connection.Close();
                    return Ok(policemen);
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
    [HttpDelete("api/delpolicemenInfo")]
    public IActionResult DeletePoliceman(string policeNumber)
    {
        try
        {
            _connection.Open();

            using (var command = _connection.CreateCommand())
            {
                command.Connection = _connection;
                command.CommandText = "DELETE FROM POLICEMEN WHERE POLICE_NUMBER = :policeNumber";
                command.Parameters.Add(":policeNumber", OracleDbType.Varchar2).Value = policeNumber;

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    _connection.Close();
                    return Ok("删除成功");
                }
                else
                {
                    _connection.Close();
                    return NotFound("未找到该警员");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"删除警员时发生错误:{ex.Message}");
            return StatusCode(500, $"删除警员时发生错误: {ex.Message}");
        }
        finally
        {
            _connection.Close();
        }
    }

}