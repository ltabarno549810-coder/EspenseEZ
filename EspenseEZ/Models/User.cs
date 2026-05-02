namespace ExpenseEZ.Models
{
	public class User
	{
		public int Id { get; set; }
		public string Name { get; set; } = "";
		public string Role { get; set; } = "";
		public string Department { get; set; } = "";
		public string Status { get; set; } = "";

		public bool IsArchived { get; set; } = false;
		public DateTime? ArchivedAt { get; set; }
	}
}