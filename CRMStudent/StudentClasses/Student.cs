using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRMStudent.StudentClasses
{
    class Student
    {
        //connection string stored in app.config
        static string myconnstring = ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;

        #region Getters And Setters
        //Getter and Setter properties that acts as a Data carrier in our app
        public int StudentNo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string IDNumber { get; set; }
        public DateTime DateEnrolled { get; set; }
        public string Campus { get; set; }

        #endregion


        #region All Methods for app functionality
        /// <summary>
        /// Select the data from the database
        /// </summary>
        /// <returns></returns>
        public DataTable Select()
        {
            SqlConnection sqlCon = new SqlConnection(myconnstring);
            DataTable dataTable = new DataTable();

            try
            {
                string sqlQuery = "SELECT * FROM Students";
                SqlCommand cmd = new SqlCommand(sqlQuery, sqlCon);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                sqlCon.Open();
                adapter.Fill(dataTable);

            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show(sqlEx.Message);
               
            }
            finally
            {
                sqlCon.Close();
            }
            
            return dataTable;

        }

        public bool Insert(Student student)
        {
            //Creating a default return type and setting its value to false
            bool isSuccess = false;

            SqlConnection sqlCon = new SqlConnection(myconnstring);           

            try
            {
                string sqlQuery = "INSERT INTO Students (FirstName, LastName, EmailAddress, IDNumber, DateEnrolled, Campus) VALUES (@FirstName, @LastName, @EmailAddress, @IDNumber, @DateEnrolled, @Campus)";
                SqlCommand cmd = new SqlCommand(sqlQuery, sqlCon);
                
                //Create Parameters to add data
                cmd.Parameters.AddWithValue("@FirstName", student.FirstName);
                cmd.Parameters.AddWithValue("@LastName", student.LastName);
                cmd.Parameters.AddWithValue("@EmailAddress", student.EmailAddress);
                cmd.Parameters.AddWithValue("@IDNumber", student.IDNumber);
                cmd.Parameters.AddWithValue("@DateEnrolled", student.DateEnrolled);
                cmd.Parameters.AddWithValue("@Campus", student.Campus);
                sqlCon.Open();

                int rows = cmd.ExecuteNonQuery();
                //if the query runs succesfully then the value of the rows will be greater than zero else
                //its value will be 0
                if (rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
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

            return isSuccess;

        }

        public bool Update(Student student)
        {            
            //Creating a default return type and setting its value to false
            bool isSuccess = false;

            SqlConnection sqlCon = new SqlConnection(myconnstring);

            try
            {
                string sqlQuery = "UPDATE Students SET FirstName = @FirstName, LastName = @LastName, EmailAddress = @EmailAddress, IDNumber = @IDNumber, DateEnrolled = @DateEnrolled, Campus = @Campus WHERE StudentNo = @StudentNo";
                SqlCommand cmd = new SqlCommand(sqlQuery, sqlCon);

                //Create Parameters to add data
                cmd.Parameters.AddWithValue("@StudentNo", student.StudentNo);
                cmd.Parameters.AddWithValue("@FirstName", student.FirstName);
                cmd.Parameters.AddWithValue("@LastName", student.LastName);
                cmd.Parameters.AddWithValue("@EmailAddress", student.EmailAddress);
                cmd.Parameters.AddWithValue("@IDNumber", student.IDNumber);
                cmd.Parameters.AddWithValue("@DateEnrolled", student.DateEnrolled);
                cmd.Parameters.AddWithValue("@Campus", student.Campus);
                sqlCon.Open();

                int rows = cmd.ExecuteNonQuery();
                //if the query runs succesfully then the value of the rows will be greater than zero else
                //its value will be 0
                if (rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
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

            return isSuccess;

            
        }

        public bool Delete(Student student)
        {
            bool isSucces = false;

            string sqlquery = "DELETE FROM Students WHERE StudentNo=@StudentNo";

            SqlConnection sqlCon = new SqlConnection();        

            try
            {
                SqlCommand cmd = new SqlCommand(sqlquery, sqlCon);
                cmd.Parameters.AddWithValue("@StudentNo", student.StudentNo);              
                sqlCon.Open();
                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    isSucces = true;
                }
                else
                {
                    isSucces = false;
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

            //SqlCommand cmd = new SqlCommand();

            return isSucces;
        }

        #endregion
    }
}
