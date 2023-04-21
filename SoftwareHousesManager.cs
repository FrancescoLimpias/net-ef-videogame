using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net_ef_videogame
{
    internal static class SoftwareHousesManager
    {

        //Connection (just a ref to Program.DB)
        internal static TournamentContext DB = Program.DB;

        internal static bool Insert(string softwareHouseName)
        {
            SoftwareHouse newSoftwareHouse = new(softwareHouseName);

            DB.SoftwareHouses.Add(newSoftwareHouse);

            return DB.SaveChanges() == 1;
        }

        internal static SoftwareHouse SearchById(long id)
        {
            return DB.SoftwareHouses.Find(id);
        }
    }
}
