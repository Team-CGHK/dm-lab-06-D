using System;
using System.Collections.Generic;
using System.IO;

namespace DiscreteMathLab6_D
{
    class Program
    {
        static StreamReader sr = new StreamReader("nextsetpartition.in");
        static StreamWriter sw = new StreamWriter("nextsetpartition.out");

        static void Main(string[] args)
        {
            int n = 0, b = 0;
            do
            {
                string[] line = sr.ReadLine().Split(' ');
                n = int.Parse(line[0]);
                if (n != 0)
                {
                    List<int>[] subsets = new List<int>[int.Parse(line[1])];
                    for (int i = 0; i < subsets.Length; i++)
                    {
                        subsets[i] = new List<int>();
                        line = sr.ReadLine().Split(' ');
                        for (int j = 0; j < line.Length; j++)
                            subsets[i].Add(int.Parse(line[j]));
                    }
                    sr.ReadLine();
                    bool[] pool = new bool[n + 1];
                    bool result = false;
                    for (int i = subsets.Length - 1; i >= 0 && !result; i--)
                    {
                        foreach (int a in subsets[i])
                            pool[a] = true;
                        bool add_after_last = false;
                        for (int k = subsets[i].Count - 1; k >= 0 && !result; k--)
                        {
                            for (int j = subsets[i][k] + 1; j <= n && !result; j++)
                                if (pool[j] && !(k == 0 && subsets[i].Count>1))
                                {
                                    result = true;
                                    pool[j] = false;
                                    for (int l = 0; l < k; l++)
                                        pool[subsets[i][l]] = false;
                                    if (k == subsets[i].Count - 1)
                                    {
                                        pool[subsets[i][k]] = false;
                                        subsets[i].Add(j);
                                    }
                                    else
                                    {
                                        subsets[i][k] = j;
                                        subsets[i].RemoveRange(k + 1, subsets[i].Count - k - 1);
                                    }
                                    int c = 0;
                                    for (int m = 0; m <= n; m++)
                                        if (pool[m]) c++;
                                    sw.WriteLine(n + " " + (i + c + 1));
                                    for (int m = 0; m <= i; m++)
                                    {
                                        for (int o = 0; o < subsets[m].Count; o++)
                                            sw.Write(subsets[m][o] + " ");
                                        sw.WriteLine();
                                    }
                                    for (int m = 0; m <= n; m++)
                                    {
                                        if (pool[m])
                                            sw.WriteLine(m);
                                    }
                                    sw.WriteLine();
                                }
                        }
                    }
                    if (!result)
                    {
                        sw.WriteLine(n + " " + n);
                        for (int i = 1; i <= n; i++)
                            sw.WriteLine(i);
                    }
                }
            } while (n != 0);
            sw.Close();
        }
    }
}
