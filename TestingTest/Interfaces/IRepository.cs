using System;
using System.Collections.Generic;

namespace TestingTest.Interfaces
{
    public interface IRepository
    {
        Object GetById(int id);
        List<Object> GetAll();
        void Update(Object obj);
        void Delete(int id);
        void Delete(Object obj);
        void Add(Object obj);
        bool AlreadyExists(Object obj);
    }
}
