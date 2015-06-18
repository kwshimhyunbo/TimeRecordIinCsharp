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
    public partial class CHECKADMIN : Form
    {
        int year = Int32.Parse(System.DateTime.Now.ToString().Substring(0, 4));
        int month = Int32.Parse(System.DateTime.Now.ToString().Substring(5, 2));
        int day = Int32.Parse(System.DateTime.Now.ToString().Substring(8, 2));
        string time = System.DateTime.Now.ToString().Substring(13, 8);
        StreamWriter fileWrite;
        private static string strConn = "Server=128.134.59.89;Database=workingrecord;Uid=math;Pwd=1234;";
        public CHECKADMIN()
        {
            InitializeComponent();
        }
        string id;
        string pass;
        private void button1_Click(object sender, EventArgs e)
        {
            id = checktextbox1.Text;
           pass =  checktextbox2.Text;
                
            using (MySqlConnection conn = new MySqlConnection(strConn))
            {
                conn.Open();

                

                string query = "SELECT id,password FROM admin where id ='" + id +"' and password ='" +pass+"';";
                string str = "SELECT id,password FROM admin where id = 'admin' and password = '1234';";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    관리자창 page = new 관리자창();
                    this.Opacity = 0;
                    writer("관리자 로그인 성공");
                     DialogResult mDial = page.ShowDialog();
                     this.Close();
                }
                else
                {
                    MessageBox.Show("로그인 실패");
                    writer("관리자 로그인 실패");
                }
               


                conn.Close();


            }
        }

        public void writer(String logtext)
        {
            logtext += "-->" + year + " " + month + " " + day + " " + time + "\r\n"; 
            fileWrite = new StreamWriter("log.txt", true);
            fileWrite.Write(logtext);
            fileWrite.Close();
      
        }

        private void checktextbox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click(sender, e);
            }
        }
    }
}
