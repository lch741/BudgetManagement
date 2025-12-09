using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    [Table("Categorys")]
    public class Category
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public List<Transaction>? Transactions { get; set; }
    }
}