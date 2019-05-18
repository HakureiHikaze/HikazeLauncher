using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using libMcArgs;
using Newtonsoft.Json.Linq;
namespace HikazeLauncher
{
    class AllConfigure
    {
        public AllConfigure()
        {
            JavaPath = "";
            player = null;
            ExtraJVMPara = "";
            ExtraMCPara = "";
            MaxMem = 0;
            MinMem = 0;
        }
        public AllConfigure(int x)
        {
            switch (x)
            {
                case 0:
                    {
                        this.JavaPath = Environment.GetEnvironmentVariable("JAVA_HOME") + @"\bin\javaw.exe";
                        this.MaxMem = 1024;
                        this.MinMem = 1024;
                        this.ExtraJVMPara = "-XX:-UseAdaptiveSizePolicy -XX:-OmitStackTraceInFastThrow -XX:HeapDumpPath=MojangTricksIntelDriversForPerformance_javaw.exe_minecraft.exe.heapdump";
                        this.ExtraMCPara = "-Dfml.ignoreInvalidMinecraftCertificates=true -Dfml.ignorePatchDiscrepancies=true";
                        this.player = new Playerinfo();
                        this.player.RegenerateUUID();
                        this.player.SetPlayerName(" ");
                        return;
                    }
            }
        }
        public string JavaPath;
        public Playerinfo player;
        public string ExtraJVMPara;
        public string ExtraMCPara;
        public int MaxMem;
        public int MinMem;
        public JObject ConvertToJson()
        {
            JObject ReturnJson = new JObject();
            ReturnJson.Add("JavaPath", JavaPath);
            ReturnJson.Add("PlayerName", player.PlayerName);
            ReturnJson.Add("PlayerUUID", player.uuid);
            ReturnJson.Add("ExtraJVMPara", ExtraJVMPara);
            ReturnJson.Add("ExtraMCPara", ExtraMCPara);
            ReturnJson.Add("MaxMem", MaxMem);
            ReturnJson.Add("MinMem", MinMem);
            return ReturnJson;
        }
    }
}