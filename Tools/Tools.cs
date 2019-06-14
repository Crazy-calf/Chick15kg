using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools
{
    public static class Tools
    {
        public static T[,] ConvertToArray<T>(this T[][] data)
        {
            T[,] res = new T[data.Length, data[0].Length];
            for (int i = 0; i < data.Length; i++)
            {
                for (int j = 0; j < data[i].Length; j++) res[i, j] = data[i][j];
            }
            return res;
        }
    }
}
