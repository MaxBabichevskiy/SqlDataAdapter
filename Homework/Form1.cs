using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Homework
{
    public partial class Form1 : Form
    {
        private string connectionString = "Data Source=KOMPUTER\\;Initial Catalog=games;Integrated Security=True";
        private string tableName = "GamesTable";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter($"SELECT * FROM {tableName}", connection);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet, tableName);
                dataGridView1.DataSource = dataSet.Tables[tableName];
            }
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = $"INSERT INTO {tableName} (Name, Value) VALUES (@Name, @Value)";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Name", nameTextBox.Text);
                    command.Parameters.AddWithValue("@Value", valueTextBox.Text);
                    int rowsAffected = command.ExecuteNonQuery();
                    LoadData();
                    nameTextBox.Clear();
                    valueTextBox.Clear();
                }
            }
        }

        private void updateButton_Click(object sender, EventArgs e)
        {

            if (dataGridView1.SelectedRows.Count > 0)
            {
                int id = (int)dataGridView1.SelectedRows[0].Cells["Id"].Value;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {  
                    connection.Open();      
                    string sql = $"UPDATE {tableName} SET Name = @Name, Value = @Value WHERE Id = @Id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {                   
                        command.Parameters.AddWithValue("@Name", nameTextBox.Text);
                        command.Parameters.AddWithValue("@Value", valueTextBox.Text);
                        command.Parameters.AddWithValue("@Id", id);
                        int rowsAffected = command.ExecuteNonQuery();
                        LoadData();
                        nameTextBox.Clear();
                        valueTextBox.Clear();
                    }
                }
            }

        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int id = (int)dataGridView1.SelectedRows[0].Cells["Id"].Value;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = $"DELETE FROM {tableName} WHERE Id = @Id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);
                        int rowsAffected = command.ExecuteNonQuery();
                        LoadData();
                        nameTextBox.Clear();
                        valueTextBox.Clear();
                    }
                }
            }
        }
    }
}
