using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

    [HttpGet("api/policemenInfo")]
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
                            salary = reader.GetString(reader.GetOrdinal("SALARY"))
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
    [HttpDelete("api/delpolicemenInfo/{policemenNumber}")]
    public IActionResult DeletePoliceman(string policemenNumber)
    {
        try
        {
            _connection.Open();

            // Check if the policeman with the given number exists
            using (var checkCommand = _connection.CreateCommand())
            {
                checkCommand.Connection = _connection;
                checkCommand.CommandText = "SELECT COUNT(*) FROM POLICEMEN WHERE POLICE_NUMBER = :policemenNumber";
                checkCommand.Parameters.Add(":policemenNumber", OracleDbType.Varchar2).Value = policemenNumber;

                int rowCount = Convert.ToInt32(checkCommand.ExecuteScalar());

                if (rowCount == 0)
                {
                    _connection.Close();
                    return NotFound("Policeman not found");
                }
            }

            // Delete the policeman if it exists
            using (var deleteCommand = _connection.CreateCommand())
            {
                deleteCommand.Connection = _connection;
                deleteCommand.CommandText = "DELETE FROM POLICEMEN WHERE POLICE_NUMBER = :policemenNumber";
                deleteCommand.Parameters.Add(":policemenNumber", OracleDbType.Varchar2).Value = policemenNumber;

                int rowsAffected = deleteCommand.ExecuteNonQuery();

                if (rowsAffected == 0)
                {
                    // This might happen if another request deleted the record concurrently
                    _connection.Close();
                    return NotFound("Policeman not found");
                }
            }

            _connection.Close();
            return Ok("Policeman deleted successfully");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting policeman: {ex.Message}");
            return StatusCode(500, $"Error deleting policeman: {ex.Message}");
        }
        finally
        {
            _connection.Close();
        }
    }
    [HttpPost("api/addpolicemenInfo")]
    public async Task<IActionResult> AddPoliceman(PolicemenInfoZYH newPoliceman)
    {
        try
        {
            // 数据完整性检查
            var validationContext = new ValidationContext(newPoliceman, serviceProvider: null, items: null);
            var validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(newPoliceman, validationContext, validationResults, true);

            if (!isValid)
            {
                // 返回验证错误信息给客户端
                var errorMessages = validationResults.Select(result => result.ErrorMessage).ToList();
                return BadRequest(errorMessages);
            }

            // 异步打开数据库连接
            await _connection.OpenAsync();

            // 开始数据库事务
            using (var transaction = _connection.BeginTransaction())
            {
                try
                {
                    using (var command = _connection.CreateCommand())
                    {
                        command.Transaction = transaction;
                        command.CommandText = "INSERT INTO POLICEMEN " +
                                              "(POLICE_NUMBER, POLICE_NAME, ID_NUMBER, BIRTHDAY, GENDER, NATION, PHONE_NUMBER, STATUS, POSITION, SALARY) " +
                                              "VALUES " +
                                              "(:policeNumber, :policeName, :idNumber, :birthday, :gender, :nation, :phoneNumber, :status, :position, :salary)";

                        // 绑定参数
                        command.Parameters.Add(":policeNumber", OracleDbType.Varchar2).Value = newPoliceman.policemenNumber;
                        command.Parameters.Add(":policeName", OracleDbType.Varchar2).Value = newPoliceman.policemenName;
                        command.Parameters.Add(":idNumber", OracleDbType.Varchar2).Value = newPoliceman.IDNumber;
                        command.Parameters.Add(":birthday", OracleDbType.Varchar2).Value = newPoliceman.birthday;
                        command.Parameters.Add(":gender", OracleDbType.Varchar2).Value = newPoliceman.gender;
                        command.Parameters.Add(":nation", OracleDbType.Varchar2).Value = newPoliceman.nation;
                        command.Parameters.Add(":phoneNumber", OracleDbType.Varchar2).Value = newPoliceman.phoneNumber;
                        command.Parameters.Add(":status", OracleDbType.Varchar2).Value = newPoliceman.policemenStatus;
                        command.Parameters.Add(":position", OracleDbType.Varchar2).Value = newPoliceman.policemenPosition;
                        command.Parameters.Add(":salary", OracleDbType.Varchar2).Value = newPoliceman.salary;

                        int rowsAffected = await command.ExecuteNonQueryAsync();

                        if (rowsAffected > 0)
                        {
                            // 提交数据库事务
                            transaction.Commit();
                            return Ok("成功添加新警员。");
                        }
                        else
                        {
                            // 回滚数据库事务
                            transaction.Rollback();
                            return BadRequest("无法添加新警员。");
                        }
                    }
                }
                catch (Exception ex)
                {
                    // 回滚数据库事务
                    transaction.Rollback();
                    Console.WriteLine($"添加新警员时发生错误：{ex.Message}");
                    return StatusCode(500, "处理请求时发生错误。");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"错误：{ex.Message}");
            return StatusCode(500, "处理请求时发生错误。");
        }
        finally
        {
            // 关闭数据库连接
            _connection.Close();
        }
    }

}