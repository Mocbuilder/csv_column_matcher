using csv_column_matcher.Services;
using System.CommandLine;
using System.CommandLine.Parsing;
using System.IO;

namespace csv_column_matcher
{
    internal class Program
    {
        static bool useCLArguments = false;
        static string baseFile = string.Empty;
        static string matchFile = string.Empty;
        static int baseFileIndex = 0;
        static int matchFileIndex = 0;
        static bool ignoreHeader = true;

        static void Main(string[] args)
        {
            Option<bool> useCLArguments = new("--useCLArguments")
            {
                Description = "Use CommandLine Arguments instead of manual input."
            };

            Option<string> baseFileOption = new("--baseFile")
            {
                Description = "The Base File that gets checked against with the Match File."
            };

            Option<string> matchFileOption = new("--matchFile")
            {
                Description = "The Match File that gets checked against the Base File."
            };

            Option<int> baseFileIndexOption = new("--baseFileIndex")
            {
                Description = "The Index of the column in the Base File that is to be matched."
            };

            Option<int> matchFileIndexOption = new("--matchFileIndex")
            {
                Description = "The Index of the column in the Match File that is to be matched."
            };

            Option<bool> IgnoreHeaderOption = new("--ignoreHeader")
            {
                Description = "Ignore the first Line of all Files."
            };

            RootCommand rootCommand = new("Console-based Backend for the CSV Column Matcher by Mocbuilder.");
            rootCommand.Options.Add(useCLArguments);
            rootCommand.Options.Add(baseFileOption);
            rootCommand.Options.Add(matchFileOption);
            rootCommand.Options.Add(baseFileIndexOption);
            rootCommand.Options.Add(matchFileIndexOption);
            rootCommand.Options.Add(IgnoreHeaderOption);

            ParseResult parseResult = rootCommand.Parse(args);
            if (!(parseResult.Errors.Count == 0))
            {
                foreach (ParseError parseError in parseResult.Errors)
                {
                    Console.Error.WriteLine(parseError.Message);
                }
            }

            if(parseResult.GetValue(useCLArguments))
            {
                baseFile = parseResult.GetValue(baseFileOption);
                matchFile = parseResult.GetValue(matchFileOption);
                baseFileIndex = parseResult.GetValue(baseFileIndexOption);
                matchFileIndex = parseResult.GetValue(matchFileIndexOption);
                ignoreHeader = parseResult.GetValue(IgnoreHeaderOption);
            }

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
                if(!useCLArguments)
                {
                    GetUserInput();
                }
                else
                {
                    Console.WriteLine("Using CommandLine Arguments.");
                }

                    List<string[]> baseInputLines = FileService.ReadAllLinesToArray(baseFile);
                List<string[]> matchInputLines = FileService.ReadAllLinesToArray(matchFile);

                List<string> baseInputColumn = FileService.GetColumnFromArrayList(baseInputLines, baseFileIndex);
                List<string> matchInputColumn = FileService.GetColumnFromArrayList(matchInputLines, matchFileIndex);

                List<int> matchingIndices = FileService.GetMatchingColumns(baseInputColumn, matchInputColumn);
                Console.WriteLine("Matching Indices: " + string.Join(", ", matchingIndices));
                foreach (int index in matchingIndices)
                {
                    Console.WriteLine($"Matching Line {index}: " + string.Join(", ", baseInputLines[index]));
                }
            }
        }

        public static void GetUserInput()
        {
            while (true)
            {
                Console.WriteLine("BaseInput file:");
                baseFile = Console.ReadLine();
                if (!MiscService.IsValidFile(baseFile))
                {
                    Console.WriteLine("Invalid file.");
                    continue;
                }

                Console.WriteLine("File is Valid.\nMatchInput File:");
                matchFile = Console.ReadLine();
                if (!MiscService.IsValidFile(matchFile))
                {
                    Console.WriteLine("Invalid file.");
                    continue;
                }

                Console.WriteLine("File is Valid.\nBaseInput Collumn:");
                baseFileIndex = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("File is Valid.\nMatchInput Collumn:");
                matchFileIndex = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Ignore First Line/Header (true/false)?");
                string boolInput = Console.ReadLine()?.Trim();

                if (bool.TryParse(boolInput, out bool result))
                {
                    Console.WriteLine($"You entered : {result}");
                    ignoreHeader = result;
                    return;
                }
                else
                {
                    Console.WriteLine("Invalid Input. Please enter 'true' or 'false'.");
                    continue;
                }
            }
            
        }
    }
}
