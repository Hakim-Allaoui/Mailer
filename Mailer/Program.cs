using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace Mailer
{
    static class Program
    {
        public static string dbName = "MailerDemo";
        public static SqlConnection con = new SqlConnection("Data Source = .;Initial catalog = master;Integrated Security = true");
        public static SqlCommand cmd = new SqlCommand("SELECT * FROM Authentications", con);
        public static SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Authentications", con);
        //public static SqlDataReader dr;
        public static DataSet ds = new DataSet();
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormConnexion());
        }

    }
}
