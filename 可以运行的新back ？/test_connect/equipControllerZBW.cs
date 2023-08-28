using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Data.Common;
using System.Reflection.PortableExecutable;
using System.Text;
//前后端进行数据交流的数据结构
[ApiController]
[Route("/")]
public class videoInfoController : ControllerBase
{
    private OracleConnection _connection;

    public videoInfoController(OracleConnection connection)
    {
        _connection = connection;
    }

    [HttpPost("api/equipmentInfo")]
    public IActionResult HandleEndpoint(inputEquipInfo inputInfo) //接收前端的数据
    {
        List<EquipInfo> videos = new List<EquipInfo>();
        Console.WriteLine($"查询数据为:{inputInfo.EquipID}\t{inputInfo.EquipType}\t{inputInfo.Equipstaion}");
        try
        {
            _connection.Open();
            // 执行 SQL 查询
            using (var command = _connection.CreateCommand())
            {
                command.Connection = _connection;
                //先写SQL语句的主体
                command.CommandText = "SELECT * FROM POLICE_EQUIPMENT WHERE 1 = 1";
                //下面的whereClause可以接到SQL语句的后面，实现SQL语句的动态变化
                StringBuilder whereClause = new StringBuilder();

                if (!string.IsNullOrEmpty(inputInfo.EquipID))
                {
                    //下面的SQL表示筛选出包含inputInfo.videoID的结果
                    whereClause.Append(" AND EQUIPMENT_ID LIKE '%' || :EquipID || '%'");
                    command.Parameters.Add(":EquipID", OracleDbType.Varchar2).Value = inputInfo.EquipID;
                }
                if (!string.IsNullOrEmpty(inputInfo.EquipType))
                {
                    whereClause.Append(" AND EQUIPMENT_TYPE LIKE '%' || :EquipType || '%'");
                    command.Parameters.Add(":EquipType", OracleDbType.Varchar2).Value = inputInfo.EquipType;
                }
                if (!string.IsNullOrEmpty(inputInfo.Equipstaion))
                {
                    whereClause.Append(" AND STATION_ID LIKE '%' || :Equipstaion || '%'");
                    command.Parameters.Add(":Equipstaion", OracleDbType.Varchar2).Value = inputInfo.Equipstaion;
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
                        EquipInfo video = new EquipInfo
                        {
                            EquipID = reader.GetString(reader.GetOrdinal("EQUIPMENT_ID")),
                            EquipType = reader.GetString(reader.GetOrdinal("EQUIPMENT_TYPE")),
                            Equipstatus = reader.GetString(reader.GetOrdinal("EQUIPMENT_STATUS")),
                            Equipstaion = reader.GetString(reader.GetOrdinal("STATION_ID")),
                        };
                        videos.Add(video);
                    }

                    _connection.Close();
                    return Ok(videos);
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
    [HttpPost("api/equipmentuseInfo")]
    public IActionResult useHandleEndpoint(inputEquipInfo inputInfo) //接收前端的数据
    {
        List<EquipInfo> videos = new List<EquipInfo>();
        List<EquipuseInfo> uses = new List<EquipuseInfo>();
        Console.WriteLine($"查询数据为:{inputInfo.EquipID}\t{inputInfo.EquipType}\t{inputInfo.Equipstaion}");
        try
        {
            _connection.Open();
            // 执行 SQL 查询
            using (var command = _connection.CreateCommand())
            {
                command.Connection = _connection;
                //先写SQL语句的主体
                command.CommandText = "SELECT * FROM POLICE_EQUIPMENT WHERE 1 = 1";
                //下面的whereClause可以接到SQL语句的后面，实现SQL语句的动态变化
                StringBuilder whereClause = new StringBuilder();

                if (!string.IsNullOrEmpty(inputInfo.EquipID))
                {
                    //下面的SQL表示筛选出包含inputInfo.videoID的结果
                    whereClause.Append(" AND EQUIPMENT_ID LIKE '%' || :EquipID || '%'");
                    command.Parameters.Add(":EquipID", OracleDbType.Varchar2).Value = inputInfo.EquipID;
                }
                if (!string.IsNullOrEmpty(inputInfo.EquipType))
                {
                    whereClause.Append(" AND EQUIPMENT_TYPE LIKE '%' || :EquipType || '%'");
                    command.Parameters.Add(":EquipType", OracleDbType.Varchar2).Value = inputInfo.EquipType;
                }
                if (!string.IsNullOrEmpty(inputInfo.Equipstaion))
                {
                    whereClause.Append(" AND STATION_ID LIKE '%' || :Equipstaion || '%'");
                    command.Parameters.Add(":Equipstaion", OracleDbType.Varchar2).Value = inputInfo.Equipstaion;
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
                        EquipInfo video = new EquipInfo
                        {
                            EquipID = reader.GetString(reader.GetOrdinal("EQUIPMENT_ID")),
                            EquipType = reader.GetString(reader.GetOrdinal("EQUIPMENT_TYPE")),
                            Equipstatus = reader.GetString(reader.GetOrdinal("EQUIPMENT_STATUS")),
                            Equipstaion = reader.GetString(reader.GetOrdinal("STATION_ID")),
                        };
                        videos.Add(video);
                    }
                    _connection.Close();
                    _connection.Open();
                    if (videos.Count > 0)
                    {
                        Console.Write("成功找到器械");
                        foreach (EquipInfo video in videos)
                        {
                            // 创建一个 OracleCommand 对象，用于执行 SQL 语句和参数化查询
                            using (var commandaa = _connection.CreateCommand())
                            {
                                // 创建寻找数据的 SQL 语句
                                commandaa.Connection = _connection;
                                //先写SQL语句的主体
                                commandaa.CommandText = "SELECT * FROM EQUIPMENT_USE WHERE EQUIPMENT_ID = :EquipID";
                                commandaa.Parameters.Add(":EquipID", OracleDbType.Varchar2).Value = video.EquipID;
                                OracleDataReader readera = commandaa.ExecuteReader();
                                while (readera.Read())
                                {
                                    EquipuseInfo user = new EquipuseInfo
                                    {
                                        equipID =video.EquipID,
                                        equipType = video.EquipType,
                                        equipstaion = video.Equipstaion,
                                        policenumber = readera.GetString(readera.GetOrdinal("POLICE_NUMBER")),
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
    [HttpPost("api/addequipmentInfo")]
    public IActionResult addhandleendpoint(inputEquipInfo inputInfo) //接收前端的数据
    {
        try
        {
            _connection.Open();
            string insertQuery = "INSERT INTO POLICE_EQUIPMENT (EQUIPMENT_ID, EQUIPMENT_TYPE, EQUIPMENT_STATUS, STATION_ID) " +
                    "VALUES (:EquipID, :EquipType, :EquipStatus, :EquipStation)";
            // 执行 SQL 查询
            if (string.IsNullOrEmpty(inputInfo.EquipType)|| string.IsNullOrEmpty(inputInfo.EquipID) || string.IsNullOrEmpty(inputInfo.Equipstaion))
            {
                return StatusCode (404,$"插入不全");
            }
            //查询警局号
            List<EquipInfo> videos = new List<EquipInfo>();
            // 执行 SQL 查询
            using (var command1 = _connection.CreateCommand())
            {
                command1.Connection = _connection;
                //先写SQL语句的主体
                command1.CommandText = "SELECT * FROM POLICE_EQUIPMENT WHERE 1 = 1";
                //下面的whereClause可以接到SQL语句的后面，实现SQL语句的动态变化
                StringBuilder whereClause = new StringBuilder();
                whereClause.Append(" AND STATION_ID LIKE '%' || :Equipstaion || '%'");
                command1.Parameters.Add(":Equipstaion", OracleDbType.Varchar2).Value = inputInfo.Equipstaion;
                using (OracleDataReader reader = command1.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        EquipInfo video = new EquipInfo
                        {
                            EquipID = reader.GetString(reader.GetOrdinal("EQUIPMENT_ID")),
                            EquipType = reader.GetString(reader.GetOrdinal("EQUIPMENT_TYPE")),
                            Equipstatus = reader.GetString(reader.GetOrdinal("EQUIPMENT_STATUS")),
                            Equipstaion = reader.GetString(reader.GetOrdinal("STATION_ID")),
                        };
                        videos.Add(video);
                    }
                    
                }
                if (videos.Count == 0)
                {
                    return StatusCode(404, $"警局不存在");
                }
            }
            using (OracleCommand command = new OracleCommand(insertQuery, _connection))
            {
                command.Connection = _connection;
                // 添加参数并设置参数值
                command.Parameters.Add(":EquipID", OracleDbType.Varchar2).Value = inputInfo.EquipID;
                command.Parameters.Add(":EquipType", OracleDbType.Varchar2).Value = inputInfo.EquipType;
                command.Parameters.Add(":EquipStatus", OracleDbType.Varchar2).Value = 1;
                command.Parameters.Add(":EquipStation", OracleDbType.Varchar2).Value = inputInfo.Equipstaion;
                // 执行插入操作
                command.ExecuteNonQuery();
                return Ok();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"增加数据时发生错误:{ex.Message}");
            return StatusCode(499, $"增加数据时发生错误: {ex.Message}");
        }
        finally
        {
            _connection.Close();
        }
    }
    [HttpPost("api/delequipmentInfo")]
    public IActionResult delHandleEndpoint(inputEquipInfo inputInfo) //接收前端的数据
    {
        List<EquipInfo> videos = new List<EquipInfo>();
        Console.WriteLine($"查询数据为:{inputInfo.EquipID}\t{inputInfo.EquipType}\t{inputInfo.Equipstaion}");
        try
        {
            _connection.Open();
            // 执行 SQL 查询
            using (var command = _connection.CreateCommand())
            {
                command.Connection = _connection;
                //先写SQL语句的主体
                command.CommandText = "SELECT * FROM POLICE_EQUIPMENT WHERE 1 = 1";
                //下面的whereClause可以接到SQL语句的后面，实现SQL语句的动态变化
                StringBuilder whereClause = new StringBuilder();

                if (!string.IsNullOrEmpty(inputInfo.EquipID))
                {
                    //下面的SQL表示筛选出包含inputInfo.videoID的结果
                    whereClause.Append(" AND EQUIPMENT_ID LIKE '%' || :EquipID || '%'");
                    command.Parameters.Add(":EquipID", OracleDbType.Varchar2).Value = inputInfo.EquipID;
                }
                if (!string.IsNullOrEmpty(inputInfo.EquipType))
                {
                    whereClause.Append(" AND EQUIPMENT_TYPE LIKE '%' || :EquipType || '%'");
                    command.Parameters.Add(":EquipType", OracleDbType.Varchar2).Value = inputInfo.EquipType;
                }
                if (!string.IsNullOrEmpty(inputInfo.Equipstaion))
                {
                    whereClause.Append(" AND STATION_ID LIKE '%' || :Equipstaion || '%'");
                    command.Parameters.Add(":Equipstaion", OracleDbType.Varchar2).Value = inputInfo.Equipstaion;
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
                        EquipInfo video = new EquipInfo
                        {
                            EquipID = reader.GetString(reader.GetOrdinal("EQUIPMENT_ID")),
                            EquipType = reader.GetString(reader.GetOrdinal("EQUIPMENT_TYPE")),
                            Equipstatus = reader.GetString(reader.GetOrdinal("EQUIPMENT_STATUS")),
                            Equipstaion = reader.GetString(reader.GetOrdinal("STATION_ID")),
                        };
                        videos.Add(video);
                    }
                    if (videos.Count > 0)
                    {
                        foreach (EquipInfo video in videos)
                        {
                            // 创建删除数据的 SQL 语句
                            string deleteQuery = "DELETE FROM POLICE_EQUIPMENT WHERE EQUIPMENT_ID = :EquipID";
                            // 创建一个 OracleCommand 对象，用于执行 SQL 语句和参数化查询
                            using (OracleCommand commandaa = new OracleCommand(deleteQuery, _connection))
                            {
                                // 添加参数并设置参数值
                                commandaa.Parameters.Add(":EquipID", OracleDbType.Varchar2).Value = video.EquipID;
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