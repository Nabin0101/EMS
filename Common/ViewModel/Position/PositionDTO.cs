using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ViewModel.Position
{
    public class PositionDTO
    {
        [Required]
        [RegularExpression(@"^[a-zA-Z]+( [a-zA-Z]+)*$", ErrorMessage = "PositionName should contain alphabetic values only")]
        public string PositionName { get; set; }
    }
}
