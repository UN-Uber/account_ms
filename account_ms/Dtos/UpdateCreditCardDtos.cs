namespace account_ms.Dtos
{
    public class UpdateCreditCardDtos
    {
        public int idClient { get; set; }
        public long cardNumber { get; set; }
        public string dueDate { get; set; }
        public int cvv { get; set; }
    }
}
