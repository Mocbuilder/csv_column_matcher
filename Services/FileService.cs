using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csv_column_matcher.Services
{
    public static class FileService
    {
        public static List<string[]> ReadAllLinesToArray(string filePath) 
        {
            string[] lines = File.ReadAllLines(filePath);
            List<string[]> result = new List<string[]>();
            foreach (string line in lines)
            {
                string[] temp = line.Split(",");
                result.Add(temp);
            }

            return result; 
        }

        public static List<string> GetColumnFromArrayList(List<string[]> lines, int columnIndex)
        {
            List<string> result = new List<string>();   
            foreach (string[] line in lines)
            {
                result.Add(line[columnIndex]);
            }

            return result;
        }

        public static bool IsStringMatch(string inputA, string inputB)
        {
            char[] arrayA = inputA.ToArray();
            int countA = Math.Min(4, arrayA.Length);
            string subA = new string(arrayA[^countA..]);

            char[] arrayB = inputB.ToArray();
            int countB = Math.Min(4, arrayB.Length);
            string subB = new string(arrayB[^countB..]);

            return subA == subB;
        }

        public static List<int> GetMatchingColumns(List<string> inputA, List<string> inputB)
        {
            List<int> matchingIndices = new List<int>();
            for (int i = 0; i < inputA.Count; i++)
            {
                for (int j = 0; j < inputB.Count; j++)
                {
                    if (IsStringMatch(inputA[i], inputB[j]))
                    {
                        matchingIndices.Add(i);
                        break;
                    }
                }
            }
            return matchingIndices;
        }
    }
}
