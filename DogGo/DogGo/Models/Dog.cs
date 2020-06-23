using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DogGo.Models
{
    public class Dog
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Hmmm... You should really add your dog's name...")]
        [MaxLength(35)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please add a breed for your dog...")]
        [MaxLength(20)]
        public string Breed { get; set; }
        public string ImageUrl { get; set; }
        public string Notes { get; set; }

        [DisplayName("Owner")]
        public int OwnerId { get; set; }
        public Owner Owner { get; set; }
       
    }
}
