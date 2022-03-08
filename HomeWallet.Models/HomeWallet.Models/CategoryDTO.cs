using HomeWallet.Domain.Enteties;
using System.Collections.Generic;

namespace HomeWallet.Models
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<OperationDTO> Operations { get; set; }
        public CategoryType CategoryType { get; set; }
    }
}