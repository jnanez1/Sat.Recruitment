namespace Entities.Interface
{
    public interface IUser
    {
        string Address { get; set; }
        string Email { get; set; }
        string Money { get; set; }
        string Name { get; set; }
        string Phone { get; set; }
        string UserType { get; set; }
    }
}