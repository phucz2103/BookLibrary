namespace BookLibrary.Domain
{
    public class Author
    {
        public int AuthorId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string National { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public string? Biography { get; set; }
        // Navigation property
        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
