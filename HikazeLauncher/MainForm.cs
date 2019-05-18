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
using System.Runtime.InteropServices;

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
            string MainPara_hln = ParaGen_ryu.CombineParaments
                (
                Player_van,
                "D:\\mc",
                "1.12.2-OptiFine_HD_U_E3",
                configure_ljc.ExtraJVMPara+configure_ljc.ExtraMCPara,
                configure_ljc.MaxMem, 
                configure_ljc.MinMem,
                false
                );
            File.WriteAllText(@".\HikazeLauncher\Last Parameters.log", configure_ljc.JavaPath+" "+MainPara_hln);
            Process Process_xb = new Process();
            ProcessStartInfo ProcessStart_hhh = new ProcessStartInfo(configure_ljc.JavaPath, MainPara_hln);
            Process_xb.StartInfo = ProcessStart_hhh;
            Process_xb.StartInfo.UseShellExecute = false;
            Process_xb.Start();
            Close();
        }

        private void BtnConfigure_Click(object sender, EventArgs e)
        {
            frmConfigure FormConfigure_bx = new frmConfigure();
            FormConfigure_bx.ShowDialog();
            SetConfigureFromPath(@".\HikazeLauncher\configure.json");
            
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            
            Player_van = new Playerinfo();
            if (!Directory.Exists(@".\HikazeLauncher"))
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(@".\HikazeLauncher");
                directoryInfo.Create();
            }
            JObject JsonObj_zsc;
            if (!File.Exists(@".\HikazeLauncher\configure.json"))
            {
                configure_ljc = new AllConfigure(0);
                using (File.Create(@".\HikazeLauncher\configure.json")) { };
                JsonObj_zsc = configure_ljc.ConvertToJson();
                File.WriteAllText(@".\HikazeLauncher\configure.json", JsonObj_zsc.ToString(Newtonsoft.Json.Formatting.Indented, null));
            }
            else
            {
                configure_ljc = new AllConfigure();
            }
            string json                         = File.ReadAllText(@".\HikazeLauncher\configure.json");
            JsonObj_zsc                         = (JObject)JsonConvert.DeserializeObject(json);
            this.label1.Text                    = JsonObj_zsc.ToString(Newtonsoft.Json.Formatting.Indented, null);
            SetConfigureFromJson(JsonObj_zsc);
            this.Opacity = 0;
            while ( this.Opacity <0.85)
            {
                this.Refresh();
                this.Opacity +=0.05;
                System.Threading.Thread.Sleep(3);
            }
            return;
        }
        private void SetConfigureFromJson(JObject JsonObj_wx)
        {
            configure_ljc.JavaPath = JsonObj_wx["JavaPath"].ToString();
            Player_van.PlayerName = JsonObj_wx["PlayerName"].ToString();
            Player_van.uuid = JsonObj_wx["PlayerUUID"].ToString();
            configure_ljc.ExtraJVMPara = JsonObj_wx["ExtraJVMPara"].ToString();
            configure_ljc.ExtraMCPara = JsonObj_wx["ExtraMCPara"].ToString();
            configure_ljc.MaxMem = Convert.ToInt32(JsonObj_wx["MaxMem"].ToString());
            configure_ljc.MinMem = Convert.ToInt32(JsonObj_wx["MinMem"].ToString());
        }
        public void SetConfigureFromPath(string Path)
        {
            string _json = File.ReadAllText(Path);
            JObject JsonObj = (JObject)JsonConvert.DeserializeObject(_json);
            SetConfigureFromJson(JsonObj);
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
            while (this.Opacity > 0)
            {
                this.Refresh();
                this.Opacity -= 0.05;
                System.Threading.Thread.Sleep(3);
            }
            Close();
        }
    }
}