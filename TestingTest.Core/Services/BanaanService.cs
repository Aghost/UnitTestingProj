using System;
using System.ComponentModel.DataAnnotations;
using TestingTest.Core.Interfaces;

namespace TestingTest.Core.Services
{
    public class BanaanService : IBanaanService
    {
        private readonly IRepository repository;

        public BanaanService(IRepository repository) {
            this.repository = repository;
        }

        public void Save(Banaan banaan) {
            if (Validate(banaan) == true) {
                repository.Add(banaan);
            }
        }

        public bool Validate(Banaan banaan) {
            if (banaan == null || string.IsNullOrEmpty(banaan.LastName) || string.IsNullOrEmpty(banaan.EmailAddress)) {
                throw new ArgumentNullException();
            }

            if ((repository.AlreadyExists(banaan) == true) || (new EmailAddressAttribute().IsValid(banaan.EmailAddress) == false)) {
                throw new ArgumentException();
            }

            return true;
        }
    }
}
