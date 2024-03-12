public class TicketFile {
    public string filePath { get; set; }
    public List<Ticket> Tickets { get; set; }

    // Constructor
    public TicketFile(string filePath) {
        filePath = filePath;
        Tickets = new List<Ticket>();

        StreamReader sr = new StreamReader(filePath);
        sr.ReadLine();
        while (!sr.EndOfStream) {
            Ticket ticket = new Ticket();
            string line = sr.ReadLine();
            
            // Get details from file
            string[] ticketDetails = line.Split(',');
            ticket.ticketId = UInt64.Parse(movieDetails[0]);
            ticket.summary = ticketDetails[1];
            ticket.status = ticketDetails[2];
            ticket.priority = ticketDetails[3];
            ticket.submitter = ticketDetails[4];
            ticket.assigned = ticketDetails[5];
            ticket.watching = ticketDetails[6].Split('|').ToList();
            Tickets.Add(ticket);
        }
        // close file when done
        sr.Close();
    }
}