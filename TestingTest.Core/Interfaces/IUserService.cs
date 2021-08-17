namespace TestingTest.Core.Interfaces
{
    public interface IUserService
    {
        void Save(User user);
        bool Validate(User user);
    }
}
