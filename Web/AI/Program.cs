using Baidu.Aip.Speech;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI
{
    class Program
    {
        // 设置APPID/AK/SK
        static string APP_ID = "11341051";
        static string API_KEY = "k4Z0WgNVf3vQYLuQKaVAuc6k";
        static string SECRET_KEY = "87YVZCmd8O26ZG1UqbMBL3YUaBUaeTxR";

        static Asr client = new Baidu.Aip.Speech.Asr(API_KEY, SECRET_KEY);

        static void Main(string[] args)
        {
            AsrData();
        }

        // 识别本地文件
        static public void AsrData()
        {
            var data = File.ReadAllBytes(@"H:\yank\企业管理系统V1.0\Web\AI\test\16k.wav");
            var result = client.Recognize(data, "pcm", 16000);
            Console.Write(result);
        }
    }
}
