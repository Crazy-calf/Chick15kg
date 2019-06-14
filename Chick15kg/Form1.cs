﻿using Markov;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
            //List<int> data = new List<int>(){
            //6,4,4,5,2,4,6,1,2,6,  5,6,4,4,6 , 5,3,6,5,2 , 5,3,3,4,4,
            //4,1,1,1,1,3,5,6,5,5,  5,5,4,6,5 , 4,1,3,1,3 , 1,3,1,2,5,
            //2,2,5,5,1,4,4,2,6,1,  5,4,6,3,2,  2,6,4,4,4,  4,3,1,5,3,
            //1,2,6,5,3,6,3,6,4,6,  2,4,4,6,3,  3,6,2,6,1,  3,2,2,6,6,
            //4,4,3,1,4,1,2,6,4,4,  1,2};//,6,4,3,6,2,5,5,5



            //data = new int[3][] {
            //    new int[] { 0, 7, 2, 3, 7, 6, 3, 3, 8, 2, 3, 7, 4, 3,3,0,4,3,3,2,3,0,6,6,0,8 },
            //    new int[] { 3, 8, 1, 9, 8, 7, 4, 3, 4, 7, 1, 9, 7, 8,1,1,7,6,5,5,4,5,2,3,5,7 },
            //    new int[] { 1, 7, 9, 4, 9, 0, 7, 1, 9, 6, 5, 2, 1, 8,5,3,0,2,0,8,7,1,6,2,0,9 }
            //};//626

            string strRes = "";

            foreach (var item in data)
            {
                List<int> dataList = item.ToList<int>();
                var result = new DiscreteMarkov(dataList, 6, 5);

                double[] res = result.PredictValue;
                string str = "";
                List<double> rates = new List<double>();
                foreach (var item1 in res)
                {
                    rates.Add(item1);
                    strRes += " " + item1.ToString("0.00");
                }
                strRes += "\r\n";
                PreResult.Add(rates);
            }




            

            richTextBox1.AppendText(strRes);
        }
    }
}