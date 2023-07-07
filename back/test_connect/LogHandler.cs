namespace LogHandler
{
    public class LogHandler
    {
        public bool writeLog(string content)
        {
            try
            {
                string path = ".\\myLog.log";
                FileStream fs = new FileStream(path, FileMode.Append);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine(content);
                sw.Close();
                fs.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
