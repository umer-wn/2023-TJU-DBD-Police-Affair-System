using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Data.Common;
using System.Reflection.PortableExecutable;
using System.Text;

//前后端进行数据交流的数据结构
public class videoInfoZYH
{
    public string videoID { get; set; }
    public string videoType { get; set; }
    public DateTime recordTime { get; set; }
    public DateTime uploadTime { get; set; }
    public string principleID { get; set; }
}

public class inputVideoInfoZYH
{
    public string videoID { get; set; }
    public string videoType { get; set; }
    public string principleID { get; set; }
}

[ApiController]
[Route("api/videoInfo")]
public class videoInfoControllerZYH : ControllerBase
{
    private OracleConnection _connection;

    public videoInfoControllerZYH(OracleConnection connection)
    {
        _connection = connection;
    }

    [HttpPost]
    public IActionResult HandleEndpoint(inputVideoInfoZYH inputInfo) //接收前端的数据
    {
        List<videoInfoZYH> videos = new List<videoInfoZYH>();
        Console.WriteLine($"查询数据为:{inputInfo.videoID}\t{inputInfo.videoType}\t{inputInfo.principleID}");
        try
        {
            _connection.Open();
            // 执行 SQL 查询
            using (var command = _connection.CreateCommand())
            {
                command.Connection = _connection;
                //先写SQL语句的主体
                command.CommandText = "SELECT * FROM VIDEOS WHERE 1 = 1";
                //下面的whereClause可以接到SQL语句的后面，实现SQL语句的动态变化
                StringBuilder whereClause = new StringBuilder();

                if (!string.IsNullOrEmpty(inputInfo.videoID))
                {
                    //下面的SQL表示筛选出包含inputInfo.videoID的结果
                    whereClause.Append(" AND VIDEO_ID LIKE '%' || :videoID || '%'");
                    command.Parameters.Add(":videoID", OracleDbType.Varchar2).Value = inputInfo.videoID;
                }
                if (inputInfo.videoType != "全部")
                {
                    whereClause.Append(" AND VIDEO_TYPE LIKE '%' || :videoType || '%'");
                    command.Parameters.Add(":videoType", OracleDbType.Varchar2).Value = inputInfo.videoType;
                }
                if (!string.IsNullOrEmpty(inputInfo.principleID))
                {
                    whereClause.Append(" AND PRINCIPLE_ID LIKE '%' || :principleID || '%'");
                    command.Parameters.Add(":principleID", OracleDbType.Varchar2).Value = inputInfo.principleID;
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
                        videoInfoZYH video = new videoInfoZYH
                        {
                            videoID = reader.GetString(reader.GetOrdinal("VIDEO_id")),
                            videoType = reader.GetString(reader.GetOrdinal("VIDEO_TYPE")),
                            recordTime = reader.GetDateTime(reader.GetOrdinal("RECORD_TIME")),
                            uploadTime = reader.GetDateTime(reader.GetOrdinal("UPLOAD_TIME")),
                            principleID = reader.GetString(reader.GetOrdinal("PRINCIPLE_ID")),
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
}