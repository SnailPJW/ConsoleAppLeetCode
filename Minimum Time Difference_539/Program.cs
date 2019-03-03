using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minimum_Time_Difference_539
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("----------- 最小時間差計算測試 ------------");
            Console.WriteLine("--------- 格式 [ 00:00 - 23:59 ] ---------");
            bool bFinished = false;
            List<string> timePoints = new List<string>();
            do
            {
                Console.Write("請輸入時間點 (結束請按 [Esc])：");
                string currentPoint = ReadLineWithCancel();
                if (currentPoint.Equals("t"))
                {
                    bFinished = true;
                }
                else
                {
                    Console.WriteLine();
                    timePoints.Add(currentPoint);
                }
            } while (!bFinished);

            Console.WriteLine("\n----------- 計算結果 ------------");
            //foreach(string element in timePoints)
            //{
            //    Console.WriteLine(element);
            //}
            Console.WriteLine($"Output : {FindMinDifference(timePoints)}");
            Console.ReadLine();
        }
        public static int FindMinDifference(IList<string> timePoints)
        {
            List<string[]> arrayList = new List<string[]>();
            foreach (string point in timePoints)
            {
                arrayList.Add(point.Split(':'));
            }

            bool conversionSuccessful;
            int number;
            List<int> valueList = new List<int>();
            foreach (string[] elements in arrayList)
            {
                foreach (string element in elements)
                {
                    conversionSuccessful = int.TryParse(element, out number);
                    if (conversionSuccessful)
                    {
                        valueList.Add(number);
                    }
                }
            }
            const int halfMaxValue = 720, maxValue = 1440;
            int[] Nums = new int[valueList.Count / 2];
            int minuValue = maxValue, count = 0;
            for (int i = 0; i < valueList.Count; i += 2)
            {
                Nums[count++] = valueList[i] * 60 + valueList[i + 1];
            }
            for (int i = 0; i < count - 1; i++)
            {
                for (int j = i + 1; j < count; j++)
                {
                    if (Nums[i] < Nums[j])
                    {
                        int temp = Nums[i];
                        Nums[i] = Nums[j];
                        Nums[j] = temp;
                    }
                    if (minuValue > Nums[i] - Nums[j])
                    {
                        minuValue = Nums[i] - Nums[j];
                        if (minuValue > halfMaxValue)
                        {
                            minuValue = maxValue - minuValue;
                        }
                    }
                }
            }

            return minuValue;
            // 00:00   12:30    >>>     12*60 + 30 = 750
            // 12:30   23:59    >>>     23*60 + 59 = 1439
            // 23:59   00:00    >>>     
            // 24:00            >>>     24*60 = 1440
            // 1440 / 2 = 720   >>>     720 / 60 = 12
        }
        private static string ReadLineWithCancel()
        {
            string result = null;

            StringBuilder buffer = new StringBuilder();

            //The key is read passing true for the intercept argument to prevent
            //any characters from displaying when the Escape key is pressed.
            ConsoleKeyInfo info = Console.ReadKey(true);
            while (info.Key != ConsoleKey.Enter && info.Key != ConsoleKey.Escape)
            {
                Console.Write(info.KeyChar);
                buffer.Append(info.KeyChar);
                info = Console.ReadKey(true);
            }

            if (info.Key == ConsoleKey.Enter)
            {
                result = buffer.ToString();
            }
            else if(info.Key == ConsoleKey.Escape)
            {
                result = "t";
            }

            return result;
        }
    }
}
