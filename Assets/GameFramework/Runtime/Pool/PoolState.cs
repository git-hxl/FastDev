namespace GameFramework
{
    public enum PoolState
    {
        None = 0,
        Allocated,
        WaitToRecycled,
        Recycled,
    }
}
