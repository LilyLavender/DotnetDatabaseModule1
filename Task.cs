public class Task : Ticket
{
    public string projectName { get; set; }
    public string dueDate { get; set; }

    public Task() {
        watching = new List<string>();
    }

    public string Display() {
        return $"{ticketId,-21}{summary,-21}{status,-21}{priority,-21}{submitter,-21}{assigned,-21}{string.Join(", ", watching),-21}{projectName,-21}{dueDate,-21}";
    }
}