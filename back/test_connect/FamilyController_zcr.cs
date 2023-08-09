using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.Infrastructure;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Data;
using System.Numerics;
using web.DTO_group2;


public struct Info
{
    public string name { get; set; }
    public string gender { get; set; }
    public string ID { get; set; }
    public List<string> crimeType { get; set; }
    public Info()
    {
        name = "";
        gender = "";
        ID = "";
        crimeType = new List<string>();
    }
}
public struct result
{
    public Info centerMan { get; set; }
    public List<Info> relatePeople { get; set; }
    public List<string> relationship { get; set; }
    public result()
    {
        centerMan=new Info();
        relatePeople=new List<Info>();
        relationship=new List<string>();
    }
}
[Route("api/queryFamily")]
[ApiController]
public class FamilyController_zcr : ControllerBase
{
    private readonly OracleConnection _connection;
    public FamilyController_zcr(OracleConnection connection)
    {
        _connection = connection;
    }
    private bool notExist(string type,List<string> types)
    {
        foreach (string i in types)
        {
            if (type == i)
                return false;
        }
        return true;
    }
    private void searchParent(ref result output,string targetID)
    {
        string sql = "select ID_num,citizen_name,gender,case_type " +
            "from citizen natural join related natural join cases " +
            "where ID_num in (" +
            "select father_ID " +
            "from citizen " +
            "where ID_num=" + targetID + ") " +
            "order by case_type";
        try
        {
            OracleCommand command = new OracleCommand(sql, _connection);
            OracleDataReader reader = command.ExecuteReader();
            Info tmp = new Info();
            while (reader.Read())
            {
                tmp.ID = reader.GetString("ID_num");
                tmp.name = reader.GetString("citizen_name");
                tmp.gender = reader.GetString("gender");
                string readyType = reader.GetString("case_type");
                if (notExist(readyType,tmp.crimeType))
                {
                    tmp.crimeType.Add(readyType);
                }

            }
            output.relatePeople.Add(tmp);
            output.relationship.Add("父亲");
        }
        catch (Exception ex)
        {
            return;
        }
        sql ="select ID_num,citizen_name,gender,case_type " +
            "from citizen natural join related natural join cases " +
            "where ID_num in (" +
            "select mother_ID " +
            "from citizen " +
            "where ID_num=" + targetID + ") " +
            "order by case_type";
        try
        {
            OracleCommand command = new OracleCommand(sql, _connection);
            OracleDataReader reader = command.ExecuteReader();
            Info tmp = new Info();
            while (reader.Read())
            {
                tmp.ID = reader.GetString("ID_num");
                tmp.name = reader.GetString("citizen_name");
                tmp.gender = reader.GetString("gender");
                string readyType = reader.GetString("case_type");
                if (notExist(readyType, tmp.crimeType))
                {
                    tmp.crimeType.Add(readyType);
                }

            }
            output.relatePeople.Add(tmp);
            output.relationship.Add("母亲");
        }
        catch (Exception ex)
        {
            return;
        }
    }
    private void searchChild(ref result output, string targetID)
    {
        string sql = "select ID_num,citizen_name,gender,case_type " +
            "from citizen natural join related natural join cases " +
            "where mother_ID=" + targetID + " or " + "father_ID=" + targetID + " " +
            "order by ID_num,case_type";
        
        try
        {
            OracleCommand command = new OracleCommand(sql, _connection);
            OracleDataReader reader = command.ExecuteReader();
            Info[] tmp = new Info[2]; // 0留存行，1读取行
            if (reader.Read())
            {
                tmp[0].ID = reader.GetString("ID_num");
                tmp[0].name = reader.GetString("citizen_name");
                tmp[0].gender = reader.GetString("gender");
                tmp[0].crimeType.Add(reader.GetString("case_type"));
            }
            while (reader.Read())
            {
                tmp[1].ID = reader.GetString("ID_num");
                tmp[1].name = reader.GetString("citizen_name");
                tmp[1].gender = reader.GetString("gender");
                string readyType = reader.GetString("case_type");
                if (tmp[0].ID == tmp[1].ID)
                {
                    // 还是同一个人
                    if (notExist(readyType, tmp[0].crimeType))
                    {
                        // 出现新犯罪类型，添加
                        tmp[0].crimeType.Add(readyType);
                    }
                }
                else
                {
                    // 换人
                    output.relatePeople.Add(tmp[0]);
                    output.relationship.Add(tmp[0].gender == "F" ? "女儿" : "儿子");
                    // 先存储信息到结果，再挪信息
                    tmp[0] = tmp[1];
                    tmp[0].crimeType.Add(readyType);
                }
            }
            // 全部读完以后，最后一个人在[0]，未写入
            output.relatePeople.Add(tmp[0]);
            output.relationship.Add(tmp[0].gender == "F" ? "女儿" : "儿子");
        }
        catch ( Exception ex )
        {
            return;
        }
    }

    [HttpPost]
    public ActionResult<result> HandleFamily(MyRequestData requestData)
    {
        string inputText = requestData.InputText; // 从请求的JSON数据中获取输入的字符串                                
        result queryResult = new result();

        string query = "select ID_num,citizen_name,gender,case_type "+
            "from citizen natural join related natural join cases " +
            "where ID_num=:temp and related_type='犯人' "+
            "order by case_type";

        try
        {
            _connection.Open();
            // 创建Oracle命令对象
            OracleCommand command = new OracleCommand(query, _connection);
            command.Parameters.Add(new OracleParameter("temp", inputText));
            OracleDataReader reader = command.ExecuteReader();
            if (reader.HasRows==false)
            {
                return Ok(new result());
            }
            else
            {
                Info tmp=new Info();
                while (reader.Read())
                { // 必然只有一个人
                    tmp.name = reader.GetString("citizen_name");
                    tmp.ID = reader.GetString("ID_num");
                    tmp.gender = reader.GetString("gender");
                    string readyType = reader.GetString("case_type");
                    if (notExist(readyType,tmp.crimeType))
                    {
                        // 类型不重复
                        tmp.crimeType.Add(readyType);
                    }
                }
                queryResult.centerMan = tmp;
                searchParent(ref queryResult, queryResult.centerMan.ID);
                searchChild(ref queryResult, queryResult.centerMan.ID);
            }
        }
        catch(Exception ex)
        {
            return Ok(new result());
        }
        finally
        {
            _connection.Close();   
        }
        return Ok(queryResult);

    }
}

