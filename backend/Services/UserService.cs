using WebApi.Entities;

namespace WebApi.Services
{
    public interface IUserService
    {
        User GetById(Guid userId);
    }
    public class UserService : IUserService
    {
        private List<User> _users = new List<User>();

        public User GetById(Guid userId)
        {
            var user = _users.Find(x => x.Id == userId);
            if (user == null)
            {
                throw new Exception("User with the given GUID does not exist");
            }
            return user;
        }
    }
}
