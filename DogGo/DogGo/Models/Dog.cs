﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DogGo.Models
{
    public class Dog
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Breed { get; set; }

        #nullable enable 
        public string? ImageUrl { get; set; }
        public string? Notes { get; set; }
        #nullable disable
        public int OwnerId { get; set; }
    }
}
