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

using System.IO;
namespace App
{
    public partial class 관리자창 : Form
    {
        int year = Int32.Parse(System.DateTime.Now.ToString().Substring(0, 4));
        int month = Int32.Parse(System.DateTime.Now.ToString().Substring(5, 2));
        int day = Int32.Parse(System.DateTime.Now.ToString().Substring(8, 2));
        string time = System.DateTime.Now.ToString().Substring(13, 8);
        private static string strConn = "Server=128.134.59.89;Database=workingrecord;Uid=math;Pwd=1234;";
        MySqlConnection myConnection;
        MySqlDataReader reader;
        MySqlCommand cmd;
        DataSet data;
        MySqlDataAdapter da;
        DataTable sTable;
        MySqlDataAdapter adpt;
        String logtext;
        StreamWriter fileWrite;

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

            using (MySqlConnection conn = new MySqlConnection(strConn))
            {
                //string query = "select * from employee where empName ='"+nametab.Text+"';";
                conn.Open();

                string query = "select empname, empNum from employee where empName = '"+nametab.Text +"';";
                data = new DataSet();
                adpt = new MySqlDataAdapter(query, conn);
                adpt.Fill(data, "employee");
                sTable = data.Tables["employee"];

                dataGridView2.DataSource = data.Tables["employee"];


                dataGridView2.ReadOnly = true;

                dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                conn.Close();
                 
                /* 이것은 되는 코드
                int day = Int32.Parse(System.DateTime.Now.ToString().Substring(8, 2));
                string query = "select e.empName,s.Go,s.Back,s.late from employee e join savedata s on e.empNum=s.empNum where e.empName='" + nametab.Text + "'and s.day="+ day+";";

                    data = new DataSet();
                    adpt = new MySqlDataAdapter(query, conn);
                    adpt.Fill(data, "savedata");
                    sTable = data.Tables["savedata"];

                    dataGridView2.DataSource = data.Tables["savedata"];
                    

                    dataGridView2.ReadOnly = true;

                    dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                    conn.Close();
                  */

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
                /*conn.Open();
                data = new DataSet();
                string query = "select * from employee";
                adpt = new MySqlDataAdapter(query, conn);
                adpt.Fill(data,"employee");
                sTable = data.Tables["employee"];
                dataGridView1.DataSource = data.Tables["employee"];
                dataGridView1.ReadOnly = true;
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                conn.Close();*/
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
          
            using (MySqlConnection conn = new MySqlConnection(strConn))
            {
                conn.Open();
                try
                {
                string query = "insert into employee(empNum,empName,empBirth) values(" + Int32.Parse(tab2Num.Text) + ",'" + tab2Name.Text + "'," + tab2Birth.Text+ ");";
                
                MySqlCommand cmd = new MySqlCommand(query, conn);
              
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("추가성공");
                    writer("이름 : " + tab2Name.Text + ", 생일 : " + tab2Birth.Text + ", 사원번호 : " + tab2Num.Text + " " + " 추가됨");
                }
                catch
                {
                    MessageBox.Show("실패");
                    writer("회원추가 실패함");
                }
            }
        }

        public void writer(String logtext)
        {
            logtext += "-->" + year + " " + month + " " + day + " " + time + "\r\n"; 
            fileWrite = new StreamWriter("log.txt", true);
            fileWrite.Write(logtext);
            fileWrite.Close();
            
        }


        private void button6_Click(object sender, EventArgs e)
        {
             using (MySqlConnection conn = new MySqlConnection(strConn))
            {
                conn.Open();

                string query = "delete from employee where empNum ='" + dataGridView1.SelectedRows[0].Cells[1].Value.ToString() + "';";
                
                MySqlCommand cmd = new MySqlCommand(query, conn);
                try
                {
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("삭제성공");
                    writer("회원 : " + dataGridView1.SelectedRows[0].Cells[0].Value.ToString() + " " + dataGridView1.SelectedRows[0].Cells[1].Value.ToString()  + " 삭제 성공");
                    data = new DataSet();
                    if (deleteName.Enabled)
                        query = "select empName,empNum,empBirth from employee where empName='" + deleteName.Text + "';";
                    else
                        query = "select empName,empNum,empBirth from employee where empNum='" + deleteNum.Text + "';";

                    adpt = new MySqlDataAdapter(query, conn);
                    adpt.Fill(data, "employee");
                    sTable = data.Tables["employee"];
                   
                    dataGridView1.DataSource = data.Tables["employee"];
                    dataGridView1.Columns[0].HeaderText = "회원이름";
                    dataGridView1.Columns[1].HeaderText = "회원번호";
                    dataGridView1.Columns[2].HeaderText = "생년월일";
                    dataGridView1.ReadOnly = true;
                    
                    dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                   
                    conn.Close();

                    
                }
                catch
                {
                    MessageBox.Show("실패");
                }
            
            }


        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            
            button6.Enabled = true;
            string query;
            using (MySqlConnection conn = new MySqlConnection(strConn))
            {
                conn.Open();
                data = new DataSet();
                if (deleteName.Enabled)
                    query = "select empName,empNum,empBirth from employee where empName='" + deleteName.Text + "';";
                else
                    query = "select empName,empNum,empBirth from employee where empNum='" + deleteNum.Text + "';";
                
                    adpt = new MySqlDataAdapter(query, conn);
                    adpt.Fill(data, "employee");
                    sTable = data.Tables["employee"];
                    dataGridView1.DataSource = data.Tables["employee"];
                    dataGridView1.ReadOnly = true;
                    dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    dataGridView1.Columns[0].HeaderText = "회원이름";
                    dataGridView1.Columns[1].HeaderText = "회원번호";
                    dataGridView1.Columns[2].HeaderText = "생년월일";
                    conn.Close();
            
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            deleteName.Enabled = true;
            deleteNum.Enabled = false;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            deleteName.Enabled = false;
            deleteNum.Enabled = true;
        }


      

        private void dataGridView2_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int day = Int32.Parse(System.DateTime.Now.ToString().Substring(8, 2));
            using (MySqlConnection conn = new MySqlConnection(strConn))
            {
                conn.Open();

                string query = "select empNum,Go,Back,late from savedata where empNum='" + dataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString() + "'and day=" + day + ";";
                data = new DataSet();
                adpt = new MySqlDataAdapter(query, conn);
                adpt.Fill(data, "savedata");
                sTable = data.Tables["savedata"];

                dataGridView2.DataSource = data.Tables["savedata"];


                dataGridView2.ReadOnly = false;

                dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                conn.Close();




            }
        }

        private void dataGridView2_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            int day = Int32.Parse(System.DateTime.Now.ToString().Substring(8, 2));
            using (MySqlConnection conn = new MySqlConnection(strConn))
            {
                conn.Open();

                string query = "Update savedata SET go = '" + dataGridView2.SelectedRows[0].Cells[1].Value.ToString() + "' where day = " + day + " and empNum = " + Int32.Parse(dataGridView2.SelectedRows[0].Cells[0].Value.ToString()) + ";";
                string query1 = "Update savedata SET back = '" + dataGridView2.SelectedRows[0].Cells[2].Value.ToString() + "' where day = " + day + " and empNum = " + Int32.Parse(dataGridView2.SelectedRows[0].Cells[0].Value.ToString()) + ";";
                string query2 = "Update savedata SET late = '" + dataGridView2.SelectedRows[0].Cells[3].Value.ToString() + "' where day = " + day + " and empNum = " + Int32.Parse(dataGridView2.SelectedRows[0].Cells[0].Value.ToString()) + ";";

                MySqlCommand cmd;
                
                    cmd = new MySqlCommand(query, conn);
                    cmd.ExecuteNonQuery();
                    cmd = new MySqlCommand(query1, conn);
                    cmd.ExecuteNonQuery();
                    cmd = new MySqlCommand(query2, conn);
                    cmd.ExecuteNonQuery();
                    writer("회원번호 : " + dataGridView2.SelectedRows[0].Cells[0].Value.ToString()+"의 정보가 수정됨");
                    conn.Close();
             
            }
        }

        private void nametab_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button5_Click(sender, e);
            }
        }

        private void tab2Num_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click(sender, e);
            }
        }

        private void deleteName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
              
                button3_Click_1(sender, e);
            }
        }

        
    }
}
