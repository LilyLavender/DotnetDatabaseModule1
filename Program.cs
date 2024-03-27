using System;

namespace SchallyLilyTicketingSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] filePaths = {"Tickets.csv", "Enhancements.csv", "Task.csv"};
            if (filePaths.Any(fp => !System.IO.File.Exists(fp))) {
                Console.WriteLine("Tickets.csv, Enhancements.csv, or Task.csv is missing!");
                Environment.Exit(1);
            }
            TicketFile[] ticketFiles = {new TicketFile(filePaths[0]), new TicketFile(filePaths[1]), new TicketFile(filePaths[2])};

            while (true) {
                Console.WriteLine("Type 1 to view csv");
                Console.WriteLine("Type 2 to add to csv");
                Console.WriteLine("Type 0 to exit");
                int option = GetInt(true, 0, 2, "", "Number must be one of the aforementioned values");
                switch(option) {
                    case 1: // View CSV
                        ViewCsv(ticketFiles);
                        break;
                    case 2: // Add to CSV
                        while (true) {
                            Console.WriteLine("Which type of ticket do you wish to add?");
                            Console.WriteLine("Type 1 to add a bug/defect ticket");
                            Console.WriteLine("Type 2 to add an enhancement ticket");
                            Console.WriteLine("Type 3 to add a task ticket");
                            Console.WriteLine("Type 0 to cancel");
                            int option2 = GetInt(true, 0, 3, "", "Number must be one of the aforementioned values");
                            if (option2 >= 1 && option2 <= 3) {
                                AddToCsv(ticketFiles, option2);
                            }
                            break;
                        }
                        break;
                    default: // Exit
                        Environment.Exit(1);
                        break;
                }
            }
        }

        static void ViewCsv(TicketFile[] ticketFiles) {
            // Print Bug Tickets
            String[] headers = new String[8]{"TicketID","Summary","Status","Priority","Submitter","Assigned","Watching","Severity"};
            foreach(String h in headers) {
                Console.Write($"{h,-21}");
            }
            Console.Write("\n");
            foreach(Ticket t in ticketFiles[0].Tickets) {
                Console.WriteLine(t.Display());
            }
            // Print Enhancements
            String[] headers2 = new String[11]{"TicketID","Summary","Status","Priority","Submitter","Assigned","Watching","Software","Cost","Reason","Estimate"};
            foreach(String h in headers2) {
                Console.Write($"{h,-21}");
            }
            Console.Write("\n");
            foreach(Ticket t in ticketFiles[1].Tickets) {
                Console.WriteLine(t.Display());
            }
            // Print Tasks
            String[] headers3 = new String[9]{"TicketID","Summary","Status","Priority","Submitter","Assigned","Watching","Project Name","Due Date"};
            foreach(String h in headers3) {
                Console.Write($"{h,-21}");
            }
            Console.Write("\n");
            foreach(Ticket t in ticketFiles[2].Tickets) {
                Console.WriteLine(t.Display());
            }
        }

        static void AddToCsv(TicketFile[] ticketFiles, int csvType) {
            
            if (csvType == 1) {
                BugTicket ticket = new BugTicket();
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
                ticket.severity = GetString("Enter the ticket's severity > ", "Severity cannot be blank");            

                // Add ticket
                ticketFiles[csvType-1].AddBugTicket(ticket);

            } else if (csvType == 2) {
                Enhancement ticket = new Enhancement();
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
                ticket.software = GetString("Enter the ticket's software > ", "Software cannot be blank");
                ticket.cost = GetString("Enter the ticket's cost > ", "Cost cannot be blank");
                ticket.reason = GetString("Enter the ticket's reason > ", "Reason cannot be blank");
                ticket.estimate = GetString("Enter the ticket's estimate > ", "Estimate cannot be blank");

                // Add ticket
                ticketFiles[csvType-1].AddEnhancement(ticket);

            } else if (csvType == 3) {
                Task ticket = new Task();
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
                ticket.projectName = GetString("Enter the ticket's Project Name > ", "Project Name cannot be blank");
                ticket.dueDate = GetString("Enter the ticket's Due Date > ", "Due Date cannot be blank");

                // Add ticket
                ticketFiles[csvType-1].AddTask(ticket);

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