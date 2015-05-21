using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

//exelcheck에 init함수 참고..

namespace App
{
    public partial class Display : Form
    {
        public Display()
        {
            InitializeComponent();
            displayFunc();
        }

        public void displayFunc()
        {
            string connectionString = "Server=128.134.59.89;Database=workingrecord;Uid=math;Pwd=1234;";
            string sql = "SELECT * FROM savedata";
            MySqlConnection connection = new MySqlConnection(connectionString);
            MySqlDataAdapter dataadapter = new MySqlDataAdapter(sql, connection);
            connection.Open();
            DataSet ds = new DataSet();
            dataadapter.Fill(ds, "savedata");
            connection.Close();
            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = "savedata";
            connection.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //이름을 입력하면 이름을 찾고 인덱싱 찾기.

            string connectionString = "Server=128.134.59.89;Database=workingrecord;Uid=math;Pwd=1234;";
            string query;
            //query = "select empName,empNum,empBirth from employee where empName='" + textBox3.Text + "';";
            //query = "select month,year,day,go from savedata where idx='" + textBox1.Text + "';";
            query = "select e.empName,s.empNum,s.year,s.month,s.day,s.Go,s.Back,s.late from employee e join savedata s on e.empNum=s.empNum where e.empName='" + textBox1.Text + "';";
            
            MySqlConnection connection = new MySqlConnection(connectionString);
            MySqlDataAdapter dataadapter = new MySqlDataAdapter(query, connection);
            connection.Open();
            DataSet ds = new DataSet();
            dataadapter.Fill(ds, "savedata");
            connection.Close();
            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = "savedata";
            connection.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string connectionString = "Server=128.134.59.89;Database=workingrecord;Uid=math;Pwd=1234;";
            string query;
            //query = "select empName,empNum,empBirth from employee where empName='" + textBox3.Text + "';";
            query = "select empNum,year,month,day,Go,Back,late from savedata where month='" + textBox3.Text + "';";
            MySqlConnection connection = new MySqlConnection(connectionString);
            MySqlDataAdapter dataadapter = new MySqlDataAdapter(query, connection);
            connection.Open();
            DataSet ds = new DataSet();
            dataadapter.Fill(ds, "savedata");
            connection.Close();
            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = "savedata";
            connection.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.CompareTo("") != 0)
            {
                string connectionString = "Server=128.134.59.89;Database=workingrecord;Uid=math;Pwd=1234;";
                string query;
                //query = "select empName,empNum,empBirth from employee where empName='" + textBox3.Text + "';";
                //query = "select empNum,year,month,day,Go,Back,late from savedata where year='" + textBox2.Text + "';";
                query = "select e.empName, s.empNum, s.year, s.month, s.day, s.Go, s.Back, s.late from employee e join savedata s on e.empNum=s.empNum where e.empName='" + textBox1.Text + "' and s.year='" +textBox2.Text + "';";
            
                MySqlConnection connection = new MySqlConnection(connectionString);
                MySqlDataAdapter dataadapter = new MySqlDataAdapter(query, connection);
                connection.Open();
                DataSet ds = new DataSet();
                dataadapter.Fill(ds, "savedata");
                connection.Close();
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "savedata";
                connection.Close();
            }
            else if (textBox1.Text.CompareTo("") == 0)
            {
                string connectionString = "Server=128.134.59.89;Database=workingrecord;Uid=math;Pwd=1234;";
                string query;
                //query = "select empName,empNum,empBirth from employee where empName='" + textBox3.Text + "';";
                query = "select empNum,year,month,day,Go,Back,late from savedata where year='" + textBox2.Text + "';";
                MySqlConnection connection = new MySqlConnection(connectionString);
                MySqlDataAdapter dataadapter = new MySqlDataAdapter(query, connection);
                connection.Open();
                DataSet ds = new DataSet();
                dataadapter.Fill(ds, "savedata");
                connection.Close();
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "savedata";
                connection.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string connectionString = "Server=128.134.59.89;Database=workingrecord;Uid=math;Pwd=1234;";
            string query;
            //query = "select empName,empNum,empBirth from employee where empName='" + textBox3.Text + "';";
            query = "select empNum,year,month,day,Go,Back,late from savedata where day='" + textBox4.Text + "';";
            MySqlConnection connection = new MySqlConnection(connectionString);
            MySqlDataAdapter dataadapter = new MySqlDataAdapter(query, connection);
            connection.Open();
            DataSet ds = new DataSet();
            dataadapter.Fill(ds, "savedata");
            connection.Close();
            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = "savedata";
            connection.Close();
        }

    }


}

