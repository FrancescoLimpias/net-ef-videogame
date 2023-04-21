using System.ComponentModel.DataAnnotations;

namespace net_ef_videogame
{
    public class Videogame
    {
        /* ***************
         * VIDEOGAME's PROPERTIES
         */
        [Key] public long Id { get; set; }
        [StringLength(50)] public string Name { get; set; }

        //Software house link
        public long SoftwareHouseId { get; set; }
        public SoftwareHouse SoftwareHouse { get; set; }


        //CONSTRUCTOR
        internal Videogame(SoftwareHouse softwareHouse, string name)
        {
            SoftwareHouse = softwareHouse;
            SoftwareHouseId = softwareHouse.Id;

            if (name.Length > 50)
                throw new ArgumentException($"The given name is too long (max: 50) [{name}]");
            Name = name;
        }
        internal Videogame(long softwareHouseId, string name)
        {
            //SoftwareHouse = softwareHouse;
            SoftwareHouseId = softwareHouseId;

            if (name.Length > 50)
                throw new ArgumentException($"The given name is too long (max: 50) [{name}]");
            Name = name;
        }

        /* ***************
         * VIDEOGAME's METHODS
         */
    }
}
