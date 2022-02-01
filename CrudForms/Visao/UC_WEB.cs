using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Visao
{
    public partial class UC_WEB : UserControl
    {
        public UC_WEB(string url)
        {
            Util.CL_Files.WriteOnTheLog("UC_WEB()", Util.Global.TipoLog.DETALHADO);

            InitializeComponent();
            this.Dock = DockStyle.Fill;
            this.web_completo.ScriptErrorsSuppressed = true;
            this.web_completo.Navigate(url);
        }
    }
}
