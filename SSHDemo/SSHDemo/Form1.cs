using Chilkat;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Flurl;
using Flurl.Http;

namespace SSHDemo
{
    public partial class Form1 : Form
    {
        Ssh ssh = new Ssh();

        public Form1()
        {
            InitializeComponent();
            txtIP.Text = "117.5.84.203";
            txtAcc.Text = "root";
            txtPass.Text = "admin";
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Global chilkatGlob = new Global();
            var success = chilkatGlob.UnlockBundle("Anything for 30-day trial.");
            if (!success)
                MessageBox.Show("Must active Chilkat");
            int port = 22;
            string hostname = txtIP.Text;
            string user = txtAcc.Text;
            string pass = txtPass.Text;

            SshTunnel tunel = new SshTunnel();
            if(tunel.Connect(hostname, port))
            {
                if(tunel.AuthenticatePw(user, pass))
                {
                    tunel.DynamicPortForwarding = true;
                    if (tunel.BeginAccepting(1080))
                    {
                        //Http http = new Http();
                        //http.SocksHostname = "localhost";
                        //http.SocksPort = 1080;
                        //http.SocksVersion = 5;

                        //http.SendCookies = true;
                        //http.SaveCookies = true;
                        //http.CookieDir = "memory";

                        //var res = http.QuickGet("https://api.myip.com/");
                        //MessageBox.Show(Encoding.UTF8.GetString(res));


                        FlurlClient client = new FlurlClient();
                        //client.Configure(setting => setting.HttpClientFactory = new ProxyHttpClientFactory("127.0.0.1", 1080));

                        var res = client.Configure(x => x.HttpClientFactory = new ProxyHttpClientFactory("127.0.0.1", 1080))
                            .Request("https://api.myip.com/").GetStringAsync().Result;

                        MessageBox.Show(res);

                        tunel.StopAccepting(true);
                        tunel.CloseTunnel(true);
                    }
                    else
                    {
                        MessageBox.Show("Not accept port 8081");
                    }
                }
                else
                {
                    MessageBox.Show("Wrong username or password.");
                }
            }
            else
            {
                MessageBox.Show("Could not connect to " + hostname);
            }
        }
    }
}
