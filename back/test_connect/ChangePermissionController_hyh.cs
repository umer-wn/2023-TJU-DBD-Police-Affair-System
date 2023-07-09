using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Data.Common;
using web.DTO_group2;



namespace WebApplication1
{
    [ApiController]
    [Route("api/permit")]
    public class ChangePermissionController_hyh:ControllerBase
    {
        private  OracleConnection _connection;

        
        public ChangePermissionController_hyh(OracleConnection connection)
        {
            _connection = connection;
        }
        [HttpPost]
        public ActionResult<string> ChangePermission(permit P)//传进的参数分别为被修改人警号和修改人警号
        {
            int i = 0;
            try
            {
                _connection.Open();
                int h_level = 0;
                int s_level = 0;
                string sql = "SELECT * FROM police_account WHERE police_number = :temp";
                OracleCommand command = new OracleCommand(sql, _connection);
                command.Parameters.Add(new OracleParameter("temp", P.h_number));//搜索修改人权限等级

                using (OracleDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        h_level = reader.GetInt32(reader.GetOrdinal("AUTHORITY"));//获取修改人权限等级
                    }
                }
                OracleCommand command1 = new OracleCommand(sql, _connection);
                command1.Parameters.Add(new OracleParameter("temp", P.s_number));//搜索被修改人权限等级
                using (OracleDataReader reader1 = command1.ExecuteReader())
                {
                    while (reader1.Read())
                    {
                        s_level = reader1.GetInt32(reader1.GetOrdinal("AUTHORITY"));//获取被修改人权限等级
                    }
                }
                if (h_level < s_level || ((int.Parse(P.level) > h_level)))//判断修改人权限是否大于被修改人权限
                {
                    return BadRequest("权限不足");
                }
                else
                {
                    sql = "UPDATE police_account SET AUTHORITY = :temp WHERE police_number = :temp2";
                    command = new OracleCommand(sql, _connection);
                    command.Parameters.Add(new OracleParameter("temp", int.Parse(P.level)));//修改被修改人权限等级
                    command.Parameters.Add(new OracleParameter("temp2", P.s_number));//被修改人警号
                    i = command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                // 处理异常或记录错误日志
                return BadRequest("权限修改失败：" + ex.Message);
            }
            finally
            {
                _connection.Close();
            }

            if (i == 0)
            {
                return BadRequest("未找到警员信息");
            }

            return Ok("权限修改成功");
        }
    }
}
