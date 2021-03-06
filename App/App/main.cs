﻿using System;
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
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;
namespace App
{
    public partial class Form1 : Form
    {
        StreamWriter fileWrite;

        private static string strConn = "Server=128.134.59.89;Database=workingrecord;Uid=math;Pwd=1234;";
       // private static string strConn = "Server=127.0.0.1;Database=timecard;Uid=root;Pwd=Marine;";
        private int currentYear;
        private int currentMonth;
        private string newTableName;
        private int checkCount;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            fileWrite = new StreamWriter("log.txt", true);
            fileWrite.Write("프로그램이 시작되었습니다.\r\n");
            fileWrite.Close();
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;//사진의 크기를 딱 맞는 크기로 줄여준다.
            checkCount=1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CHECKADMIN admin = new CHECKADMIN();
            DialogResult mDial = admin.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
            fileWrite = new StreamWriter("log.txt", true);
            fileWrite.Write("프로그램이 종료되었습니다.\r\n");
            fileWrite.Close();
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void MainOk_Click(object sender, EventArgs e)//확인 해서 db에 저장
        {
            int currentDay=Int32.Parse(System.DateTime.Now.ToString().Substring(8, 2));
            string pastTableName;
            
            currentYear = Int32.Parse(System.DateTime.Now.ToString().Substring(0, 4));
            currentMonth = Int32.Parse(System.DateTime.Now.ToString().Substring(5, 2));
            
            if (currentMonth < 10)
            {
                newTableName = currentYear.ToString() + "0"+currentMonth.ToString();
            }
            else
            {
                newTableName = currentYear.ToString() + currentMonth.ToString();
            }

            if (currentDay == 18 && checkCount==1)
            {
                string sql;
                if (currentMonth == 1)
                {
                    pastTableName = (currentYear - 1).ToString() + "12";
                }
                else if(currentMonth==10)
                {
                    pastTableName = currentYear.ToString() + "09";
                }
                else if (currentMonth > 10)
                {
                    pastTableName = currentYear.ToString() + (currentMonth - 1).ToString();
                }
                else
                {
                    pastTableName = currentYear.ToString() +"0" +(currentMonth - 1).ToString();
                }
                sql = "select * from " + "workingrecord." + pastTableName;
                
                try
                {
                    saveExel(sql, pastTableName);
                }catch(Exception ex){
                    MessageBox.Show("Excel 설치가 되어 있지 않습니다. 설치하고 사용하세요");
                }finally{
                    checkCount=2;
                }
            }

            if(currentDay==9){
                checkCount=1;
            }

            using (MySqlConnection conn = new MySqlConnection(strConn))
            {
                conn.Open();
                try
                {
                    string sql = "CREATE TABLE `workingrecord`.`"+newTableName+"` (`idx` INT AUTO_INCREMENT,  `empNum` INT(11) NOT NULL,  `year` INT(11) NOT NULL,  `month` INT(11) NOT NULL,  `day` INT(11) NOT NULL,   `Go` TIME ,   `Back` TIME , `late` TIME,  PRIMARY KEY (`idx`))";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                   // MessageBox.Show("create Table Error");
                }
                finally
                {
                    conn.Close();
                }
            }

            deleteTable();//2년 지난 테이블 삭제

            //string saveText;
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
                //conn.Open();
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
            string logText=" ";

            MessageBox.Show("출근 시간:"+time);

            using (MySqlConnection conn = new MySqlConnection(strConn))
            {
                conn.Open();
                string saveDataQuery = "insert into savedata (empNum, year, month, day, Go) values("+ Int32.Parse(empNum.Text) +"," + year + "," + month + "," + day + ",'" + time + "');";
                string eachTableQuery = "insert into " + "workingrecord." + newTableName.ToString() + " (empNum, year, month, day, Go) values(" + Int32.Parse(empNum.Text) + "," + year + "," + month + "," + day + ",'" + time + "');";
                
                MySqlCommand cmd = new MySqlCommand(saveDataQuery, conn);
                cmd.ExecuteNonQuery();

                MySqlCommand cmd2 = new MySqlCommand(eachTableQuery, conn);
                cmd2.ExecuteNonQuery();

                conn.Close();
            }

            logText += empNum.Text +" : "+ year + month + day + time + "에 출근하셨습니다.\r\n";
            fileWrite = new StreamWriter("log.txt", true);
            fileWrite.Write(logText);
            fileWrite.Close();
        }

        private void UpdateBack()
        {
            int year = Int32.Parse(System.DateTime.Now.ToString().Substring(0, 4));
            int month = Int32.Parse(System.DateTime.Now.ToString().Substring(5, 2));
            int day = Int32.Parse(System.DateTime.Now.ToString().Substring(8, 2));
            string time = System.DateTime.Now.ToString().Substring(13, 8);
            string logText = " ";

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
                                string eachTableUpdateQuery = "update " + "workingrecord." + newTableName + " set Back='" + time + "' where empNum=" + empNum.Text + " && year=" + year + " && month=" + month + " && day=" + day + ";";

                                MySqlCommand cmd = new MySqlCommand(updateQuery, conn);
                                cmd.ExecuteNonQuery();

                                MySqlCommand cmd2 = new MySqlCommand(eachTableUpdateQuery, conn);
                                cmd2.ExecuteNonQuery();

                                logText += empNum.Text +" :"+ year+month+day+time+" 에 퇴근하셨습니다.\r\n";
                                fileWrite = new StreamWriter("log.txt", true);
                                fileWrite.Write(logText);
                                fileWrite.Close();
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
                    string logText = "";
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
                                string eachTableUpdateQuery = "update " + "workingrecord." + newTableName + " set late='" + time + "' where empNum=" + empNum.Text + " && year=" + year + " && month=" + month + " && day=" + day + ";";
                                
                                MySqlCommand cmd = new MySqlCommand(updateQuery, conn);
                                cmd.ExecuteNonQuery();

                                MySqlCommand cmd2 = new MySqlCommand(eachTableUpdateQuery, conn);
                                cmd2.ExecuteNonQuery();

                                logText += empNum.Text + " :" + year + month + day + time + " 에 퇴근하셨습니다.\r\n";
                                fileWrite = new StreamWriter("log.txt", true);
                                fileWrite.Write(logText);
                                fileWrite.Close();
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

        private void button4_Click(object sender, EventArgs e)
        {
           excelcheck execel= new excelcheck();
           DialogResult dResult = execel.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Display display = new Display();
            DialogResult dResult = display.ShowDialog();
            //display.Show();
            
        }

        private void empNum_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                MainOk_Click(sender, e);
            }
        }

        private void deleteTable()//2년 지난 table 삭제
        {
            int pastYear;
            int pastMonth;
            MySqlDataReader reader;
            using (MySqlConnection conn = new MySqlConnection(strConn))
            {
                conn.Open();
                conn.ChangeDatabase("workingrecord");//table change
                MySqlCommand cmd = new MySqlCommand("show tables", conn);
                reader = cmd.ExecuteReader();
                try
                {
                    while (reader.Read())       //반복해서 읽기
                    {
                        pastYear=Int32.Parse(reader.GetString(0).Substring(0,4));
                        pastMonth=Int32.Parse(reader.GetString(0).Substring(4,2));

                        if (currentYear - pastYear == 2 && currentMonth - pastMonth == 0)//2년 지났는지 check
                        {
                            MessageBox.Show(reader.GetString(0));
                            string deleteTableSql = "drop table " + "workingrecord."+reader.GetString(0);
                            MySqlCommand cmd2 = new MySqlCommand(deleteTableSql, conn);
                            reader.Close();
                            cmd2.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.ToString());
                    //MessageBox.Show("delete error");
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        private void saveExel(String sql,String tableName)//,DataGridView dgv)
        {
            MySqlConnection connection = new MySqlConnection(strConn);
            MySqlDataAdapter dataadapter = new MySqlDataAdapter(sql, connection);
            DataSet ds = new DataSet();
            connection.Open();
            dataadapter.Fill(ds, tableName);
            connection.Close();
          
            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;

            xlApp = new Excel.Application();
            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

            int i = 0;


            foreach (DataRow r in ds.Tables[0].Rows)
            {
                
                for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                {
                    xlWorkSheet.Cells[1, j + 1] = ds.Tables[0].Columns[j].ToString();
                    xlWorkSheet.Cells[i + 2, j + 1] = r[j].ToString();
                }
                i++;
            }

            xlWorkBook.SaveAs(tableName, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            xlWorkBook.Close(true, misValue, misValue);
            xlApp.Quit();

            releaseObject(xlWorkSheet);
            releaseObject(xlWorkBook);
            releaseObject(xlApp);
            
        }

        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Exception Occured while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }
    }
}
