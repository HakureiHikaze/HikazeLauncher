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
using Newtonsoft.Json.Linq;
using System.IO;
using Newtonsoft.Json;

namespace HikazeLauncher
{
    public partial class MainForm : Form
    {
        AllConfigure configure_ljc;
        Playerinfo Player_van;
        public MainForm()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            ParamentsGen ParaGen_ryu = new ParamentsGen();
            
            JObject JsonObj_jrt = new JObject();
            
            Player_van.SetPlayerName("Hikaze");
            Player_van.RegenerateUUID();
            string ExtraPara_zhy = "-XX:-UseAdaptiveSizePolicy -XX:-OmitStackTraceInFastThrow -Dfml.ignoreInvalidMinecraftCertificates=true -Dfml.ignorePatchDiscrepancies=true -XX:HeapDumpPath=MojangTricksIntelDriversForPerformance_javaw.exe_minecraft.exe.heapdump";
            string MainPara_hln = ParaGen_ryu.CombineParaments(Player_van, "", "D:\\mc", "1.12.2-OptiFine_HD_U_E3", ExtraPara_zhy, 4096, 2048, false);
            File.WriteAllText(@".\HikazeLauncher\Last Parameters.log", configure_ljc.JavaPath+" "+MainPara_hln);
            Process Process_xb = new Process();
            ProcessStartInfo ProcessStart_hhh = new ProcessStartInfo(configure_ljc.JavaPath+@"\bin\javaw.exe", MainPara_hln);
            Process_xb.StartInfo = ProcessStart_hhh;
            Process_xb.StartInfo.UseShellExecute = false;
            Process_xb.Start();
            Close();
        }

        private void BtnConfigure_Click(object sender, EventArgs e)
        {
            frmConfigure FormConfigure_bx = new frmConfigure();
            FormConfigure_bx.ShowDialog();
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            configure_ljc = new AllConfigure();
            Player_van = new Playerinfo();
            if (!Directory.Exists(@".\HikazeLauncher"))
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(@".\HikazeLauncher");
                directoryInfo.Create();
            }
            JObject JsonObj_zsc = new JObject();
            AllConfigure configure_wyy = new AllConfigure();
            if (!File.Exists(@".\HikazeLauncher\configure.json"))
            {
                using (File.Create(@".\HikazeLauncher\configure.json")) { };
                configure_wyy.JavaPath = Environment.GetEnvironmentVariable("JAVA_HOME")+@"\bin\javaw.exe";
                configure_wyy.MaxMem = 1024;
                configure_wyy.MinMem = 1024;
                configure_wyy.ExtraPara = "-XX:-UseAdaptiveSizePolicy -XX:-OmitStackTraceInFastThrow -Dfml.ignoreInvalidMinecraftCertificates=true -Dfml.ignorePatchDiscrepancies=true -XX:HeapDumpPath=MojangTricksIntelDriversForPerformance_javaw.exe_minecraft.exe.heapdump";
                configure_wyy.player = new Playerinfo();
                configure_wyy.player.RegenerateUUID();
                configure_wyy.player.SetPlayerName(" ");
                JsonObj_zsc = configure_wyy.ConvertToJson();
                File.WriteAllText(@".\HikazeLauncher\configure.json", JsonObj_zsc.ToString(Newtonsoft.Json.Formatting.Indented, null));
            }
            string json = File.ReadAllText(@".\HikazeLauncher\configure.json");
            JsonObj_zsc = (JObject)JsonConvert.DeserializeObject(json);
            this.label1.Text = JsonObj_zsc.ToString(Newtonsoft.Json.Formatting.Indented, null);
            configure_ljc.JavaPath = JsonObj_zsc["JavaPath"].ToString();
            Player_van.PlayerName = JsonObj_zsc["PlayerName"].ToString();
            Player_van.uuid = JsonObj_zsc["PlayerUUID"].ToString();
            configure_ljc.ExtraPara = JsonObj_zsc["ExtraPara"].ToString();
            configure_ljc.MaxMem = Convert.ToInt32(JsonObj_zsc["MaxMem"].ToString());
            configure_ljc.MinMem = Convert.ToInt32(JsonObj_zsc["MinMem"].ToString());
            return;
        }
        Point mouseOff;
        bool leftFlag = false;
        private void Frm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseOff = new Point(-e.X, -e.Y);
                leftFlag = true;
            }
        }
        private void Frm_MouseMove(object sender, MouseEventArgs e)
        {
            if (leftFlag)
            {
                Point mouseSet = Control.MousePosition;
                mouseSet.Offset(mouseOff.X, mouseOff.Y);
                this.Location = mouseSet;
            }
        }
        private void Frm_MouseUp(object sender, MouseEventArgs e)
        {
            if (leftFlag)
            {
                leftFlag = false;
            }
        }

        private void Button1_Click_1(object sender, EventArgs e)
        {
            Close();
        }
    }
}