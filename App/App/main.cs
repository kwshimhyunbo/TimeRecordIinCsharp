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
    public partial class Form1 : Form
    {
        private static string strConn = "Server=localhost;Database=workingrecord;Uid=root;Pwd=1234;";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;//사진의 크기를 딱 맞는 크기로 줄여준다.
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new 관리자창().Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void MainOk_Click(object sender, EventArgs e)//확인 해서 db에 저장
        {
            if (checkEmployee())
            {
                if (radioGo.Checked)
                {
                    if(checkAlreadyGo()){
                        MessageBox.Show("이미 출근하셨습니다");
                    }//값이 이미 들어가 있는경우 좀 생각해보자
                    else{
                        InsertGo();
                    }
                }
                else if (radioBack.Checked)
                {
                     UpdateBack();   
                }
                else if (radioLate.Checked)
                {
                     UpdateLate();
                }
            }
            else
            {
                MessageBox.Show("근로자가 아닙니다");
            }

        }

        private bool checkEmployee()
        {
            DataSet ds = new DataSet();

            using (MySqlConnection conn = new MySqlConnection(strConn))
            {
                string sql = "SELECT empNum from employee";
                MySqlDataAdapter adpt = new MySqlDataAdapter(sql, conn);
                adpt.Fill(ds, "employee");
            }

            //MessageBox.Show(ds.Tables[0].Rows.ToString());

            foreach (DataRow r in ds.Tables[0].Rows)
            {
                if (r[0].ToString() == empNum.Text)
                {
                    MessageBox.Show(empNum.Text);
                    return true;
                }
            }
            ds.Clear();
            ds.Dispose();
            ds.Reset();
            return false;
        }

        private bool checkAlreadyGo()
        {
            int year = Int32.Parse(System.DateTime.Now.ToString().Substring(0, 4));
            int month = Int32.Parse(System.DateTime.Now.ToString().Substring(5, 2));
            int day = Int32.Parse(System.DateTime.Now.ToString().Substring(8, 2));

            DataSet ds=new DataSet();

            using (MySqlConnection conn = new MySqlConnection(strConn))
            {
                string sql = "SELECT * from savedata";
                MySqlDataAdapter adpt = new MySqlDataAdapter(sql, conn);
                adpt.Fill(ds, "savedata");
                conn.Close();
            }

            foreach (DataRow r in ds.Tables[0].Rows)
            {
                //MessageBox.Show(r[5].ToString());
                if (empNum.Text ==r[1].ToString() && r[2].ToString()==year.ToString() && r[3].ToString()==month.ToString() && r[4].ToString()==day.ToString() && r[5].ToString() !=null )
                {
                    //MessageBox.Show(empNum.Text);
                    
                    return true;
                }
            }
            ds.Clear();
            ds.Dispose();
            ds.Reset();
            return false;
        }

        private void InsertGo()
        {
            string time = System.DateTime.Now.ToString().Substring(13, 8);
            int year = Int32.Parse(System.DateTime.Now.ToString().Substring(0, 4));
            int month = Int32.Parse(System.DateTime.Now.ToString().Substring(5, 2));
            int day = Int32.Parse(System.DateTime.Now.ToString().Substring(8, 2));

            MessageBox.Show("출근 시간:"+time);

            using (MySqlConnection conn = new MySqlConnection(strConn))
            {
                conn.Open();
                string query = "insert into savedata (empNum, year, month, day, Go) values("+ Int32.Parse(empNum.Text) +"," + year + "," + month + "," + day + ",'" + time + "');";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            
        }

        private void UpdateBack()
        {
            int year = Int32.Parse(System.DateTime.Now.ToString().Substring(0, 4));
            int month = Int32.Parse(System.DateTime.Now.ToString().Substring(5, 2));
            int day = Int32.Parse(System.DateTime.Now.ToString().Substring(8, 2));
            string time = System.DateTime.Now.ToString().Substring(13, 8);

            DataSet ds = new DataSet();

            using (MySqlConnection conn = new MySqlConnection(strConn))
            {
                conn.Open();
                string selectSql = "SELECT * from savedata";
                MySqlDataAdapter adpt = new MySqlDataAdapter(selectSql, conn);
                adpt.Fill(ds, "savedata");

                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    if (r[7].GetType().ToString() == "System.DBNull")
                    {
                        if (empNum.Text == r[1].ToString() && r[2].ToString() == year.ToString() && r[3].ToString() == month.ToString() && r[4].ToString() == day.ToString())
                        {
                            if (r[6].GetType().ToString() != "System.DBNull")//MessageBox.Show(r[6].ToString()+"hi");
                            {
                                MessageBox.Show("이미 퇴근 하셨습니다.");
                            }
                            else
                            {
                                MessageBox.Show("퇴근 시간:" + time);
                                string updateQuery = "update savedata set Back='" + time + "' where empNum=" + empNum.Text + " && year=" + year + " && month=" + month + " && day=" + day + ";";
                                MySqlCommand cmd = new MySqlCommand(updateQuery, conn);
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                    else
                    {
                        if (empNum.Text == r[1].ToString() && r[2].ToString() == year.ToString() && r[3].ToString() == month.ToString() && r[4].ToString() == day.ToString())
                        {
                            MessageBox.Show("이미 잔업퇴근 하셨습니다.");
                        }
                    }
                    //MessageBox.Show(r[6].GetType().ToString());
                }
            }
        }

        private void UpdateLate()
        {
            int year = Int32.Parse(System.DateTime.Now.ToString().Substring(0, 4));
            int month = Int32.Parse(System.DateTime.Now.ToString().Substring(5, 2));
            int day = Int32.Parse(System.DateTime.Now.ToString().Substring(8, 2));
            string time = System.DateTime.Now.ToString().Substring(13, 8);
            DataSet ds = new DataSet();

            using (MySqlConnection conn = new MySqlConnection(strConn))
            {
                conn.Open();
                string selectSql = "SELECT * from savedata";
                
               
                MySqlDataAdapter adpt = new MySqlDataAdapter(selectSql, conn);
                adpt.Fill(ds, "savedata");
                
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    //MessageBox.Show(r[6].ToString());
                    if (r[6].GetType().ToString() == "System.DBNull")
                    {
                        if (empNum.Text == r[1].ToString() && r[2].ToString() == year.ToString() && r[3].ToString() == month.ToString() && r[4].ToString() == day.ToString())
                        {
                            // MessageBox.Show(empNum.Text);
                            if (r[7].GetType().ToString() != "System.DBNull")
                            {
                                MessageBox.Show("이미 잔업퇴근 하셨습니다.");
                            }
                            else
                            {
                                MessageBox.Show("잔업퇴근 시간:" + time);
                                string updateQuery = "update savedata set late='" + time + "' where empNum=" + empNum.Text + " && year=" + year + " && month=" + month + " && day=" + day + ";";
                                MySqlCommand cmd = new MySqlCommand(updateQuery, conn);
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                    else
                    {
                        if (empNum.Text == r[1].ToString() && r[2].ToString() == year.ToString() && r[3].ToString() == month.ToString() && r[4].ToString() == day.ToString())
                        {
                            MessageBox.Show("이미 퇴근하셨습니다.");
                        }
                    }
                }
            }
        }

        private void empNum_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
