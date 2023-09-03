using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Data.Common;
using System.Drawing;
using System.Runtime;
using System.Text.RegularExpressions;
using web.DTO_group2;


[ApiController]
[Route("/")]
public class PoliceEmployControllerLHM : ControllerBase
{
    private OracleConnection _connection;


    public PoliceEmployControllerLHM(OracleConnection connection)
    {
        _connection = connection;
    }
    public int CheckBudget([FromQuery] string? stationID, [FromQuery] int? salary)
    {
        int stationBudget = 0;
        try
        {
            _connection.Open();
            string sql = "SELECT BUDGET FROM POLICE_STATION WHERE STATION_ID = :id";
            OracleCommand command_budget = new OracleCommand(sql, _connection);

            command_budget.Parameters.Add(new OracleParameter("id", stationID));
           
            using (var reader = command_budget.ExecuteReader())
            {
                if (reader.Read())
                {
                    stationBudget = reader.GetInt32(0);
                }
            };
            // ��ѯ��ְ��Ա����
            sql = "SELECT SALARY FROM Employ WHERE STATION_ID = :id AND OUTTIME IS NULL";
            OracleCommand cmd_salary = new OracleCommand(sql, _connection);
            cmd_salary.Parameters.Add(new OracleParameter("id", stationID));
            using (var reader = cmd_salary.ExecuteReader())
            {

                string temp ="";
                while (reader.Read())
                {
                    temp=(string)reader[0];
                    stationBudget -= Convert.ToInt32(temp);
                }

            }

        }
        catch (Exception ex)
        {
            Console.WriteLine("Error CheckBudget: " + ex.Message);
        }
        finally
        {
            _connection.Close();
        }
        return stationBudget-(int)salary;
    }

    public int CloseEmploy(string policemenID)
    {
        
        try
        {
            _connection.Open();
            string sql = "SELECT * FROM Employ WHERE POLICE_NUMBER = :id AND OUTTIME IS NULL";

            OracleCommand cmd = new OracleCommand(sql, _connection);
            cmd.Parameters.Add(new OracleParameter("id", policemenID));
            using (OracleDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    // ���ó�ְʱ��Ϊ��ǰʱ��
                    sql = "UPDATE Employ SET OUTTIME = :time WHERE POLICE_NUMBER = :id AND OUTTIME IS NULL";

                    OracleCommand updateCmd = new OracleCommand(sql, _connection);
                    updateCmd.Parameters.Add(new OracleParameter("time", DateTime.Now));
                    updateCmd.Parameters.Add(new OracleParameter("id", policemenID));

                    updateCmd.ExecuteNonQuery();
                }
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine("Error CloseEmploy: " + ex.Message);
        }
        finally
        {
            _connection.Close();
        }

        return 0;
    }

    public int insertEmploy(inputEmployInfo employInfo)
    {
        try
        {
            _connection.Open();

            string sql = "INSERT INTO Employ VALUES (:id, :station, :time, null, :salary)";

            OracleCommand cmd = new OracleCommand(sql, _connection);

            cmd.Parameters.Add(new OracleParameter("id", employInfo.selectPolicemenID));
            cmd.Parameters.Add(new OracleParameter("station", employInfo.selectStationID));
            cmd.Parameters.Add(new OracleParameter("time", DateTime.Now));
            cmd.Parameters.Add(new OracleParameter("salary", employInfo.salary));

            cmd.ExecuteNonQuery();

        }
        catch (Exception ex)
        {
            Console.WriteLine("Error insertEmploy: " + ex.Message);
        }
        finally
        {
            _connection.Close();
        }

        return 0;
    }
    [HttpPost("api/addEmploy")]
    public IActionResult AddEmploy(inputEmployInfo employInfo)
    {
        outEmployInfo result = new outEmployInfo();
        try
        {
            int budget = CheckBudget(employInfo.selectStationID, employInfo.salary);
            if (budget< 0) {
                budget = -budget;
                result.failReason =budget.ToString();
                return Ok(result);
            }
            else
                result.failReason = "success";
            result.EmployTime = DateTime.Now.ToString("yyyy-MM-dd");
            CloseEmploy(employInfo.selectPolicemenID);
            insertEmploy(employInfo);
            return Ok(result);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"��������ʱ��������:{ex.Message}");
            return StatusCode(499, $"��������ʱ��������: {ex.Message}");
        }
        finally
        {
            ;
        }
    }

}

