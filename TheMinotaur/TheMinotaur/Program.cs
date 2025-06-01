/*
TO-DO:
 - (DONE) Learn out how to generate mazes
 - Create entities and the player
 - Write PopulateMaze()
 - Add a Structure class
 - Make CleanMaze() more efficient
*/

namespace TheMinotaur
{
    public enum State
    {
        MainMenu,
        Options,
        Game
    }

    public class Program
    {
        static bool running;
        static bool gameLoop;
        static State programState;

        static void Main(string[] args)
        {
            running = true;
            programState = State.MainMenu;

            while (running)
            {
                Console.Clear();
                switch (programState)
                {
                    case State.Game:
                        World gameWorld = new World();
                        gameLoop = true;
                        while (gameLoop)
                        {
                            Console.SetCursorPosition(0, 0);
                            gameWorld.currentMap.PrintMaze();

                            // This block doesn't get player input, as thet is handled by the player object
                            gameWorld.currentMap.Loop();
                        }
                        break;
                    case State.MainMenu:
                        {
                            Console.Write("Play Game (p)\nOptions (o)\nOpen Manual (h)\nExit (x)\n");
                            char input = Console.ReadKey().KeyChar;
                            switch (input)
                            {
                                case 'p': // Enter Game state
                                    programState = State.Game;
                                    break;
                                case 'o': // Enter Option state
                                    programState = State.Options;
                                    break;
                                case 'h': // Open manual
                                    break;
                                case 'x': // Set running = false
                                    running = false;
                                    break;
                                default:
                                    break;
                            }
                        }
                        break;
                    case State.Options:
                        {
                            Console.Write("Exit (x)\n");
                            char input = Console.ReadKey().KeyChar;
                            switch (input)
                            {
                                case 'h': // Open manual
                                    break;
                                case 'x': // Exit options state, returning to main menu
                                    programState = State.MainMenu;
                                    break;
                                default:
                                    break;
                            }
                        }
                        break;
                }
            }
        }

        public static void ExitGame()
        {
            programState = State.MainMenu;
            gameLoop = false;
        }
    }
}