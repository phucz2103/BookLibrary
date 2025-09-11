namespace BookLibrary.Domain
{
    public class BorrowDetail  // chi tiết phiếu mượn (mượn sách nào, số lượng bao nhiêu)
    {
        public int BorrowId { get; set; }
        public BorrowOrder BorrowOrder { get; set; } = null!;
        public int BookId { get; set; }
        public Book Book { get; set; } = null!;
        public int Quantity { get; set; }
    }
}
