using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO;
using System.Runtime.InteropServices;

namespace MultiExcelToOneExcel
{
    public partial class Frm_Main : Form
    {
        private string _TempExcel = string.Empty;
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern int GetWindowThreadProcessId(IntPtr hwnd, out   int ID);   
        public Frm_Main()
        {
            InitializeComponent();
        }

        private void btn_SelectMultiExcel_Click(object sender, EventArgs e)
        {
            txt_MultiExcel.Text = "";//清空文本框
            OpenFileDialog openMultiExcel = new OpenFileDialog();//实例化打开对话框对象
         //   openMultiExcel.Filter = "Excel文件|*.xls;Excel文件|*.xlxs";//设置打开文件筛选器
            openMultiExcel.Multiselect = true;//设置打开对话框中可以多选
            if (openMultiExcel.ShowDialog() == DialogResult.OK)//判断是否选择了文件
            {
                for (int i = 0; i < openMultiExcel.FileNames.Length; i++)//遍历选择的多个文件
                    txt_MultiExcel.Text += openMultiExcel.FileNames[i] + ",";//显示选择的多个Excel文件
            }
        }

       

        private void btn_Gather_Click(object sender, EventArgs e)
        {
            try
            {
                _TempExcel = Path.GetTempPath();
                _TempExcel +=DateTime.Now.ToString("yyyyMMddHHmmssffff")+".xls";

                object miss = System.Reflection.Missing.Value;//定义object缺省值
                string[] P_str_Names = txt_MultiExcel.Text.Split(',');//存储所有选择的Excel文件名
                string P_str_Name = "";//存储遍历到的Excel文件名
                List<string> P_list_SheetNames = new List<string>();//实例化泛型集合对象，用来存储工作表名称
                Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();//实例化Excel对象[[
                excel.Visible = false;
                excel.DisplayAlerts = false;
                //打开指定的Excel文件
                Microsoft.Office.Interop.Excel.Workbook workbook = excel.Application.Workbooks.Add(true);
 
                Microsoft.Office.Interop.Excel.Worksheet newWorksheet =(Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets.Add(miss, miss, miss, miss);//创建新工作表

                for (int i = 0; i < P_str_Names.Length - 1; i++)//遍历所有选择的Excel文件名
                {
                    P_str_Name = P_str_Names[i];//记录遍历到的Excel文件名
                    //指定要复制的工作簿
                    Microsoft.Office.Interop.Excel.Workbook Tempworkbook = excel.Application.Workbooks.Open(P_str_Name, miss, miss, miss, miss, miss, miss, miss, miss, miss, miss, miss, miss, miss, miss);

                    for (int j = 0; j < Tempworkbook.Sheets.Count; j++)//遍历所有工作表
                    {
                        //指定要复制的工作表
                        Microsoft.Office.Interop.Excel.Worksheet TempWorksheet = (Microsoft.Office.Interop.Excel.Worksheet)Tempworkbook.Sheets.get_Item(j + 1); 
                        TempWorksheet.Copy(miss, newWorksheet);//将工作表内容复制到目标工作表中

                    }

                    Tempworkbook.Close(false, miss, miss);//关闭临时工作簿

                }
                ((Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets["Sheet1"]).Delete();
                ((Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets["Sheet2"]).Delete();

                workbook.SaveAs(_TempExcel, Microsoft.Office.Interop.Excel.XlFileFormat.xlExcel8, miss, miss, miss, miss, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, miss, miss, miss, miss, miss);
                workbook.Close(false, miss, miss);//关闭目标工作簿
               
                MessageBox.Show("已经将所有选择的Excel工作表汇总到了一个Excel工作表中！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                excel.Workbooks.Close();
                excel.Quit();


                IntPtr t = new IntPtr(excel.Hwnd);
                int k = 0;
                GetWindowThreadProcessId(t, out k);
                System.Diagnostics.Process p = System.Diagnostics.Process.GetProcessById(k);
                p.Kill();
                //System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                //System.Runtime.InteropServices.Marshal.ReleaseComObject(newWorksheet);
                //System.Runtime.InteropServices.Marshal.ReleaseComObject(excel.Workbooks);
                //System.Runtime.InteropServices.Marshal.ReleaseComObject(excel);
                //workbook = null;
                //newWorksheet = null;
                //excel = null;

              //  GC.Collect();  
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void btn_Browse_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_TempExcel))
            {
                return;
            }
            System.Diagnostics.Process.Start(_TempExcel);//打开选择的Excel文件
        }

        private void CloseProcess(string P_str_Process)//关闭进程
        {
            System.Diagnostics.Process[] excelProcess = System.Diagnostics.Process.GetProcessesByName(P_str_Process);//实例化进程对象
            foreach (System.Diagnostics.Process p in excelProcess)
                p.Kill();//关闭进程
            System.Threading.Thread.Sleep(10);//使线程休眠10毫秒
        }
    }
}
