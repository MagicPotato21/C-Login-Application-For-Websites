using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Get the username and password from the text boxes
            string username = textBox1.Text;
            string password = textBox2.Text;

            // Connect to the database and verify the username and password
            string connectionString = "Server=localhost;Database=test;Uid=root123;Pwd=test;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    if (connection.State == ConnectionState.Open)
                    {
                        // Connection was successful, check the username and password
                        string usernameQuery = "SELECT COUNT(*) FROM usernames WHERE username = @username";
                        string passwordQuery = "SELECT COUNT(*) FROM passwords WHERE password = @password";

                        using (SqlCommand usernameCommand = new SqlCommand(usernameQuery, connection))
                        {
                            usernameCommand.Parameters.AddWithValue("@username", username);
                            int usernameCount = (int)usernameCommand.ExecuteScalar();

                            using (SqlCommand passwordCommand = new SqlCommand(passwordQuery, connection))
                            {
                                passwordCommand.Parameters.AddWithValue("@password", password);
                                int passwordCount = (int)passwordCommand.ExecuteScalar();

                                if (usernameCount == 1 && passwordCount == 1)
                                {
                                    // Login is successful, show form 3
                                    Form3 form3 = new Form3();
                                    form3.Show();
                                    this.Hide();
                                }
                                else
                                {
                                    // Login is unsuccessful, show error message
                                    MessageBox.Show("Invalid username or password");
                                }
                            }
                        }
                    }
                    else
                    {
                        // Connection was unsuccessful
                        MessageBox.Show("Error connecting to database");
                    }
                }
                catch (SqlException ex)
                {
                    // There was an error connecting to the database
                    MessageBox.Show("Error connecting to database: " + ex.Message);
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}