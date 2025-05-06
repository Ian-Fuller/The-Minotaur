/*
TO-DO:
 - Learn out how to generate mazes
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
                        World gameWorld = new World();
                        bool gameLoop = true;
                        while (gameLoop)
                        {
                            gameWorld.currentMap.PrintMap();

                            char input = Console.ReadKey().KeyChar;
                            switch (input)
                            {
                                case 'h': // Open manual
                                    break;
                                case 'x': // Exit options state, returning to main menu
                                    programState = State.MainMenu;
                                    gameLoop = false;
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