using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace BookLibrary.Domain
{
    public class Fine
    {
        [Key]
        public int FineId { get; set; }
        public int BorrowId { get; set; }
        public BorrowOrder BorrowOrder { get; set; } = null!;
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }
        [Required]
        public string Status { get; set; } = "Unpaid";
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? PaidAt { get; set; }
        public Guid CreatedBy { get; set; }
    }
}
