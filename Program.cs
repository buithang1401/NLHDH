using System;
using System.Text.RegularExpressions;

public class GFG
{

    static int[,] mat = new int[10, 6];

    static void arrangeArrival(int num, int[,] mat)
    {
        for (int i = 0; i < num; i++)
        {
            for (int j = 0; j < num - i - 1; j++)
            {
                if (mat[j, 1] > mat[j + 1, 1])
                {
                    for (int k = 0; k < 5; k++)
                    {
                        int temp = mat[j, k];
                        mat[j, k] = mat[j + 1, k];
                        mat[j + 1, k] = temp;
                    }
                }
            }
        }
    }

    static void completionTime(int num, int[,] mat)
    {
        int temp, val = -1;
        mat[0, 3] = mat[0, 1] + mat[0, 2];
        mat[0, 5] = mat[0, 3] - mat[0, 1];
        mat[0, 4] = mat[0, 5] - mat[0, 2];

        for (int i = 1; i < num; i++)
        {
            temp = mat[i - 1, 3];
            int low = mat[i, 2];
            for (int j = i; j < num; j++)
            {
                if (temp >= mat[j, 1] && low >= mat[j, 2])
                {
                    low = mat[j, 2];
                    val = j;
                }
            }
            mat[val, 3] = temp + mat[val, 2];
            mat[val, 5] = mat[val, 3] - mat[val, 1];
            mat[val, 4] = mat[val, 5] - mat[val, 2];
            for (int k = 0; k < 6; k++)
            {
                int tem = mat[val, k];
                mat[val, k] = mat[i, k];
                mat[i, k] = tem;
            }
        }
    }

    // Driver Code
    static public void Main()
    {

        int num;
        String line;

        // count program
        string s = File.ReadAllText(@"D:\C#\LapLichCPU\input.txt");
        Regex r = new Regex("\n", RegexOptions.Multiline);
        MatchCollection mc = r.Matches(s);
        num = mc.Count + 1;

        // read file
        StreamReader sr = new StreamReader(@"D:\C#\LapLichCPU\input.txt");
        for (int i = 0; i < num; i++)
        {
            line = sr.ReadLine();
            String[] item = line.Split("-");

            mat[i, 0] = Convert.ToInt32(item[0]);
            mat[i, 1] = Convert.ToInt32(item[1]);
            mat[i, 2] = Convert.ToInt32(item[2]);
        }

        Console.WriteLine("Before Arrange...");
        Console.WriteLine(
            "Process ID\tArrival Time\tBurst Time");
        for (int i = 0; i < num; i++)
        {
            Console.WriteLine(mat[i, 0] + "\t\t" + mat[i, 1]
                              + "\t\t" + mat[i, 2]);
        }

        arrangeArrival(num, mat);
        completionTime(num, mat);
        Console.WriteLine("Final Result...");
        Console.WriteLine(
            "Process ID\tArrival Time\tBurst"
            + " Time\tWaiting Time\tTurnaround Time");
        for (int i = 0; i < num; i++)
        {
            Console.WriteLine(mat[i, 0] + "\t\t" + mat[i, 1]
                              + "\t\t" + mat[i, 2] + "\t\t"
                              + mat[i, 4] + "\t\t"
                              + mat[i, 5]);
        }
    }
}
