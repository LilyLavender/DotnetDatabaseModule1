public class TicketFile {
    public string filePath { get; set; }
    public List<Ticket> Tickets { get; set; }

    // Constructor
    public TicketFile(string filePath) {
        this.filePath = filePath;
        Tickets = new List<Ticket>();

        StreamReader sr = new StreamReader(filePath);
        sr.ReadLine();
        while (!sr.EndOfStream) {
            if (filePath == "Tickets.csv") {
                BugTicket ticket = new BugTicket();
                string line = sr.ReadLine();
                // Get details from file
                string[] ticketDetails = line.Split(',');
                ticket.ticketId = UInt64.Parse(ticketDetails[0]);
                ticket.summary = ticketDetails[1];
                ticket.status = ticketDetails[2];
                ticket.priority = ticketDetails[3];
                ticket.submitter = ticketDetails[4];
                ticket.assigned = ticketDetails[5];
                ticket.watching = ticketDetails[6].Split('|').ToList();
                ticket.severity = ticketDetails[7];
                Tickets.Add(ticket);
            } else if (filePath == "Enhancements.csv") {
                Enhancement ticket = new Enhancement();
                string line = sr.ReadLine();
                // Get details from file
                string[] ticketDetails = line.Split(',');
                ticket.ticketId = UInt64.Parse(ticketDetails[0]);
                ticket.summary = ticketDetails[1];
                ticket.status = ticketDetails[2];
                ticket.priority = ticketDetails[3];
                ticket.submitter = ticketDetails[4];
                ticket.assigned = ticketDetails[5];
                ticket.watching = ticketDetails[6].Split('|').ToList();
                ticket.software = ticketDetails[7];
                ticket.cost = ticketDetails[8];
                ticket.reason = ticketDetails[9];
                ticket.estimate = ticketDetails[10];
                Tickets.Add(ticket);
            } else if (filePath == "Task.csv") {
                Task ticket = new Task();
                string line = sr.ReadLine();
                // Get details from file
                string[] ticketDetails = line.Split(',');
                ticket.ticketId = UInt64.Parse(ticketDetails[0]);
                ticket.summary = ticketDetails[1];
                ticket.status = ticketDetails[2];
                ticket.priority = ticketDetails[3];
                ticket.submitter = ticketDetails[4];
                ticket.assigned = ticketDetails[5];
                ticket.watching = ticketDetails[6].Split('|').ToList();
                ticket.projectName = ticketDetails[7];
                ticket.dueDate = ticketDetails[8];
                Tickets.Add(ticket);
            }
        }
        // close file when done
        sr.Close();
    }

    public void AddBugTicket(BugTicket ticket) {
        // Generate ticket id
        ticket.ticketId = Tickets.Max(t => t.ticketId) + 1;
        StreamWriter sw = new StreamWriter(filePath, true);
        sw.Write($"\n{ticket.ticketId},{ticket.summary},{ticket.status},{ticket.priority},{ticket.submitter},{ticket.assigned},{string.Join("|", ticket.watching)},{ticket.severity}");
        sw.Close();
        // Add ticket to list
        Tickets.Add(ticket);
    }

    public void AddEnhancement(Enhancement ticket) {
        // Generate ticket id
        ticket.ticketId = Tickets.Max(t => t.ticketId) + 1;
        StreamWriter sw = new StreamWriter(filePath, true);
        sw.Write($"\n{ticket.ticketId},{ticket.summary},{ticket.status},{ticket.priority},{ticket.submitter},{ticket.assigned},{string.Join("|", ticket.watching)},{ticket.software},{ticket.cost},{ticket.reason},{ticket.estimate}");
        sw.Close();
        // Add ticket to list
        Tickets.Add(ticket);
    }

    public void AddTask(Task ticket) {
        // Generate ticket id
        ticket.ticketId = Tickets.Max(t => t.ticketId) + 1;
        StreamWriter sw = new StreamWriter(filePath, true);
        sw.Write($"\n{ticket.ticketId},{ticket.summary},{ticket.status},{ticket.priority},{ticket.submitter},{ticket.assigned},{string.Join("|", ticket.watching)},{ticket.projectName},{ticket.dueDate}");
        sw.Close();
        // Add ticket to list
        Tickets.Add(ticket);
    }
}