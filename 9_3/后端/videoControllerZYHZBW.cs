using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Data.Common;
using System.Reflection.PortableExecutable;
using System.Text;


[ApiController]
[Route("/")]
public class videoInfoControllerZYH : ControllerBase
{
    private OracleConnection _connection;

    public videoInfoControllerZYH(OracleConnection connection)
    {
        _connection = connection;
    }

    [HttpPost("api/videoInfo")]
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
    [HttpPost("api/addvideoInfo")]
    public IActionResult AddVideo(inputVideoInfoZYH newVideo)
    {
        try
        {
            _connection.Open();
            using (var command = _connection.CreateCommand())
            {
                // Prepare an SQL INSERT statement to add a new video record
                command.CommandText = "INSERT INTO VIDEOS (VIDEO_ID, VIDEO_TYPE, RECORD_TIME, UPLOAD_TIME, PRINCIPLE_ID) " +
                                      "VALUES (:videoID, :videoType, :recordTime, :uploadTime, :principleID)";
                command.Parameters.Add(":videoID", OracleDbType.Varchar2).Value = newVideo.videoID;
                command.Parameters.Add(":videoType", OracleDbType.Varchar2).Value = newVideo.videoType;
                DateTime dateTime;
                string format = "yyyy-MM-ddTHH:mm:ss.fffffffzzz"; // 包含日期、时间、毫秒和时区的格式字符串
                string time = "2021-12-31T16:55:18.000Z";
                DateTime.TryParseExact(time, format, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dateTime);
                command.Parameters.Add(":recordTime", OracleDbType.Date).Value = DateTime.Now; // Set the current time as the record time
                command.Parameters.Add(":uploadTime", OracleDbType.Date).Value = DateTime.Now; // Set the current time as the upload time
                command.Parameters.Add(":principleID", OracleDbType.Varchar2).Value = newVideo.principleID;

                int rowsAffected = command.ExecuteNonQuery();

                _connection.Close();

                if (rowsAffected > 0)
                {
                    return Ok("Video record added successfully");
                }
                else
                {
                    return BadRequest("Failed to add video record");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error while adding video record: {ex.Message}");
            return StatusCode(500, $"Error while adding video record: {ex.Message}");
        }
        finally
        {
            _connection.Close();
        }
    }
    [HttpDelete("api/delvideoInfo/{videoID}")]
    public IActionResult DeleteVideo(string videoID)
    {
        try
        {
            _connection.Open();
            using (var command = _connection.CreateCommand())
            {
                // Prepare an SQL DELETE statement to delete a video record by videoID
                command.CommandText = "DELETE FROM VIDEOS WHERE VIDEO_ID = :videoID";
                command.Parameters.Add(":videoID", OracleDbType.Varchar2).Value = videoID;

                int rowsAffected = command.ExecuteNonQuery();

                _connection.Close();

                if (rowsAffected > 0)
                {
                    return Ok("Video record deleted successfully");
                }
                else
                {
                    return BadRequest("No matching video record found to delete");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error while deleting video record: {ex.Message}");
            return StatusCode(500, $"Error while deleting video record: {ex.Message}");
        }
        finally
        {
            _connection.Close();
        }
    }


}