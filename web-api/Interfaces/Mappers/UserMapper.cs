using WebApi.Data.Models;
using WebApi.Interfaces.Models;

namespace WebApi.Interfaces.Mappers
{
    public class UserMapper : BaseMapper<UserModel, User>
    {
        public override void Map(UserModel source, User destination)
        {
            destination.FirstName = source.FirstName;
            destination.LastName = source.LastName;
            destination.Email = source.Email;
            destination.Username = source.Username;
        }
    }
}