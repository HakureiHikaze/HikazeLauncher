using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using libMcArgs;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Experiment
{
    class Program
    {
        static void Main(string[] args)
        {
            /*-Dminecraft.client.jar=D:\\mc\\.minecraft\\versions\\1.12.2-OptiFine_HD_U_E3\\1.12.2-OptiFine_HD_U_E3.jar
             * -XX:-UseAdaptiveSizePolicy
             * -XX:-OmitStackTraceInFastThrow
             * -Dfml.ignoreInvalidMinecraftCertificates=true
             * -Dfml.ignorePatchDiscrepancies=true
             * -XX:HeapDumpPath=MojangTricksIntelDriversForPerformance_javaw.exe_minecraft.exe.heapdump
             * -Djava.library.path=D:\\mc\\.minecraft\\versions\\1.12.2-OptiFine_HD_U_E3\\1.12.2-OptiFine_HD_U_E3-natives
             * \"-Dminecraft.launcher.brand=Hello Minecraft! Launcher\"
             * -Dminecraft.launcher.version=2.7.9.55";
            */
            ParamentsGen ParaGen01 = new ParamentsGen();
            Playerinfo player1 = new Playerinfo();
            player1.SetPlayerName("Hikaze");
            player1.RegenerateUUID();
            string ext = "-XX:-UseAdaptiveSizePolicy -XX:-OmitStackTraceInFastThrow -Dfml.ignoreInvalidMinecraftCertificates=true -Dfml.ignorePatchDiscrepancies=true -XX:HeapDumpPath=MojangTricksIntelDriversForPerformance_javaw.exe_minecraft.exe.heapdump \"-Dminecraft.launcher.brand=Hello Minecraft! Launcher\" -Dminecraft.launcher.version=2.7.9.55";
            string a = ParaGen01.CombineParaments(player1, "", "D:\\mc", "1.12.2-OptiFine_HD_U_E3", ext, 4096, 2048, false);
            Console.WriteLine(a);
            Process p = new Process();
            ProcessStartInfo s = new ProcessStartInfo("\"C:\\Program Files\\AdoptOpenJDK\\jdk-8.0.202.08\\bin\\java.exe\"", a);
            s.UseShellExecute = false;
            s.RedirectStandardOutput = true;
            p.StartInfo = s;
            p.Start();
            //Console.WriteLine( ParaGen01.GetJarPackName("org.scala-lang.plugins:scala-continuations-library_2.11:1.0.2"));
            //JObject j1 =new JObject { { "name", 123 } };
            //ArrayList arr1 = new ArrayList();
            //ParaGen01.GetPacksFromJson("D:\\mc", "1.12.2-OptiFine_HD_U_E3", arr1);
            //ParaGen01.ConvertPackNameFromList(arr1);
            //arr1 = ParaGen01.RemoveDuplicate(arr1);
            //for (int i = 0; i < arr1.Count; i++)
            //{
            //    Console.WriteLine(arr1[i]);
            //}
        }
    }
}

