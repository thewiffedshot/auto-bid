namespace WebApi.Interfaces.Mappers;

public abstract class BaseMapper<TSrc, TDest> : IMapper<TSrc, TDest> where TDest : new()
{
    public virtual TDest Map(TSrc source)
    {
        var dest = new TDest();
        Map(source, dest);
        return dest;
    }

    public abstract void Map(TSrc source, TDest destination);
}