namespace WebApi.Interfaces.Mappers;

public interface IMapper<TSrc, TDest>
{
    public TDest Map(TSrc source);
    public void Map(TSrc source, TDest destination);
}

public interface ICollectionMapper<TSrc, TDest> : IMapper<ICollection<TSrc>, ICollection<TDest>>
{
}
