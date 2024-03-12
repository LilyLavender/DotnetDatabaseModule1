using System;

namespace SchallyLilyTicketingSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = "Tickets.csv";
            if (!System.IO.File.Exists(filePath)) {
                Console.WriteLine("Tickets.csv does not exist, please add a valid Tickets.csv first");
                Environment.Exit(1);
            }
            TicketFile ticketFile = new TicketFile(filePath);

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
            
            Ticket ticket = new Ticket();
            // Print questions for user
            ticket.summary = GetString("Enter the ticket's summary > ", "Summary cannot be blank");
            ticket.status = GetString("Enter the ticket's status > ", "Status cannot be blank");
            ticket.priority = GetString("Enter the ticket's priority > ", "Priority cannot be blank");
            ticket.submitter = GetString("Enter the ticket's submitter > ", "Submitter cannot be blank");
            ticket.assigned = GetString("Enter who the ticket is for > ", "Assignee cannot be blank");
            String input = "";
            do {
                input = GetString("Enter a watcher of the ticket, and \"done\" when done > ", "Watcher cannot be blank");
                if (input != "done") {
                    ticket.watching.Add(input);
                }
            } while (input != "done");
            // No watchers
            if (ticket.watching.Count == 0) {
                ticket.watching.Add("(no watchers)");
            }

            // Add ticket
            ticketFile.AddTicket(ticket);
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