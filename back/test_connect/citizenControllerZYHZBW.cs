using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using System.Text;
using System.Collections.Generic;
using System.Data.Common;
using System.Reflection.PortableExecutable;


[ApiController]
[Route("/")]
public class citizenInfoControllerZYH : ControllerBase
{
    private OracleConnection _connection;

    public citizenInfoControllerZYH(OracleConnection connection)
    {
        _connection = connection;
    }

    [HttpPost("api/citizenInfo")]
    public IActionResult HandleEndpoint(inputCitizenInfoZYH inputInfo) //接收前端的数据
    {
        List<citizenInfoZYH> citizens = new List<citizenInfoZYH>();
        Console.WriteLine($"查询数据为:{inputInfo.IDNum}\t{inputInfo.citizenName}\t{inputInfo.gender}");
        try
        {
            _connection.Open();
            // 执行 SQL 查询
            using (var command = _connection.CreateCommand())
            {
                command.Connection = _connection;
                //先写SQL语句的主体
                command.CommandText = "SELECT * FROM CITIZEN WHERE 1 = 1";
                //下面的whereClause可以接到SQL语句的后面，实现SQL语句的动态变化
                StringBuilder whereClause = new StringBuilder();

                if (!string.IsNullOrEmpty(inputInfo.IDNum))
                {
                    //下面的SQL表示筛选出包含inputInfo.IDNum的结果
                    whereClause.Append(" AND ID_NUM LIKE '%' || :IDNum || '%'");
                    command.Parameters.Add(":IDNum", OracleDbType.Varchar2).Value = inputInfo.IDNum;
                }
                if (!string.IsNullOrEmpty(inputInfo.citizenName))
                {
                    whereClause.Append(" AND CITIZEN_NAME LIKE '%' || :citizenName || '%'");
                    command.Parameters.Add(":citizenName", OracleDbType.Varchar2).Value = inputInfo.citizenName;
                }
                if (inputInfo.gender != "全部")
                {
                    whereClause.Append(" AND GENDER LIKE '%' || :gender || '%'");
                    command.Parameters.Add(":gender", OracleDbType.Char).Value = inputInfo.gender;
                }

                //将后续SQL语句合并到主语句中
                if (whereClause.Length > 0)
                {
                    command.CommandText += whereClause.ToString();
                }

                //在控制台输出一下查询语句，方便检错
                Console.WriteLine($"查询数据SQL为:{command.CommandText}");
                //下面进行查询，查询结果放在reader里
                using (OracleDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        citizenInfoZYH citizen = new citizenInfoZYH
                        {
                            IDNum = reader.GetString(reader.GetOrdinal("ID_NUM")),
                            citizenName = reader.GetString(reader.GetOrdinal("CITIZEN_NAME")),
                            gender = reader.GetString(reader.GetOrdinal("GENDER")),
                            fatherID = reader.GetString(reader.GetOrdinal("FATHER_ID")),
                            motherID = reader.GetString(reader.GetOrdinal("MOTHER_ID"))
                        };
                        citizens.Add(citizen);
                    }

                    _connection.Close();
                    return Ok(citizens);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"查询数据时发生错误:{ex.Message}");
            return StatusCode(499, $"查询数据时发生错误: {ex.Message}");
        }
        finally
        {
            _connection.Close();
        }
    }
    [HttpPost("api/addCitizen")]
    public IActionResult AddCitizen([FromBody] citizenInfoZYH newCitizen)
    {
        try
        {
            // Validate the incoming data here (e.g., required fields, data formats).
            if (!IsValid(newCitizen))
            {
                return BadRequest("Invalid data. Please check the input.");
            }

            using (_connection)
            {
                _connection.Open();
                using (var transaction = _connection.BeginTransaction()) // Start a transaction
                {
                    // Check if the citizen already exists based on IDNum.
                    string checkExistenceSql = "SELECT COUNT(*) FROM CITIZEN WHERE ID_NUM = :IDNum";
                    using (var checkExistenceCommand = new OracleCommand(checkExistenceSql, _connection))
                    {
                        checkExistenceCommand.Parameters.Add(":IDNum", OracleDbType.Varchar2).Value = newCitizen.IDNum;
                        int existingCitizenCount = Convert.ToInt32(checkExistenceCommand.ExecuteScalar());

                        if (existingCitizenCount > 0)
                        {
                            return Conflict("A citizen with the same ID number already exists.");
                        }
                    }

                    // Check if the father exists.
                    if (!ParentExists(newCitizen.fatherID))
                    {
                        return BadRequest("The father does not exist in the database.");
                    }

                    // Check if the mother exists.
                    if (!ParentExists(newCitizen.motherID))
                    {
                        return BadRequest("The mother does not exist in the database.");
                    }

                    // If all checks pass, insert the new citizen data.
                    string insertSql = "INSERT INTO CITIZEN (ID_NUM, CITIZEN_NAME, GENDER, FATHER_ID, MOTHER_ID) " +
                                       "VALUES (:IDNum, :citizenName, :gender, :fatherID, :motherID)";
                    using (var insertCommand = new OracleCommand(insertSql, _connection))
                    {
                        insertCommand.Parameters.Add(":IDNum", OracleDbType.Varchar2).Value = newCitizen.IDNum;
                        insertCommand.Parameters.Add(":citizenName", OracleDbType.Varchar2).Value = newCitizen.citizenName;
                        insertCommand.Parameters.Add(":gender", OracleDbType.Varchar2).Value = newCitizen.gender;
                        insertCommand.Parameters.Add(":fatherID", OracleDbType.Varchar2).Value = newCitizen.fatherID;
                        insertCommand.Parameters.Add(":motherID", OracleDbType.Varchar2).Value = newCitizen.motherID;

                        int rowsAffected = insertCommand.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            // Commit the transaction if everything is successful.
                            transaction.Commit();

                            // Return the newly created citizen with its ID or any other relevant data.
                            return Created($"/api/citizenInfo/{newCitizen.IDNum}", newCitizen);
                        }
                        else
                        {
                            return BadRequest("Failed to add the citizen.");
                        }
                    }
                }
            }
        }
        catch (OracleException ex)
        {
            Console.WriteLine($"Error adding citizen to the database: {ex.Message}");
            // Log the exception using a logging framework.
            return StatusCode(500, $"An error occurred while adding the citizen: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error adding citizen: {ex.Message}");
            // Log the exception using a logging framework.
            return StatusCode(500, $"An error occurred while adding the citizen: {ex.Message}");
        }
    }

    private bool ParentExists(string parentID)
    {
        string checkParentExistenceSql = "SELECT COUNT(*) FROM CITIZEN WHERE ID_NUM = :ParentID";
        using (var checkParentExistenceCommand = new OracleCommand(checkParentExistenceSql, _connection))
        {
            checkParentExistenceCommand.Parameters.Add(":ParentID", OracleDbType.Varchar2).Value = parentID;
            int parentExistenceCount = Convert.ToInt32(checkParentExistenceCommand.ExecuteScalar());

            return parentExistenceCount > 0;
        }
    }

    private bool IsValid(citizenInfoZYH citizen)
    {
        // Implement your data validation logic here, e.g., check required fields and data formats.
        // Return true if the data is valid, false otherwise.
        return !string.IsNullOrEmpty(citizen.IDNum) &&
               !string.IsNullOrEmpty(citizen.citizenName) &&
               !string.IsNullOrEmpty(citizen.gender);
    }


    [HttpDelete("api/deleteCitizen")]
    public IActionResult DeleteCitizen(string idNum)
    {
        try
        {
            using (_connection)
            {
                _connection.Open();

                // Check if the citizen exists before attempting to delete.
                string checkExistenceSql = "SELECT COUNT(*) FROM CITIZEN WHERE ID_NUM = :IDNum";
                using (var checkExistenceCommand = new OracleCommand(checkExistenceSql, _connection))
                {
                    checkExistenceCommand.Parameters.Add(":IDNum", OracleDbType.Varchar2).Value = idNum;
                    int existingCitizenCount = Convert.ToInt32(checkExistenceCommand.ExecuteScalar());

                    if (existingCitizenCount == 0)
                    {
                        return NotFound("The specified citizen does not exist.");
                    }
                }

                // If the citizen exists, delete it.
                string deleteSql = "DELETE FROM CITIZEN WHERE ID_NUM = :IDNum";
                using (var deleteCommand = new OracleCommand(deleteSql, _connection))
                {
                    deleteCommand.Parameters.Add(":IDNum", OracleDbType.Varchar2).Value = idNum;

                    int rowsAffected = deleteCommand.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        return Ok("Citizen deleted successfully.");
                    }
                    else
                    {
                        return BadRequest("Failed to delete the citizen.");
                    }
                }
            }
        }
        catch (OracleException ex)
        {
            Console.WriteLine($"Error deleting citizen from the database: {ex.Message}");
            return StatusCode(500, $"An error occurred while deleting the citizen: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting citizen: {ex.Message}");
            return StatusCode(500, $"An error occurred while deleting the citizen: {ex.Message}");
        }
    }
    [HttpPut("api/updateCitizen/{idNum}")]
    public IActionResult UpdateCitizen(string idNum, [FromBody] citizenInfoZYH updatedCitizen)
    {
        try
        {
            using (_connection)
            {
                _connection.Open();
                using (var transaction = _connection.BeginTransaction()) // Start a transaction
                {
                    // Check if the citizen with the specified IDNum exists.
                    if (!CitizenExists(idNum))
                    {
                        return NotFound("The specified citizen does not exist.");
                    }

                    // Validate the incoming data here (e.g., required fields, data formats).
                    if (!IsValida(updatedCitizen))
                    {
                        return BadRequest("Invalid data. Please check the input.");
                    }

                    // Update the citizen data.
                    string updateSql = "UPDATE CITIZEN SET CITIZEN_NAME = :citizenName, GENDER = :gender, " +
                                       "FATHER_ID = :fatherID, MOTHER_ID = :motherID WHERE ID_NUM = :IDNum";
                    using (var updateCommand = new OracleCommand(updateSql, _connection))
                    {
                        updateCommand.Parameters.Add(":IDNum", OracleDbType.Varchar2).Value = idNum;
                        updateCommand.Parameters.Add(":citizenName", OracleDbType.Varchar2).Value = updatedCitizen.citizenName;
                        updateCommand.Parameters.Add(":gender", OracleDbType.Varchar2).Value = updatedCitizen.gender;
                        updateCommand.Parameters.Add(":fatherID", OracleDbType.Varchar2).Value = updatedCitizen.fatherID;
                        updateCommand.Parameters.Add(":motherID", OracleDbType.Varchar2).Value = updatedCitizen.motherID;

                        int rowsAffected = updateCommand.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            // Commit the transaction if the update is successful.
                            transaction.Commit();

                            // Return a success message or the updated citizen data.
                            return Ok("Citizen updated successfully.");
                        }
                        else
                        {
                            return BadRequest("Failed to update the citizen.");
                        }
                    }
                }
            }
        }
        catch (OracleException ex)
        {
            Console.WriteLine($"Error updating citizen in the database: {ex.Message}");
            // Log the exception using a logging framework.
            return StatusCode(500, $"An error occurred while updating the citizen: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating citizen: {ex.Message}");
            // Log the exception using a logging framework.
            return StatusCode(500, $"An error occurred while updating the citizen: {ex.Message}");
        }
    }

    private bool CitizenExists(string idNum)
    {
        string checkCitizenExistenceSql = "SELECT COUNT(*) FROM CITIZEN WHERE ID_NUM = :IDNum";
        using (var checkCitizenExistenceCommand = new OracleCommand(checkCitizenExistenceSql, _connection))
        {
            checkCitizenExistenceCommand.Parameters.Add(":IDNum", OracleDbType.Varchar2).Value = idNum;
            int citizenExistenceCount = Convert.ToInt32(checkCitizenExistenceCommand.ExecuteScalar());

            return citizenExistenceCount > 0;
        }
    }

    private bool IsValida(citizenInfoZYH citizen)
    {
        // Implement your data validation logic here, e.g., check required fields and data formats.
        // Return true if the data is valid, false otherwise.
        return !string.IsNullOrEmpty(citizen.IDNum) &&
               !string.IsNullOrEmpty(citizen.citizenName) &&
               !string.IsNullOrEmpty(citizen.gender);
    }


}