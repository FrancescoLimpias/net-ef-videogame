using Microsoft.Data.SqlClient;

namespace net_ef_videogame
{

    internal class Program
    {
        //Connection 
        internal static TournamentContext DB;

        //Operations
        enum Operation
        {
            LIST,
            INSERT,
            SEARCHID,
            SEARCHNAME,
            DELETE,
            EXIT
        }

        static void Main(string[] args)
        {

            //Attempt connection to database
            try
            {
                DB = new TournamentContext();
            }
            catch
            {
                string ERROR = "Unable to connect to database!";
                Console.WriteLine($"[ERR] {ERROR}");
                throw new Exception(ERROR);
            }

            //Ask user for a prompt
            while (true)
                AskPrompt();
        }

        //Show Prompt video
        static void AskPrompt()
        {
            Console.Clear();

            //Greet the user
            Console.WriteLine("Welcome to VIDEOGAMES MANAGER!");

            //Ask for operation to perform
            Console.WriteLine("\r\nWhich operation would you like to do?");
            foreach (Operation operationName in Enum.GetValues(typeof(Operation)))
            {
                Console.WriteLine($" - {operationName}");
            }
            Console.Write("\r\noperation: ");
            Operation operation = UConsole.AskStringToCast<Operation>(
                (input) =>
                {
                    return Enum.Parse<Operation>(input);
                });
            Console.WriteLine();


            //Operation SWITCH
            switch (operation)
            {
                case Operation.LIST:

                    Console.WriteLine("Videogames list");
                    List<Videogame> videogames = VideogamesManager.List();

                    if (videogames.Count > 0)
                    {
                        Console.WriteLine("\r\nID - VIDEOGAME");
                        foreach (Videogame game in videogames)
                            Console.WriteLine($" {game.Id} - {game.Name}");
                    }
                    else
                    {
                        Console.WriteLine("No videogames found...");
                    }
                    break;
                case Operation.INSERT:

                    Console.Write("Insert a name (max 50 chars!): ");

                    //Attempt insertion and store result
                    bool success = VideogamesManager.Insert(UConsole.AskStringToCast((input) =>
                    {
                        if (input.Length > 50)
                            throw new Exception();
                        return input;
                    }));

                    Console.WriteLine(
                        success ?
                        "Videogame added."
                        : "Error!"
                        );

                    break;
                case Operation.SEARCHID:

                    Console.Write("Insert an ID: ");
                    Videogame? videogame = VideogamesManager.SearchById(UConsole.AskLong());

                    if (videogame != null)
                    {
                        Console.WriteLine($"[FOUND] Name: {videogame.Name}");
                    }
                    else
                    {
                        Console.WriteLine("[NOT FOUND] No videogame for the given ID");
                    }
                    break;
                case Operation.SEARCHNAME:

                    Console.Write("Insert a name: ");
                    List<Videogame> foundVideogames = VideogamesManager.SearchByName(UConsole.AskString());

                    if (foundVideogames.Count > 0)
                    {
                        Console.WriteLine("\r\nID - VIDEOGAME");
                        foreach (Videogame game in foundVideogames)
                            Console.WriteLine($" {game.Id} - {game.Name}");
                    }
                    else
                    {
                        Console.WriteLine("No videogames found...");
                    }
                    break;
                case Operation.DELETE:
                    Console.Write("Insert an EXISTING ID: ");

                    //Ask for ID and look if ID is EXISTENT -> CONVERT to VIDEOGAME
                    Videogame gameToDelete = UConsole.AskStringToCast<Videogame>((input) =>
                    {
                        Videogame? game = VideogamesManager.SearchById(Convert.ToInt64(input));
                        /* Keeps asking for a ID until 
                         * 1) given input is convertible to long
                         * 2) long is an existent ID
                         */
                        return game == null ? throw new NullReferenceException() : game;
                    });

                    //Ask for CONFIRMATION
                    Console.Write($"Are you sure you want to delete {gameToDelete.Name}? (yes/no) ");
                    bool confirmation = UConsole.AskYesNo();
                    if (confirmation)
                    {
                        if (VideogamesManager.Delete(gameToDelete.Id))
                            Console.WriteLine($"{gameToDelete.Name} successfully deleted!");
                        else
                            Console.WriteLine($"Error while trying to delete {gameToDelete.Name}");
                    }
                    else
                        Console.WriteLine("Operation aborted...");

                    break;
                case Operation.EXIT:

                    //Bye to user, wait for the user to read the message
                    Console.WriteLine("\r\n\r\nBye!");
                    System.Threading.Thread.Sleep(1500);

                    //EXIT
                    System.Environment.Exit(0);
                    break;
            }

            //Wait before restarting window
            Console.WriteLine("\r\n\r\nPress any key to continue...");
            Console.ReadKey();
        }
    }
}