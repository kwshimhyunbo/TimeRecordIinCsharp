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
        private static string strConn = "Server=localhost;Database=timecard;Uid=root;Pwd=Marine;";

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
            admin app = new admin();
            app.Show();
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
                    InsertGo();
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

            foreach (DataRow r in ds.Tables[0].Rows)
            {
                MessageBox.Show(r[0].ToString());
                MessageBox.Show(r[0].ToString());
                if (r[0].ToString() == empNum.Text)
                {
                    //MessageBox.Show(empNum.Text);
                    return true;
                }
            }
            return false;
        }

        private void InsertGo()
        {
            string time = System.DateTime.Now.ToString().Substring(13, 9);
            int year = Int32.Parse(System.DateTime.Now.ToString().Substring(0, 4));
            int month = Int32.Parse(System.DateTime.Now.ToString().Substring(5, 2));
            int day = Int32.Parse(System.DateTime.Now.ToString().Substring(8, 2));


            MessageBox.Show(time);
            using (MySqlConnection conn = new MySqlConnection(strConn))
            {
                conn.Open();
                string query = "insert into savedata (empNum, year, month, day, Go) values("+ Int32.Parse(empNum.Text) +"," + year + "," + month + "," + day + ",'" + time + "');";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.ExecuteNonQuery();
            }
        }

        private void UpdateBack()
        {
            int year = Int32.Parse(System.DateTime.Now.ToString().Substring(0, 4));
            int month = Int32.Parse(System.DateTime.Now.ToString().Substring(5, 2));
            int day = Int32.Parse(System.DateTime.Now.ToString().Substring(8, 2));
            string time = System.DateTime.Now.ToString().Substring(13, 7);
            using (MySqlConnection conn = new MySqlConnection(strConn))
            {
                conn.Open();
                string query = "update savedata set Back='" + time + "';";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.ExecuteNonQuery();
            }
        }

        private void UpdateLate()
        {
            int year = Int32.Parse(System.DateTime.Now.ToString().Substring(0, 4));
            int month = Int32.Parse(System.DateTime.Now.ToString().Substring(5, 2));
            int day = Int32.Parse(System.DateTime.Now.ToString().Substring(8, 2));
            string time = System.DateTime.Now.ToString().Substring(13, 7);
            using (MySqlConnection conn = new MySqlConnection(strConn))
            {
                conn.Open();
                string query = "update savedata set Late='" + time + "' where year ="+year+" && month="+month+" day="+day+";";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
