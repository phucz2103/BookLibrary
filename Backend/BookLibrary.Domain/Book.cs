
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

        public Author Author { get; set; } = null!;
        public int? AuthorId { get; set; } 

        public Publishers? Publisher { get; set; }
        public int? PublisherId { get; set; }

        [Required]
        public DateTime PublishedDate { get; set; }

        [Required]
        public string BookImg { get; set; }

        [Required]
        [MaxLength(50)]
        public string Description { get; set; }

        [Required]
        public int Quantity { get; set; }

        public int AvailableQuantity { get; set; } = 0;

        [MaxLength(50)]
        public bool Status { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime LastUpdatedAt { get; set; }

        [Required]
        public Guid CreatedBy { get; set; }

        public int? CategoryId { get; set; }
        public Category? Category { get; set; }
        public ICollection<BorrowDetail> BorrowDetails { get; set; } = new List<BorrowDetail>();

    }
}
