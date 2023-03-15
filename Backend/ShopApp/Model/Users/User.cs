using Shop.Model;

namespace ShopApp.Model.Users
{
    public class User : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public string Phone { get; set; }

        public int? RoleId { get; set; }
        public UserRole UserRole { get; set; }
        public IEnumerable<Payment> PaymentMethod { get; set; }
    }
}
