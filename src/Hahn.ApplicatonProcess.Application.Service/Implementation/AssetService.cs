using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using Hahn.ApplicatonProcess.Application.Data.Context;
using Hahn.ApplicatonProcess.Application.Data.Exceptions;
using Hahn.ApplicatonProcess.Application.Domain.Entities;
using Hahn.ApplicatonProcess.Application.Service.Dtos;
using Hahn.ApplicatonProcess.Application.Service.Exceptions;
using Hahn.ApplicatonProcess.Application.Service.Interface;


namespace Hahn.ApplicatonProcess.Application.Service.Implementation
{
    public class AssetService : IAssetService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<Asset> _validator;
        private readonly IHttpClientFactory _clientFactory;

        public AssetService(IUnitOfWork unitOfWork,
            IMapper mapper,
            IValidator<Asset> validator,
            IHttpClientFactory clientFactory)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validator = validator;
            _clientFactory = clientFactory;
        }

        public async Task<AssetReadDto> CreaterAssetAsync(AssetCreateDto assetCreateDto)
        {
            if ( assetCreateDto == null)
            {
                throw new ArgumentNullException();
            }

            var asset = _mapper.Map<Asset>(assetCreateDto);

            if ( asset.CountryOfDepartment != null)
            {
                var countryValid = await ValidateCountry(asset.CountryOfDepartment);

                if (countryValid)
                {
                    _validator.Validate(asset);
                    _unitOfWork.Assets.Add(asset);
                    _unitOfWork.SaveChanges();

                    return _mapper.Map<AssetReadDto>(asset);
                }

                throw new AssetException("Country is not valid.");
            }

            _validator.Validate(asset);
            _unitOfWork.Assets.Add(asset);
            _unitOfWork.SaveChanges();

            return _mapper.Map<AssetReadDto>(asset);
        }

        public void DeleteAsset(int id)
        {
            var asset = _unitOfWork.Assets.Get(id);

            if (asset == null)
            {
                throw new EntityNotFoundException((int)ErrorType.AssetNotFound);
            }

            _unitOfWork.Assets.Remove(asset);
            _unitOfWork.SaveChanges();
        }

        public AssetReadDto GetAssetById(int id)
        {
            var asset = _unitOfWork.Assets.Get(id);

            return _mapper.Map<AssetReadDto>(asset);
        }

        public void UpdateAsset(AssetUpdateDto assetUpdateDto)
        {
            var asset = _unitOfWork.Assets.Get(assetUpdateDto.ID);

            if ( asset == null)
            {
                throw new EntityNotFoundException((int)ErrorType.AssetNotFound);
            }

            _mapper.Map(assetUpdateDto, asset);
            _unitOfWork.SaveChanges();
        }

        public async Task<bool> ValidateCountry(string country)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://restcountries.eu/rest/v2/name/{country}");
            var client = _clientFactory.CreateClient();
            var response = await client.SendAsync(request);

            if ( response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
