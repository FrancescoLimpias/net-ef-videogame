using Microsoft.Data.SqlClient;

namespace net_ef_videogame
{
    internal static class VideogamesManager
    {

        //Connection (just a ref to Program.DB)
        internal static TournamentContext DB = Program.DB;

        internal static bool Insert(string videogameName)
        {
            Videogame newVideogame = new(videogameName);

            DB.Videogames.Add(newVideogame);

            return DB.SaveChanges() == 1;
        }

        internal static Videogame? SearchById(long id)
        {
            return DB.Videogames.Find(id);
        }

        internal static List<Videogame> SearchByName(string name)
        {
            List<Videogame> videogames = new List<Videogame>();

            string query = "SELECT * FROM videogames WHERE UPPER(name) LIKE @Name";

            SqlCommand command = new SqlCommand(query, Program.SQL);
            command.Parameters.AddWithValue("@Name", $"%{name.ToUpper()}%");

            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                videogames.Add(new Videogame(reader.GetInt64(0), reader.GetString(1)));
            }

            return videogames;
        }

        internal static bool Delete(long id)
        {
            string query = "DELETE FROM videogames WHERE id = @Id";

            using SqlCommand command = new SqlCommand(query, Program.SQL);
            command.Parameters.AddWithValue("@Id", id);

            return command.ExecuteNonQuery() > 0;
        }

        internal static List<Videogame> List()
        {
            List<Videogame> videogames = new();
            string selectionQuery = "SELECT * FROM videogames";

            //Build SQL command
            using SqlCommand command = Program.SQL.CreateCommand();
            command.Connection = Program.SQL;
            command.CommandText = selectionQuery;

            using SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                videogames.Add(new Videogame(reader.GetInt64(0), reader.GetString(1)));
            }

            return videogames;
        }
    }
}
