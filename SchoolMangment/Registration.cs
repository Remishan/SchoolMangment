using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SchoolMangment
{
    public partial class Registration : Form
    {
        SqlConnection connection;
        SqlCommand command;
        public Registration()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            numberoflist();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string gender;
            if(rbtnFemale.Checked==true)
            {
                gender = "F";

            }
            else
            {
                gender = "M";
            }
            SqlConnection connection = new SqlConnection(@"Server=DESKTOP-C0MLP28\SQLEXPRESS;Database=CricketTeamManagement;Trusted_Connection=true;");
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "PlayerData";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("Name", txtName.Text);
            command.Parameters.AddWithValue("Gender",gender);
            command.Parameters.AddWithValue("Email",txtEmail.Text);
            command.Parameters.AddWithValue("Phone", txtPhone.Text);
            command.Parameters.AddWithValue("Salary", Convert.ToInt32(txtSalary.Text));
            command.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show("Done");

            numberoflist();
            txtName.Clear();
            txtPhone.Clear();
            txtSalary.Clear();
            txtEmail.Clear();

        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            string teamId="";
            string name="";
            string gender="";
            string Phone="";
            string email="";
            string salary="";
            if (txtId.Text=="")
            {
                MessageBox.Show("please enter id");
            }
            else
            {
                connection = new SqlConnection(@"Server=DESKTOP-C0MLP28\SQLEXPRESS;Database=CricketTeamManagement;Trusted_Connection=true;");
                connection.Open();
                command = connection.CreateCommand();
                command.CommandText = "PlayerList";
                command.CommandType = CommandType.StoredProcedure;
               // command.Parameters.AddWithValue("Id", txtId.Text);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                       teamId = reader["Id"].ToString();
                     name = reader["Name"].ToString();
                     gender = reader["Gender"].ToString();
                     Phone = reader["Phone"].ToString();
                     email = reader["Email"].ToString();
                     salary = reader["Salary"].ToString();
                    if (txtId.Text == teamId)
                        break;
                }
                if (txtId.Text == teamId)
                {
                    txtInformation.AppendText("User Information" + "\r\n" + ".............." + "\r\n" + "Name:" + name + "\r\n" + "Gender:" + gender + "\r\n" + "Phone number:" + Phone + "\r\n" + "email id:" + email + "\r\n" + "Salary:" + salary);
                    
                }
                else
                {
                    MessageBox.Show("Not Found");
                }
               // txtInformation.Clear();

            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
        public void numberoflist()
        {
            connection = new SqlConnection(@"Server=DESKTOP-C0MLP28\SQLEXPRESS;Database=CricketTeamManagement;Trusted_Connection=true;");
            connection.Open();
            command = connection.CreateCommand();
            command.CommandText = "TeamCount";
            command.CommandType = CommandType.StoredProcedure;
          int count=Convert.ToInt32( command.ExecuteScalar());
            lblCount.Text = count.ToString();
        }
    }
}
