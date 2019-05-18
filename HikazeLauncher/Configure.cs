using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
    public partial class frmConfigure : Form
    {
        JObject JsonObj_jsir;
        string Settings;
        public frmConfigure()
        {
            InitializeComponent();
        }

        private void SaveAndQuit(object sender, EventArgs e)
        {
            WriteConfigToJson();
            
            while (this.Opacity >0)
            {
                this.Refresh();
                this.Opacity -= 0.05;
                System.Threading.Thread.Sleep(3);
            }
            
            Close();
        }

        private void Button2_Click(object sender, EventArgs e)
        {

            if (Environment.GetEnvironmentVariable("ProgramFiles").Contains("(x86)"))
            {
                this.OpenFile.InitialDirectory = Environment.GetEnvironmentVariable("ProgramFiles").Replace(" (x86)", "");
            }
            else
            {
                this.OpenFile.InitialDirectory = Environment.GetEnvironmentVariable("ProgramFiles");
            }
            this.OpenFile.FileName = "javaw.exe";
            this.OpenFile.ShowDialog();
        }

        private void OpenFile_FileOk(object sender, CancelEventArgs e)
        {
            this.txtJavaPath.Text = this.OpenFile.FileName;
        }

        private void FrmConfigure_Load(object sender, EventArgs e)
        {
            LoadConfigFromJson();
            
            
            this.Opacity = 0;
            while (this.Opacity < 0.85)
            {
                this.Refresh();
                this.Opacity += 0.05;
                System.Threading.Thread.Sleep(3);
            }
        }

        Point mouseOff;
        bool leftFlag = false;
        private void FrmConfigure_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseOff = new Point(-e.X, -e.Y);
                leftFlag = true;
            }
        }
        private void FrmConfigure_MouseMove(object sender, MouseEventArgs e)
        {
            if (leftFlag)
            {
                Point mouseSet = Control.MousePosition;
                mouseSet.Offset(mouseOff.X, mouseOff.Y);
                this.Location = mouseSet;
            }
        }
        private void FrmConfigure_MouseUp(object sender, MouseEventArgs e)
        {
            if (leftFlag)
            {
                leftFlag = false;
            }
        }

        private void BtnAuto_Click(object sender, EventArgs e)
        {

        }

        private void Label4_Click(object sender, EventArgs e)
        {

        }
        private void WriteConfigToJson()
        {
            JsonObj_jsir["JavaPath"] = txtJavaPath.Text;
            JsonObj_jsir["ExtraMCPara"] = textBox3.Text;
            JsonObj_jsir["ExtraJVMPara"] = textBox4.Text;
            JsonObj_jsir["MinMem"] = Convert.ToInt32(textBox1.Text);
            JsonObj_jsir["MaxMem"] = Convert.ToInt32(textBox2.Text);
            File.WriteAllText(@".\HikazeLauncher\configure.json", JsonObj_jsir.ToString(Newtonsoft.Json.Formatting.Indented, null));
        }
        private void LoadConfigFromJson()
        {
            Settings = File.ReadAllText(@".\HikazeLauncher\configure.json");
            JsonObj_jsir = (JObject)JsonConvert.DeserializeObject(Settings);
            txtJavaPath.Text = JsonObj_jsir["JavaPath"].ToString();
            textBox3.Text = JsonObj_jsir["ExtraMCPara"].ToString();
            textBox4.Text = JsonObj_jsir["ExtraJVMPara"].ToString();
            textBox1.Text = JsonObj_jsir["MinMem"].ToString();
            textBox2.Text = JsonObj_jsir["MaxMem"].ToString();
        }
    }
}
