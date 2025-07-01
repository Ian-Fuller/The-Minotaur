using System.Globalization;

namespace TheMinotaur
{
    internal class World
    {
        Player player;
        Dictionary<string, Maze> maps;
        public Maze currentMap;
        int mapX;
        int mapY;

        public World()
        {
            mapX = 0;
            mapY = 0;
            maps = new Dictionary<string, Maze>();
            currentMap = new Maze();
            maps.Add($"{mapX}, {mapY}", currentMap);

            // Add player
            Player player = new Player();

            Random rand = new Random();

            bool running = true;
            while (running)
            {
                int t = (rand.Next(0, currentMap.top));
                int l = (rand.Next(0, currentMap.left));

                if (currentMap.tiles[t, l] == ' ')
                {
                    currentMap.entities[[t, l]] = player;
                    running = false;
                }
            }
        }

        public char Loop()
        {
            return currentMap.Loop(this);
        }

        public void ChangeMap(KeyValuePair<int[], Entity> player)
        {
            Console.WriteLine($"{player.Key[0]}, {player.Key[1]}");
            int moveX = player.Key[1] == 0 ? -1 : 1; // X uses the second index because the key value pair uses a top left system
            int moveY = player.Key[0] == 0 ? -1 : 1;

            mapX += moveX;
            mapY += moveY;
            if (maps.ContainsKey($"{mapX}, {mapY}"))
            {
                currentMap = maps[$"{mapX}, {mapY}"];
            }
            else
            {
                currentMap = new Maze();
                maps.Add($"{mapX}, {mapY}", currentMap);
                // calculate player location in new maze
                currentMap.entities.Add([player.Key[0], player.Key[1]], player.Value);
            }
        }
    }
}
