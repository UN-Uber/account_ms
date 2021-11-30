using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace account_ms.Models
{
    [Table("CreditCard")]  
    public class CreditCard
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Card Id")]
        public int idCard { get; set; }

        [Column("idClient", TypeName = "integer")]
        [Required]
        public int idClient { get; set; }

        [ForeignKey("idClient")]
        public Client client { get; set; }

        [Required]
        [Column("cardNumber")]
        [Display(Name = "Card Number")]
        public long cardNumber { get; set; }

        [Required]
        [Column("dueDate", TypeName = "varChar(150)")]
        [Display(Name = "Due Date")]
        public string dueDate { get; set; }

        [Required]
        [Column("cvv", TypeName = "integer")]
        [Display(Name = "CVV")]
        public int cvv { get; set; }
    }
}