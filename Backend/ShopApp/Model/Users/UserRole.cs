using Shop.Model;

namespace ShopApp.Model.Users
{
    public class UserRole : Entity
    {
        public string RoleName { get; set; }
        public string RoleId { get; set; }
        public List<User> Users { get; set; }
    }
}
