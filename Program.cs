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
                        viewCsv();
                        break;
                    case 2:
                        addToCsv();
                        break;
                    default:
                        Environment.Exit(1);
                        break;
                }
            }
        }

        static void viewCsv() {
            string file = "Tickets.csv";
            if (System.IO.File.Exists(file))
            {
                StreamReader sr = new StreamReader(file);
                String[][] fullCsv = new String[File.ReadLines(file).Count()][];
                for (int i = 0; i < fullCsv.Length; i++)
                {
                    string line = sr.ReadLine();
                    fullCsv[i] = line.Split(',');
                }
                sr.Close();

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

        static void addToCsv() {
            string file = "Tickets.csv";
            if (System.IO.File.Exists(file))
            {
                StreamReader sr = new StreamReader(file);
                String[] headers = new String[File.ReadLines(file).Count()];
                for (int i = 0; i < headers.Length; i++)
                {
                    string line = sr.ReadLine();
                    headers[i] = line.Substring(0, line.IndexOf(","));
                }
                sr.Close();
                
                string[] fullFile = File.ReadAllLines(file);
                for (int i = 0; i < headers.Length; i++) {
                    fullFile[i] += $",{GetString($"Enter your \"{headers[i]}\" value > ", $"\"{headers[i]}\" value cannot be blank")}";
                }
                File.WriteAllLines(file, fullFile);

                Console.WriteLine("Entry added to Tickets.csv\n");
                
            } else {
                Console.WriteLine("Tickets.csv does not exist!");
            }
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