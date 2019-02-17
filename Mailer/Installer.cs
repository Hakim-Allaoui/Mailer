using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace Mailer
{
    [RunInstaller(true)]
    public partial class Installer : System.Configuration.Install.Installer
    {
        static SqlConnection con = new SqlConnection(@"Data Source=.;Initial catalog=master;Integrated security=True");
        static SqlCommand com = new SqlCommand();
        public void scriptDb()
        {
            com = new SqlCommand("CREATE DATABASE MailerDemo", con);
            con.Open();
            com.ExecuteNonQuery();
            con.Close();
        }
        public void scriptTabl()
        {
            Assembly Asm = Assembly.GetExecutingAssembly();
            Stream str = Asm.GetManifestResourceStream("Mailer.script.txt");
            StreamReader read = new StreamReader(str);
            string req = read.ReadToEnd();

            SqlCommand script = new SqlCommand(req, con);
            con.Open();
            con.ChangeDatabase("MailerDemo");
            script.ExecuteNonQuery();
            con.Close();
        }
        public void scriptProc()
        {
            Assembly Asm = Assembly.GetExecutingAssembly();
            Stream str = Asm.GetManifestResourceStream("Mailer.scriptProc.txt");
            StreamReader read = new StreamReader(str);
            string req2 = read.ReadToEnd();

            SqlCommand scriptProc = new SqlCommand(req2, con);
            con.Open();
            con.ChangeDatabase("MailerDemo");
            scriptProc.ExecuteNonQuery();
            con.Close();
        }
        public Installer()
        {
            InitializeComponent();

        }
        public override void Install(IDictionary stateSaver)
        {
            base.Install(stateSaver);
            try
            {
                scriptDb();
                scriptTabl();
                scriptProc();

                string fullname = Context.Parameters["fullname"];
                string birthday = Context.Parameters["birthday"];
                string username = Context.Parameters["username"];
                string password = Context.Parameters["password"];

                con = new SqlConnection(@"Data Source=.;Initial catalog=MailerDemo;Integrated security=True");

                string cmdAuthanticat = "INSERT INTO Authentications([login],[password]) VALUES ('" + username + "','" + password + "' )";
                com = new SqlCommand(cmdAuthanticat, con);
                con.Open();
                com.ExecuteNonQuery();
                con.Close();
                
                string cmdUser = "INSERT INTO Users(fullName,DateN,[login]) VALUES ('" + fullname + "','" + birthday + "','" + username + "' )";
                com = new SqlCommand(cmdUser, con);
                con.Open();
                com.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception E)
            {
                MessageBox.Show("Error : " + E.Message);
            }
        }
    }
}
