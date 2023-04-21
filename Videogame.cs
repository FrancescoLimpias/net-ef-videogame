using System.ComponentModel.DataAnnotations;

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
        internal Videogame(string name)
        {
            if (name.Length > 50)
                throw new ArgumentException($"The given name is too long (max: 50) [{name}]");
            Name = name;
        }

        /* ***************
         * VIDEOGAME's METHODS
         */
    }
}
