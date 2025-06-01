// Entities could use functions such as LoopAction(), InteractAction(), and PlayerInRange()

namespace TheMinotaur
{
    internal struct DataChange
    {
        public int[] coord;
    }

    internal class Entity
    {
        public char tile { get; set;  }

        public Entity() { }

        public virtual DataChange LoopAction() { return new DataChange(); }
    }

    internal class Player : Entity
    {
        public Player() : base()
        {
            tile = 'i';
        }

        public override DataChange LoopAction() // String is used to return data to the maze class, so it can act on it
        {
            DataChange data = new DataChange();

            char input = Console.ReadKey().KeyChar;
            switch(input)
            {
                case 'w':
                    data.coord = [-1, 0];
                    break;
                case 'a':
                    data.coord = [0, -1];
                    break;
                case 's':
                    data.coord = [1, 0];
                    break;
                case 'd':
                    data.coord = [0, 1];
                    break;
                case 'h': // Open manual
                    break;
                case 'x': // Exit game state, returning to main menu
                    Program.ExitGame();
                    break;
                default:
                    break;
            }

            return data;
        }
    }

    internal class Monster : Entity
    {

    }

    internal class Obstacle : Entity
    {

    }

    internal class Item : Entity
    {
        public Item(char tile) : base()
        {
            this.tile = tile;
        }
    }
}
