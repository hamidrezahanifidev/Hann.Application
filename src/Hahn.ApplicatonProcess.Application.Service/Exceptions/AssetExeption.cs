using System;
using System.Collections.Generic;
using FluentValidation.Results;

namespace Hahn.ApplicatonProcess.Application.Service.Exceptions
{
    public class AssetException : Exception
    {
        public AssetException(string message) : base(message)
        {
        }

    }
}
