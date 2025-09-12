using BookLibrary.Domain;

namespace BookLibrary.API.Features.ListBook
{
    public class BookDetailResponse
    {
        public int BookId { get; set; }
        public string? BookImg { get; set; }
        public string? Title { get; set; }
        public string? Author { get; set; }
        public string? Publisher { get; set; }
        public int YearPublished { get; set; }
        public string? Description { get; set; }
        public string? CategoryName { get; set; }
        public int Quantity { get; set; }
        public int QuantityAvailable { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
    }
}
