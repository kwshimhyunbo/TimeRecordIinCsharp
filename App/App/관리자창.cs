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
    public partial class 관리자창 : Form
    {
        private static string strConn = "Server=localhost;Database=workingrecord;Uid=root;Pwd=1234;";
        MySqlConnection myConnection;
        MySqlDataReader reader;
        MySqlCommand cmd;
        DataTable data;
        MySqlDataAdapter da;


        public 관리자창()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            using (MySqlConnection conn = new MySqlConnection(strConn))
            {   
                conn.Open();
                string query = "select * from employee";
                MySqlDataAdapter adpt = new MySqlDataAdapter(query, conn);
                adpt.Fill(ds, "employee");
            }

            foreach (DataRow r in ds.Tables[0].Rows)
            {
                if ((tab1name.Text == r[1].ToString()) && (tab1Birth.Text == r[2].ToString()))
                {
                    MessageBox.Show("정보확인");
                    getInfo();
                }
            }
         
          


        }

        public void getInfo()
        {
           
        }


        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void 관리자창_Load(object sender, EventArgs e)
        {
            using (MySqlConnection conn = new MySqlConnection(strConn))
            {
                conn.Open();
                data = new DataTable();
                string query = "select * from employee";
                MySqlDataAdapter adpt = new MySqlDataAdapter(query, conn);
                adpt.Fill(data);
                dataGrid1.DataSource = data;
            }

          
          
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
          
            using (MySqlConnection conn = new MySqlConnection(strConn))
            {
                conn.Open();
                string query = "insert into employee(empNum,empName,empBirth) values(" + Int32.Parse(tab2Num.Text) + ",'" + tab2Name.Text + "'," + tab2Birth.Text+ ");";
                
                MySqlCommand cmd = new MySqlCommand(query, conn);
                try
                {
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("추가성공");
                }
                catch
                {
                    MessageBox.Show("실패");
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DataSet mydataset = new DataSet();
            DataSet changemydataset = new DataSet();
            using (MySqlConnection conn = new MySqlConnection(strConn))
            {
                conn.Open();
                string query = "select * from employee";
                MySqlDataAdapter adpt = new MySqlDataAdapter(query, conn);
                adpt.Fill(mydataset, "employee");

                int curRow = dataGrid1.CurrentRowIndex;
                mydataset.Tables["employee"].Rows[curRow].Delete();

                dataGrid1.DataSource = mydataset.Tables["employee"].DefaultView;	
            }

          
        }
    }
}
