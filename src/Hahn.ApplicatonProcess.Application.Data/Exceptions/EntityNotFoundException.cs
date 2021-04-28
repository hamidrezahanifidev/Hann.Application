using System;
namespace Hahn.ApplicatonProcess.Application.Data.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public int _id;

        public EntityNotFoundException(int id)
        {
            _id = id;
        }
    }
}
