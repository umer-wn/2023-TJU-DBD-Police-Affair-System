using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Data.Common;
using System.Reflection.PortableExecutable;
using System.Text;

//前后端进行数据交流的数据结构
public class caseInfo
{
    public string caseID { get; set; }
    public string caseType { get; set; }
    public string status { get; set; }
    public DateTime registerTime { get; set; }
    public string address { get; set; }
    public string ranking { get; set; }
}

public class inputCaseInfo
{
    public string caseID { get; set; }
    public string caseType { get; set; }
    public string status { get; set; }
    public string address { get; set; }
    public string ranking { get; set; }
}

[ApiController]
[Route("api/caseInfo")]
public class caseInfoController : ControllerBase
{
    private OracleConnection _connection;

    public caseInfoController(OracleConnection connection)
    {
        _connection = connection;
    }

    [HttpPost]
    public IActionResult HandleEndpoint(inputCaseInfo inputInfo) //接收前端的数据
    {
        List<caseInfo> cases = new List<caseInfo>();
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
                        caseInfo Case = new caseInfo
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
}



public class inputCaseID
{
    public string caseID { get; set; }
}

public class caseDetails
{
    public string phone { get; set; }
    public DateTime callsTime { get; set; }
    public string videoID { get; set; }
    public string videoType { get; set; }
    public DateTime recordTime { get; set; }
    public string IDNum { get; set; }
    public string relatedType { get; set; }
    public string policemenNumber { get; set; }
    public string policemenName { get; set; }
    public string IDNumber { get; set; }
}


[ApiController]
[Route("api/caseDetails")]
public class caseDetailsController : ControllerBase
{
    private OracleConnection _connection;

    public caseDetailsController(OracleConnection connection)
    {
        _connection = connection;
    }

    [HttpPost]
    public IActionResult HandleEndpoint(inputCaseID inputID) //接收前端的数据
    {
        List<caseDetails> cases = new List<caseDetails>();
        Console.WriteLine($"查询数据为:{inputID.caseID}");
        try
        {
            _connection.Open();
            // 执行 SQL 查询
            using (var command = _connection.CreateCommand())
            {
                command.Connection = _connection;
                //先写SQL语句的主体
                command.CommandText = "SELECT PHONE_NUMBER, CALLS_TIME, VIDEO_ID, VIDEO_TYPE, RECORD_TIME" +
                    ", ID_NUM, RELATED_TYPE, POLICE_NUMBER, POLICE_NAME, ID_NUMBER" +
                    " FROM CALLS OUTJOIN CASES ON " +
                    " WHERE 1 = 1";
                

                Console.WriteLine($"查询数据SQL为:{command.CommandText}");
                using (OracleDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        caseDetails Case = new caseDetails
                        {
                            phone = reader.GetString(reader.GetOrdinal("PHONE_NUMBER")),
                            callsTime = reader.GetDateTime(reader.GetOrdinal("CALLS_TIME")),
                            videoID = reader.GetString(reader.GetOrdinal("VIDEO_ID")),
                            videoType = reader.GetString(reader.GetOrdinal("VIDEO_TYPE")),
                            recordTime = reader.GetDateTime(reader.GetOrdinal("RECORD_TIME")),
                            IDNum = reader.GetString(reader.GetOrdinal("ID_NUM")),
                            relatedType = reader.GetString(reader.GetOrdinal("RELATED_TYPE")),
                            policemenNumber = reader.GetString(reader.GetOrdinal("POLICE_NUMBER")),
                            policemenName = reader.GetString(reader.GetOrdinal("POLICE_NAME")),
                            IDNumber = reader.GetString(reader.GetOrdinal("ID_NUMBER"))
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
}
