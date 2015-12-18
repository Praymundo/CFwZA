using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Json;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CFwZA
{
    public partial class FrmMain : FrmBase
    {
        private const String EndPointPattern = "https://sandbox.zamzar.com/v1/formats/{0}";
        public FrmMain()
        {
            InitializeComponent();
        }

        private void configurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (Form frmConfig = new FrmConfiguration())
            {
                frmConfig.ShowDialog(this);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == Wrappers.NativeMethods.WM_SHOWME)
            {
                ShowMe();
            }
            base.WndProc(ref m);
        }

        private void ShowMe()
        {
            if (WindowState == FormWindowState.Minimized)
            {
                WindowState = FormWindowState.Normal;
            }

            // get our current "TopMost" value (ours will always be false though)
            // make our form jump to the top of everything and set it back to whatever it was
            //var top = TopMost;
            //TopMost = true;
            //TopMost = top;

            // Set Window to Foreground
            Wrappers.NativeMethods.SetForegroundWindow(this.Handle);

            // Set focus
            Focus();
        }

        async private void button1_Click(object sender, EventArgs e)
        {
            string apiKey = _Settings.ZamzarApiKey;
            string endpoint = String.Format(EndPointPattern, txtFormat.Text);

            JsonValue json = await Query(apiKey, endpoint);

            StringBuilder msg = new StringBuilder();
            if ((json.ContainsKey("errors")) && (json["errors"] != null))
            {
                JsonArray objErrors = (JsonArray)json["errors"];
                msg.AppendLine("+----------------");
                msg.AppendLine("| Errors: ");
                msg.AppendLine("| Code  - Message");
                msg.AppendLine("+----------------");
                foreach (JsonObject objError in objErrors)
                {
                    string code = (string)objError["code"];
                    string desc = (string)objError["message"];

                    msg.AppendLine(String.Format("| {0,-5} - {1}", code, desc));
                }
                msg.AppendLine("+----------------");
            }
            else
            {
                JsonArray aTargets = (JsonArray)json["targets"];
                for (int i = 0; i < aTargets.Count; i++)
                {
                    JsonObject obj = (JsonObject)aTargets[i];
                    string name = (string)obj["name"];
                    string cost = (string)obj["credit_cost"];
                    msg.AppendLine(name + ": " + cost);
                }
            }
            textBox1.Text = msg.ToString();
        }

        async Task<JsonValue> Query(string key, string url)
        {
            using (HttpClientHandler handler = new HttpClientHandler { Credentials = new NetworkCredential(key, "") })
            using (HttpClient client = new HttpClient(handler))
            using (HttpResponseMessage response = await client.GetAsync(url))
            using (HttpContent content = response.Content)
            {
                string data = await content.ReadAsStringAsync();
                return JsonObject.Parse(data);
            }
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            txtFormat.Text = "pdfx";
            textBox1.Font = new Font(FontFamily.GenericMonospace, 8);
            textBox1.Text = "Teste de letra\r\n120   \r\n   123";
        }
    }
}
