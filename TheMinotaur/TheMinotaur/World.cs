using System.Globalization;

namespace TheMinotaur
{
    internal class World
    {
        Dictionary<string, Maze> maps;
        public Maze currentMap;
        int mapX;
        int mapY;

        public World()
        {
            mapX = 0;
            mapY = 0;
            currentMap = new Maze();
            maps = new Dictionary<string, Maze>();
            maps.Add($"{mapX}, {mapY}", currentMap);
        }
    }
}
