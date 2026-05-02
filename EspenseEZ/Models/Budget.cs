using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseEZ.Models
{
	public class Budget
	{
		public int Id { get; set; }
		public string Department { get; set; } = "";

		[Column(TypeName = "decimal(18,2)")]
		public decimal Amount { get; set; }
		public bool IsArchived { get; set; } = false;
		public DateTime? ArchivedAt { get; set; }
	}
}