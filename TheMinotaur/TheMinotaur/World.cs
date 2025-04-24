using System.Globalization;

namespace TheMinotaur
{
    internal class World
    {
        Dictionary<string, Map> maps;
        public Map currentMap;
        int mapX;
        int mapY;

        public World()
        {
            mapX = 0;
            mapY = 0;
            currentMap = new Map();
            maps = new Dictionary<string, Map>();
            maps.Add($"{mapX}, {mapY}", currentMap);
        }
    }
}
