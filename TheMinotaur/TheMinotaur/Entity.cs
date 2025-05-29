namespace TheMinotaur
{
    internal class Entity
    {
        public char tile { get; set;  }

        public Entity() { }
    }

    internal class Player : Entity
    {
        public Player() : base()
        {
            tile = 'i';
        }

        public void SayHello()
        {
            Console.WriteLine("Hello!");
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
