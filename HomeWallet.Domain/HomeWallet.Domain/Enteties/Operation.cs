using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeWallet.Domain.Enteties
{
    public class Operation
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}