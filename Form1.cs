using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//This is for connecting using SQL;
using System.Data.SqlClient;

namespace CSharp_Assignment_4363
{
    public partial class Form1 : Form
    {
        ////////////////////////////////////////////////////////the string below is what connects me to the Walton back-end system
        string connectionstring = "Data Source = essql1.walton.uark.edu; Initial Catalog = ISYS4283SP22U56; User ID = ISYS4283SP22U56; Password = GohogsUA1";
        SqlConnection connection;
        SqlCommand command;
        SqlDataReader datareader;
        //////////////////////////////////////////////////////
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ////////////////////////////////////////////////////////this form load initially populates the combo box to select a student
            try
            {
                connection = new SqlConnection(connectionstring);
                connection.Open();
                string sql = "SELECT Student_Name FROM Student";
                command = new SqlCommand(sql, connection);
                datareader = command.ExecuteReader();
                while (datareader.Read())
                {
                    cmb_SelectStudent.Items.Add(datareader[0].ToString());
                }
                connection.Close();
                command.Dispose();
                datareader.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR! " + ex);
            }
            ///////////////////////////////////////////////////////
            ///
            /// 
            ////////////////////////////////////////////////////////this form load initially populates the combo box to select an instructor
            try
            {
                connection = new SqlConnection(connectionstring);
                connection.Open();
                string sql = "SELECT Instructor_Name FROM Instructor";
                command = new SqlCommand(sql, connection);
                datareader = command.ExecuteReader();
                while (datareader.Read())
                {
                    cmb_SelectInstructor.Items.Add(datareader[0].ToString());
                }
                connection.Close();
                command.Dispose();
                datareader.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR! " + ex);
            }
            ///////////////////////////////////////////////////////
            ///
            ////////////////////////////////////////////////////////this form load initially populates the combo box to select a class
            try
            {
                connection = new SqlConnection(connectionstring);
                connection.Open();
                string sql = "SELECT Class_Name FROM Class";
                command = new SqlCommand(sql, connection);
                datareader = command.ExecuteReader();
                while (datareader.Read())
                {
                    cmb_SelectClass.Items.Add(datareader[0].ToString());
                }
                connection.Close();
                command.Dispose();
                datareader.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR! " + ex);
            }

            ////////////////////////////////////////////////////////this form load initially populates the combo box to select an instructor in the class tab
            try
            {
                connection = new SqlConnection(connectionstring);
                connection.Open();
                string sql = "SELECT Instructor_Name FROM Instructor";
                command = new SqlCommand(sql, connection);
                datareader = command.ExecuteReader();
                while (datareader.Read())
                {
                    cmb_ClassSelectTeacher.Items.Add(datareader[0].ToString());
                }
                connection.Close();
                command.Dispose();
                datareader.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR! " + ex);
            }

            ////////////////////////////////////////////////////////this form load initially populates the combo box in the enroll tab
            try
            {
                connection = new SqlConnection(connectionstring);
                connection.Open();
                string sql = "SELECT Enroll_ID FROM Enroll";
                command = new SqlCommand(sql, connection);
                datareader = command.ExecuteReader();
                while (datareader.Read())
                {
                    cmb_EnrollRecord.Items.Add(datareader[0].ToString());
                }
                connection.Close();
                command.Dispose();
                datareader.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR! " + ex);
            }
            ///////////////////////////////////////////////////////
            ///
            ////////////////////////////////////////////////////////this form load initially populates the combo box to select a student in the enroll tab
            try
            {
                connection = new SqlConnection(connectionstring);
                connection.Open();
                string sql = "SELECT Student_ID FROM Student";
                command = new SqlCommand(sql, connection);
                datareader = command.ExecuteReader();
                while (datareader.Read())
                {
                    cmb_EnrollSelectStudent.Items.Add(datareader[0].ToString());
                }
                connection.Close();
                command.Dispose();
                datareader.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR! " + ex);
            }
            ///////////////////////////////////////////////////////
            ///
            ////////////////////////////////////////////////////////this form load initially populates the combo box to select a class in the enroll tab
            try
            {
                connection = new SqlConnection(connectionstring);
                connection.Open();
                string sql = "SELECT Class_ID FROM Class";
                command = new SqlCommand(sql, connection);
                datareader = command.ExecuteReader();
                while (datareader.Read())
                {
                    cmb_EnrollSelectClass.Items.Add(datareader[0].ToString());
                }
                connection.Close();
                command.Dispose();
                datareader.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR! " + ex);
            }
            ///////////////////////////////////////////////////////
        }

        private void cmb_SelectStudent_SelectedIndexChanged(object sender, EventArgs e)
        {
            ////////////////////////////////////////////////////////using the above, this translates all of the variables from the character table
            try
            {

                if (cmb_SelectStudent.SelectedIndex > -1)
                {
                    connection = new SqlConnection(connectionstring);
                    connection.Open();
                    string sql = "SELECT Student_ID, Student_Name, Major, Grade_Level, Age FROM Student WHERE Student_Name = '" + cmb_SelectStudent.SelectedItem.ToString() + "'";
                    command = new SqlCommand(sql, connection);
                    datareader = command.ExecuteReader();
                    while (datareader.Read())
                    {
                        txt_StudentID.Text = datareader[0].ToString();
                        txt_StudentName.Text = datareader[1].ToString();
                        txt_StudentMajor.Text = datareader[2].ToString();
                        txt_StudentGradeLevel.Text = datareader[3].ToString();
                        txt_StudentAge.Text = datareader[4].ToString();
                    }

                    connection.Close();
                    command.Dispose();
                    datareader.Close();

                    var sql2 = "SELECT * FROM Class as C INNER JOIN Enroll as E on E.Class_ID = C.Class_ID INNER JOIN Student as S on S.Student_ID = E.Student_ID WHERE Student_Name = '" + txt_StudentName.Text + "'";
                    var da = new SqlDataAdapter(sql2, connection);
                    var ds = new DataSet();
                    da.Fill(ds);
                    dgv_Student.DataSource = ds.Tables[0];
                }
                else
                {
                    MessageBox.Show("You must select a student.");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR! " + ex);
            }
            ///////////////////////////////////////////////////////
        }

        private void btn_NewStudent_Click(object sender, EventArgs e)
        {
            //////////the @ signs below represent scalar variables that allow us to interact with the UI
            try
            {

                connection = new SqlConnection(connectionstring);
                connection.Open();
                int answer;
                string sql = "INSERT INTO Student VALUES (@Student_Name, @Major, @Grade_Level, @Age)";
                command = new SqlCommand(sql, connection);

                command.Parameters.AddWithValue("@Student_Name", txt_StudentName.Text);
                command.Parameters.AddWithValue("@Major", txt_StudentMajor.Text);
                command.Parameters.AddWithValue("@Grade_Level", txt_StudentGradeLevel.Text);
                command.Parameters.AddWithValue("@Age", txt_StudentAge.Text);
                answer = command.ExecuteNonQuery();
                MessageBox.Show("Success! You have entered " + answer + " student.");

                connection.Close();
                command.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR! " + ex);
            }
            //////////
        }

        private void btn_DeleteStudent_Click(object sender, EventArgs e)
        {
            try
            {
                connection = new SqlConnection(connectionstring);
                connection.Open();
                int answer;
                string sql = "DELETE FROM Student WHERE Student_ID = @Student_ID";
                command = new SqlCommand(sql, connection);

                command.Parameters.AddWithValue("@Student_ID", txt_StudentID.Text);

                answer = command.ExecuteNonQuery();
                MessageBox.Show("Success! You have deleted " + answer + " student record.");

                connection.Close();
                command.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR! " + ex);
            }
        }

        private void btn_StudentUpdate_Click(object sender, EventArgs e)
        {
            /////
            try
            {
                connection = new SqlConnection(connectionstring);
                connection.Open();

                string sql = "UPDATE Student SET Student_Name=@Student_Name, Major=@Major, Grade_Level=@Grade_Level, Age=@Age WHERE Student_ID=@Student_ID";
                int answer;
                command = new SqlCommand(sql, connection);

                command.Parameters.AddWithValue("@Student_Name", txt_StudentName.Text);
                command.Parameters.AddWithValue("@Major", txt_StudentMajor.Text);
                command.Parameters.AddWithValue("@Grade_Level", txt_StudentGradeLevel.Text);
                command.Parameters.AddWithValue("@Age", txt_StudentAge.Text);
                command.Parameters.AddWithValue("@Student_ID", txt_StudentID.Text);

                answer = command.ExecuteNonQuery();

                connection.Close();
                command.Dispose();
                MessageBox.Show("Success! You have modified " + answer + " student record.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR! " + ex);
            }
            /////
        }

        private void btn_StudentRefresh_Click(object sender, EventArgs e)
        {
            /////this is the refresh button
            //how to clear a text box
            try
            {
                txt_StudentID.Text = "";
                txt_StudentName.Text = "";
                txt_StudentMajor.Text = "";
                txt_StudentGradeLevel.Text = "";
                txt_StudentAge.Text = "";

                //how to clear a combobox
                cmb_SelectStudent.SelectedIndex = -1;

                //how to clear a datagrid
                dgv_Student.DataSource = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR! " + ex);
            }
        }

        private void cmb_SelectInstructor_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                if (cmb_SelectInstructor.SelectedIndex > -1)
                {
                    connection = new SqlConnection(connectionstring);
                    connection.Open();
                    string sql = "SELECT Instructor_ID, Instructor_Name, Instructor_Email FROM Instructor WHERE Instructor_Name = '" + cmb_SelectInstructor.SelectedItem.ToString() + "'";
                    command = new SqlCommand(sql, connection);
                    datareader = command.ExecuteReader();
                    while (datareader.Read())
                    {
                        txt_InstructorID.Text = datareader[0].ToString();
                        txt_InstructorName.Text = datareader[1].ToString();
                        txt_InstructorEmail.Text = datareader[2].ToString();
                    }

                    connection.Close();
                    command.Dispose();
                    datareader.Close();

                    var sql2 = "SELECT * FROM Class as C INNER JOIN Instructor as I on I.Instructor_ID = C.Instructor_ID WHERE Instructor_Name = '" + txt_InstructorName.Text + "'";
                    var da = new SqlDataAdapter(sql2, connection);
                    var ds = new DataSet();
                    da.Fill(ds);
                    dgv_Instructor.DataSource = ds.Tables[0];
                }
                else
                {
                    MessageBox.Show("You must select an instructor.");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR! " + ex);
            }
            ///////////////////////////////////////////////////////
        }

        private void btn_NewInstructor_Click(object sender, EventArgs e)
        {
            //////////the @ signs below represent scalar variables that allow us to interact with the UI
            try
            {

                connection = new SqlConnection(connectionstring);
                connection.Open();
                int answer;
                string sql = "INSERT INTO Instructor VALUES (@Instructor_Name, @Instructor_Email)";
                command = new SqlCommand(sql, connection);

                command.Parameters.AddWithValue("@Instructor_Name", txt_InstructorName.Text);
                command.Parameters.AddWithValue("@Instructor_Email", txt_InstructorEmail.Text);
                answer = command.ExecuteNonQuery();
                MessageBox.Show("Success! You have entered " + answer + " instructor.");

                connection.Close();
                command.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR! " + ex);
            }
            //////////
        }

        private void btn_UpdateInstructor_Click(object sender, EventArgs e)
        {
            /////
            try
            {
                connection = new SqlConnection(connectionstring);
                connection.Open();

                string sql = "UPDATE Instructor SET Instructor_Name=@Instructor_Name, Instructor_Email=@Instructor_Email WHERE Instructor_ID=@Instructor_ID";
                int answer;
                command = new SqlCommand(sql, connection);

                command.Parameters.AddWithValue("@Instructor_Name", txt_InstructorName.Text);
                command.Parameters.AddWithValue("@Instructor_Email", txt_InstructorEmail.Text);
                command.Parameters.AddWithValue("@Instructor_ID", txt_InstructorID.Text);

                answer = command.ExecuteNonQuery();

                connection.Close();
                command.Dispose();
                MessageBox.Show("Success! You have modified " + answer + " instructor record.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR! " + ex);
            }
            /////
        }

        private void btn_DeleteInstructor_Click(object sender, EventArgs e)
        {
            try
            {
                connection = new SqlConnection(connectionstring);
                connection.Open();
                int answer;
                string sql = "DELETE FROM Instructor WHERE Instructor_ID = @Instructor_ID";
                command = new SqlCommand(sql, connection);

                command.Parameters.AddWithValue("@Instructor_ID", txt_InstructorID.Text);

                answer = command.ExecuteNonQuery();
                MessageBox.Show("Success! You have deleted " + answer + " instructor record.");

                connection.Close();
                command.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR! " + ex);
            }
        }

        private void btn_InstructorRefresh_Click(object sender, EventArgs e)
        {
            /////this is the refresh button
            //how to clear a text box
            try
            {
                txt_InstructorID.Text = "";
                txt_InstructorName.Text = "";
                txt_InstructorEmail.Text = "";

                //how to clear a combobox
                cmb_SelectInstructor.SelectedIndex = -1;

                //how to clear a datagrid
                dgv_Instructor.DataSource = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR! " + ex);
            }
        }

        private void cmb_SelectClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            ////////////////////////////////////////////////////////using the above, this translates all of the variables from the character table
            try
            {

                if (cmb_SelectClass.SelectedIndex > -1)
                {
                    connection = new SqlConnection(connectionstring);
                    connection.Open();
                    string sql = "SELECT Class_ID, Class_Name, Class_Time, Class_Room FROM Class WHERE Class_Name = '" + cmb_SelectClass.SelectedItem.ToString() + "'";
                    command = new SqlCommand(sql, connection);
                    datareader = command.ExecuteReader();
                    while (datareader.Read())
                    {
                        txt_ClassID.Text = datareader[0].ToString();
                        txt_ClassName.Text = datareader[1].ToString();
                        txt_ClassTime.Text = datareader[2].ToString();
                        txt_ClassRoom.Text = datareader[3].ToString();
                    }

                    connection.Close();
                    command.Dispose();
                    datareader.Close();

                    var sql2 = "SELECT * FROM Class WHERE Class_Name = '" + txt_ClassName.Text + "'";
                    var da = new SqlDataAdapter(sql2, connection);
                    var ds = new DataSet();
                    da.Fill(ds);
                    dgv_Class.DataSource = ds.Tables[0];
                }
                else
                {
                    MessageBox.Show("You must select a class.");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR! " + ex);
            }
            ///////////////////////////////////////////////////////
        }

        private void btn_CreateClass_Click(object sender, EventArgs e)
        {
            //////////the @ signs below represent scalar variables that allow us to interact with the UI
            try
            {

                connection = new SqlConnection(connectionstring);
                connection.Open();
                int answer;
                string sql = "INSERT INTO Class VALUES (@Class_Name, @Instructor_ID, @Class_Time, @Class_Room)";
                command = new SqlCommand(sql, connection);

                command.Parameters.AddWithValue("@Class_Name", txt_ClassName.Text);
                command.Parameters.AddWithValue("@Instructor_ID", txt_ClassInstructor.Text);
                command.Parameters.AddWithValue("@Class_Time", txt_ClassTime.Text);
                command.Parameters.AddWithValue("@Class_Room", txt_ClassRoom.Text);
                answer = command.ExecuteNonQuery();
                MessageBox.Show("Success! You have entered " + answer + " new class.");

                connection.Close();
                command.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR! " + ex);
            }
            //////////
        }

        private void btn_UpdateClass_Click(object sender, EventArgs e)
        {
            try
            {
                connection = new SqlConnection(connectionstring);
                connection.Open();

                string sql = "UPDATE Class SET Class_Name=@Class_Name, Class_Time=@Class_Time, Class_Room=@Class_Room WHERE Class_ID=@Class_ID";
                int answer;
                command = new SqlCommand(sql, connection);

                command.Parameters.AddWithValue("@Class_Name", txt_ClassName.Text);
                command.Parameters.AddWithValue("@Class_Time", txt_ClassTime.Text);
                command.Parameters.AddWithValue("@Class_Room", txt_ClassRoom.Text);
                command.Parameters.AddWithValue("@Class_ID", txt_ClassID.Text);

                answer = command.ExecuteNonQuery();

                connection.Close();
                command.Dispose();
                MessageBox.Show("Success! You have modified " + answer + " class record.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR! " + ex);
            }
        }

        private void btn_DeleteClass_Click(object sender, EventArgs e)
        {
            try
            {
                connection = new SqlConnection(connectionstring);
                connection.Open();
                int answer;
                string sql = "DELETE FROM Class WHERE Class_ID = @Class_ID";
                command = new SqlCommand(sql, connection);

                command.Parameters.AddWithValue("@Class_ID", txt_ClassID.Text);

                answer = command.ExecuteNonQuery();
                MessageBox.Show("Success! You have deleted " + answer + " class record.");

                connection.Close();
                command.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR! " + ex);
            }
        }

        private void btn_ClassRefresh_Click(object sender, EventArgs e)
        {
            /////this is the refresh button
            //how to clear a text box
            try
            {
                txt_ClassID.Text = "";
                txt_ClassInstructor.Text = "";
                txt_ClassName.Text = "";
                txt_ClassTime.Text = "";
                txt_ClassRoom.Text = "";

                //how to clear a combobox
                cmb_SelectClass.SelectedIndex = -1;
                cmb_ClassSelectTeacher.SelectedIndex = -1;

                //how to clear a datagrid
                dgv_Class.DataSource = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR! " + ex);
            }
        }

        private void cmb_ClassSelectTeacher_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                if (cmb_ClassSelectTeacher.SelectedIndex > -1)
                {
                    connection = new SqlConnection(connectionstring);
                    connection.Open();
                    string sql = "SELECT Instructor_Name FROM Instructor";
                    command = new SqlCommand(sql, connection);
                    datareader = command.ExecuteReader();
                    while (datareader.Read())
                    {
                        cmb_ClassSelectTeacher.Items.Add(datareader[0].ToString());
                    }
                    connection.Close();
                    command.Dispose();
                    datareader.Close();

                    connection.Open();
                    sql = "SELECT Instructor_ID FROM Instructor WHERE Instructor_Name = '" + cmb_ClassSelectTeacher.SelectedItem.ToString() + "'";
                    command = new SqlCommand(sql, connection);
                    datareader = command.ExecuteReader();
                    while (datareader.Read())
                    {
                        txt_ClassInstructor.Text = datareader[0].ToString();
                    }
                    connection.Close();
                    datareader.Close();
                    command.Dispose();
                }
                else
                {
                    MessageBox.Show("You must select an instructor.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR! " + ex);
            }
        }

        private void btn_HomeExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btn_StudentTab_Click(object sender, EventArgs e)
        {
            TabDirectory.SelectTab(tab_Student);
        }

        private void btn_InstructorTab_Click(object sender, EventArgs e)
        {
            TabDirectory.SelectTab(tab_Instructor);
        }

        private void btn_ClassesTab_Click(object sender, EventArgs e)
        {
            TabDirectory.SelectTab(tab_Classes);
        }

        private void btn_EnrollTab_Click(object sender, EventArgs e)
        {
            TabDirectory.SelectTab(tab_RemoveStudent);
        }

        private void txt_EnrollSearchClassRoster_TextChanged(object sender, EventArgs e)
        {
            connection = new SqlConnection(connectionstring);

            connection.Open();
            var sql2 = "SELECT * FROM Student as S INNER JOIN Enroll as E on E.Student_ID = S.Student_ID INNER JOIN Class as C on C.Class_ID = E.Class_ID WHERE Class_Name LIKE '%" + txt_EnrollSearchClassRoster.Text + "%'";
            var da = new SqlDataAdapter(sql2, connection);
            var ds = new DataSet();
            da.Fill(ds);
            dgv_EnrollClassRoster.DataSource = ds.Tables[0];

            connection.Close();
            datareader.Close();
            command.Dispose();
        }

        private void cmb_EnrollRecord_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                if (cmb_EnrollRecord.SelectedIndex > -1)
                {
                    connection = new SqlConnection(connectionstring);
                    connection.Open();
                    string sql = "SELECT Enroll_ID FROM Enroll WHERE Enroll_ID = '" + cmb_EnrollRecord.SelectedItem.ToString() + "'";
                    command = new SqlCommand(sql, connection);
                    datareader = command.ExecuteReader();
                    while (datareader.Read())
                    {
                        txt_EnrollID.Text = datareader[0].ToString();
                    }

                    connection.Close();
                    command.Dispose();
                    datareader.Close();

                    var sql2 = "SELECT * FROM Student as S INNER JOIN Enroll as E on E.Student_ID = S.Student_ID WHERE Enroll_ID = '" + txt_EnrollID.Text + "'";
                    var da = new SqlDataAdapter(sql2, connection);
                    var ds = new DataSet();
                    da.Fill(ds);
                    dgv_Enroll.DataSource = ds.Tables[0];
                }
                else
                {
                    MessageBox.Show("You must select an enrollment record.");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR! " + ex);
            }
        }

        private void cmb_EnrollSelectStudent_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                connection = new SqlConnection(connectionstring);
                connection.Open();
                string sql = "SELECT Student_ID from Student";
                command = new SqlCommand(sql, connection);
                datareader = command.ExecuteReader();
                while (datareader.Read())
                {
                    cmb_EnrollSelectStudent.Items.Add(datareader[0].ToString());
                }
                connection.Close();
                datareader.Close();
                command.Dispose();

                if (cmb_EnrollSelectStudent.SelectedIndex > -1)
                {
                    connection = new SqlConnection(connectionstring);
                    connection.Open();
                    sql = "SELECT Student_ID, Student_Name FROM Student WHERE Student_ID = '" + cmb_EnrollSelectStudent.SelectedItem.ToString() + "'";
                    command = new SqlCommand(sql, connection);
                    datareader = command.ExecuteReader();
                    while (datareader.Read())
                    {
                        txt_EnrollStudentID.Text = datareader[0].ToString();
                        txt_EnrollStudentName.Text = datareader[1].ToString();
                    }
                    connection.Close();
                    command.Dispose();
                    datareader.Close();

                }
                else
                {
                    MessageBox.Show("You must select a student.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR! " + ex);
            }
        }

        private void cmb_EnrollSelectClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                connection = new SqlConnection(connectionstring);
                connection.Open();
                string sql = "SELECT Class_ID from Class";
                command = new SqlCommand(sql, connection);
                datareader = command.ExecuteReader();
                while (datareader.Read())
                {
                    cmb_EnrollSelectClass.Items.Add(datareader[0].ToString());
                }
                connection.Close();
                datareader.Close();
                command.Dispose();

                if (cmb_EnrollSelectStudent.SelectedIndex > -1)
                {
                    connection.Open();
                    sql = "SELECT Class_ID, Class_Name FROM Class WHERE Class_ID = '" + cmb_EnrollSelectClass.SelectedItem.ToString() + "'";
                    command = new SqlCommand(sql, connection);
                    datareader = command.ExecuteReader();
                    while (datareader.Read())
                    {
                        txt_EnrollClassID.Text = datareader[0].ToString();
                        txt_EnrollClassName.Text = datareader[1].ToString();
                    }
                    connection.Close();
                    datareader.Close();
                    command.Dispose();
                }
                else
                {
                    MessageBox.Show("You must select a class.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR! " + ex);
            }
        }

        private void btn_EnrollStudent_Click(object sender, EventArgs e)
        {
            //////////the @ signs below represent scalar variables that allow us to interact with the UI
            try
            {

                connection = new SqlConnection(connectionstring);
                connection.Open();
                int answer;
                string sql = "INSERT INTO Enroll VALUES (@Class_ID, @Student_ID)";
                command = new SqlCommand(sql, connection);

                command.Parameters.AddWithValue("@Class_ID", txt_EnrollClassID.Text);
                command.Parameters.AddWithValue("@Student_ID", txt_EnrollStudentID.Text);
                answer = command.ExecuteNonQuery();
                MessageBox.Show("Success! You have enrolled " + answer + " student.");

                connection.Close();
                command.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR! " + ex);
            }
            //////////
        }

        private void btn_EnrollRemoveStudent_Click(object sender, EventArgs e)
        {
            try
            {
                connection = new SqlConnection(connectionstring);
                connection.Open();
                int answer;
                string sql = "DELETE FROM Enroll WHERE Enroll_ID = @Enroll_ID";
                command = new SqlCommand(sql, connection);

                command.Parameters.AddWithValue("@Enroll_ID", txt_EnrollID.Text);

                answer = command.ExecuteNonQuery();
                MessageBox.Show("Success! You have removed " + answer + " enrollment record.");

                connection.Close();
                command.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR! " + ex);
            }
        }

        private void btn_EnrollRefresh_Click(object sender, EventArgs e)
        {
            /////this is the refresh button
            //how to clear a text box
            try
            {
                txt_EnrollID.Text = "";
                txt_EnrollStudentID.Text = "";
                txt_EnrollStudentName.Text = "";
                txt_EnrollClassID.Text = "";
                txt_EnrollClassName.Text = "";
                txt_EnrollSearchClassRoster.Text = "";

                //how to clear a combobox
                cmb_EnrollRecord.SelectedIndex = -1;
                cmb_EnrollSelectStudent.SelectedIndex = -1;
                cmb_EnrollSelectClass.SelectedIndex = -1;

                //how to clear a datagrid
                dgv_Enroll.DataSource = null;
                dgv_EnrollClassRoster.DataSource = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR! " + ex);
            }
        }

        private void btn_StudentHome_Click(object sender, EventArgs e)
        {
            TabDirectory.SelectTab(tab_Home);
        }

        private void btn_InstructorHome_Click(object sender, EventArgs e)
        {
            TabDirectory.SelectTab(tab_Home);
        }

        private void btn_ClassHome_Click(object sender, EventArgs e)
        {
            TabDirectory.SelectTab(tab_Home);
        }

        private void btn_EnrollHome_Click(object sender, EventArgs e)
        {
            TabDirectory.SelectTab(tab_Home);
        }
    }
}