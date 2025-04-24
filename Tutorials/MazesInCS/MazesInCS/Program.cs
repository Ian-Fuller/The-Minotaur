namespace MazesInCS
{
    class Program
    {
        static void Main(string[] args)
        {
            Grid maze = new Grid(10, 10);
            maze = Algorithms.Sidewinder(maze);
            Console.WriteLine(maze.ToString());
        }
    }
}