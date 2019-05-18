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

        private void Button1_Click(object sender, EventArgs e)
        {
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
            JsonObj_jsir = new JObject();
            Settings = File.ReadAllText(@".\HikazeLauncher\configure.json");
            JsonObj_jsir = (JObject)JsonConvert.DeserializeObject(Settings);
            this.txtJavaPath.Text = JsonObj_jsir["JavaPath"].ToString();
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
    }
}
