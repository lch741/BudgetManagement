using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Transaction
    {
    public int Id { get; set; }
    public required string Name { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime? Date { get; set; }
    public required string Amount { get; set; }
    public int CategoryId { get; set; }
    public Category? Category { get; set; }
    public TransactionType TransactionType { get; set; }
    }

    public enum TransactionType
    {
        [Display(Name = "Income")]
        Income = 1,

        [Display(Name = "Expense")]
        Expense = 2
    }
        
}