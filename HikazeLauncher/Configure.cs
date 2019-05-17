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
namespace HikazeLauncher
{
    public partial class frmConfigure : Form
    {
        public frmConfigure()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            JObject JsonObj_jsir = new JObject();
            JsonObj_jsir.Add("JavaPath", txtJavaPath.Text);
        }
    }
}
