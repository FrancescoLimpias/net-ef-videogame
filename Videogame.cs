using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net_ef_videogame
{
    public class Videogame
    {
        /* ***************
         * VIDEOGAME's PROPERTIES
         */
        [Key] public long Id { get; set; }
        public string Name { get; set; }

        //CONSTRUCTOR
        internal Videogame(long id, string name)
        {
            Id = id;

            if (name.Length > 50)
                throw new ArgumentException($"The given name is too long (max: 50) [{name}]");
            Name = name;
        }

        /* ***************
         * VIDEOGAME's METHODS
         */
    }
}
