public class Enhancement : Ticket
{
    public string software { get; set; }
    public string cost { get; set; }
    public string reason { get; set; }
    public string estimate { get; set; }

    public Enhancement() {
        watching = new List<string>();
    }

    public string Display() {
        return $"{ticketId,-21}{summary,-21}{status,-21}{priority,-21}{submitter,-21}{assigned,-21}{string.Join(", ", watching),-21}{software,-21}{cost,-21}{reason,-21}{estimate,-21}";
    }
}