using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DogGo.Models.ViewModels
{
    public class WalkerViewModel
    {
        public Walker Walker { get; set; }
        public List<Walks> Walks { get; set; }
        public string Duration
        {
            get
            {
                int totalMinutes = 0;

                foreach (Walks walk in Walks)
                {
                    totalMinutes += walk.Duration;
                }
                TimeSpan timespan = TimeSpan.FromMinutes(totalMinutes);
                return timespan.ToString(@"hh\:mm");
            }
        }
        public Neighborhood Neighborhood { get; set; }
    }
}
