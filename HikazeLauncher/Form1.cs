using libMcArgs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HikazeLauncher
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            ParamentsGen ParaGen01 = new ParamentsGen();
            Playerinfo player1 = new Playerinfo();
            player1.SetPlayerName("Hikaze");
            player1.RegenerateUUID();
            string ext = "-XX:-UseAdaptiveSizePolicy -XX:-OmitStackTraceInFastThrow -Dfml.ignoreInvalidMinecraftCertificates=true -Dfml.ignorePatchDiscrepancies=true -XX:HeapDumpPath=MojangTricksIntelDriversForPerformance_javaw.exe_minecraft.exe.heapdump \"-Dminecraft.launcher.brand=Hello Minecraft! Launcher\" -Dminecraft.launcher.version=2.7.9.55";
            string a = ParaGen01.CombineParaments(player1, "", "D:\\mc", "1.12.2-OptiFine_HD_U_E3", ext, 4096, 2048, false);
            Process process = new Process();
            ProcessStartInfo s = new ProcessStartInfo("\"C:\\Program Files\\AdoptOpenJDK\\jdk-8.0.202.08\\bin\\java.exe\"", a);
            process.StartInfo = s;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            //process.StartInfo.CreateNoWindow = true;
            process.Start();
            Close();
        }
    }
}
