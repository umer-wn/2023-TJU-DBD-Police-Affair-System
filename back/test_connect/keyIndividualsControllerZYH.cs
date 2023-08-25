using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Text.Json;

[ApiController]
[Route("api/keyIndividualsStatistics/repeatOffenderNameStatistics")]
public class repeatOffenderStatisticsControllerZYH : ControllerBase
{
    [HttpGet]
    public IActionResult HandleEndpoint()
    {
        keyIndividualsZYH ki = new keyIndividualsZYH();
        ki.getRepeatOffenderNameStatistics();
        return Ok(ki.repeatOffendersName);
    }
}

[ApiController]
[Route("api/keyIndividualsStatistics/repeatOffenderInfoStatistics")]
public class repeatOffenderInfoStatisticsControllerZYH : ControllerBase
{
    [HttpGet]
    public IActionResult HandleEndpoint()
    {
        keyIndividualsZYH ki = new keyIndividualsZYH();
        ki.getRepeatOffenderInfoStatistics();
        return Ok(ki.repeatOffendersInfo);
    }
}
