using AutoMapper;
using EmployeeManagementAPI.Models;
using EmployeeManagementAPI.DTOs;

namespace EmployeeManagementAPI.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Department Mappings
            CreateMap<Department, DepartmentDto>()
                .ForMember(dest => dest.Employees, opt => opt.MapFrom(src => src.Employees));
            CreateMap<CreateDepartmentDto, Department>();
            CreateMap<UpdateDepartmentDto, Department>();

            // Employee Mappings
            CreateMap<Employee, EmployeeDto>()
                .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department != null ? src.Department.Name : null));
            CreateMap<CreateEmployeeDto, Employee>();
            CreateMap<UpdateEmployeeDto, Employee>();
            CreateMap<Employee, EmployeeSlimDto>();
        }
    }
}
