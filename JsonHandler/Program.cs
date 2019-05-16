using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using libMcArgs;
using Newtonsoft.Json;

namespace JsonHandler
{
    class Program
    {
        static void Main(string[] args)
        {
            Playerinfo playerInfo1 = new Playerinfo();
            playerInfo1.SetPlayerName("Hikaze");
            playerInfo1.RegenerateUUID();
            string fp = System.Environment.CurrentDirectory + "\\info.json";
            if (!File.Exists(fp))  // 判断是否已有相同文件 
            {
                FileStream fs1 = new FileStream(fp, FileMode.Create, FileAccess.ReadWrite);
                fs1.Close();
            }
            File.WriteAllText(fp, JsonConvert.SerializeObject(playerInfo1));
        }
    }
}
