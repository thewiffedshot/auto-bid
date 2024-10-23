namespace WebApi.Interfaces
{
    public interface IEntity<Model>
    {
        public Model ToModel();
    }
}