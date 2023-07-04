using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;

[ApiController]
[Route("api/data")]
public class DataController : ControllerBase
{
    private readonly OracleConnection _connection;

    public DataController(OracleConnection connection)
    {
        _connection = connection;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Student>> GetStudents()
    {
        var students = new List<Student>();

        try
        {
            _connection.Open();
            string sql = "SELECT id, name, dept_name, tot_cred FROM student";
            OracleCommand command = new OracleCommand(sql, _connection);

            using (OracleDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    // 创建Student对象并设置属性值
                    var student = new Student
                    {
                        Id = reader.GetString(reader.GetOrdinal("id")),
                        Name = reader.GetString(reader.GetOrdinal("name")),
                        DeptName = reader.GetString(reader.GetOrdinal("dept_name")),
                        TotCred = reader.GetInt32(reader.GetOrdinal("tot_cred"))
                    };

                    students.Add(student);
                }
            }
        }
        catch (Exception ex)
        {
            // 处理异常
            return StatusCode(500, $"查询数据时发生错误: {ex.Message}");
        }
        finally
        {
            _connection.Close();
        }

        return Ok(students);
    }
}

public class Student
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string DeptName { get; set; }
    public int TotCred { get; set; }
}
