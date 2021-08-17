using System;
using System.Collections.Generic;
using TestingTest.Core.Interfaces;

namespace TestingTest.Core
{
    public class Repository : IRepository
    {
        public void Add(Object obj) {
            throw new NotImplementedException();
        }

        public void Delete(int id) {
            throw new NotImplementedException();
        }

        public void Delete(Object obj) {
            throw new NotImplementedException();
        }

        public List<Object> GetAll() {
            throw new NotImplementedException();
        }

        public Object GetById(int id) {
            throw new NotImplementedException();
        }

        public void Update(Object obj) {
            throw new NotImplementedException();
        }

        public bool AlreadyExists(Object obj) {
            throw new NotImplementedException();
        }
    }
}
