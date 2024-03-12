public class Ticket
{
    public UInt64 ticketId { get; set; }
    public string summary { get; set; }
    public string status { get; set; }
    public string priority { get; set; }
    public string submitter { get; set; }
    public string assigned { get; set; }
    public List<string> watching { get; set; }

    // Constructor
    public Ticket() {
        watching = new List<string>();
    }

    public string Display() {
        return $"{ticketId,-21}{summary,-21}{status,-21}{priority,-21}{submitter,-21}{assigned,-21}{string.Join(", ", watching),-21}\n";
    }
}