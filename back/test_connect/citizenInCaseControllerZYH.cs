using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Data.Common;
using System.Reflection.PortableExecutable;
using System.Text;



[ApiController]
[Route("/")]
public class citizenInCaseInfoControllerZYH : ControllerBase
{
    private OracleConnection _connection;

    public citizenInCaseInfoControllerZYH(OracleConnection connection)
    {
        _connection = connection;
    }

    [HttpPost("api/citizenInCaseInfo")]
    public IActionResult HandleEndpoint(inputCitizenInCaseInfoZYH inputInfo) //接收前端的数据
    {
        List<citizenInCaseInfoZYH> cases = new List<citizenInCaseInfoZYH>();
        
        try
        {
            _connection.Open();
            // 执行 SQL 查询
            using (var command = _connection.CreateCommand())
            {
                command.Connection = _connection;
                //先写SQL语句的主体
                command.CommandText = "SELECT CASE_ID,CASE_TYPE,STATUS,REGISTER_TIME,ADDRESS,RANKING,ID_NUM,RELATED_TYPE,CITIZEN_NAME,GENDER" +
                    " FROM CASES NATURAL JOIN RELATED NATURAL JOIN CITIZEN WHERE 1 = 1";
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
                    whereClause.Append(" AND ADDRESS LIKE '%' || :address || '%'");
                    command.Parameters.Add(":address", OracleDbType.Varchar2).Value = inputInfo.address;
                }
                if (inputInfo.ranking != "全部")
                {
                    whereClause.Append(" AND RANKING = :ranking");
                    command.Parameters.Add(":ranking", OracleDbType.Char).Value = inputInfo.ranking;
                }
                if (!string.IsNullOrEmpty(inputInfo.IDNum))
                {
                    whereClause.Append(" AND ID_NUM LIKE '%' || :IDNum || '%'");
                    command.Parameters.Add(":IDNum", OracleDbType.Varchar2).Value = inputInfo.IDNum;
                }
                if (inputInfo.relatedType != "全部")
                {
                    whereClause.Append(" AND RELATED_TYPE = :relatedType");
                    command.Parameters.Add(":relatedType", OracleDbType.Varchar2).Value = inputInfo.relatedType;
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
                        citizenInCaseInfoZYH Case = new citizenInCaseInfoZYH
                        {
                            caseID = reader.GetString(reader.GetOrdinal("CASE_ID")),
                            caseType = reader.GetString(reader.GetOrdinal("CASE_TYPE")),
                            status = reader.GetString(reader.GetOrdinal("STATUS")),
                            registerTime = reader.GetDateTime(reader.GetOrdinal("REGISTER_TIME")),
                            address = reader.GetString(reader.GetOrdinal("ADDRESS")),
                            ranking = reader.GetString(reader.GetOrdinal("RANKING")),
                            IDNum = reader.GetString(reader.GetOrdinal("ID_NUM")),
                            relatedType = reader.GetString(reader.GetOrdinal("RELATED_TYPE")),
                            citizenName = reader.GetString(reader.GetOrdinal("CITIZEN_NAME")),
                            gender = reader.GetString(reader.GetOrdinal("GENDER"))
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
    [HttpPost("api/addcitizenInCaseInfo")]
    public IActionResult AddCase([FromBody] citizenInCaseInfoZYH inputInfo)
    {
        try
        {
            _connection.Open();

            // 检验输入数据的完整性
            if (string.IsNullOrEmpty(inputInfo.caseID))
            {
                _connection.Close();
                return BadRequest("案件编号不能为空");
            }

            if (string.IsNullOrEmpty(inputInfo.caseType))
            {
                _connection.Close();
                return BadRequest("案件类型不能为空");
            }
            // Check if the citizen exists
            string citizenExistSql = "SELECT COUNT(*) FROM CITIZEN WHERE ID_NUM = :IDNum";
            using (OracleCommand citizenExistCommand = new OracleCommand(citizenExistSql, _connection))
            {
                citizenExistCommand.Parameters.Add(new OracleParameter(":IDNum", OracleDbType.Varchar2)).Value = inputInfo.IDNum;
                int citizenCount = Convert.ToInt32(citizenExistCommand.ExecuteScalar());

                if (citizenCount == 0)
                {
                    _connection.Close();
                    return BadRequest("该公民不存在");
                }
            }

            // Check if the case exists
            string caseExistSql = "SELECT COUNT(*) FROM CASES WHERE CASE_ID = :caseID";
            using (OracleCommand caseExistCommand = new OracleCommand(caseExistSql, _connection))
            {
                caseExistCommand.Parameters.Add(new OracleParameter(":caseID", OracleDbType.Varchar2)).Value = inputInfo.caseID;
                int caseCount = Convert.ToInt32(caseExistCommand.ExecuteScalar());

                if (caseCount == 0)
                {
                    _connection.Close();
                    return BadRequest("该案件不存在");
                }
            }
            // 继续检验其他字段的完整性...

            // 定义 INSERT SQL 语句
            string insertSql = @"
            INSERT INTO CASES (CASE_ID, CASE_TYPE, STATUS, REGISTER_TIME, ADDRESS, RANKING, ID_NUM, RELATED_TYPE, CITIZEN_NAME, GENDER)
            VALUES (:caseID, :caseType, :status, :registerTime, :address, :ranking, :IDNum, :relatedType, :citizenName, :gender)";

            using (OracleCommand command = new OracleCommand(insertSql, _connection))
            {
                // 添加参数
                command.Parameters.Add(new OracleParameter(":caseID", OracleDbType.Varchar2)).Value = inputInfo.caseID;
                command.Parameters.Add(new OracleParameter(":caseType", OracleDbType.Varchar2)).Value = inputInfo.caseType;
                command.Parameters.Add(new OracleParameter(":status", OracleDbType.Varchar2)).Value = inputInfo.status;
                command.Parameters.Add(new OracleParameter(":registerTime", OracleDbType.Date)).Value = inputInfo.registerTime;
                command.Parameters.Add(new OracleParameter(":address", OracleDbType.Varchar2)).Value = inputInfo.address;
                command.Parameters.Add(new OracleParameter(":ranking", OracleDbType.Char)).Value = inputInfo.ranking;
                command.Parameters.Add(new OracleParameter(":IDNum", OracleDbType.Varchar2)).Value = inputInfo.IDNum;
                command.Parameters.Add(new OracleParameter(":relatedType", OracleDbType.Varchar2)).Value = inputInfo.relatedType;
                command.Parameters.Add(new OracleParameter(":citizenName", OracleDbType.Varchar2)).Value = inputInfo.citizenName;
                command.Parameters.Add(new OracleParameter(":gender", OracleDbType.Varchar2)).Value = inputInfo.gender;

                // 执行插入操作
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    _connection.Close();
                    return Ok("记录添加成功");
                }
                else
                {
                    _connection.Close();
                    return BadRequest("记录添加失败");
                }
            }
        }
        catch (OracleException ex)
        {
            Console.WriteLine($"添加记录时发生 Oracle 数据库错误: {ex.Message}");
            return StatusCode(500, $"添加记录时发生 Oracle 数据库错误: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"添加记录时发生错误: {ex.Message}");
            return StatusCode(500, $"添加记录时发生错误: {ex.Message}");
        }
        finally
        {
            _connection.Close();
        }
    }
    [HttpDelete("api/delcitizenInCaseInfo/{caseID}")]
    public IActionResult DeleteCase(string caseID)
    {
        using (var transaction = _connection.BeginTransaction())
        {
            try
            {
                // 检查要删除的记录是否存在
                string selectSql = "SELECT COUNT(*) FROM CASES WHERE CASE_ID = :caseID";

                // 创建命令对象
                using (var command = _connection.CreateCommand())
                {
                    command.Transaction = transaction;
                    command.CommandText = selectSql;
                    command.Parameters.Add(new OracleParameter(":caseID", OracleDbType.Varchar2) { Value = caseID });

                    // 执行查询并获取结果
                    int count = 0;
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            count = reader.GetInt32(0);
                        }
                    }

                    if (count == 0)
                    {
                        transaction.Rollback();
                        return NotFound("找不到要删除的记录");
                    }
                }

                // 执行删除操作
                string deleteSql = "DELETE FROM CASES WHERE CASE_ID = :caseID";

                // 创建命令对象
                using (var command = _connection.CreateCommand())
                {
                    command.Transaction = transaction;
                    command.CommandText = deleteSql;
                    command.Parameters.Add(new OracleParameter(":caseID", OracleDbType.Varchar2) { Value = caseID });

                    // 执行删除操作
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        transaction.Commit();
                        return Ok("记录删除成功");
                    }
                    else
                    {
                        transaction.Rollback();
                        return StatusCode(500, "删除记录失败");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"删除记录时发生错误: {ex.Message}");
                transaction.Rollback();
                return StatusCode(500, $"删除记录时发生错误: {ex.Message}");
            }
        }
    }



}