using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

[ApiController]
[Route("/")]
public class AttendControllerZBW : ControllerBase
{
    private OracleConnection _connection;

    public AttendControllerZBW(OracleConnection connection)
    {
        _connection = connection;
    }

    [HttpPost("api/attendInfo")]
    public IActionResult HandleEndpoint(attendinfo inputInfo)
    {
        List<attendinfo> cases = new List<attendinfo>();
        Console.WriteLine($"查询数据为:{inputInfo.policemanNumber}\t{inputInfo.area}");
        try
        {
            _connection.Open();
            using (var command = _connection.CreateCommand())
            {
                command.Connection = _connection;
                command.CommandText = "SELECT * FROM PATROL WHERE 1 = 1";
                StringBuilder whereClause = new StringBuilder();

                if (!string.IsNullOrEmpty(inputInfo.policemanNumber))
                {
                    whereClause.Append(" AND POLICE_NUMBER LIKE '%' || :policemanNumber || '%'");
                    command.Parameters.Add(":policemanNumber", OracleDbType.Varchar2).Value = inputInfo.policemanNumber;
                }
                if (!string.IsNullOrEmpty(inputInfo.area))
                {
                    whereClause.Append(" AND AREA LIKE '%' || :area || '%'");
                    command.Parameters.Add(":area", OracleDbType.Varchar2).Value = inputInfo.area;
                }
                if (!string.IsNullOrEmpty(inputInfo.area))
                {
                    string format = "yyyy-MM-ddTHH:mm:ss.fffffffzzz"; // 包含日期、时间、毫秒和时区的格式字符串
                    DateTime dateTime;
                    //DateTime.TryParseExact(inputInfo.time, format, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dateTime);
                    whereClause.Append(" AND PATROL_TIME LIKE '%' || :area || '%'");
                    command.Parameters.Add(":area", OracleDbType.Date).Value = inputInfo.time;// dateTime;
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
                            area = reader.GetString(reader.GetOrdinal("AREA")),
                            time = reader.GetDateTime(reader.GetOrdinal("PATROL_TIME")),
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
                command.CommandText = "INSERT INTO PATROL (POLICE_NUMBER, AREA, PATROL_TIME) " +
                                      "VALUES (:policemanNumber, :area, :patrolTime)";
                command.Parameters.Add(":policemanNumber", OracleDbType.Varchar2).Value = newRecord.policemanNumber;
                command.Parameters.Add(":area", OracleDbType.Varchar2).Value = newRecord.area;
                command.Parameters.Add(":patrolTime", OracleDbType.Date).Value = newRecord.time;//dateTime;
                
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
    public IActionResult DeleteRecord(attendinfo deleteInfo)
    {
        try
        {
            _connection.Open();
            using (var command = _connection.CreateCommand())
            {
                command.CommandText = "DELETE FROM PATROL WHERE 1 = 1";
                StringBuilder whereClause = new StringBuilder();
              
                if (!string.IsNullOrEmpty(deleteInfo.policemanNumber))
                {
                    whereClause.Append(" AND POLICE_NUMBER LIKE '%' || :policemanNumber || '%'");
                    command.Parameters.Add(":policemanNumber", OracleDbType.Varchar2).Value = deleteInfo.policemanNumber;
                }
                if (!string.IsNullOrEmpty(deleteInfo.area))
                {
                    whereClause.Append(" AND AREA LIKE '%' || :area || '%'");
                    command.Parameters.Add(":area", OracleDbType.Varchar2).Value = deleteInfo.area;
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
