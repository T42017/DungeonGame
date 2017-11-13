namespace DungeonMonoGame
{
    public static class Extensions
    {
        public static int Index(this int[,] array, Vector2Int index)
        {
            return array[index.Y, index.X];
        }
    }
}
