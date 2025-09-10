using Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace BookLibrary.Domain
{
    public class BorrowOrder // phiếu mượn sách
    {
        [Key]
        public int BorrowOrderId { get; set; }

        [Required]
        public Guid UserId { get; set; }
        public User? User { get; set; }

        [Required]
        public DateTime BorrowDate { get; set; }
        [Required]
        public DateTime DueDate { get; set; }

        public DateTime? ReturnDate { get; set; }

        [Required]
        public string Status { get; set; } = string.Empty;

        public Guid CreatedBy { get; set; }

        public ICollection<BorrowDetail> BorrowDetails { get; set; } = new List<BorrowDetail>(); // 1 phiếu mượn có nhiều sách
        public Fine? Fine { get; set; }
    }
}
