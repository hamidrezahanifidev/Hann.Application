using System;
using Hahn.ApplicatonProcess.Application.Domain.Enums;

namespace Hahn.ApplicatonProcess.Application.Service.Dtos
{
    public class AssetCreateDto
    {
        /// <summary>
        /// The id of the asset
        /// </summary>
        /// <example>1</example>
        public int ID { get; set; }

        /// <summary>
        /// The name of the asset
        /// </summary>
        /// <example>MyAsset</example>
        public string AssetName { get; set; }

        /// <summary>
        /// The department of the asset
        /// </summary>
        /// <example>0</example>
        public Department Department { get; set; }

        /// <summary>
        /// The country of department of the asset
        /// </summary>
        /// <example>Germany</example>
        public string CountryOfDepartment { get; set; }

        /// <summary>
        /// The email of department of the asset
        /// </summary>
        /// <example>Hann@gmail.com</example>
        public string EmailOfDepartment { get; set; }

        /// <summary>
        /// The purchase date of the asset
        /// </summary>
        /// <example>2021-04-19T10:36:51.656Z</example>
        public DateTime PurchaseDate { get; set; }

        /// <summary>
        /// The condition of the asset
        /// </summary>
        /// <example>false</example>
        public bool Broken { get; set; }
    }
}
