namespace ExpenseEZ.Models
{
	public class AuditLog   // NOT AuditLogs
	{
		public int Id { get; set; }
		public string Action { get; set; } = "";
		public DateTime Date { get; set; }
	}
}