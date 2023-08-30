using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

[ApiController]
[Route("/")]
public class StationInfoControllerZYH : ControllerBase
{
    private OracleConnection _connection;
    public StationInfoControllerZYH(OracleConnection connection)
    {
        _connection = connection;
    }
    [HttpGet("api/stationInfo")]
    public IActionResult HandleEndpoint(StationInfoZYH inputStation)
    {
        List<StationInfoZYH> stations = new List<StationInfoZYH>();
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
                        StationInfoZYH station = new StationInfoZYH
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
    [HttpDelete("api/addStationInfo")]
    public IActionResult AddStation(StationInfoZYH newStation)
    {
        try
        {
            _connection.Open();

            // Validate data integrity
            if (string.IsNullOrEmpty(newStation.stationID) ||
                string.IsNullOrEmpty(newStation.stationName) ||
                string.IsNullOrEmpty(newStation.city) ||
                string.IsNullOrEmpty(newStation.address) ||
                newStation.budget == null)
            {
                return StatusCode(400, "Invalid data. All fields are required.");
            }

            // Check if the station with the given ID already exists
            using (var checkCommand = _connection.CreateCommand())
            {
                checkCommand.Connection = _connection;
                checkCommand.CommandText = "SELECT COUNT(*) FROM police_station WHERE station_ID = :stationID";
                checkCommand.Parameters.Add(":stationID", OracleDbType.Varchar2).Value = newStation.stationID;

                int existingStationCount = Convert.ToInt32(checkCommand.ExecuteScalar());

                if (existingStationCount > 0)
                {
                    return StatusCode(400, $"Station with ID {newStation.stationID} already exists.");
                }
            }

            // Insert the new station into the database
            using (var insertCommand = _connection.CreateCommand())
            {
                insertCommand.Connection = _connection;
                insertCommand.CommandText = "INSERT INTO police_station (station_ID, station_Name, city, address, budget) " +
                    "VALUES (:stationID, :stationName, :city, :address, :budget)";

                insertCommand.Parameters.Add(":stationID", OracleDbType.Varchar2).Value = newStation.stationID;
                insertCommand.Parameters.Add(":stationName", OracleDbType.Varchar2).Value = newStation.stationName;
                insertCommand.Parameters.Add(":city", OracleDbType.Varchar2).Value = newStation.city;
                insertCommand.Parameters.Add(":address", OracleDbType.Varchar2).Value = newStation.address;
                insertCommand.Parameters.Add(":budget", OracleDbType.Int32).Value = newStation.budget;

                insertCommand.ExecuteNonQuery();
            }

            _connection.Close();

            // Return a success message or the newly added station's ID
            return Ok($"Station with ID {newStation.stationID} has been added.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error adding station: {ex.Message}");
            return StatusCode(500, $"Error adding station: {ex.Message}");
        }
        finally
        {
            _connection.Close();
        }
    }
    [HttpPost("api/delStationInfo")]
    public IActionResult DeleteStation(StationInfoZYH stationToDelete)
    {
        try
        {
            using (_connection)
            {
                _connection.Open();

                // Check if the station with the given ID exists
                using (var checkCommand = _connection.CreateCommand())
                {
                    checkCommand.CommandText = "SELECT COUNT(*) FROM police_station WHERE station_ID = :stationID";
                    checkCommand.Parameters.Add(":stationID", OracleDbType.Varchar2).Value = stationToDelete.stationID;

                    int existingStationCount = Convert.ToInt32(checkCommand.ExecuteScalar());

                    if (existingStationCount == 0)
                    {
                        return StatusCode(400, $"Station with ID {stationToDelete.stationID} does not exist.");
                    }
                }

                // Delete the station from the database
                using (var deleteCommand = _connection.CreateCommand())
                {
                    deleteCommand.CommandText = "DELETE FROM police_station WHERE station_ID = :stationID";
                    deleteCommand.Parameters.Add(":stationID", OracleDbType.Varchar2).Value = stationToDelete.stationID;

                    int rowsDeleted = deleteCommand.ExecuteNonQuery();

                    if (rowsDeleted > 0)
                    {
                        return Ok($"Station with ID {stationToDelete.stationID} has been deleted.");
                    }
                    else
                    {
                        return StatusCode(500, $"Error deleting station with ID {stationToDelete.stationID}. No rows were affected.");
                    }
                }
            }
        }
        catch (OracleException ex) when (ex.Number == 1) // Oracle错误代码1表示唯一键冲突
        {
            Console.WriteLine($"Error deleting station: {ex.Message}");
            return StatusCode(400, $"Error deleting station: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting station: {ex.Message}");
            return StatusCode(500, $"Error deleting station: {ex.Message}");
        }
    }
}
