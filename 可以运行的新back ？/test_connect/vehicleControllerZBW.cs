using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using System.Text;

[ApiController]
[Route("/")]
public class vehicleController : ControllerBase
{
    private OracleConnection _connection;
    public vehicleController(OracleConnection connection)
    {
        _connection = connection;
    }
    [HttpPost("api/vehicles")]
    public IActionResult SearchVehicles(inputVehicleinfo input)
    {
        List<Vehicleinfo> Vehiclem = new List<Vehicleinfo>();
        Console.WriteLine($"查询数据为:{input.VID}\t{input.VTYPE}\t{input.VST}");
        try
        {
            //下面的whereClause可以接到SQL语句的后面，实现SQL语句的动态变化
            _connection.Open();
            using (var command = _connection.CreateCommand())
            {
                string sql = "SELECT * FROM VEHICLE WHERE 1 = 1";
                command.Connection = _connection;
                command.CommandText = sql;
                StringBuilder whereClause = new StringBuilder();
                if (!string.IsNullOrEmpty(input.VID))
                {
                    whereClause.Append(" AND Vehicle_ID = :vehicleID");
                    command.Parameters.Add(":vehicleID", OracleDbType.Varchar2).Value = input.VID;
                }
                if (!string.IsNullOrEmpty(input.VTYPE))
                {
                    whereClause.Append(" AND Vehicle_Type LIKE '%' || :VehicleType || '%'");
                    command.Parameters.Add(":VehicleType", OracleDbType.Varchar2).Value = input.VTYPE;
                }
                if (!string.IsNullOrEmpty(input.VST))
                {
                    whereClause.Append(" AND Status = :vStatus");
                    command.Parameters.Add(":vStatus", OracleDbType.Varchar2).Value = input.VST;
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
                        var vehiclea = new Vehicleinfo()
                        {
                            Vehicle_ID = reader.GetString(reader.GetOrdinal("Vehicle_ID")),
                            Vehicle_Type = reader.GetString(reader.GetOrdinal("Vehicle_Type")),
                            Status = reader.GetString(reader.GetOrdinal("Status"))
                        };
                        Vehiclem.Add(vehiclea);
                    }
                    _connection.Close();
                    return Ok(Vehiclem);
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
    [HttpPost("api/usevehicles")]
    public IActionResult useHandleEndpoint(inputVehicleinfo input) //接收前端的数据
    {
        List<Vehicleinfo> videos = new List<Vehicleinfo>();
        List<Vehicleuseinfo> uses = new List<Vehicleuseinfo>();
        Console.WriteLine($"查询数据为:{input.VID}\t{input.VTYPE}\t{input.VST}");
        try
        {
            _connection.Open();
            // 执行 SQL 查询
            using (var command = _connection.CreateCommand())
            {
                command.Connection = _connection;
                //先写SQL语句的主体
                command.CommandText = "SELECT * FROM VEHICLE WHERE 1 = 1";
                //下面的whereClause可以接到SQL语句的后面，实现SQL语句的动态变化
                StringBuilder whereClause = new StringBuilder();

                if (!string.IsNullOrEmpty(input.VID))
                {
                    whereClause.Append(" AND vehicle_ID = :vehicleID");
                    command.Parameters.Add(":vehicleID", OracleDbType.Varchar2).Value = input.VID;
                }
                if (!string.IsNullOrEmpty(input.VTYPE))
                {
                    whereClause.Append(" AND Vehicle_Type LIKE '%' || :VehicleType || '%'");
                    command.Parameters.Add(":VehicleType", OracleDbType.Varchar2).Value = input.VTYPE;
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
                        var vehiclea = new Vehicleinfo()
                        {
                            Vehicle_ID = reader.GetString(reader.GetOrdinal("Vehicle_ID")),
                            Vehicle_Type = reader.GetString(reader.GetOrdinal("Vehicle_Type")),
                            Status = reader.GetString(reader.GetOrdinal("Status"))
                        };
                        videos.Add(vehiclea);
                    }
                    _connection.Close();
                    _connection.Open();
                    if (videos.Count > 0)
                    {
                        Console.Write("成功找到车辆");
                        foreach (Vehicleinfo video in videos)
                        {
                            // 创建一个 OracleCommand 对象，用于执行 SQL 语句和参数化查询
                            using (var commandaa = _connection.CreateCommand())
                            {
                                // 创建寻找数据的 SQL 语句
                                commandaa.Connection = _connection;
                                //先写SQL语句的主体
                                commandaa.CommandText = "SELECT * FROM VEHICLE_USE WHERE VEHICLE_ID = :vehID";
                                commandaa.Parameters.Add(":vehID", OracleDbType.Varchar2).Value = video.Vehicle_ID;
                                OracleDataReader readera = commandaa.ExecuteReader();
                                while (readera.Read())
                                {
                                    Vehicleuseinfo user = new Vehicleuseinfo
                                    {
                                        VehicleID = readera.GetString(readera.GetOrdinal("VEHICLE_ID")),
                                        StationID = readera.GetString(readera.GetOrdinal("STATION_ID")),
                                        borrowtime = readera.GetString(readera.GetOrdinal("BORROW_TIME")),
                                        returntime = readera.GetString(readera.GetOrdinal("RETURN_TIME"))
                                    };
                                    uses.Add(user);
                                }
                            }
                        }
                    }
                    _connection.Close();
                    return Ok(uses);
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
    [HttpPost("api/addvehicles")]
    public IActionResult addHandleEndpoint(inputVehicleinfo input) //接收前端的数据
    {
        Console.WriteLine($"查询数据为:{input.VID}\t{input.VTYPE}\t{input.VST}");
        try
        {
            //下面的whereClause可以接到SQL语句的后面，实现SQL语句的动态变化
            _connection.Open();
            string insertQuery = "INSERT INTO VEHICLE (Vehicle_ID, Vehicle_Type, Status) " +
                      "VALUES (:vehicleID, :vehicleType, :vStatus)";
            // 执行 SQL 查询
            if (string.IsNullOrEmpty(input.VID) || string.IsNullOrEmpty(input.VTYPE) || string.IsNullOrEmpty(input.VST))
            {
                return StatusCode(404, $"插入不全");
            }
            //查询警局号
            List<Vehicleinfo> videos = new List<Vehicleinfo>();
            // 执行 SQL 查询
            using (var command1 = _connection.CreateCommand())
            {
                command1.Connection = _connection;
                //先写SQL语句的主体
                command1.CommandText = "SELECT * FROM POLICE_EQUIPMENT WHERE 1 = 1";
                //下面的whereClause可以接到SQL语句的后面，实现SQL语句的动态变化
                StringBuilder whereClause = new StringBuilder();
                whereClause.Append(" AND VEHICLE_ID LIKE '%' || :Equipstaion || '%'");
                command1.Parameters.Add(":Equipstaion", OracleDbType.Varchar2).Value = input.VID;
                using (OracleDataReader reader = command1.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Vehicleinfo video = new Vehicleinfo
                        {
                            Vehicle_ID = reader.GetString(reader.GetOrdinal("Vehicle_ID")),
                            Vehicle_Type = reader.GetString(reader.GetOrdinal("Vehicle_TYPE")),
                            Status = reader.GetString(reader.GetOrdinal("STATUS")),
                        };
                        videos.Add(video);
                    }

                }
                if (videos.Count == 0)
                {
                    return StatusCode(404, $"警车不存在");
                }
            }
            using (OracleCommand command = new OracleCommand(insertQuery, _connection))
            {
                command.Connection = _connection;
                // 添加参数并设置参数值
                command.Parameters.Add(":vehicleID", OracleDbType.Varchar2).Value = input.VID;
                command.Parameters.Add(":vehicleType", OracleDbType.Varchar2).Value = input.VTYPE;
                command.Parameters.Add(":vStatus", OracleDbType.Varchar2).Value = 1;
                // 执行插入操作
                command.ExecuteNonQuery();
                return Ok();
            }

          
        }
        catch (Exception ex)
        {
            Console.WriteLine($"插入数据时发生错误:{ex.Message}");
            return StatusCode(499, $"插入数据时发生错误: {ex.Message}");
        }
        finally
        {
            _connection.Close();
        }
    }
    [HttpPost("api/delvehicles")]
    public IActionResult delHandleEndpoint(inputVehicleinfo inputInfo) //接收前端的数据
    {
        List<Vehicleinfo> videos = new List<Vehicleinfo>();
        Console.WriteLine($"查询数据为:{inputInfo.VID}\t{inputInfo.VTYPE}\t{inputInfo.VST}");
        try
        {
            _connection.Open();
            // 执行 SQL 查询
            using (var command = _connection.CreateCommand())
            {
                //下面的whereClause可以接到SQL语句的后面，实现SQL语句的动态变化
                string sql = "SELECT * FROM VEHICLE WHERE 1 = 1";
                command.Connection = _connection;
                command.CommandText = sql;
                StringBuilder whereClause = new StringBuilder();
                if (!string.IsNullOrEmpty(inputInfo.VID))
                {
                    whereClause.Append(" AND vehicle_ID = :vehicleID");
                    command.Parameters.Add(":vehicleID", OracleDbType.Varchar2).Value = inputInfo.VID;
                }
                if (!string.IsNullOrEmpty(inputInfo.VTYPE))
                {
                    whereClause.Append(" AND Vehicle_Type LIKE '%' || :VehicleType || '%'");
                    command.Parameters.Add(":VehicleType", OracleDbType.Varchar2).Value = inputInfo.VTYPE;
                }
                if (!string.IsNullOrEmpty(inputInfo.VST))
                {
                    whereClause.Append(" AND Status = :vStatus");
                    command.Parameters.Add(":vStatus", OracleDbType.Varchar2).Value = inputInfo.VST;
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
                        var vehiclea = new Vehicleinfo()
                        {
                            Vehicle_ID = reader.GetString(reader.GetOrdinal("Vehicle_ID")),
                            Vehicle_Type = reader.GetString(reader.GetOrdinal("Vehicle_Type")),
                            Status = reader.GetString(reader.GetOrdinal("Status"))
                        };
                        videos.Add(vehiclea);
                    }
                    if (videos.Count > 0)
                    {
                        foreach (Vehicleinfo video in videos)
                        {
                            // 创建删除数据的 SQL 语句
                            string deleteQuery = "DELETE FROM VEHICLE WHERE VEHICLE_ID = :vehiclID";
                            // 创建一个 OracleCommand 对象，用于执行 SQL 语句和参数化查询
                            using (OracleCommand commandaa = new OracleCommand(deleteQuery, _connection))
                            {
                                // 添加参数并设置参数值
                                commandaa.Parameters.Add(":vehiclID", OracleDbType.Varchar2).Value = video.Vehicle_ID;
                                // 执行删除操作
                                commandaa.ExecuteNonQuery();
                            }
                        }
                    }
                    _connection.Close();
                    return Ok(videos);//返回删除掉的项目
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"删除数据时发生错误:{ex.Message}");
            return StatusCode(499, $"删除数据时发生错误: {ex.Message}");
        }
        finally
        {
            _connection.Close();
        }
    }
    private void HandleEndpoint(string equipstaion)
    {
        throw new NotImplementedException();
    }
}
