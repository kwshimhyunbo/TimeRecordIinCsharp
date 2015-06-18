/* 2015 소프트웨어실습3
 * 2009602104 김덕현
 * 
 * 이름 검색 부분 검색
 * 엔터 단축키
 * 시간계산
 * 동명이인
 * 엑셀출력-시급계산
*/

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

using Excel= Microsoft.Office.Interop.Excel;


namespace App
{
    public partial class Display : Form
    {
        string sumsum;
        double moneysum;

        public Display()
        {
            InitializeComponent();
            label10.Text = "";
            label11.Text = "";
            label12.Text = "";
            label13.Text = "";
            label14.Text = "";
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
            int l;

            string connectionString = "Server=128.134.59.89;Database=workingrecord;Uid=math;Pwd=1234;";
            string query;
            string query1;
            string query2;
            //query = "select e.empName, s.empNum, s.year, s.month, s.day, s.Go, s.Back, s.late from employee e join savedata s on e.empNum=s.empNum where e.empName='";
            query = "select e.empName, e.empBirth, e.empNum, s.year, s.month, s.day, s.Go, s.Back, s.late from employee e join savedata s on e.empNum=s.empNum where ";
            query1 = "";
            query2 = "select sec_to_time(sum(unix_timestamp(ifnull(s.Back, s.late))-unix_timestamp(s.Go))) from employee e join savedata s on e.empNum=s.empNum where ";
            //"select e.empName, s.empNum, s.year, s.month, s.day, s.Go, s.Back, s.late from employee e join savedata s on e.empNum=s.empNum where e.empName='" + textBox1.Text + "' and s.year='" + textBox2.Text + "';";
            if (textBox1.Text.CompareTo("") != 0)
            {
                query1 += "e.empName like '%"+textBox1.Text+"%'";
            }
            /*
            if ((textBox1.Text.CompareTo("") != 0) && (textBox5.Text.CompareTo("") != 0))
            {
                query1 += " and e.empBirth='" + textBox5.Text + "'";
            }
            if ((textBox1.Text.CompareTo("") == 0) && (textBox5.Text.CompareTo("") != 0))
            {
                query1 += "e.empBirth='" + textBox5.Text + "'";
            }
             * */

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

            if (((textBox1.Text.CompareTo("") != 0) || (textBox2.Text.CompareTo("") != 0) || (textBox3.Text.CompareTo("") != 0) || (textBox4.Text.CompareTo("") != 0)) && (textBox5.Text.CompareTo("") != 0))
            {
                query1 += " and e.empBirth='" + textBox5.Text + "'";
            }
            else if (((textBox1.Text.CompareTo("") == 0) && (textBox2.Text.CompareTo("") == 0) && (textBox3.Text.CompareTo("") == 0) && (textBox4.Text.CompareTo("") == 0)) && (textBox5.Text.CompareTo("") != 0))
            {
                query1 += "e.empBirth='" + textBox5.Text + "'";
            }



            if ((textBox1.Text.CompareTo("") == 0) && (textBox2.Text.CompareTo("") == 0) && (textBox3.Text.CompareTo("") == 0) && (textBox4.Text.CompareTo("") == 0) && (textBox5.Text.CompareTo("")==0))
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

            try
            {
                MySqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                sumsum = reader.GetString(0);  //Convert.ToString(reader);
                //label8.Text = sumsum;
                l = sumsum.Length;

                //label10.Text =  Convert.ToString(l);
                //int l;
                //l = sumsum.Length;
                if (l == 8)
                {
                    label11.Text = Convert.ToString(0);
                    label12.Text = sumsum.Substring(0, l - 6);
                    label13.Text = sumsum.Substring(l - 5, 2);
                    label14.Text = sumsum.Substring(l - 2, 2);
                }
                else if (l > 8)
                {
                    label11.Text = sumsum.Substring(0, l - 9);
                    label12.Text = sumsum.Substring(l - 8, 2);
                    label13.Text = sumsum.Substring(l - 5, 2);
                    label14.Text = sumsum.Substring(l - 2, 2);
                }

                moneysum = (Convert.ToInt32(label11.Text) * 24.0 + Convert.ToInt32(label12.Text) + Convert.ToDouble(label13.Text)/60.0) * 6000;
                label10.Text = Convert.ToString(moneysum);

                //label11.Text = label8.Text.Substring(0,7);
                //label11.Text = Convert.ToString(l - 1);
                reader.Close();
                DataSet ds = new DataSet();
                dataadapter.Fill(ds, "savedata");
                connection.Close();
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "savedata";
                connection.Close();
            }
            catch
            {
                MessageBox.Show("다시 입력하세요.");
            }
            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            label10.Text = "";
            label11.Text = "";
            label12.Text = "";
            label13.Text = "";
            label14.Text = "";
            //label8.Text = "";
            displayFunc();
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button4_Click(sender ,e);
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button4_Click(sender ,e);
            }
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button4_Click(sender, e);
            }
        }

        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button4_Click(sender, e);
            }
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

        private void button2_Click(object sender, EventArgs e)
        {
            

            SaveFileDialog sd = new SaveFileDialog();
            sd.Title = "Select Excel Sheet to Export or Create New !";
            sd.Filter = "Excel files (*.xls)|*.xls";
            sd.FilterIndex = 0;

            if (sd.ShowDialog() == DialogResult.OK)
            {
                Excel.Application xlApp;
                Excel.Workbook xlWorkBook;
                Excel.Worksheet xlWorkSheet;
                object misValue = System.Reflection.Missing.Value;

                Int16 i, j;

                xlApp = new Excel.Application();
                xlWorkBook = xlApp.Workbooks.Add(misValue);

                xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
                
                
                for (j=0; j <= dataGridView1.Columns.Count - 1; j++)
                {
                    xlWorkSheet.Cells[1, j+1] = dataGridView1.Columns[j].HeaderText;
                }
                 
                for (i = 0; i <= dataGridView1.RowCount - 2; i++)
                {
                    for (j = 0; j <= dataGridView1.ColumnCount - 1; j++)
                    {
                        xlWorkSheet.Cells[i + 2, j + 1] = dataGridView1[j, i].Value.ToString();

                        if ((i == dataGridView1.RowCount - 2) && (j == dataGridView1.ColumnCount - 1))
                        {
                            xlWorkSheet.Cells[i + 4, j + 1] = moneysum;
                        }
                    }
                }

                xlWorkBook.SaveAs(sd.FileName, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                xlWorkBook.Close(true, misValue, misValue);
                xlApp.Quit();

                //releaseObject(xlWorkSheet);
                //releaseObject(xlWorkBook);
                //releaseObject(xlApp);

            }



            /*
            saveFileDialog1.InitialDirectory = "C:";
            saveFileDialog1.Title = "Save as Excel File";
            saveFileDialog1.FileName = "sample";
            saveFileDialog1.Filter = "Excel Files(2003)|*.xls|Excel Files(2007)|*.xlsx";
            if (saveFileDialog1.ShowDialog() != DialogResult.Cancel)
            {
                Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
                ExcelApp.Application.Workbooks.Add(Type.Missing);

                ExcelApp.Columns.ColumnWidth = 20;

                
                for (int i = 1; i < dataGridView1.Columns.Count + 1; i++)
                {
                    ExcelApp.Cells[1, i] = dataGridView1.Columns[i - 1].HeaderText;
                }
                
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    {
                        ExcelApp.Cells[i + 2, j + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                    }
                }
                ExcelApp.ActiveWorkbook.SaveCopyAs(saveFileDialog1.FileName.ToString());
                ExcelApp.ActiveWorkbook.Saved = true;
                ExcelApp.Quit();

            }
             * */
        }

        private void textBox5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button4_Click(sender, e);
            }
        }




    }

}


