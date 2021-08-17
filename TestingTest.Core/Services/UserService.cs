using System;
using System.ComponentModel.DataAnnotations;
using TestingTest.Core.Interfaces;

namespace TestingTest.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository repository;

        public UserService(IRepository repository) {
            this.repository = repository;
        }

        public void Save(User user) {
            if (Validate(user) == true) {
                repository.Add(user);
            }

        }

        public bool Validate(User user) {
            if (user == null || string.IsNullOrEmpty(user.LastName) || string.IsNullOrEmpty(user.EmailAddress)) {
                throw new ArgumentNullException();
            }

            if ((repository.AlreadyExists(user) == true) || (new EmailAddressAttribute().IsValid(user.EmailAddress) == false)) {
                throw new ArgumentException();
            }

            return true;
        }
    }
}
