using System;
using FluentValidation;
using Hahn.ApplicatonProcess.Application.Data.Context;
using Hahn.ApplicatonProcess.Application.Data.Exceptions;
using Hahn.ApplicatonProcess.Application.Domain.Entities;

namespace Hahn.ApplicatonProcess.Application.Data.Validators
{
    public class AssetValidator : AbstractValidator<Asset>
    {
        public AssetValidator()
        {
            RuleFor(p => p.AssetName)
                .Length(5, 50)
                .OnAnyFailure(p =>
                {
                    throw new EntityException("Lenght of asset name is invalid!");
                });

            RuleFor(p => p.Department)
                .IsInEnum()
                .OnAnyFailure(p =>
                {
                    throw new EntityException("Department value is invalid!");
                });

            RuleFor(p => p.EmailOfDepartment)
                .EmailAddress()
                .OnAnyFailure(p =>
                {
                    throw new EntityException("Email address is invalid!");
                });

            RuleFor(p => p.Broken)
                .NotNull()
                .OnAnyFailure(p =>
                {
                    throw new EntityException("Broken value can not be null!");
                });

            RuleFor(p => p.PurchaseDate)
               .Must(PurchaseDateIsValid)
               .OnAnyFailure(p =>
               {
                   throw new EntityException("Purchase date must be older than 1 year!");
               });

        }

        protected bool PurchaseDateIsValid(DateTime date)
        {
            if (date.Year >= 1)
            {
                return true;
            }

            return false;
        }
    
    }
}
