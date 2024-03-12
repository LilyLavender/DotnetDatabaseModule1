using System;

namespace SchallyLilyTicketingSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = "Tickets.csv";
            TicketFile ticketFile = new TicketFile(filePath);
            if (!System.IO.File.Exists(filePath)) {
                Console.WriteLine("Tickets.csv does not exist, please add a valid Tickets.csv first");
                Environment.Exit(1);
            }

            while (true) {
                Console.WriteLine("Type 1 to view csv");
                Console.WriteLine("Type 2 to add to csv");
                Console.WriteLine("Type 0 to exit");
                int option = GetInt(true, 0, 2, "", "Number must be one of the aforementioned values");
                switch(option) {
                    case 1:
                        ViewCsv(filePath, ticketFile);
                        break;
                    case 2:
                        AddToCsv(filePath, ticketFile);
                        break;
                    default:
                        Environment.Exit(1);
                        break;
                }
            }
        }

        static void ViewCsv(string filePath, TicketFile ticketFile) {
            // Print Headers
            String[] headers = new String[7]{"TicketID","Summary","Status","Priority","Submitter","Assigned","Watching"};
            foreach(String h in headers) {
                Console.Write($"{h,-21}");
            }
            Console.Write("\n");
            // Print data
            foreach(Ticket t in ticketFile.Tickets) {
                Console.WriteLine(t.Display());
            }
        }

        static void AddToCsv(string filePath, TicketFile ticketFile) {
            /*
            if (System.IO.File.Exists(filePath))
            {
                // Add line header (id, summary, status, etc) to headers array
                StreamReader sr = new StreamReader(filePath);
                String[] headers = new String[File.ReadLines(filePath).Count()];
                for (int i = 0; i < headers.Length; i++)
                {
                    string? line = sr.ReadLine();
                    headers[i] = line.Substring(0, line.IndexOf(","));
                }
                sr.Close();
                
                // Print question for user
                string[] fullFile = File.ReadAllLines(filePath);
                //fullFile[0] += "," + GenerateNewID(filePath);
                for (int i = 1; i < headers.Length; i++) {
                    fullFile[i] += $",{GetString($"Enter your \"{headers[i]}\" value > ", $"\"{headers[i]}\" value cannot be blank")}";
                }
                // Add to csv
                File.WriteAllLines(filePath, fullFile);

                Console.WriteLine("Entry added to Tickets.csv\n");
            } else {
                Console.WriteLine("Tickets.csv does not exist!");
            }
            */
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