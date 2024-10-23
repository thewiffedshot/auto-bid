namespace WebApi.Interfaces
{
    public interface IEntityModel<Entity>
    {
        public Entity ToEntity(Guid? existingRecordId = null);
    }
}