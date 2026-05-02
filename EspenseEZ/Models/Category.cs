namespace ExpenseEZ.Models
{
	public class Category
	{
		public int Id { get; set; }
		public string Name { get; set; } = "";

		public bool IsArchived { get; set; } = false;
		public DateTime? ArchivedAt { get; set; }
	}
}