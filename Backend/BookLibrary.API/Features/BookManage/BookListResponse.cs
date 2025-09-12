namespace BookLibrary.API.Features.ListBook
{
    public class BookListResponse
    {
        public int BookId { get; set; }
        public string? Title { get; set; }
        public string? BookImg { get; set; }
        public string? Author { get; set; }
        public string? Publisher { get; set; }
        public string? CategoryName { get; set; }
        public string? YearPublished { get; set; }
        public int QuantityAvailable { get; set; }

    }
}
