using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.Infrastructure;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Data;
using System.Numerics;

public class CaseRequestData
{
    public string inputTextCase { get; set; }
}

public class PoliceRequestData
{
    public string inputTextPolice { get; set; }
}

public class ModifyRequestData
{
    public string inputCase { get; set; }
}

[Route("api/queryCase")]
[ApiController]
public class CaseController : ControllerBase
{
    private readonly OracleConnection _connection;
    public CaseController(OracleConnection connection)
    {
        _connection = connection;
    }

    [HttpPost]
    public ActionResult<IEnumerable<string>> CaseQuery(CaseRequestData requestData)
    {
        string input = requestData.inputTextCase;
        string sql = "select case_ID,case_type,status,ranking " +
            "from cases " +
            "where case_ID='" + input + "'";
        string[] tmp = new string[4];
        try
        {
            _connection.Open();
            OracleCommand command = new OracleCommand(sql, _connection);
            OracleDataReader reader = command.ExecuteReader();
            if (reader.HasRows==false)
            {
                return Ok(tmp);
            }
            else
            {
                while (reader.Read())
                {
                    tmp[0] = reader.GetString("case_ID");
                    tmp[1] = reader.GetString("case_type");
                    tmp[2] = reader.GetString("status");
                    tmp[3] = reader.GetString("ranking");
                }
            }
        }
        catch(Exception ex)
        {
            return StatusCode(500, $"查询数据时发生错误: {ex.Message}");
        }
        finally
        {
            _connection.Close();   
        }
        return Ok(tmp);
    }
}

[Route("api/queryPolice")]
[ApiController]
public class PoliceController : ControllerBase
{
    private readonly OracleConnection _connection;
    public PoliceController(OracleConnection connection)
    {
        _connection = connection;
    }

    [HttpPost]
    public ActionResult<IEnumerable<string>> PoliceQuery(PoliceRequestData requestData)
    {
        string input = requestData.inputTextPolice;
        string sql = "select police_number,police_name,phone_number,status,position " +
            "from policemen " +
            "where police_number='" + input + "'";
        string[] tmp = new string[5];
        try
        {
            _connection.Open();
            OracleCommand command = new OracleCommand(sql, _connection);
            OracleDataReader reader = command.ExecuteReader();
            if (reader.HasRows==false)
            {
                return Ok(tmp);
            }
            else
            {
                while (reader.Read())
                {
                    tmp[0] = reader.GetString("police_number");
                    tmp[1] = reader.GetString("police_name");
                    tmp[2] = reader.GetString("phone_number");
                    tmp[3] = reader.GetString("status");
                    tmp[4] = reader.GetString("position");
                }
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"查询数据时发生错误: {ex.Message}");
        }
        finally
        {
            _connection.Close();
        }
        return Ok(tmp);
    }
}

[Route("api/modifyCaseStatus")]
[ApiController]
public class ModifyController : ControllerBase
{
    private readonly OracleConnection _connection;
    public ModifyController(OracleConnection connection)
    {
        _connection = connection;
    }

    [HttpPut]
    public ActionResult<IEnumerable<string>> StatusModify(ModifyRequestData requestData)
    {
        string input = requestData.inputCase;
        string sql = "update cases " +
            "set status='调查' " +
            "where case_ID='" + input + "'";
        try
        {
            _connection.Open();
            OracleCommand command = new OracleCommand(sql, _connection);
            command.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"查询数据时发生错误: {ex.Message}");
        }
        finally
        {
            _connection.Close();
        }
        return Ok("");
    }
}