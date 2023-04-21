using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net_ef_videogame
{
    public class SoftwareHouse
    {
        /* ***************
         * HOUSE's PROPERTIES
         */
        [Key] public long Id { get; set; }
        [StringLength(50)] public string Name { get; set; }

        //CONSTRUCTOR
        public SoftwareHouse(string name)
        {
            if (name.Length > 50)
                throw new ArgumentException($"The given name is too long (max: 50) [{name}]");
            Name = name;
        }

        /* ***************
         * HOUSE's METHODS
         */
    }
}
