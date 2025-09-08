using System.ComponentModel.DataAnnotations;

namespace BookLibrary.Domain
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Author { get; set; } = string.Empty;

        [Required]
        public DateTime PublishedDate { get; set; }

        [Required]
        [MaxLength(50)]
        public string Description { get; set; }

        [Required]
        public int Quantity { get; set; }

        public int AvailableQuantity { get; set; } = 0;

        [MaxLength(50)]
        public string? Status { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required]
        public int CreatedBy { get; set; }

        public int? CategoryId { get; set; }
        public Category? Category { get; set; }
        public ICollection<BorrowDetail> BorrowDetails { get; set; } = new List<BorrowDetail>();

    }
}
