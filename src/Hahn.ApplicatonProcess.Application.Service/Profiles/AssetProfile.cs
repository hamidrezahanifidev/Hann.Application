using System;
using AutoMapper;
using Hahn.ApplicatonProcess.Application.Domain.Entities;
using Hahn.ApplicatonProcess.Application.Domain.Enums;
using Hahn.ApplicatonProcess.Application.Service.Dtos;

namespace Hahn.ApplicatonProcess.Application.Service.Profiles
{
    public class AssetProfile : Profile
    {
        public AssetProfile()
        {
            CreateMap<AssetCreateDto, Asset>();
            CreateMap<Asset, AssetReadDto>()
                .ForMember(destination => destination.DepartmentName, opt => opt.MapFrom(source => Enum.GetName(typeof(Department), source.Department)))
                .ForMember(destination => destination.DepartmentCode, opt => opt.MapFrom(source => source.Department));
            CreateMap<AssetUpdateDto, Asset>();
        }
    }
}
