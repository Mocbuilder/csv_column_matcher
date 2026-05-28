using csv_column_matcher.Services;
namespace csv_column_matcher
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                MainLoop();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                Console.ReadLine();
            } 
        }

        public static void MainLoop()
        {
            while (true)
            {
                Console.WriteLine("BaseInput file:");
                string? baseFile = Console.ReadLine();
                if (!MiscService.IsValidFile(baseFile))
                {
                    Console.WriteLine("Invalid file.");
                    continue;
                }

                Console.WriteLine("File is Valid.\nMatchInput File:");
                string? matchFile = Console.ReadLine();
                if (!MiscService.IsValidFile(matchFile))
                {
                    Console.WriteLine("Invalid file.");
                    continue;
                }

                Console.WriteLine("File is Valid.\nBaseInput Collumn:");
                int baseInputColumnIndex = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("File is Valid.\nMatchInput Collumn:");
                int matchInputColumnIndex = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Ignore First Line/Header (true/false)?");
                bool ignoreHeader = true;
                string boolInput = Console.ReadLine()?.Trim();

                if (bool.TryParse(boolInput, out bool result))
                {
                    Console.WriteLine($"You entered : {result}");
                    ignoreHeader = result;
                }
                else
                {
                    Console.WriteLine("Invalid Input. Please enter 'true' or 'false'.");
                    continue;
                }

                List<string[]> baseInputLines = FileService.ReadAllLinesToArray(baseFile);
                List<string[]> matchInputLines = FileService.ReadAllLinesToArray(matchFile);

                List<string> baseInputColumn = FileService.GetColumnFromArrayList(baseInputLines, baseInputColumnIndex);
                List<string> matchInputColumn = FileService.GetColumnFromArrayList(matchInputLines, matchInputColumnIndex);

                List<int> matchingIndices = FileService.GetMatchingColumns(baseInputColumn, matchInputColumn);
                Console.WriteLine("Matching Indices: " + string.Join(", ", matchingIndices));
                foreach (int index in matchingIndices)
                {
                    Console.WriteLine($"Matching Line {index}: " + string.Join(", ", baseInputLines[index]));
                }
            }
        }
    }
}
