using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    [Table("Transactions")]
    public class Transaction
    {
    public int Id { get; set; }
    public required string Name { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public required DateOnly Date { get; set; }
    public required decimal Amount { get; set; }
    public int CategoryId { get; set; }
    public required Category Category { get; set; }
    public TransactionType TransactionType { get; set; }
    public required string AppUserId { get; set; }
    public required AppUser AppUser { get; set; }
    }

    public enum TransactionType
    {
        [Display(Name = "Income")]
        Income = 1,

        [Display(Name = "Expense")]
        Expense = 2
    }
        
}