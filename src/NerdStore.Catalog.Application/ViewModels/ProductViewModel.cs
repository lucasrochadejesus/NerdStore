using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NerdStore.Catalog.Application.ViewModels
{
    public class ProductViewModel
    {
        [Key]
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }

        [Required(ErrorMessage = "Field {0} is mandatory")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Field {0} is mandatory")]
        public string Description { get; set; }


        [Required(ErrorMessage = "Field {0} is mandatory")]
        public bool Active { get; set; }


        [Required(ErrorMessage = "Field {0} is mandatory")]
        public DateTime DtCreation { get; set; }


        [Required(ErrorMessage = "Field {0} is mandatory")]
        public decimal Price { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Field {0} must be > 1")]
        [Required(ErrorMessage = "Field {0} is mandatory")]
        public int StockQuantity { get; set; }


        [Required(ErrorMessage = "Field {0} is mandatory")]
        public string Image { get; set; }
       
        [Required(ErrorMessage = "Field {0} is mandatory")]
        public string ModelNumber { get; set; } 

        [Range(1, int.MaxValue, ErrorMessage = "Field {0} must be > 1")]
        [Required(ErrorMessage = "Field {0} is mandatory")]
        public decimal Height { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Field {0} must be > 1")]
        [Required(ErrorMessage = "Field {0} is mandatory")]
        public decimal Width { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Field {0} must be > 1")]
        [Required(ErrorMessage = "Field {0} is mandatory")]
        public decimal Length { get; set; }

        [Required(ErrorMessage = "Field {0} is mandatory")]
        public int BrandId { get; set; }

        public IEnumerable<CategoryViewModel> Categories { get; set; }
    }
}
