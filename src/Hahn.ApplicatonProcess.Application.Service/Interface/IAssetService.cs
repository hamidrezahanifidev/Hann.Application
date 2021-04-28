using System;
using System.Threading.Tasks;
using Hahn.ApplicatonProcess.Application.Domain.Entities;
using Hahn.ApplicatonProcess.Application.Service.Dtos;

namespace Hahn.ApplicatonProcess.Application.Service.Interface
{
    public interface IAssetService
    {
        Task<AssetReadDto> CreaterAssetAsync(AssetCreateDto assetCreateDto);
        void UpdateAsset(AssetUpdateDto assetUpdateDto);
        void DeleteAsset(int id);
        AssetReadDto GetAssetById(int id);
    }
}
