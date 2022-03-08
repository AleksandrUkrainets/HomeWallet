using System;
using System.ComponentModel.DataAnnotations;

namespace HomeWallet.PwaApp.Models
{
    public class OperationPwa
    {
        public int Id { get; set; }

        [Required]
        [Range(1.0, Double.MaxValue, ErrorMessage = "The field {0} must be greater than {1}.")]
        public decimal Amount { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [Range(1.0, Double.MaxValue, ErrorMessage = "The Category field is required")]
        public int CategoryId { get; set; }

        //[Required]
        public CategoryPwa Category { get; set; }
    }
}