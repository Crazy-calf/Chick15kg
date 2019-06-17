using Markov;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chick15kg
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public int[][] data;
        public List<List<double>> PreResult = new List<List<double>>();

        private void Button2_Click(object sender, EventArgs e)
        {
            DataTable dt = getData().Tables[0];
            #region create newTable
            DataTable resDt = new DataTable();
            DataColumn taxColumn = new DataColumn();
            taxColumn.DataType = System.Type.GetType("System.Decimal");
            taxColumn.ColumnName = "tax";
            taxColumn.Expression = "price * 0.0862";
            resDt.Columns.Add("1", Type.GetType("System.String"));
            resDt.Columns.Add("2", Type.GetType("System.String"));
            resDt.Columns.Add("3", Type.GetType("System.String"));
            resDt.Columns.Add("4", Type.GetType("System.String"));
            resDt.Columns.Add("5", Type.GetType("System.String"));
            resDt.Columns.Add("6", Type.GetType("System.String"));
            resDt.Columns.Add("7", Type.GetType("System.String"));
            resDt.Columns.Add("8", Type.GetType("System.String"));
            resDt.Columns.Add("9", Type.GetType("System.String"));
            resDt.Columns.Add("0", Type.GetType("System.String"));
            #endregion
                
            data = new int[3][];
            for (int i =0; i< 3;i++)
            {
                data[i] = new int[dt.Rows.Count];
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                data[0][i] = Convert.ToInt32(dt.Rows[i][0]);
                data[1][i] = Convert.ToInt32(dt.Rows[i][1]);
                data[2][i] = Convert.ToInt32(dt.Rows[i][2]);
            }


            string strRes = "";

            for (int i = 0; i < data.Length; i++)
            {
            
                List<int> dataList = data[i].ToList<int>();
                //TODO:算法有问题，待确认，目前灵敏度不高
                var result = new DiscreteMarkov(dataList, 10, 5);

                double[] res = result.PredictValue;
                string str = "";
                List<double> rates = new List<double>();

                DataRow newRow;
                newRow = resDt.NewRow();

                for (int j = 0; j < res.Length; j++)
                {
                    newRow[j] = res[j].ToString("0.00");
                    rates.Add(res[j]);
                    strRes += " " + res[j].ToString("0.00");
                }
                resDt.Rows.Add(newRow);
               
                strRes += "\r\n";
                PreResult.Add(rates);
            }

            dataGridView1.DataSource = resDt;
            //richTextBox1.AppendText(strRes);
        }

        //D:/project/winform/dp/tool/Chick15kg/Chick15kg/plData.xlsx
        public DataSet getData()
        {
            //打开文件
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "Excel(*.xlsx)|*.xlsx|Excel(*.xls)|*.xls";
            file.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            file.Multiselect = false;
            if (file.ShowDialog() == DialogResult.Cancel)
                return null;
            //判断文件后缀
            var path = file.FileName;
            string fileSuffix = System.IO.Path.GetExtension(path);
            if (string.IsNullOrEmpty(fileSuffix))
                return null;
            using (DataSet ds = new DataSet())
            {
                //判断Excel文件是2003版本还是2007版本
                string connString = "";
                if (fileSuffix == ".xls")
                    connString = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + path + ";" + ";Extended Properties=\"Excel 8.0;HDR=YES;IMEX=1\"";
                else
                    connString = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + path + ";" + ";Extended Properties=\"Excel 12.0;HDR=YES;IMEX=1\"";
                //读取文件
                string sql_select = " SELECT * FROM [Sheet1$P:R]";
                using (OleDbConnection conn = new OleDbConnection(connString))
                using (OleDbDataAdapter cmd = new OleDbDataAdapter(sql_select, conn))
                {
                    conn.Open();
                    cmd.Fill(ds);
                }
                if (ds == null || ds.Tables.Count <= 0) return null;
                return ds;
            }
        }
    }
}
