using System;

namespace SchallyLilyTicketingSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true) {
                Console.WriteLine("Type 1 to view csv");
                Console.WriteLine("Type 2 to add to csv");
                Console.WriteLine("Type 0 to exit");
                int option = GetInt(true, 0, 2, "", "Number must be one of the aforementioned values");
                switch(option) {
                    case 1:
                        ViewCsv();
                        break;
                    case 2:
                        AddToCsv();
                        break;
                    default:
                        Environment.Exit(1);
                        break;
                }
            }
        }

        static void ViewCsv() {
            string file = "Tickets.csv";
            if (System.IO.File.Exists(file))
            {
                // Add data to fullCsv array
                StreamReader sr = new StreamReader(file);
                String[][] fullCsv = new String[File.ReadLines(file).Count()][];
                for (int i = 0; i < fullCsv.Length; i++)
                {
                    string? line = sr.ReadLine();
                    fullCsv[i] = line.Split(',');
                }
                sr.Close();

                // Print from fullCsv array
                for (int j = 0; j < fullCsv[0].Length; j++) {
                    for (int i = 0; i < fullCsv.Length; i++) {
                        Console.Write($"{fullCsv[i][j],-21}");
                    }
                    Console.Write("\n");
                }
            } else {
                Console.WriteLine("Tickets.csv does not exist!");
            }
        }

        static void AddToCsv() {
            string file = "Tickets.csv";
            if (System.IO.File.Exists(file))
            {
                // Add line header (id, summary, status, etc) to headers array
                StreamReader sr = new StreamReader(file);
                String[] headers = new String[File.ReadLines(file).Count()];
                for (int i = 0; i < headers.Length; i++)
                {
                    string? line = sr.ReadLine();
                    headers[i] = line.Substring(0, line.IndexOf(","));
                }
                sr.Close();
                
                // Print question for user
                string[] fullFile = File.ReadAllLines(file);
                fullFile[0] += "," + GenerateNewID(file);
                for (int i = 1; i < headers.Length; i++) {
                    fullFile[i] += $",{GetString($"Enter your \"{headers[i]}\" value > ", $"\"{headers[i]}\" value cannot be blank")}";
                }
                // Add to csv
                File.WriteAllLines(file, fullFile);

                Console.WriteLine("Entry added to Tickets.csv\n");
            } else {
                Console.WriteLine("Tickets.csv does not exist!");
            }
        }

        static int GenerateNewID(string filePath)
        {
            string line = File.ReadAllLines(filePath).First();
            int maxId = 0;

            string[] parts = line.Split(',');
            foreach (string part in parts) {
                if (int.TryParse(part, out int id)) {
                    maxId = Math.Max(maxId, id);
                }
            }

            return maxId + 1;
        }

        static int GetInt(bool restrictValues, int intMin, int intMax, string prompt, string errorMsg) {
        
            string? userString = "";
            int userInt = 0;
            bool repSuccess = false;
            do {
                Console.Write(prompt);
                userString = Console.ReadLine();
    
                if (Int32.TryParse(userString, out userInt)) {
                    if (restrictValues)
                    {
                        if (userInt >= intMin && userInt <= intMax) {
                            repSuccess = true;
                        }
                    }
                    else
                    {
                        repSuccess = true;
                    }
                }
    
                // Output error
                if (!repSuccess) {
                    Console.WriteLine(errorMsg);
                }
            } while(!repSuccess);
    
            return userInt;
    
        }

        public static string GetString(string prompt, string errorMsg) {

            string? userString = "";
            bool repSuccess = false;
            do
            {
                Console.Write(prompt);
                userString = Console.ReadLine();

                if (!String.IsNullOrEmpty(userString))
                {
                    repSuccess = true;
                }

                // Output error
                if (!repSuccess)
                {
                    Console.WriteLine(errorMsg);
                }
            } while (!repSuccess);

            return userString;

        }
    }
}