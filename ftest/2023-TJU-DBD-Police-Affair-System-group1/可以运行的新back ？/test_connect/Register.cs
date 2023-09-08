using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.Infrastructure;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Data;
using System.Numerics;
using web.DTO_group2;



namespace Register
{
    [ApiController]
    
    public class Register : ControllerBase
    {
        public struct RequestData
        {
            public string ID_number { get; set; }
            public string birthday { get; set; }
            public string email { get; set; }
            public string gender { get; set; }
            public string nation { get; set; }
            public string phone_number { get; set; }
            public string police_name { get; set; }
            public string police_number { get; set; }
            public string position { get; set; }
            public string status { get; set; }
        }
        public struct LegalJudgeMessage
        {
            public string queryType { get; set; }
            public string queryContent { get; set; }
        }
        private readonly OracleConnection _connection;
        public Register(OracleConnection connection)
        {
            _connection = connection;
        }
        [HttpPut]
        [Route("api/Register")]
        public ActionResult<string> InsertRegister(RequestData requestData)
        {
           
            string result = "success";
            string pwd = requestData.police_number;
            string query = "insert into policemen " +
                "values(:police_number," +
                ":police_name," +
                ":ID_number," +
                ":birthday," +
                ":gender," +
                ":nation," +
                ":phone_number," +
                ":email," +
                ":status," +
                ":position," +
                ":pwd)";

            try
            {
                _connection.Open();
                // 创建Oracle命令对象
                OracleCommand command = new OracleCommand(query, _connection);
                command.Parameters.Add(new OracleParameter("police_number", requestData.police_number));
                command.Parameters.Add(new OracleParameter("police_name", requestData.police_name));
                command.Parameters.Add(new OracleParameter("ID_number", requestData.ID_number));
                command.Parameters.Add(new OracleParameter("birthday", requestData.birthday));
                command.Parameters.Add(new OracleParameter("gender", requestData.gender));
                command.Parameters.Add(new OracleParameter("nation", requestData.nation));
                command.Parameters.Add(new OracleParameter("phone_number", requestData.phone_number));
                command.Parameters.Add(new OracleParameter("email", requestData.email));
                command.Parameters.Add(new OracleParameter("status", requestData.status));
                command.Parameters.Add(new OracleParameter("position", requestData.position));
                command.Parameters.Add(new OracleParameter("pwd", pwd));
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                result = "fail";
                return Ok(result);
            }
            finally
            {
                _connection.Close();
            }
            return Ok(result);

        }

        [HttpPost]
        [Route("api/Register")]
        public ActionResult<string> SearchLegalInfomation(LegalJudgeMessage legalJudgeMessage)
        {
            string sql = "";
            if (legalJudgeMessage.queryType == "police_number")
                sql = "select police_number " +
                    "from policemen " +
                    "where police_number=:temp";
            else if (legalJudgeMessage.queryType == "ID_number")
                sql = "select ID_number " +
                    "from policemen " +
                    "where ID_number=:temp";

            try
            {
                OracleCommand command = new OracleCommand(sql, _connection);
                command.Parameters.Add(new OracleParameter("temp", legalJudgeMessage.queryContent));
                _connection.Open();
                OracleDataReader reader = command.ExecuteReader();
                if (reader.HasRows == false)
                {
                    return Ok("ok");
                }
                else
                {
                    return Ok("fail");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Ok("not");
            }
            finally
            {
                _connection.Close();
            }

        }
    }
}

