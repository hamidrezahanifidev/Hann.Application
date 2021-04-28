using System;
using Hahn.ApplicatonProcess.Application.Domain.Enums;

namespace Hahn.ApplicatonProcess.Application.Domain.Entities
{
    public class Asset
    {
        public int ID { get; set; }

        public string AssetName { get; set; }

        public Department Department { get; set; }

        public string CountryOfDepartment { get; set; }

        public string EmailOfDepartment { get; set; }

        public DateTime PurchaseDate { get; set; }

        public bool Broken { get; set; }
    }
}
