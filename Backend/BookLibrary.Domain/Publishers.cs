namespace BookLibrary.Domain
{
    public class Publishers
    {
        public int PublisherId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        // Navigation property
        public ICollection<Book> Books { get; set; }
    }
}
