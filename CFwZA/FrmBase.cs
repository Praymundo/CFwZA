using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CFwZA
{
    public partial class FrmBase : Form
    {
        internal Properties.Settings _Settings => CFwZA.Properties.Settings.Default;
        public FrmBase()
        {
            InitializeComponent();
        }
    }
}
