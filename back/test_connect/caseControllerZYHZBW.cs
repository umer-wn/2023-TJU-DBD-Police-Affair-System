using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Data.Common;
using System.Reflection.PortableExecutable;
using System.Text;


[ApiController]
[Route("/")]
public class caseInfoControllerZYH : ControllerBase
{
    private OracleConnection _connection;

    public caseInfoControllerZYH(OracleConnection connection)
    {
        _connection = connection;
    }

    [HttpGet("api/caseInfo")]
    public IActionResult HandleEndpoint(inputCaseInfoZYH inputInfo) //接收前端的数据
    {
        List<caseInfoZYH> cases = new List<caseInfoZYH>();
        Console.WriteLine($"查询数据为:{inputInfo.caseID}\t{inputInfo.caseType}\t{inputInfo.status}\t{inputInfo.address}\t{inputInfo.ranking}");
        try
        {
            _connection.Open();
            // 执行 SQL 查询
            using (var command = _connection.CreateCommand())
            {
                command.Connection = _connection;
                //先写SQL语句的主体
                command.CommandText = "SELECT * FROM CASES WHERE 1 = 1";
                //下面的whereClause可以接到SQL语句的后面，实现SQL语句的动态变化
                StringBuilder whereClause = new StringBuilder();

                if (!string.IsNullOrEmpty(inputInfo.caseID))
                {
                    //下面的SQL表示筛选出包含inputInfo.caseID的结果
                    whereClause.Append(" AND CASE_ID LIKE '%' || :caseID || '%'");
                    command.Parameters.Add(":caseID", OracleDbType.Varchar2).Value = inputInfo.caseID;
                }
                if (inputInfo.caseType != "全部")
                {
                    whereClause.Append(" AND CASE_TYPE LIKE '%' || :caseType || '%'");
                    command.Parameters.Add(":caseType", OracleDbType.Varchar2).Value = inputInfo.caseType;
                }
                if (inputInfo.status != "全部")
                {
                    whereClause.Append(" AND STATUS LIKE '%' || :status || '%'");
                    command.Parameters.Add(":status", OracleDbType.Varchar2).Value = inputInfo.status;
                }
                if (!string.IsNullOrEmpty(inputInfo.address))
                {
                    //下面的SQL表示筛选出包含inputInfo.caseID的结果
                    whereClause.Append(" AND ADDRESS LIKE '%' || :address || '%'");
                    command.Parameters.Add(":address", OracleDbType.Varchar2).Value = inputInfo.address;
                }
                if (inputInfo.ranking != "全部")
                {
                    whereClause.Append(" AND RANKING = :ranking");
                    command.Parameters.Add(":ranking", OracleDbType.Char).Value = inputInfo.ranking;
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
                        caseInfoZYH Case = new caseInfoZYH
                        {
                            caseID = reader.GetString(reader.GetOrdinal("CASE_ID")),
                            caseType = reader.GetString(reader.GetOrdinal("CASE_TYPE")),
                            status = reader.GetString(reader.GetOrdinal("STATUS")),
                            registerTime = reader.GetDateTime(reader.GetOrdinal("REGISTER_TIME")),
                            address = reader.GetString(reader.GetOrdinal("ADDRESS")),
                            ranking = reader.GetString(reader.GetOrdinal("RANKING"))
                        };
                        cases.Add(Case);
                    }

                    _connection.Close();
                    return Ok(cases);
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
    [HttpPost("api/addcaseInfo")]
    public IActionResult AddCase(caseInfoZYH newCase)
    {
        try
        {
            string connectionString = "..."; // Your Oracle connection string.

            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                connection.Open();

                // Construct and execute an INSERT SQL command to add the new case to the database.
                string sqlQuery = "INSERT INTO CASES (CASE_ID, CASE_TYPE, STATUS, REGISTER_TIME, ADDRESS, RANKING) " +
                                  "VALUES (:caseID, :caseType, :status, :registerTime, :address, :ranking)";

                using (OracleCommand command = new OracleCommand(sqlQuery, connection))
                {
                    // Bind parameters to the SQL command.
                    command.Parameters.Add(":caseID", OracleDbType.Varchar2).Value = newCase.caseID;
                    command.Parameters.Add(":caseType", OracleDbType.Varchar2).Value = newCase.caseType;
                    command.Parameters.Add(":status", OracleDbType.Varchar2).Value = newCase.status;
                    command.Parameters.Add(":registerTime", OracleDbType.Date).Value = newCase.registerTime;
                    command.Parameters.Add(":address", OracleDbType.Varchar2).Value = newCase.address;
                    command.Parameters.Add(":ranking", OracleDbType.Char).Value = newCase.ranking;

                    // Execute the SQL command.
                    command.ExecuteNonQuery();
                }

                connection.Close();
            }

            return Ok("Case added successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error adding case: " + ex.Message);
            return StatusCode(500, "Error adding case: " + ex.Message);
        }
    }
    [HttpDelete("api/delcaseInfo")]
    public IActionResult DeleteCase(string caseID)
    {
        try
        {
            string connectionString = "..."; // Your Oracle connection string.

            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                connection.Open();

                // Construct and execute a DELETE SQL command to remove the case from the database.
                string sqlQuery = "DELETE FROM CASES WHERE CASE_ID = :caseID";

                using (OracleCommand command = new OracleCommand(sqlQuery, connection))
                {
                    // Bind the parameter to the SQL command.
                    command.Parameters.Add(":caseID", OracleDbType.Varchar2).Value = caseID;

                    // Execute the SQL command.
                    command.ExecuteNonQuery();
                }

                connection.Close();
            }

            return Ok("Case deleted successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error deleting case: " + ex.Message);
            return StatusCode(500, "Error deleting case: " + ex.Message);
        }
    }

}