using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

//前后端进行数据交流的数据结构
public class StationInfo
{
    public string stationID { get; set; }
    public string stationName { get; set; }
    public string city { get; set; }
    public string address { get; set; }
    public int? budget { get; set; }
}

[ApiController]
[Route("api/stationInfo")]
public class StationInfoController : ControllerBase
{
    private OracleConnection _connection;

    public StationInfoController(OracleConnection connection)
    {
        _connection = connection;
    }

    [HttpPost]
    public IActionResult HandleEndpoint(StationInfo inputStation)
    {
        List<StationInfo> stations = new List<StationInfo>();
        Console.WriteLine($"查询数据为:{inputStation.stationID}\t{inputStation.stationName}\t{inputStation.city}\t{inputStation.address}\t{inputStation.budget}\t");
        try
        {
            _connection.Open();
            // 执行 SQL 查询
            using (var command = _connection.CreateCommand())
            {
                command.Connection = _connection;
                command.CommandText = "SELECT * FROM police_station WHERE 1 = 1";
                StringBuilder whereClause = new StringBuilder();

                if (!string.IsNullOrEmpty(inputStation.stationID))
                {
                    whereClause.Append(" AND station_ID = :stationID");
                    command.Parameters.Add(":stationID", OracleDbType.Varchar2).Value = inputStation.stationID;
                }
                if (!string.IsNullOrEmpty(inputStation.stationName))
                {
                    whereClause.Append(" AND station_Name LIKE '%' || :stationName || '%'");
                    command.Parameters.Add(":stationName", OracleDbType.Varchar2).Value = inputStation.stationName;
                }
                if (!string.IsNullOrEmpty(inputStation.city))
                {
                    whereClause.Append(" AND city LIKE '%' || :city || '%'");
                    command.Parameters.Add(":city", OracleDbType.Varchar2).Value = inputStation.city;
                }
                if (!string.IsNullOrEmpty(inputStation.address))
                {
                    whereClause.Append(" AND address LIKE '%' || :address || '%'");
                    command.Parameters.Add(":address", OracleDbType.Varchar2).Value = inputStation.address;
                }
                int? budget = inputStation.budget;
                if (budget.HasValue)
                {
                    whereClause.Append(" AND budget <= :budget");
                    command.Parameters.Add(":budget", OracleDbType.Int32).Value = inputStation.budget;
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
                        StationInfo station = new StationInfo
                        {
                            stationID = reader.GetString(reader.GetOrdinal("station_ID")),
                            stationName = reader.GetString(reader.GetOrdinal("station_Name")),
                            city = reader.GetString(reader.GetOrdinal("city")),
                            address = reader.GetString(reader.GetOrdinal("address")),
                            budget = reader.GetInt32(reader.GetOrdinal("budget"))
                        };
                        stations.Add(station);
                    }

                    _connection.Close();
                    return Ok(stations);
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
