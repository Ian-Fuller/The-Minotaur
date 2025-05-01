namespace MazesInCS
{
    class Program
    {
        static void Main(string[] args)
        {
            Grid maze = new Grid(10, 10);
            maze = Algorithms.DepthFirst(maze);
            Console.WriteLine(maze.ToString());
        }
    }
}