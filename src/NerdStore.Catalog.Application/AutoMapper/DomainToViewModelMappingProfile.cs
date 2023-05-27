using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using NerdStore.Catalog.Application.ViewModels;
using NerdStore.Catalog.Domain;

namespace NerdStore.Catalog.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            
            CreateMap<Product, ProductViewModel>()
                .ForMember(d => d.Length, o => o.MapFrom(s => s.Dimensions.Length))
                .ForMember(d => d.Height, o => o.MapFrom(s => s.Dimensions.Height))
                .ForMember(d => d.Width, o => o.MapFrom(s => s.Dimensions.Width));

            CreateMap<Category, CategoryViewModel>();
            
        }
    }
}
