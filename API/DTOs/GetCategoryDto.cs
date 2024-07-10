using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class GetCategoryDto
    {
        public Guid Id { get; set; }
		public string Name { get; set; }
		public List<CategoryBooksDto> Books { get; set; }
    }
}