using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.Infrastructure;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Data;
using System.Numerics;

public class MyRequestData
{
    public string InputText { get; set; }
}
public struct result
{
    public int level;
    public string name;
    public string ID;
    public string gender;
}

[Route("api/queryFamily")]
[ApiController]
public class FamilyController : ControllerBase
{
    private readonly OracleConnection _connection;
    public FamilyController(OracleConnection connection)
    {
        _connection = connection;
    }
    /*
     若有重复内容，返回false
     */
    private bool checkRepeated(string ID_num,List<result> myresult)
    {
        foreach (result i in myresult)
        {
            if (i.ID == ID_num)
                return false;
        }
        return true;
    }
    /**
     父母递归树
     **/
    public void searchParent(ref List<result> output,int curLevel,string curID)
    {
        curLevel++;
        string query = "select ID_num,citizen_name,gender " +
            "from citizen natural join related " +
            "where related_type='犯人'" + " and " + "ID_num in (" +
            "select father_ID " +
            "from citizen " +
            "where ID_num=" + curID + " " +
            "union " +
            "select mother_ID " +
            "from citizen " +
            "where ID_num=" + curID + ");";
        using (OracleCommand command = new OracleCommand(query, _connection))
        {
            result temp;
            // 执行查询
            using (OracleDataReader reader = command.ExecuteReader())
            {
                // 处理查询结果
                while (reader.Read())
                {
                    // 从reader中获取查询结果的字段值
                    temp.level = curLevel;
                    temp.ID = reader.GetString("ID_num");
                    temp.name = reader.GetString("citizen_name");
                    temp.gender = reader.GetString("gender");
                    if (checkRepeated(temp.ID,output))
                    { // 无重复，加入结果集
                        output.Add(temp);
                        search(ref output, curLevel, curID);

                    }
                    // 否则跳过
                }
            }
        }
    }
    /**
     子孙递归树
     **/
    public void searchChild(ref List<result> output, int curLevel,string curID)
    {
        curLevel--;
        string query = "select ID_num,citizen_name,gender " +
            "from citizen natural join related " +
            "where related_type='犯人'" +
            "and (father_ID=" + curID + " or mother_ID=" + curID + ")";
        using (OracleCommand command = new OracleCommand(query, _connection))
        {
            result temp;
            // 执行查询
            using (OracleDataReader reader = command.ExecuteReader())
            {
                // 处理查询结果
                while (reader.Read())
                {
                    // 从reader中获取查询结果的字段值
                    temp.level = curLevel;
                    temp.ID = reader.GetString("ID_num");
                    temp.name = reader.GetString("citizen_name");
                    temp.gender = reader.GetString("gender");
                    if (checkRepeated(temp.ID, output))
                    { // 无重复，加入结果集
                        output.Add(temp);
                        search(ref output, curLevel, curID);
                    }
                    // 否则跳过
                }
            }
        }
    }
    /**
     根节点递归树，前序-左父母树
     ref return: output
     **/
    public void search(ref List<result> output, int curLevel, string curID)
    {
        searchParent(ref output, curLevel, curID); // 左父母树搜索
        searchChild(ref output, curLevel, curID); // 子孙树搜索
    }
    [HttpPost]
    public ActionResult<IEnumerable<result>> HandleFamily(MyRequestData requestData)
    {
        string inputText = requestData.InputText; // 从请求的JSON数据中获取输入的字符串                                
        List<result> queryResult = new List<result>();
        int curLevel = 0;
        string query = "select ID_num,citizen_name,gender " +
            "from citizen natural join related " +
            "where ID_num=" + inputText + " and related_type=" + "'犯人'" + ";";
        _connection.Open();
        // 创建Oracle命令对象
        using (OracleCommand command = new OracleCommand(query, _connection))
        {
            result temp;
            // 执行查询
            using (OracleDataReader reader = command.ExecuteReader())
            {
                // 处理查询结果
                while (reader.Read())
                {
                    // 从reader中获取查询结果的字段值
                    temp.level = curLevel;
                    temp.ID = reader.GetString("ID_num");
                    temp.name = reader.GetString("citizen_name");
                    temp.gender = reader.GetString("gender");
                    queryResult.Add(temp);
                }
            }
        }
        search(ref queryResult, curLevel, inputText); // 必然只能查到一个人，所以直接进递归
        _connection.Close();
        return Ok(queryResult);
    }
}

