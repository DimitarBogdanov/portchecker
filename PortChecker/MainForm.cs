using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Windows.Forms;

namespace PortChecker
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private static int[] GetUsedPorts(string startsWith = "")
        {
            IPGlobalProperties ipProperties = IPGlobalProperties.GetIPGlobalProperties();
            IPEndPoint[] tcpEndpoints = ipProperties.GetActiveTcpListeners();
            IPEndPoint[] udpEndpoints = ipProperties.GetActiveUdpListeners();
            List<int> ports = new();

            foreach (IPEndPoint endpoint in tcpEndpoints)
            {
                int port = endpoint.Port;
                if (port.ToString().StartsWith(startsWith) && !ports.Contains(port))
                {
                    ports.Add(port);
                }
            }
            foreach (IPEndPoint endpoint in udpEndpoints)
            {
                int port = endpoint.Port;
                if (port.ToString().StartsWith(startsWith) && !ports.Contains(port))
                {
                    ports.Add(port);
                }
            }

            ports.Sort();
            return ports.ToArray();
        }

        private void FillList(int[] ports)
        {
            listView1.Clear();

            foreach (int port in ports)
            {
                if (port != 0)
                {
                    listView1.Items.Add(port.ToString());
                }
            }
        }

        private void FilterButtonClick(object sender, EventArgs e)
        {
            FillList(GetUsedPorts(textBox1.Text));
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            FillList(GetUsedPorts(textBox1.Text));
        }
    }
}
