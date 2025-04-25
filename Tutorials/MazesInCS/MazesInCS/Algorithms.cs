using System.Diagnostics;

namespace MazesInCS
{
    public class Algorithms
    {
        public static Grid BinaryTree(Grid grid, int seed = -1)
        {
            var rand = seed >= 0 ? new Random(seed) : new Random();

            foreach (var cell in grid.Cells)
            {
                var neighbors = new[] { cell.North, cell.East }.Where(c => c != null).ToList();
                if (!neighbors.Any())
                {
                    continue;
                }

                var neighbor = ListExtensions.Sample(neighbors, rand);
                if (neighbor != null)
                {
                    cell.Link(neighbor);
                }
            }

            return grid;
        }

        public static Grid Sidewinder(Grid grid, int seed = -1)
        {
            var rand = seed >= 0 ? new Random(seed) : new Random();

            foreach (var row in grid.Row)
            {
                var run = new List<Cell>();

                foreach (var cell in row)
                {
                    run.Add(cell);

                    var atEasternBoundary = cell.East == null;
                    var atNorthernBoundary = cell.North == null;

                    var shouldCloseOut = atEasternBoundary || (!atNorthernBoundary && rand.Next(2) == 0);

                    if (shouldCloseOut)
                    {
                        var member = ListExtensions.Sample(run, rand);
                        if (member.North != null)
                        {
                            member.Link(member.North);
                        }
                        run.Clear();
                    }
                    else
                    {
                        cell.Link(cell.East);
                    }
                }
            }

            return grid;
        }

        // Pick a cell at random, place it on the queue and mark it as visited
        // Travel to an adjacent cell that hasn't been visited yet, add it to the queue, link it to the previous cell, and mark it as visited
        // Repeat the above process until at a cell which has no adjacent unvisited cells, then travel back up the queue until a cell with an adjacent unvisited cell is found
        // This process should ultimately lead to the algorithm ending back up at the starting cell, which will signal the algorithm being done.
        public static Grid DepthFirst(Grid grid, int seed = -1)
        {
            var rand = seed >= 0 ? new Random(seed) : new Random();


            return grid;
        }
    }
}
