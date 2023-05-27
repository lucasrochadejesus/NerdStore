using AutoMapper;
using NerdStore.Catalog.Application.ViewModels;
using NerdStore.Catalog.Domain;

namespace NerdStore.Catalog.Application.AutoMapper;

public class ViewModelToDomainMappingProfile : Profile
{
    public ViewModelToDomainMappingProfile()
    {
        CreateMap<ProductViewModel, Product>()
            .ConstructUsing(p => 
                new Product(p.Name,
                    p.Description,
                    p.Active,
                    p.DtCreation,
                    p.Price,
                    p.CategoryId,
                    p.Image,
                    p.ModelNumber,
                    new Dimensions(p.Height,p.Width,p.Length)));



        CreateMap<CategoryViewModel,Category>()
            .ConstructUsing(c => new Category(c.Name,c.Code));
    }
}