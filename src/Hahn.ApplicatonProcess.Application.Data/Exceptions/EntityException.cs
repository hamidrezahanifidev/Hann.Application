using System;
namespace Hahn.ApplicatonProcess.Application.Data.Exceptions
{
    public class EntityException : Exception
    {
        public EntityException(string message): base(message)
        {
        }
    }
}
