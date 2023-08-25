using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Net;
using System.IO;



public class MyRequestData
{
    public string InputText { get; set; }
}

[Route("api/query")]
[ApiController]
public class MyController : ControllerBase
{
    // 用于发送验证码
    class CheckSumBuilder
    {
        // 计算并获取CheckSum
        public static String getCheckSum(String appSecret, String nonce, String curTime)
        {
            byte[] data = Encoding.Default.GetBytes(appSecret + nonce + curTime);
            byte[] result;

            SHA1 sha = new SHA1CryptoServiceProvider();
            // This is one implementation of the abstract class SHA1.
            result = sha.ComputeHash(data);

            return getFormattedText(result);
        }

        // 计算并获取md5值
        public static String getMD5(String requestBody)
        {
            if (requestBody == null)
                return null;

            // Create a new instance of the MD5CryptoServiceProvider object.
            MD5 md5Hasher = MD5.Create();

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(requestBody));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return getFormattedText(Encoding.Default.GetBytes(sBuilder.ToString()));
        }

        private static String getFormattedText(byte[] bytes)
        {
            char[] HEX_DIGITS = { '0', '1', '2', '3', '4', '5',
            '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f' };
            int len = bytes.Length;
            StringBuilder buf = new StringBuilder(len * 2);
            for (int j = 0; j < len; j++)
            {
                buf.Append(HEX_DIGITS[(bytes[j] >> 4) & 0x0f]);
                buf.Append(HEX_DIGITS[bytes[j] & 0x0f]);
            }
            return buf.ToString();
        }
    }
    // 用于发送验证码
    class HttpClient
    {
        //发起Http请求
        public static string HttpPost(string url, Stream data, IDictionary<object, string> headers = null)
        {
            System.Net.WebRequest request = HttpWebRequest.Create(url);
            request.Method = "POST";
            if (data != null)
                request.ContentLength = data.Length;
            //request.ContentType = "application/x-www-form-urlencoded;charset=utf-8";

            if (headers != null)
            {
                foreach (var v in headers)
                {
                    if (v.Key is HttpRequestHeader)
                        request.Headers[(HttpRequestHeader)v.Key] = v.Value;
                    else
                        request.Headers[v.Key.ToString()] = v.Value;
                }
            }
            HttpWebResponse response = null;
            try
            {
                // Get the response.
                response = (HttpWebResponse)request.GetResponse();
                // Display the status.
                Console.WriteLine(response.StatusDescription);
                // Get the stream containing content returned by the server.
                Stream dataStream = response.GetResponseStream();
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                string responseFromServer = reader.ReadToEnd();


                // Display the content.
                //Console.WriteLine(responseFromServer);
                // Cleanup the streams and the response.
                reader.Close();
                dataStream.Close();
                response.Close();
                return responseFromServer;
            }
            catch (Exception e)
            {
               // Console.WriteLine(e.Message);
               //Console.WriteLine(response.StatusDescription);
               return e.Message;
            }
        }
    }

    [HttpPost]
    public IActionResult HandleEndpoint(MyRequestData requestData)
    {

        // 手机号
        string telephone = requestData.InputText; // 从请求的JSON数据中获取输入的字符串
        // 在这里处理接收到的字符串

        String url = "https://api.netease.im/sms/sendcode.action";
        url += $"?templateid=22518746&mobile={telephone}";//请输入正确的手机号



        //网易云信分配的账号，请替换你在管理后台应用下申请的Appkey
        String appKey = "e4ab5a8dc9255ca0be2d9b883ac38a31";
        //网易云信分配的密钥，请替换你在管理后台应用下申请的appSecret
        String appSecret = "7346e6f6a129";
        //随机数（最大长度128个字符）
        String nonce = "546565464546514";

        TimeSpan ts = DateTime.Now.ToUniversalTime() - new DateTime(1970, 1, 1);
        Int32 ticks = System.Convert.ToInt32(ts.TotalSeconds);
        //当前UTC时间戳，从1970年1月1日0点0 分0 秒开始到现在的秒数(String)
        String curTime = ticks.ToString();
        //SHA1(AppSecret + Nonce + CurTime),三个参数拼接的字符串，进行SHA1哈希计算，转化成16进制字符(String，小写)
        String checkSum = CheckSumBuilder.getCheckSum(appSecret, nonce, curTime);

        IDictionary<object, String> headers = new Dictionary<object, String>();
        headers["AppKey"] = appKey;
        headers["Nonce"] = nonce;
        headers["CurTime"] = curTime;
        headers["CheckSum"] = checkSum;
        headers["ContentType"] = "application/x-www-form-urlencoded;charset=utf-8";
        //执行Http请求
        string inform  = HttpClient.HttpPost(url, null, headers);

        

        return Ok("OK");
    }
}

