using Oracle.ManagedDataAccess.Client;

// 赵毅辉编写使用
// caseStatisticsController.cs使用了本类
// 用于统计各项犯罪数据
public class CaseStatisticsZYH
{
    // 立案数
    public int numFiling { get; set; }
    // 调查数
    public int numInvestigating { get; set; }
    // 结案数
    public int numClose { get; set; }
    // 强奸案数
    public int numRape { get; set; }
    // 抢劫案数
    public int numRobbery { get; set; }
    // 故意伤害案数
    public int numInjury { get; set; }
    // 盗窃案数
    public int numTheft { get; set; }
    // 诈骗案数
    public int numFraud { get; set; }
    // 谋杀案数
    public int numMurder { get; set; }
    // 案件记录的最小年份
    public int minYear { get; set; }
    // 案件记录的最大年份
    public int maxYear { get; set; }
    // 每年每月案件数量
    public Dictionary<int, Dictionary<int, int>> numYearMonth { get; set; }
    // 存储城市名和对应次数的字典
    public Dictionary<string, int> cityCount { get; set; }
    // 每年每月案件数量
    public Dictionary<int, Dictionary<int, int>> numCityYearMonth { get; set; }

    /* 下面是方法 */
    // 构造函数
    public CaseStatisticsZYH()
    {
        numYearMonth = new Dictionary<int, Dictionary<int, int>>();
        cityCount = new Dictionary<string, int>();
        numCityYearMonth = new Dictionary<int, Dictionary<int, int>>();
    }
    public void getStatusStatistics()
    {
        try
        {
            string connectionString = "User ID=C##police;Password=police;Data Source=(DESCRIPTION = (ADDRESS_LIST= (ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT =1521))) (CONNECT_DATA = (SERVICE_NAME = orcl)))";
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                connection.Open();

                using (OracleCommand command = new OracleCommand("SELECT COUNT(*) FROM CASES WHERE STATUS = '立案'", connection))
                {
                    numFiling = Convert.ToInt32(command.ExecuteScalar());
                }

                using (OracleCommand command = new OracleCommand("SELECT COUNT(*) FROM CASES WHERE STATUS = '调查'", connection))
                {
                    numInvestigating = Convert.ToInt32(command.ExecuteScalar());
                }

                using (OracleCommand command = new OracleCommand("SELECT COUNT(*) FROM CASES WHERE STATUS = '结案'", connection))
                {
                    numClose = Convert.ToInt32(command.ExecuteScalar());
                }

                connection.Close();
            }
        }
        catch (Exception ex)
        {
            // 处理异常
            Console.WriteLine("CaseStatisticsZYH类的getStatusStatistics函数发生异常：" + ex.Message);
            throw;
        }
    }
    public void getTypeStatistics()
    {
        try
        {
            string connectionString = "User ID=C##police;Password=police;Data Source=(DESCRIPTION = (ADDRESS_LIST= (ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT =1521))) (CONNECT_DATA = (SERVICE_NAME = orcl)))";
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                connection.Open();

                using (OracleCommand command = new OracleCommand("SELECT COUNT(*) FROM CASES WHERE CASE_TYPE = '强奸'", connection))
                {
                    numRape = Convert.ToInt32(command.ExecuteScalar());
                }

                using (OracleCommand command = new OracleCommand("SELECT COUNT(*) FROM CASES WHERE CASE_TYPE = '抢劫'", connection))
                {
                    numRobbery = Convert.ToInt32(command.ExecuteScalar());
                }

                using (OracleCommand command = new OracleCommand("SELECT COUNT(*) FROM CASES WHERE CASE_TYPE = '故意伤害'", connection))
                {
                    numInjury = Convert.ToInt32(command.ExecuteScalar());
                }

                using (OracleCommand command = new OracleCommand("SELECT COUNT(*) FROM CASES WHERE CASE_TYPE = '盗窃'", connection))
                {
                    numTheft = Convert.ToInt32(command.ExecuteScalar());
                }

                using (OracleCommand command = new OracleCommand("SELECT COUNT(*) FROM CASES WHERE CASE_TYPE = '诈骗'", connection))
                {
                    numFraud = Convert.ToInt32(command.ExecuteScalar());
                }

                using (OracleCommand command = new OracleCommand("SELECT COUNT(*) FROM CASES WHERE CASE_TYPE = '谋杀'", connection))
                {
                    numMurder = Convert.ToInt32(command.ExecuteScalar());
                }

                connection.Close();
            }
        }
        catch (Exception ex)
        {
            // 处理异常
            Console.WriteLine("CaseStatisticsZYH类的getTypeStatistics函数发生异常：" + ex.Message);
            throw;
        }
    }
    public void getYearRange()
    {
        try
        {
            string connectionString = "User ID=C##police;Password=police;Data Source=(DESCRIPTION = (ADDRESS_LIST= (ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT =1521))) (CONNECT_DATA = (SERVICE_NAME = orcl)))";
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                connection.Open();

                string sqlQuery = "SELECT MIN(EXTRACT(YEAR FROM REGISTER_TIME)) AS min_year, MAX(EXTRACT(YEAR FROM REGISTER_TIME)) AS max_year FROM CASES";
                using (OracleCommand command = new OracleCommand(sqlQuery, connection))
                {
                    using (OracleDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            minYear = Convert.ToInt32(reader["min_year"]);
                            maxYear = Convert.ToInt32(reader["max_year"]);
                        }
                    }
                }

                connection.Close();
            }
        }
        catch (Exception ex)
        {
            // 处理异常
            Console.WriteLine("CaseStatisticsZYH类的getYearRange函数发生异常：" + ex.Message);
            throw;
        }
    }
    public void getYearMonthStatistics()
    {
        try
        {
            string connectionString = "User ID=C##police;Password=police;Data Source=(DESCRIPTION = (ADDRESS_LIST= (ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT =1521))) (CONNECT_DATA = (SERVICE_NAME = orcl)))";
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                getYearRange();

                connection.Open();

                for (int year = minYear; year <= maxYear; year++)
                {
                    Dictionary<int, int> numMonth = new Dictionary<int, int>();

                    for (int month = 1; month <= 12; month++)
                    {
                        string sql = $"SELECT COUNT(*) FROM CASES WHERE EXTRACT(YEAR FROM REGISTER_TIME) = {year} AND EXTRACT(MONTH FROM REGISTER_TIME) = {month}";
                        using (OracleCommand command = new OracleCommand(sql, connection))
                        {
                            int count = Convert.ToInt32(command.ExecuteScalar());
                            numMonth.Add(month, count);
                        }
                    }

                    numYearMonth.Add(year, numMonth);
                }

                connection.Close();
            }
        }
        catch (Exception ex)
        {
            // 处理异常
            Console.WriteLine("CaseStatisticsZYH类的getYearMonthStatistics函数发生异常：" + ex.Message);
            throw;
        }
    }
    public void getCityStatistics()
    {
        try
        {
            string connectionString = "User ID=C##police;Password=police;Data Source=(DESCRIPTION = (ADDRESS_LIST= (ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT =1521))) (CONNECT_DATA = (SERVICE_NAME = orcl)))";
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                connection.Open();

                using (OracleCommand command = new OracleCommand("SELECT SUBSTR(ADDRESS, 1, INSTR(ADDRESS, '市')-1) AS CITY, COUNT(*) AS ROW_COUNT FROM CASES GROUP BY SUBSTR(ADDRESS, 1, INSTR(ADDRESS, '市')-1)", connection))
                {
                    using (OracleDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string city = reader["CITY"].ToString();
                            int count = Convert.ToInt32(reader["ROW_COUNT"]);

                            cityCount.Add(city, count);
                        }
                    }
                }

                connection.Close();
            }
        }
        catch (Exception ex)
        {
            // 处理异常
            Console.WriteLine("CaseStatisticsZYH类的getCityStatistics函数发生异常：" + ex.Message);
            throw;
        }
    }
    public string[] getCityName()
    {
        try
        {
            string connectionString = "User ID=C##police;Password=police;Data Source=(DESCRIPTION = (ADDRESS_LIST= (ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT =1521))) (CONNECT_DATA = (SERVICE_NAME = orcl)))";
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                connection.Open();

                using (OracleCommand command = new OracleCommand("SELECT DISTINCT SUBSTR(ADDRESS, 1, INSTR(ADDRESS, '市')-1) AS CITY FROM CASES", connection))
                {
                    using (OracleDataReader reader = command.ExecuteReader())
                    {
                        List<string> cities = new List<string>();

                        while (reader.Read())
                        {
                            string city = reader["CITY"].ToString();
                            cities.Add(city);
                        }

                        return cities.ToArray();
                    }
                }

                connection.Close();
            }
        }
        catch (Exception ex)
        {
            // 处理异常
            Console.WriteLine("AddressStatistics类的getCityName函数发生异常：" + ex.Message);
            throw;
        }
    }
    public void getCityYearMonthStatistics(string cityName)
    {
        try
        {
            string connectionString = "User ID=C##police;Password=police;Data Source=(DESCRIPTION = (ADDRESS_LIST= (ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT =1521))) (CONNECT_DATA = (SERVICE_NAME = orcl)))";
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                getYearRange();

                connection.Open();

                for (int year = minYear; year <= maxYear; year++)
                {
                    Dictionary<int, int> numMonth = new Dictionary<int, int>();

                    for (int month = 1; month <= 12; month++)
                    {
                        string sql = $"SELECT COUNT(*) FROM CASES WHERE EXTRACT(YEAR FROM REGISTER_TIME) = {year} AND EXTRACT(MONTH FROM REGISTER_TIME) = {month} AND ADDRESS LIKE '%{cityName}%'";
                        using (OracleCommand command = new OracleCommand(sql, connection))
                        {
                            int count = Convert.ToInt32(command.ExecuteScalar());
                            numMonth.Add(month, count);
                        }
                    }

                    numCityYearMonth.Add(year, numMonth);
                }

                connection.Close();
            }
        }
        catch (Exception ex)
        {
            // 处理异常
            Console.WriteLine("CaseStatisticsZYH类的getCityYearMonthStatistics函数发生异常：" + ex.Message);
            throw;
        }
    }
}

// 赵毅辉编写使用
// caseControllerZYHZBW.cs使用了本类
// 用于储存返回给前端的CASES表的查询数据
public class caseInfoZYH
{
    public string caseID { get; set; }
    public string caseType { get; set; }
    public string status { get; set; }
    public DateTime registerTime { get; set; }
    public string address { get; set; }
    public string ranking { get; set; }
}

// 赵毅辉编写使用
// caseControllerZYHZBW.cs使用了本类
// 用于储存前端传来的数据
public class inputCaseInfoZYH
{
    public string caseID { get; set; }
    public string caseType { get; set; }
    public string status { get; set; }
    public string address { get; set; }
    public string ranking { get; set; }
}


// 赵毅辉编写使用
// citizenControllerZYHZBW.cs使用了本类
// 用于储存返回给前端的CITIZEN表的查询数据
public class citizenInfoZYH
{
    public string IDNum { get; set; }
    public string citizenName { get; set; }
    public string gender { get; set; }
    public string fatherID { get; set; }
    public string motherID { get; set; }
}

// 赵毅辉编写使用
// citizenControllerZYHZBW.cs使用了本类
// 用于储存前端传来的数据
public class inputCitizenInfoZYH
{
    public string IDNum { get; set; }
    public string citizenName { get; set; }
    public string gender { get; set; }
}

// 赵毅辉编写使用
// citizenInCaseControllerZYHZBW.cs使用了本类
// 用于储存返回给前端的CITIZEN表、CASES表的查询数据
public class citizenInCaseInfoZYH
{
    public string caseID { get; set; }
    public string caseType { get; set; }
    public string status { get; set; }
    public DateTime registerTime { get; set; }
    public string address { get; set; }
    public string ranking { get; set; }
    public string IDNum { get; set; }
    public string relatedType { get; set; }
    public string citizenName { get; set; }
    public string gender { get; set; }
}

// 赵毅辉编写使用
// citizenInCaseControllerZYHZBW.cs使用了本类
// 用于储存前端传来的数据
public class inputCitizenInCaseInfoZYH
{
    public string caseID { get; set; }
    public string caseType { get; set; }
    public string status { get; set; }
    public string address { get; set; }
    public string ranking { get; set; }
    public string IDNum { get; set; }
    public string relatedType { get; set; }
}

// 赵毅辉编写使用
// PolicemenControllerZYH.cs使用了本类
// 用于储存返回给前端的POLICE表的查询数据
public class PolicemenInfoZYH
{
    public string policemenNumber { get; set; }
    public string policemenName { get; set; }
    public string IDNumber { get; set; }
    public string birthday { get; set; }
    public string gender { get; set; }
    public string nation { get; set; }
    public string phoneNumber { get; set; }
    public string policemenStatus { get; set; }
    public string policemenPosition { get; set; }
    public string salary { get; set; }
}

// 赵毅辉编写使用
// PolicemenControllerZYH.cs使用了本类
// 用于储存前端传来的数据
public class inputPolicemenInfoZYH
{
    public string policemenNumber { get; set; }
    public string policemenName { get; set; }
    public string policemenStatus { get; set; }
    public string policemenPosition { get; set; }
}

// 赵毅辉编写使用
// PolicemenControllerZYHZBW.cs使用了本类
// 用于储存前端传来的数据 & 返回给前端的STATION表的查询数据
public class StationInfoZYH
{
    public string stationID { get; set; }
    public string stationName { get; set; }
    public string city { get; set; }
    public string address { get; set; }
    public int? budget { get; set; }
}

// 赵毅辉编写使用
// videoControllerZYHZBW.cs使用了本类
// 用于储存返回给前端的VIDEO表的查询数据
public class videoInfoZYH
{
    public string videoID { get; set; }
    public string videoType { get; set; }
    public DateTime recordTime { get; set; }
    public DateTime uploadTime { get; set; }
    public string principleID { get; set; }
}

// 赵毅辉编写使用
// videoControllerZYHZBW.cs使用了本类
// 用于储存前端传来的数据
public class inputVideoInfoZYH
{
    public string videoID { get; set; }
    public string videoType { get; set; }
    public string principleID { get; set; }
}