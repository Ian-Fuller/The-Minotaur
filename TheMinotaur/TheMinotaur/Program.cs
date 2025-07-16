/*
TO-DO:
 - (DONE) Learn out how to generate mazes
 - (IN PROGRESS) Create entities and the player
 - (IN PROGRESS) Write PopulateMaze()
 - Enable travel between mazes (player travels to the correct maze, but does not appear in the doorway, and the game encounters the loop bug)
 - Add a Structure class
 - Make CleanMaze() more efficient
 - Fix the bug where the program loops infintely and doesn't accept input
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
        // These variables are class members, rather than inside the main() function because the player Entity needs to be able to access them easily

        static void Main(string[] args)
        {
            bool running = true;
            State programState = State.MainMenu;

            while (running)
            {
                Console.Clear();
                switch (programState)
                {
                    case State.Game:
                        {
                            World gameWorld = new World();
                            bool gameLoop = true;
                            while (gameLoop)
                            {
                                Console.SetCursorPosition(0, 0);
                                gameWorld.currentMap.PrintMaze();

                                char playerInput = gameWorld.Loop();
                                if (playerInput == 'x')
                                {
                                    gameLoop = false;
                                }
                            }

                            Console.Write("-= GAME PAUSED =-\nPlay Game (Continue) (p)\nOptions (o)\nOpen Manual (h)\nExit to Main Menu (x)\n");
                            char input = Console.ReadKey().KeyChar;
                            switch (input)
                            {
                                case 'p': // Enter Game state
                                    // Do nothing
                                    break;
                                case 'o': // Enter Option state
                                    programState = State.Options;
                                    break;
                                case 'h': // Open manual
                                    break;
                                case 'x': // Set running = false
                                    programState = State.MainMenu;
                                    break;
                                default:
                                    break;
                            }
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
    }
}