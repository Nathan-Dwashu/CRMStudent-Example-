using CRMStudent.StudentClasses;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CRMStudent
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        static string myconnstring = ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;
        Student student = new Student();

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadData();           
        }

        public void LoadData()
        {
            //Load the data onto the datagridview
            DataTable dataTable = student.Select();
            dgvStudentsList.DataSource = dataTable;            
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            const string message = "Do you want to Exit?";
            const string caption = "Exit App";

            var result = MessageBox.Show(message, caption, MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }          
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //Get the value from the input field
            student.FirstName = txtBoxFirstName.Text;
            student.LastName = txtBoxLastName.Text;
            student.EmailAddress = txtBoxEmailAddress.Text;
            student.IDNumber = txtBoxIDNo.Text;
            student.DateEnrolled = Convert.ToDateTime(txtBoxDateEnrol.Text);
            student.Campus = cboCampus.Text;

            bool success = student.Insert(student);
            if (success)
            {
                MessageBox.Show("New Student added successfully");
            }
            else
            {
                MessageBox.Show("Failed to add new Student");
            }

            LoadData();
            Clear();
        }

        public void Clear()
        {
            txtBoxStudentNo.Text = "";
            txtBoxFirstName.Text = " ";
            txtBoxLastName.Text = " ";
            txtBoxEmailAddress.Text = " ";
            txtBoxIDNo.Text = " ";
            txtBoxDateEnrol.Text = " ";
            cboCampus.Text = " ";
        }

        private void dgvStudentsList_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            // Get the data from the dgv and load it onto the textboxes respectively
            // identitfy the row on which mouse is clicked
            int rowIndex = e.RowIndex;
            txtBoxStudentNo.Text = dgvStudentsList.Rows[rowIndex].Cells[0].Value.ToString();
            txtBoxFirstName.Text = dgvStudentsList.Rows[rowIndex].Cells[1].Value.ToString();
            txtBoxLastName.Text = dgvStudentsList.Rows[rowIndex].Cells[2].Value.ToString();
            txtBoxEmailAddress.Text = dgvStudentsList.Rows[rowIndex].Cells[3].Value.ToString();
            txtBoxIDNo.Text = dgvStudentsList.Rows[rowIndex].Cells[4].Value.ToString();
            txtBoxDateEnrol.Text = dgvStudentsList.Rows[rowIndex].Cells[5].Value.ToString();
            cboCampus.Text = dgvStudentsList.Rows[rowIndex].Cells[6].Value.ToString();

        }        

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //Get the value from the input field
            student.StudentNo = int.Parse(txtBoxStudentNo.Text);
            student.FirstName = txtBoxFirstName.Text;
            student.LastName = txtBoxLastName.Text;
            student.EmailAddress = txtBoxEmailAddress.Text;
            student.IDNumber = txtBoxIDNo.Text;
            student.DateEnrolled = Convert.ToDateTime(txtBoxDateEnrol.Text);
            student.Campus = cboCampus.Text;

            bool success = student.Update(student);
            if (success)
            {
                MessageBox.Show("Student updated successfully");
                LoadData();
                Clear();
            }
            else
            {
                MessageBox.Show("Failed to update Student");
            }

            
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //Get the StudentNo from the App
            student.StudentNo = Convert.ToInt32(txtBoxStudentNo.Text);
            bool success = student.Delete(student);
            if (success)
            {
                MessageBox.Show("Student record deleted sucessfully");
                LoadData();
                Clear();
            }
            else
            {
                MessageBox.Show("Failed to delete Student record");
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void txtBoxSearch_TextChanged(object sender, EventArgs e)
        {
            //Get the value from the box
            string keyword = txtBoxSearch.Text;
            SqlConnection sqlCon = new SqlConnection(myconnstring);
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Students WHERE FirstName LIKE '%"+keyword+ "%' OR LastName  LIKE '%"+keyword+"%' OR EmailAddress  LIKE '%"+keyword+"%' OR IDNumber  LIKE '%"+keyword+"%' OR DateEnrolled  LIKE '%"+keyword+"%' OR Campus  LIKE '%"+keyword+"%'", sqlCon);

            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dgvStudentsList.DataSource = dataTable;
        }

        private void Form1_Shown(object sender, EventArgs e)
        {            
            lblUser.Text = $"Welcome, {Login_Form.username}";

            if (Login_Form.role == "user")
            {
                txtBoxFirstName.Enabled = false;
                txtBoxLastName.Enabled = false;
                txtBoxEmailAddress.Enabled = false;
                txtBoxIDNo.Enabled = false;
                txtBoxDateEnrol.Enabled = false;
                cboCampus.Enabled = false;
                btnAdd.Enabled = false;
                btnUpdate.Enabled = false;
                btnDelete.Enabled = false;
                btnClear.Enabled = false;
            }
            else
            {
                txtBoxFirstName.Enabled = true;
                txtBoxLastName.Enabled = true;
                txtBoxEmailAddress.Enabled = true;
                txtBoxIDNo.Enabled = true;
                txtBoxDateEnrol.Enabled = true;
                cboCampus.Enabled = true;
                btnAdd.Enabled = true;
                btnUpdate.Enabled = true;
                btnDelete.Enabled = true;
                btnClear.Enabled = true;
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            printDocument1.Print();
        }
    }
}
