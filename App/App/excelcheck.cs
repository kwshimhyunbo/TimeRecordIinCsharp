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
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;

namespace App
{
    public partial class excelcheck : Form
    {
        public excelcheck()
        {
            InitializeComponent();
            init();
        }

        public void init()
        {
            string connectionString = "Server=128.134.59.89;Database=workingrecord;Uid=math;Pwd=1234;";

            string sql = "SELECT * FROM employee";
            MySqlConnection connection = new MySqlConnection(connectionString);
            MySqlDataAdapter dataadapter = new MySqlDataAdapter(sql, connection);
            DataSet ds = new DataSet();
            connection.Open();
            dataadapter.Fill(ds, "employee");
            connection.Close();
           
            dataGridView2.DataSource = ds;
            dataGridView2.DataMember = "employee";
            dataGridView2.Columns[0].HeaderText = "회원이름";
            dataGridView2.Columns[1].HeaderText = "회원번호";
            dataGridView2.Columns[2].HeaderText = "생년월일";
        }



        public void toExcel(String sql,String tableName,DataGridView dgv)
        {
            SaveFileDialog sd = new SaveFileDialog();
            sd.Title = "파일을 저장합니다.";
            sd.Filter = "Excel files (*.xls)|*.xls";
            sd.FilterIndex = 0;

            string connectionString = "Server=128.134.59.89;Database=workingrecord;Uid=math;Pwd=1234;";


            MySqlConnection connection = new MySqlConnection(connectionString);
            MySqlDataAdapter dataadapter = new MySqlDataAdapter(sql, connection);
            DataSet ds = new DataSet();
            connection.Open();
            dataadapter.Fill(ds, tableName);
            connection.Close();

            dgv.DataSource = ds;
            dgv.DataMember = tableName;

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

                for (i = 0; i <= dgv.RowCount - 2; i++)
                {
                    for (j = 0; j <= dgv.ColumnCount - 1; j++)
                    {
                        xlWorkSheet.Cells[i + 1, j + 1] = dgv[j, i].Value.ToString();
                    }
                }

                xlWorkBook.SaveAs(sd.FileName, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                xlWorkBook.Close(true, misValue, misValue);
                xlApp.Quit();

                releaseObject(xlWorkSheet);
                releaseObject(xlWorkBook);
                releaseObject(xlApp);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string sql = "SELECT * FROM employee";
            toExcel(sql, "employee",dataGridView2);
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

        private void excelcheck_Load(object sender, EventArgs e)
        {

        }
    }
}
