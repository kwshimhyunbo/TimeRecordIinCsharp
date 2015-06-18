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
            string query1;
            string query2;
            //query = "select e.empName, s.empNum, s.year, s.month, s.day, s.Go, s.Back, s.late from employee e join savedata s on e.empNum=s.empNum where e.empName='";
            query = "select e.empName, s.empNum, s.year, s.month, s.day, s.Go, s.Back, s.late from employee e join savedata s on e.empNum=s.empNum where ";
            query1 = "";
            query2 = "select sec_to_time(sum(unix_timestamp(ifnull(s.Back, s.late))-unix_timestamp(s.Go))) from employee e join savedata s on e.empNum=s.empNum where ";
            //"select e.empName, s.empNum, s.year, s.month, s.day, s.Go, s.Back, s.late from employee e join savedata s on e.empNum=s.empNum where e.empName='" + textBox1.Text + "' and s.year='" + textBox2.Text + "';";
            if (textBox1.Text.CompareTo("") != 0)
            {
                query1 += "e.empName='"+textBox1.Text+"'";
            }
            
            if ((textBox1.Text.CompareTo("")!=0) && (textBox2.Text.CompareTo("") != 0))
            {
                query1 += " and s.year='" +textBox2.Text+"'";
            }
            else if ((textBox1.Text.CompareTo("") == 0) && (textBox2.Text.CompareTo("") != 0))
            {
                query1 += "s.year='" + textBox2.Text + "'";
            }

            if (((textBox1.Text.CompareTo("") != 0) || (textBox2.Text.CompareTo("") != 0)) && (textBox3.Text.CompareTo("") != 0))
            {
                query1 += " and s.month='" + textBox3.Text + "'";
            }
            else if ((textBox1.Text.CompareTo("") == 0) && (textBox2.Text.CompareTo("") == 0) && (textBox3.Text.CompareTo("") != 0))
            {
                query1 += "s.month='" + textBox3.Text + "'";
            }

            if (((textBox1.Text.CompareTo("") != 0) || (textBox2.Text.CompareTo("") != 0) || (textBox3.Text.CompareTo("") !=0)) && (textBox4.Text.CompareTo("") != 0))
            {
                query1 += " and s.day='" + textBox4.Text + "'";
            }
            else if (((textBox1.Text.CompareTo("") == 0) && (textBox2.Text.CompareTo("") == 0) && (textBox3.Text.CompareTo("") == 0)) && (textBox4.Text.CompareTo("") != 0))
            {
                query1 += "s.day='" + textBox4.Text + "'";
            }

            if ((textBox1.Text.CompareTo("") == 0) && (textBox2.Text.CompareTo("") == 0) && (textBox3.Text.CompareTo("") == 0) && (textBox4.Text.CompareTo("") == 0))
            {
                query = "select * from savedata";
                query2 = "select 0";
                //label8.Text = Convert.ToString('0');
            }
            query1 += ";";
            query += query1;

            query2 += query1;
            
            //MessageBox.Show(query2);
            
            MySqlConnection connection = new MySqlConnection(connectionString);
            MySqlDataAdapter dataadapter = new MySqlDataAdapter(query, connection);

            connection.Open();

            MySqlCommand cmd = new MySqlCommand(query2, connection);
            MySqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            label8.Text = reader.GetString(0);  //Convert.ToString(reader);
            reader.Close();

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

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            label8.Text = "";
            displayFunc();
        }

        private void button2_Click(object sender, EventArgs e)
        {
          
        }

    }

}


