public class BugTicket : Ticket
{
    public string severity { get; set; }

    public BugTicket() {
        watching = new List<string>();
    }

    public string Display() {
        return $"{ticketId,-21}{summary,-21}{status,-21}{priority,-21}{submitter,-21}{assigned,-21}{string.Join(", ", watching),-21}{severity,-21}";
    }
}