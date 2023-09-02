using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Data.Common;
using System.Reflection.PortableExecutable;
using System.Text;


[ApiController]
[Route("/")]
public class attendControllerZBW : ControllerBase
{
    private OracleConnection _connection;

    public attendControllerZBW(OracleConnection connection)
    {
        _connection = connection;
    }

    [HttpPost("api/attendInfo")]
    public IActionResult HandleEndpoint(attendinputinfo inputInfo) //接收前端的数据
    {
        List<attendinfo> cases = new List<attendinfo>();
        Console.WriteLine($"查询数据为:{inputInfo.policemanNumber}\t{inputInfo.stationID}");
        try
        {
            _connection.Open();
            // 执行 SQL 查询
            using (var command = _connection.CreateCommand())
            {
                command.Connection = _connection;
                //先写SQL语句的主体
                command.CommandText = "SELECT * FROM EMPLOY WHERE 1 = 1";
                //下面的whereClause可以接到SQL语句的后面，实现SQL语句的动态变化
                StringBuilder whereClause = new StringBuilder();

                if (!string.IsNullOrEmpty(inputInfo.policemanNumber))
                {
                    //下面的SQL表示筛选出包含inputInfo.caseID的结果
                    whereClause.Append(" AND POLICE_NUMBER LIKE '%' || :policemanNumber || '%'");
                    command.Parameters.Add(":policemanNumber", OracleDbType.Varchar2).Value = inputInfo.policemanNumber;
                }
                if (!string.IsNullOrEmpty(inputInfo.stationID))
                {
                    whereClause.Append(" AND STATION_ID LIKE '%' || :stationID || '%'");
                    command.Parameters.Add(":stationID", OracleDbType.Varchar2).Value = inputInfo.stationID;
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
                        attendinfo Case = new attendinfo
                        {
                            policemanNumber = reader.GetString(reader.GetOrdinal("POLICE_NUMBER")),
                            stationID = reader.GetString(reader.GetOrdinal("STATION_ID")),
                            intime = reader.GetDateTime(reader.GetOrdinal("INTIME")),
                            outtime = reader.IsDBNull(reader.GetOrdinal("OUTTIME")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("OUTTIME"))
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
    [HttpPost("api/addattendInfo")]
    public IActionResult AddRecord(attendinfo newRecord)
    {
        try
        {
            _connection.Open();
            using (var command = _connection.CreateCommand())
            {
                // Prepare an SQL INSERT statement to add a new record
                command.CommandText = "INSERT INTO EMPLOY (POLICE_NUMBER, STATION_ID, INTIME, OUTTIME) " +
                                      "VALUES (:policemanNumber, :stationID, :intime, :outtime)";
                command.Parameters.Add(":policemanNumber", OracleDbType.Varchar2).Value = newRecord.policemanNumber;
                command.Parameters.Add(":stationID", OracleDbType.Varchar2).Value = newRecord.stationID;
                command.Parameters.Add(":intime", OracleDbType.Date).Value = newRecord.intime;
                if (newRecord.outtime == null)
                    command.Parameters.Add(":outtime", OracleDbType.Date).Value = DBNull.Value;
                else
                    command.Parameters.Add(":outtime", OracleDbType.Date).Value = newRecord.outtime;

                int rowsAffected = command.ExecuteNonQuery();

                _connection.Close();

                if (rowsAffected > 0)
                {
                    return Ok("Record added successfully");
                }
                else
                {
                    return BadRequest("Failed to add record");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error while adding record: {ex.Message}");
            return StatusCode(500, $"Error while adding record: {ex.Message}");
        }
        finally
        {
            _connection.Close();
        }
    }
    [HttpPost("api/delattendInfo")]
    public IActionResult DeleteRecord(attendinputinfo deleteInfo)
    {
        try
        {
            _connection.Open();
            using (var command = _connection.CreateCommand())
            {
                // Prepare an SQL DELETE statement to delete records based on criteria
                command.CommandText = "DELETE FROM EMPLOY WHERE 1 = 1";
                StringBuilder whereClause = new StringBuilder();

                if (!string.IsNullOrEmpty(deleteInfo.policemanNumber))
                {
                    whereClause.Append(" AND POLICE_NUMBER LIKE '%' || :policemanNumber || '%'");
                    command.Parameters.Add(":policemanNumber", OracleDbType.Varchar2).Value = deleteInfo.policemanNumber;
                }
                if (!string.IsNullOrEmpty(deleteInfo.stationID))
                {
                    whereClause.Append(" AND STATION_ID LIKE '%' || :stationID || '%'");
                    command.Parameters.Add(":stationID", OracleDbType.Varchar2).Value = deleteInfo.stationID;
                }
                if (whereClause.Length > 0)
                {
                    command.CommandText += whereClause.ToString();
                }

                int rowsAffected = command.ExecuteNonQuery();

                _connection.Close();

                if (rowsAffected > 0)
                {
                    return Ok("Records deleted successfully");
                }
                else
                {
                    return BadRequest("No records found to delete");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error while deleting records: {ex.Message}");
            return StatusCode(500, $"Error while deleting records: {ex.Message}");
        }
        finally
        {
            _connection.Close();
        }
    }

}