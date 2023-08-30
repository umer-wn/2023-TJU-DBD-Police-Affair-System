using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Data.Common;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
//前后端进行数据交流的数据结构
[ApiController]
[Route("/")]
public class equipInfoController : ControllerBase
{
    private OracleConnection _connection;

    public equipInfoController(OracleConnection connection)
    {
        _connection = connection;
    }

    [HttpGet("api/EquipList")]
    public IActionResult SearchEQUIPs([FromQuery] string? equipId, [FromQuery] string? equipType, [FromQuery] string? equipStation, [FromQuery] string? equipStatus)
    {
        List<EquipInfo> equips = new List<EquipInfo>();
        try
        {
            //下面的whereClause可以接到SQL语句的后面，实现SQL语句的动态变化
            _connection.Open();
            using (var command = _connection.CreateCommand())
            {
                string sql = "SELECT * FROM POLICE_EQUIPMENT WHERE 1 = 1";
                command.Connection = _connection;
                command.CommandText = sql;
                StringBuilder whereClause = new StringBuilder();
                if (!string.IsNullOrEmpty(equipId))
                {
                    whereClause.Append(" AND EQUIPMENT_ID LIKE '%' || :equipID || '%'");
                    command.Parameters.Add(":equipID", OracleDbType.Varchar2).Value = equipId;
                }
                if (!string.IsNullOrEmpty(equipType))
                {
                    whereClause.Append(" AND EQUIPMENT_TYPE = :equipType");
                    command.Parameters.Add(":equipType", OracleDbType.Varchar2).Value = equipType;
                }
                if (!string.IsNullOrEmpty(equipStation))
                {
                    whereClause.Append(" AND STATION_ID LIKE '%' || :equipStation || '%'");
                    command.Parameters.Add(":equipStation", OracleDbType.Varchar2).Value = equipStation;
                }
                if (!string.IsNullOrEmpty(equipStatus))
                {
                    whereClause.Append(" AND EQUIPMENT_STATUS = :equipStatus");
                    command.Parameters.Add(":equipStatus", OracleDbType.Varchar2).Value = equipStatus;
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
                        EquipInfo equip = new EquipInfo()
                        {
                            EquipID = reader.GetString(reader.GetOrdinal("EQUIPMENT_ID")),
                            EquipType = reader.GetString(reader.GetOrdinal("EQUIPMENT_TYPE")),
                            EquipStatus = reader.GetString(reader.GetOrdinal("EQUIPMENT_STATUS")),
                            EquipStation = reader.GetString(reader.GetOrdinal("STATION_ID"))
                        };
                        equips.Add(equip);
                    }
                    _connection.Close();
                    return Ok(equips);
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


    [HttpGet("api/equipUseInfo")]
    public IActionResult UseHandleEndpoint([FromQuery] string? equipID)
    {
        List<EquipUseInfo> uses = new List<EquipUseInfo>();
        try
        {
            _connection.Open();
            // 执行 SQL 查询
            using (var command = _connection.CreateCommand())
            {
                command.Connection = _connection;
                //先写SQL语句的主体
                command.CommandText = "SELECT * FROM EQUIPMENT_USE WHERE EQUIPMENT_ID = :equipID";
                command.Parameters.Add(":equipID", OracleDbType.Varchar2).Value = equipID;

                Console.WriteLine($"查询数据SQL为:{command.CommandText}");
                using (OracleDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        EquipUseInfo ause = new EquipUseInfo
                        {
                            equipID = reader.GetString(reader.GetOrdinal("EQUIPMENT_ID")),
                            policemenID = reader.GetString(reader.GetOrdinal("POLICE_NUMBER")),
                            borrowtime = reader.GetDateTime(reader.GetOrdinal("BORROW_TIME")),
                            returntime = reader.IsDBNull(reader.GetOrdinal("RETURN_TIME")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("RETURN_TIME"))
                        };
                        uses.Add(ause);
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

    [HttpPost("api/addEquip")]
    public IActionResult AddHandleEndpoint([FromQuery] string? equipID, [FromQuery] string? equipType, [FromQuery] string? equipStation)
    {
        try
        {
            _connection.Open();
            string insertQuery = "INSERT INTO POLICE_EQUIPMENT (EQUIPMENT_ID, EQUIPMENT_TYPE, EQUIPMENT_STATUS, STATION_ID) " +
                        "VALUES (:equipID, :equipType, '0', :equipStation)";

            if (string.IsNullOrEmpty(equipID) || string.IsNullOrEmpty(equipType) || string.IsNullOrEmpty(equipStation))
                return Ok("新增警械失败！信息不全！");

            if (!Regex.IsMatch(equipID, @"^\d+$"))
                return Ok("无效的警械编号！");

            if (equipID.Length < 7)
                return Ok("警械编号过短！请完善编号！");

            if (!Regex.IsMatch(equipStation, @"^\d+$"))
                return Ok("无效的警局编号！");

            if (equipStation.Length < 9)
                return Ok("警局编号过短！请完善编号！");

            // 查询警械编号是否存在
            using (var command1 = _connection.CreateCommand())
            {
                command1.Connection = _connection;
                command1.CommandText = "SELECT * FROM POLICE_EQUIPMENT WHERE EQUIPMENT_ID = :equipID";
                command1.Parameters.Add(":equipID", OracleDbType.Varchar2).Value = equipID;
                using (OracleDataReader reader = command1.ExecuteReader())
                    if (reader.HasRows)
                        return Ok("已存在该警械编号！请重新选定编号！");
            }

            // 查询警局编号是否存在
            using (var command1 = _connection.CreateCommand())
            {
                command1.Connection = _connection;
                command1.CommandText = "SELECT * FROM POLICE_STATION WHERE STATION_ID = :equipStation";
                command1.Parameters.Add(":equipStation", OracleDbType.Varchar2).Value = equipStation;
                using (OracleDataReader reader = command1.ExecuteReader())
                    if (!reader.HasRows)
                        return Ok("不存在该警局！请重新输入！");
            }

            using (OracleCommand command = new OracleCommand(insertQuery, _connection))
            {
                command.Connection = _connection;
                command.Parameters.Add(":equipID", OracleDbType.Varchar2).Value = equipID;
                command.Parameters.Add(":equipType", OracleDbType.Varchar2).Value = equipType;
                command.Parameters.Add(":equipStation", OracleDbType.Varchar2).Value = equipStation;

                command.ExecuteNonQuery();
                // 插入成功，返回 JSON 数据
                var response = new { data = "成功" };
                return Ok(JsonSerializer.Serialize(response));
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

    [HttpDelete("api/delEquip")]
    public IActionResult DeleteHandleEndpoint([FromQuery] string? equipID)
    {
        try
        {
            _connection.Open();

            // 检查警械ID是否规范
            if (string.IsNullOrEmpty(equipID) || equipID.Length < 7 || !Regex.IsMatch(equipID, @"^\d+$"))
                return Ok("警械编号无效！请重新输入！");

            // 查询该警械ID对应的行是否存在
            using (var command = _connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM POLICE_EQUIPMENT WHERE EQUIPMENT_ID = :equipID";
                command.Parameters.Add(":equipID", OracleDbType.Varchar2).Value = equipID;

                using (OracleDataReader reader = command.ExecuteReader())
                {
                    if (!reader.HasRows)
                        return Ok("未找到该编号的警械！");
                }
            }

            // 执行删除相关记录的操作
            using (var command = _connection.CreateCommand())
            {
                command.CommandText = "DELETE FROM EQUIPMENT_USE WHERE EQUIPMENT_ID = :equipID";
                command.Parameters.Add(":equipID", OracleDbType.Varchar2).Value = equipID;
                command.ExecuteNonQuery();
            }

            // 执行删除警械操作
            using (var command = _connection.CreateCommand())
            {
                command.CommandText = "DELETE FROM POLICE_EQUIPMENT WHERE EQUIPMENT_ID = :equipID";
                command.Parameters.Add(":equipID", OracleDbType.Varchar2).Value = equipID;
                command.ExecuteNonQuery();
            }

            // 返回成功的JSON响应
            var response = new { data = "成功" };
            return Ok(JsonSerializer.Serialize(response));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"删除数据时发生错误: {ex.Message}");
            return StatusCode(499, $"删除数据时发生错误: {ex.Message}");
        }
        finally
        {
            _connection.Close();
        }
    }

    [HttpPost("api/addEquipUseRecord")]
    public IActionResult AddEquipUseRecord([FromQuery] string? equipID, [FromQuery] string? policemenID)
    {
        try
        {
            _connection.Open();
            OracleTransaction transaction = _connection.BeginTransaction(); // 开始数据库事务

            string insertQuery = "INSERT INTO EQUIPMENT_USE (EQUIPMENT_ID, POLICE_NUMBER, BORROW_TIME, RETURN_TIME) " +
                                 "VALUES (:equipID, :policemenID, :bTime, :rTime)";

            if (string.IsNullOrEmpty(equipID) || string.IsNullOrEmpty(policemenID))
                return Ok("新增记录失败！信息不全！");

            if (!Regex.IsMatch(equipID, @"^\d+$"))
                return Ok("无效的警械编号！");

            if (!Regex.IsMatch(policemenID, @"^\d+$"))
                return Ok("无效的警员编号！");

            if (equipID.Length < 7)
                return Ok("警械编号过短！请完善编号！");
            if (policemenID.Length < 7)
                return Ok("警员编号过短！请完善编号！");

            // 查询警员编号是否存在
            using (var command1 = _connection.CreateCommand())
            {
                command1.Connection = _connection;
                command1.CommandText = "SELECT * FROM POLICEMEN WHERE POLICE_NUMBER = :policemenID";
                command1.Parameters.Add(":policemenID", OracleDbType.Varchar2).Value = policemenID;
                using (OracleDataReader reader = command1.ExecuteReader())
                    if (!reader.HasRows)
                        return Ok("不存在该警员！请重新输入！");
            }

            // 查询警械编号是否存在且状态为可用
            bool isEquipInUse;
            using (var command3 = _connection.CreateCommand())
            {
                command3.Connection = _connection;
                command3.CommandText = "SELECT * FROM POLICE_EQUIPMENT WHERE EQUIPMENT_ID = :equipID FOR UPDATE";
                command3.Parameters.Add(":equipID", OracleDbType.Varchar2).Value = equipID;
                using (OracleDataReader reader = command3.ExecuteReader())
                {
                    if (!reader.HasRows)
                        return Ok("不存在该警械编号！");
                    reader.Read();
                    int status = Convert.ToInt32(reader["EQUIPMENT_STATUS"]);
                    if (status == 1)
                    {
                        isEquipInUse = true;
                    }
                    else
                    {
                        isEquipInUse = false;
                        // 更新警械状态为使用中
                        using (var command4 = _connection.CreateCommand())
                        {
                            command4.Connection = _connection;
                            command4.CommandText = "UPDATE POLICE_EQUIPMENT SET EQUIPMENT_STATUS = '1' WHERE EQUIPMENT_ID = :equipID";
                            command4.Parameters.Add(":equipID", OracleDbType.Varchar2).Value = equipID;
                            command4.ExecuteNonQuery();
                        }
                    }
                }
            }

            if (isEquipInUse)
            {
                transaction.Rollback(); // 警械正在使用中，回滚事务
                return Ok("该警械使用中！");
            }

            // 获取当前时间
            DateTime borrowTime = DateTime.Now;

            using (OracleCommand command = new OracleCommand(insertQuery, _connection))
            {
                command.Connection = _connection;
                command.Parameters.Add(":equipID", OracleDbType.Varchar2).Value = equipID;
                command.Parameters.Add(":policemenID", OracleDbType.Varchar2).Value = policemenID;
                command.Parameters.Add(":bTime", OracleDbType.Date).Value = borrowTime;
                command.Parameters.Add(":rTime", OracleDbType.Date).Value = DBNull.Value;

                // 执行插入操作
                command.ExecuteNonQuery();

                // 提交事务
                transaction.Commit();

                // 插入成功，返回 JSON 数据
                var response = new { data = "成功" };
                return Ok(JsonSerializer.Serialize(response));
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

    [HttpPut("api/returnEquip")]
    public IActionResult returnEquip([FromQuery] string? equipID, [FromQuery] string? policemenID)
    {
        try
        {
            _connection.Open();
            OracleTransaction transaction = _connection.BeginTransaction(); // 开始数据库事务

            string insertQuery = "UPDATE EQUIPMENT_USE SET RETURN_TIME = :rTime WHERE EQUIPMENT_ID = :equipID AND POLICE_NUMBER = :policemenID AND RETURN_TIME IS NULL";
            
            if (string.IsNullOrEmpty(equipID) || string.IsNullOrEmpty(policemenID))
                return Ok("新增记录失败！信息不全！");

            if (!Regex.IsMatch(equipID, @"^\d+$"))
                return Ok("无效的警械编号！");

            if (!Regex.IsMatch(policemenID, @"^\d+$"))
                return Ok("无效的警员编号！");

            if (equipID.Length < 7)
                return Ok("警械编号过短！请完善编号！");
            if (policemenID.Length < 7)
                return Ok("警员编号过短！请完善编号！");

            // 查询警员编号是否存在
            using (var command1 = _connection.CreateCommand())
            {
                command1.Connection = _connection;
                command1.CommandText = "SELECT * FROM POLICEMEN WHERE POLICE_NUMBER = :policemenID";
                command1.Parameters.Add(":policemenID", OracleDbType.Varchar2).Value = policemenID;
                using (OracleDataReader reader = command1.ExecuteReader())
                    if (!reader.HasRows)
                        return Ok("不存在该警员！请重新输入！");
            }

            // 查询警械编号是否存在且状态为使用中
            bool isEquipInUse;
            using (var command3 = _connection.CreateCommand())
            {
                command3.Connection = _connection;
                command3.CommandText = "SELECT * FROM POLICE_EQUIPMENT WHERE EQUIPMENT_ID = :equipID FOR UPDATE";
                command3.Parameters.Add(":equipID", OracleDbType.Varchar2).Value = equipID;
                using (OracleDataReader reader = command3.ExecuteReader())
                {
                    if (!reader.HasRows)
                        return Ok("不存在该警械编号！");
                    reader.Read();
                    int status = Convert.ToInt32(reader["EQUIPMENT_STATUS"]);
                    if (status == 0)
                    {
                        isEquipInUse = false;
                    }
                    else
                    {
                        isEquipInUse = true;
                        // 更新警械状态为空闲
                        using (var command4 = _connection.CreateCommand())
                        {
                            command4.Connection = _connection;
                            command4.CommandText = "UPDATE POLICE_EQUIPMENT SET EQUIPMENT_STATUS = '0' WHERE EQUIPMENT_ID = :equipID";
                            command4.Parameters.Add(":equipID", OracleDbType.Varchar2).Value = equipID;
                            command4.ExecuteNonQuery();
                        }
                    }
                }
            }

            if (!isEquipInUse)
            {
                transaction.Rollback(); // 警械空闲，回滚事务
                return Ok("该警械空闲中！");
            }

            // 获取当前时间
            DateTime returnTime = DateTime.Now;

            using (OracleCommand command = new OracleCommand(insertQuery, _connection))
            {
                command.Connection = _connection;
                command.Parameters.Add(":equipID", OracleDbType.Varchar2).Value = equipID;
                command.Parameters.Add(":policemenID", OracleDbType.Varchar2).Value = policemenID;
                command.Parameters.Add(":rTime", OracleDbType.Date).Value = returnTime;

                // 执行插入操作
                int count = command.ExecuteNonQuery();
                Console.WriteLine($"成功更新 {count} 条记录。");

                // 提交事务
                transaction.Commit();

                // 插入成功，返回 JSON 数据
                var response = new { data = "成功" };
                return Ok(JsonSerializer.Serialize(response));
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

}