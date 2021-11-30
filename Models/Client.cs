using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace account_ms.Models
{
    [Table("Client") ]
    public class Client
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Client ID")]
        public int idClient { get; set; } 

        [Required]
        [Column("fName", TypeName = "varChar(150)")]
        [Display(Name ="First Name")]
        public string fName { get; set; }

        [Column("sName", TypeName = "varChar(150)")]
        [Display(Name = "Second Name")]
        public string sName { get; set; }

        [Required]
        [Column("sureName", TypeName = "varChar(150)")]
        [Display(Name = "SureName")]
        public string sureName { get; set; }

        [Required]
        [Column("Active", TypeName = "integer")]
        [Display(Name = "Active")]
        public int active { get; set; }

        [Required]
        [Column("email", TypeName = "varChar(150)")]
        [Display(Name = "Email")]
        public string email { get; set; }

        [Required]
        [Column("telNumber")]
        [Display(Name = "Phone Number")]
        public long telNumber { get; set; }

        [Required]
        [Column("password", TypeName = "varChar(150)")]
        [Display(Name = "Password")]
        public string password { get; set; }

        [JsonIgnore]
        public List<CreditCard> creditCards { get; set; }
    }
}
