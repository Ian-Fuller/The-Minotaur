namespace MazesInCS
{
    public static class ListExtensions
    {
        public static T Sample<T>(List<T> list, Random rand = null)
        {
            if (rand == null)
            {
                rand = new Random();
            }
            return list[rand.Next(list.Count)];
        }
    }
}
