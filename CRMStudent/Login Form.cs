using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRMStudent
{
    public partial class Login_Form : Form
    {
        public Login_Form()
        {
            InitializeComponent();
        }

        static string myconnstring = ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;

        public static string username;
        public static string role;

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string password;

            username = txtBoxUsername.Text;
            password = txtBoxPassword.Text;             

            SqlConnection sqlCon = new SqlConnection(myconnstring);
            DataTable dataTable = new DataTable();            

            try
            {
                string sqlQuery = "SELECT * FROM Users WHERE Username LIKE '%" + username + "%' AND Password  LIKE '%" + password + "%'";
                SqlCommand cmd = new SqlCommand(sqlQuery, sqlCon);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                sqlCon.Open();
                adapter.Fill(dataTable);                

                int rows = dataTable.Rows.Count;
                //if the query runs succesfully then the value of the rows will be greater than zero else
                //its value will be 0
                if (rows == 1)
                {
                    role = dataTable.Rows[0].Field<string>(5);                    
                    Form1 frm1 = new Form1();                    
                    frm1.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Wrong Username or Password. Please try again", "Incorrect Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show(sqlEx.Message);

            }
            finally
            {
                sqlCon.Close();
            } 
        }

    }
}
