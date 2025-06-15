// Entities could use functions such as LoopAction(), InteractAction(), and PlayerInRange()

namespace TheMinotaur
{
    internal struct DataChange
    {
        public char playerInput;
        public int[] move;
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

            data.playerInput = Console.ReadKey().KeyChar;
            switch(data.playerInput)
            {
                case 'w':
                    data.move = [-1, 0];
                    break;
                case 'a':
                    data.move = [0, -1];
                    break;
                case 's':
                    data.move = [1, 0];
                    break;
                case 'd':
                    data.move = [0, 1];
                    break;
                case 'h': // Open manual
                    break;
                case 'x': // Pause the game
                    // Do nothing. The loop() function in Maze.cs will handle this
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

    internal class Door : Entity
    {
        public Door() : base()
        {
            this.tile = ' ';
        }
    }
}
