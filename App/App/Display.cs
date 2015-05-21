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

        private void button4_Click(object sender, EventArgs e)
        {
            string connectionString = "Server=128.134.59.89;Database=workingrecord;Uid=math;Pwd=1234;";
            string query;

            //query = "select e.empName, s.empNum, s.year, s.month, s.day, s.Go, s.Back, s.late from employee e join savedata s on e.empNum=s.empNum where e.empName='";
            query = "select e.empName, s.empNum, s.year, s.month, s.day, s.Go, s.Back, s.late from employee e join savedata s on e.empNum=s.empNum where ";
            //"select e.empName, s.empNum, s.year, s.month, s.day, s.Go, s.Back, s.late from employee e join savedata s on e.empNum=s.empNum where e.empName='" + textBox1.Text + "' and s.year='" + textBox2.Text + "';";
            if (textBox1.Text.CompareTo("") != 0)
            {
                query += "e.empName='"+textBox1.Text+"'";
            }
            
            if ((textBox1.Text.CompareTo("")!=0) && (textBox2.Text.CompareTo("") != 0))
            {
                query += " and s.year='" +textBox2.Text+"'";
            }
            else if ((textBox1.Text.CompareTo("") == 0) && (textBox2.Text.CompareTo("") != 0))
            {
                query += "s.year='" + textBox2.Text + "'";
            }

            if (((textBox1.Text.CompareTo("") != 0) || (textBox2.Text.CompareTo("") != 0)) && (textBox3.Text.CompareTo("") != 0))
            {
                query += " and s.month='" + textBox3.Text + "'";
            }
            else if ((textBox1.Text.CompareTo("") == 0) && (textBox2.Text.CompareTo("") == 0) && (textBox3.Text.CompareTo("") != 0))
            {
                query += "s.month='" + textBox3.Text + "'";
            }

            if (((textBox1.Text.CompareTo("") != 0) || (textBox2.Text.CompareTo("") != 0) || (textBox3.Text.CompareTo("") !=0)) && (textBox4.Text.CompareTo("") != 0))
            {
                query += " and s.day='" + textBox4.Text + "'";
            }
            else if (((textBox1.Text.CompareTo("") == 0) && (textBox2.Text.CompareTo("") == 0) && (textBox3.Text.CompareTo("") == 0)) && (textBox4.Text.CompareTo("") != 0))
            {
                query += "s.day='" + textBox4.Text + "'";
            }

            if ((textBox1.Text.CompareTo("") == 0) && (textBox2.Text.CompareTo("") == 0) && (textBox3.Text.CompareTo("") == 0) && (textBox4.Text.CompareTo("") == 0))
            {
                query = "select * from savedata";
            }
            query += ";";

            //MessageBox.Show(query);
            
            MySqlConnection connection = new MySqlConnection(connectionString);
            MySqlDataAdapter dataadapter = new MySqlDataAdapter(query, connection);
            connection.Open();
            DataSet ds = new DataSet();
            dataadapter.Fill(ds, "savedata");
            connection.Close();
            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = "savedata";
            connection.Close();

            /*
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
             * */
        }


    }

}


