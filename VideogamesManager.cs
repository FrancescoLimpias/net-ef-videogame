using Microsoft.Data.SqlClient;

namespace net_ef_videogame
{
    internal static class VideogamesManager
    {

        //Connection (just a ref to Program.DB)
        internal static TournamentContext DB = Program.DB;

        internal static bool Insert(SoftwareHouse softwareHouse, string videogameName)
        {
            Videogame newVideogame = new(softwareHouse, videogameName);

            DB.Videogames.Add(newVideogame);

            return DB.SaveChanges() == 1;
        }

        internal static Videogame SearchById(long id)
        {
            return DB.Videogames.Find(id);
        }

        internal static List<Videogame> SearchByName(string name)
        {
            return DB.Videogames.Where(videogame => videogame.Name.Contains(name)).ToList();
        }

        internal static bool Delete(long id)
        {
            Videogame videogameToDelete = SearchById(id);

            if (videogameToDelete == null)
                return false;

            DB.Videogames.Remove(videogameToDelete);
            return DB.SaveChanges() == 1;
        }

        internal static List<Videogame> List()
        {
            return DB.Videogames.ToList();
        }
    }
}
