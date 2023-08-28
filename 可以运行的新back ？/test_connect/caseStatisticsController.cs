using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Data.Common;
using System.Reflection.PortableExecutable;
using System.Text;


[ApiController]
[Route("api/caseStatistics/caseStatusStatistics")]
public class caseStatusStatisticsControllerZYH : ControllerBase
{
    private OracleConnection _connection;

    public caseStatusStatisticsControllerZYH(OracleConnection connection)
    {
        _connection = connection;
    }

    [HttpPost]
    public IActionResult HandleEndpoint(inputCitizenInCaseInfoZYH inputInfo) //接收前端的数据
    {
        List<citizenInCaseInfoZYH> cases = new List<citizenInCaseInfoZYH>();

        
        _connection.Open();

        _connection.Close();

        return Ok(1);
    }
}