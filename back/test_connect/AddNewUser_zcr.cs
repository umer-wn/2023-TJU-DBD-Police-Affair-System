using FamilyController_zcr;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.Infrastructure;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Data;
using System.Numerics;
using web.DTO_group2;

namespace AddNewUser_zcr
{
    public struct SigninInfo
    {
        public string police_number { get; set; }
        public string police_name { get; set; }
        public string ID_number { get; set; }
        public string birthday { get; set; }
        public string gender { get; set; }
        public string nation { get; set; }
        public string phone_number { get; set; }
        public string email { get; set; }
        public string status { get; set; }
        public string position { get; set; }
        public SigninInfo()
        {
            police_number = "";
            police_name = "";
            ID_number = "";
            birthday = "";
            gender = "";
            nation = "";
            phone_number = "";
            email = "";
            status = "";
            position = "";
        }
    }
    public struct MyRequestData
    {
        public SigninInfo signinInfo;
    }
    public struct LegalJudgeMessage
    {
        public string type { get; set; }
        public string message { get; set; }
    }
    [Route("api/addNewUser")]
    [ApiController]
    public class AddNewUser_zcr : ControllerBase
    {
        private readonly OracleConnection _connection;
        public AddNewUser_zcr(OracleConnection connection)
        {
            _connection = connection;
        }
        [HttpPut]
        public ActionResult<string> HandleInsert(MyRequestData requestData)
        {
            SigninInfo info = requestData.signinInfo; // 从请求的JSON数据中获取                                
            string result = "success";
            string pwd = info.police_number;
            string query = "insert into policemen " +
                "values(:_police_number," +
                ":_police_name," +
                ":_ID_number," +
                ":_birthday," +
                ":_gender," +
                ":_nation," +
                ":_phone_number," +
                ":_email," +
                ":_status," +
                ":_position," +
                ":_pwd)";

            try
            {
                _connection.Open();
                // 创建Oracle命令对象
                OracleCommand command = new OracleCommand(query, _connection);
                command.Parameters.Add(new OracleParameter("_police_number", info.police_number));
                command.Parameters.Add(new OracleParameter("_police_name", info.police_name));
                command.Parameters.Add(new OracleParameter("_ID_number", info.ID_number));
                command.Parameters.Add(new OracleParameter("_birthday", info.birthday));
                command.Parameters.Add(new OracleParameter("_gender", info.birthday));
                command.Parameters.Add(new OracleParameter("_nation", info.nation));
                command.Parameters.Add(new OracleParameter("_phone_number", info.phone_number));
                command.Parameters.Add(new OracleParameter("_email", info.email));
                command.Parameters.Add(new OracleParameter("_status", info.status));
                command.Parameters.Add(new OracleParameter("_position", info.position));
                command.Parameters.Add(new OracleParameter("_pwd", pwd));
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

        [HttpGet]
        public ActionResult<string> SearchLegalInfomation(LegalJudgeMessage legalJudgeMessage)
        {
            string sql = "select :_type from policemen where :_type = :_message";
            try
            {
                OracleCommand command = new OracleCommand(sql, _connection);
                command.Parameters.Add(new OracleParameter("_type", legalJudgeMessage.type));
                command.Parameters.Add(new OracleParameter("_message", legalJudgeMessage.message));
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
