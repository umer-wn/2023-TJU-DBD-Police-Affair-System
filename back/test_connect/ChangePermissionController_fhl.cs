using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Data.Common;
using System.Drawing;
using web.DTO_group2;



namespace WebApplication1
{
    [ApiController]
    [Route("api/permit")]
    public class ChangePermissionController_fhl:ControllerBase
    {
        private  OracleConnection _connection;

        
        public ChangePermissionController_fhl(OracleConnection connection)
        {
            _connection = connection;
        }
        [HttpPost]
        public ActionResult<string> ChangePermission(permit P)
        {
            int i = 0;
            try
            {
                _connection.Open();
                int s_level = 0;
                string sql = "SELECT * FROM police_account WHERE police_number = :temp";
                
                OracleCommand command1 = new OracleCommand(sql, _connection);
                command1.Parameters.Add(new OracleParameter("temp", P.s_number));//搜索被修改人权限等级
                using (OracleDataReader reader1 = command1.ExecuteReader())
                {
                    while (reader1.Read())
                    {
                        s_level = reader1.GetInt32(reader1.GetOrdinal("AUTHORITY"));//获取被修改人权限等级
                    }
                }
                P.F_level = s_level.ToString();
                sql = "INSERT INTO permission_manage(submit_ID, change_ID, F_level, L_level, status, reason) VALUES(:submitID, :changeID, :Flevel, :Llevel, :status, :reason)";
                //string sql1 = "INSERT INTO permission_manage VALUES('3406524','9726541','1', '4', '待处理', 'test1')";
                OracleCommand command = new OracleCommand(sql, _connection);
                command.Parameters.Add(new OracleParameter("submitID", P.h_number));//申请修改人警号
                command.Parameters.Add(new OracleParameter("changeID", P.s_number));//被修改人警号
                command.Parameters.Add(new OracleParameter("Flevel", s_level));//被修改人权限等级
                command.Parameters.Add(new OracleParameter("Llevel", P.L_level));//修改等级
                command.Parameters.Add(new OracleParameter("status", "待处理"));//修改状态
                command.Parameters.Add(new OracleParameter("reason", P.reason));//修改原因
                i = command.ExecuteNonQuery();
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
