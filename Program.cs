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

            INSERT_GAME,
            INSERT_HOUSE,

            SEARCH_BY_ID,
            SEARCH_BY_NAME,
            SEARCH_BY_HOUSE,

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
                Console.WriteLine($" - {operationName.ToString().Replace("_", " ")}");
            }
            Console.Write("\r\noperation: ");
            Operation operation = UConsole.AskStringToCast<Operation>(
                (input) =>
                {
                    try
                    {
                        return Enum.Parse<Operation>(input.Replace(" ", "_"));
                    }
                    catch
                    {
                        return Enum.Parse<Operation>(input);
                    }
                });
            Console.WriteLine();


            //Operation SWITCH
            switch (operation)
            {
                case Operation.LIST:
                    {
                        Console.WriteLine("Videogames list");
                        List<Videogame> videogames = VideogamesManager.List();

                        //explicitly load related software houses
                        foreach (Videogame videogame in videogames)
                            DB.Entry(videogame).Reference(game => game.SoftwareHouse).Load();

                        if (videogames.Count > 0)
                        {
                            Console.WriteLine("\r\nID - VIDEOGAME - SOFTWARE HOUSE");
                            foreach (Videogame game in videogames)
                                Console.WriteLine($" {game.Id} - {game.Name} - {game.SoftwareHouse.Name}");
                        }
                        else
                        {
                            Console.WriteLine("No videogames found...");
                        }
                    }
                    break;
                case Operation.INSERT_HOUSE:
                    {
                        Console.Write("Insert a name (max 50 chars!): ");
                        string softwareHouseName = UConsole.AskStringToCast((input) =>
                        {
                            if (input.Length > 50)
                                throw new Exception();
                            return input;
                        });

                        //Attempt insertion and store result
                        bool success = SoftwareHousesManager.Insert(softwareHouseName);

                        Console.WriteLine(
                            success ?
                            $"Software House '{softwareHouseName}' added."
                            : "Error!"
                            );
                    }
                    break;
                case Operation.INSERT_GAME:
                    {
                        //Ask for ID and look if ID is EXISTENT -> CONVERT to SOFTWARE HOUSE
                        Console.Write("Insert the ID of an EXISTING software house: ");
                        SoftwareHouse softwareHouse = UConsole.AskStringToCast((input) =>
                        {
                            SoftwareHouse? softwareHouse = SoftwareHousesManager.SearchById(Convert.ToInt64(input));
                            /* Keeps asking for a ID until 
                             * 1) given input is convertible to long
                             * 2) long is an existent ID
                             */
                            return softwareHouse == null ? throw new NullReferenceException() : softwareHouse;
                        });

                        Console.Write("Insert a name (max 50 chars!): ");
                        string videogameName = UConsole.AskStringToCast((input) =>
                        {
                            if (input.Length > 50)
                                throw new Exception();
                            return input;
                        });

                        //Attempt insertion and store result
                        bool success = VideogamesManager.Insert(softwareHouse, videogameName);

                        Console.WriteLine(
                            success ?
                            $"Videogame '{videogameName}' by '{softwareHouse.Name}' added."
                            : "Error!"
                            );
                    }
                    break;
                case Operation.SEARCH_BY_ID:
                    {
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
                    }
                    break;
                case Operation.SEARCH_BY_NAME:

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
                case Operation.SEARCH_BY_HOUSE:
                    {
                        Console.Write("Insert an ID: ");
                        SoftwareHouse? softwareHouse = SoftwareHousesManager.SearchById(UConsole.AskLong());

                        if (softwareHouse == null)
                        {
                            Console.WriteLine("[NOT FOUND] No software house for the given ID");
                            break;
                        }

                        //explicitly load Videogames
                        DB.Entry(softwareHouse).Collection(softwareHouse => softwareHouse.Videogames).Load();

                        if (softwareHouse.Videogames.Count > 0)
                        {
                            Console.WriteLine($"\r\n{softwareHouse.Name}");
                            Console.WriteLine("ID - VIDEOGAME");
                            foreach (Videogame game in softwareHouse.Videogames)
                                Console.WriteLine($" {game.Id} - {game.Name}");
                        }
                        else
                        {
                            Console.WriteLine("No videogames found...");
                        }

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