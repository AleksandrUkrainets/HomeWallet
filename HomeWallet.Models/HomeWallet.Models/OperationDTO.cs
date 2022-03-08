using System;

namespace HomeWallet.Models
{
    public class OperationDTO
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public int CategoryId { get; set; }
        public CategoryDTO Category { get; set; }
    }
}