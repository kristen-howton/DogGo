using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DogGo.Models
{
    public class Walker
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Hmmm... You should really add your Name...")]
        [MaxLength(35)]
        public string Name { get; set; }

        [DisplayName("Neighborhood")]
        public int NeighborhoodId { get; set; }
        public string ImageUrl { get; set; }
    }
}
