using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Threading;

namespace Mailer
{
    public partial class FormConnexion : Form
    {
        static SqlConnection con = new SqlConnection(@"Data Source=.;Initial catalog=MailerDemo;Integrated security=True");
        static SqlCommand com = new SqlCommand();
        //****************************//Move Form//****************************
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        //______________________________________________________
        Bitmap img1 = Properties.Resources.Eye_24px;
        Bitmap img2 = Properties.Resources.Hide_24px_2;
        public FormConnexion()
        {
            InitializeComponent();
            statuMsg.Location = new Point(310, -55);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void panel3_Click(object sender, EventArgs e)
        {

            if (panel3.Left < 645) { 
                for (int i = 0; i < 150; i++)
                {
                    panel2.Left += 2;
                    panel3.Left += 2;
                    label2.Left += 2;
                    panel4.Left += 1;
                }
            panel3.BackgroundImage = Mailer.Properties.Resources.Panel_About_Us;
            }
            else
            {
                for (int i = 0; i < 50; i++)
                {
                    panel2.Left -= 6;
                    panel3.Left -= 6;
                    label2.Left -= 6;
                    panel4.Left -= 3;
                }
                panel3.BackgroundImage = Mailer.Properties.Resources.panel_hide;
            }
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.facebook.com/hvkiw");
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.facebook.com/Unezz.ch3");
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.facebook.com/lbaLauNes.ii");
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        
        private void pictureBox4_Click(object sender, EventArgs e)
        {
            if (txtPass.PasswordChar.ToString() == "•")
            {
                pictureBox4.Image = Properties.Resources.Hide_24px_2;
                txtPass.PasswordChar = new char();
            }
            else
            {
                pictureBox4.Image = Properties.Resources.Eye_24px;
                txtPass.PasswordChar = char.Parse("•");
            }
        }
        public string Authentication(string log, string pass)
        {
            com = new SqlCommand("PS_Authentication", con);
            com.CommandType = CommandType.StoredProcedure;
            SqlParameter p1 = com.Parameters.Add("@log", SqlDbType.VarChar, 50);
            p1.SourceColumn = "log";
            SqlParameter p2 = com.Parameters.Add("@p", SqlDbType.VarChar, 50);
            p2.SourceColumn = "p";
            SqlParameter result = com.Parameters.Add("@msg", SqlDbType.VarChar, 50);
            result.Direction = ParameterDirection.Output;
            p1.Value = log;
            p2.Value = pass;

            con.Open();
            com.ExecuteNonQuery();
            con.Close();

            return result.Value.ToString();
        }
        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            string msg = Authentication(txtUser.Text, txtPass.Text);
            
            if (msg == "Login Succeed")
            {
                statuMsg.Iconimage = Properties.Resources.succeed;
                statuMsg.Text = msg;
                statuMsg.Location = new Point(310, -10);
                Thread.Sleep(10000);
                Form2 f = new Form2();
                f.Show();
                Hide();
            }
            else
            {
                statuMsg.Iconimage = Properties.Resources.failed;
                statuMsg.BackColor = Color.Red;
                statuMsg.Text = msg;
                statuMsg.Location = new Point(310, -10);
            }
        }
    }
}