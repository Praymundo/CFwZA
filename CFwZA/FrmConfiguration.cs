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
    public partial class FrmConfiguration : FrmBase
    {
        public FrmConfiguration()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_Settings.ZamzarApiKey != textBox1.Text)
                _Settings.ZamzarApiKey = textBox1.Text;

            if (_Settings.Modified)
                _Settings.Save();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FrmConfiguration_Load(object sender, EventArgs e)
        {
            textBox1.Text = _Settings.ZamzarApiKey;
        }
    }
}
