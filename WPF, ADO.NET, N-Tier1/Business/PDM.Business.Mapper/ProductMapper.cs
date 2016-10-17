using PDM.Business.Entities;
using PDM.Data.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDM.Business.Mapper
{
    public static class ProductMapper
    {
        public static void MapDtoToBusiness(ProductDto source, ProductEntity target)
        {
            target.ProductID = source.ProductID;
            target.Name = source.Name;
            target.ProductNumber = source.ProductNumber;
            target.MakeFlag = source.MakeFlag;
            target.Color = source.Color;
            target.StandardCost = source.StandardCost;
            target.ListPrice = source.ListPrice;
            target.Size = source.Size;
            target.Weight = source.Weight;
            target.Style = source.Style;
            target.SellStartDate = source.SellStartDate;
            target.ModifiedDate = source.ModifiedDate;
            target.ProductSubCategoryID = source.ProductSubCategoryID;
            target.ProductModelID = source.ProductModelID;
            target.PhotoID = source.PhotoID;
        }

        public static void MapBusinessToDto(ProductEntity source, ProductDto target)
        {
            target.ProductID = source.ProductID;
            target.Name = source.Name;
            target.ProductNumber = source.ProductNumber;
            target.MakeFlag = source.MakeFlag;
            target.Color = source.Color;
            target.StandardCost = source.StandardCost;
            target.ListPrice = source.ListPrice;
            target.Size = source.Size;
            target.Weight = source.Weight;
            target.Style = source.Style;
            target.SellStartDate = source.SellStartDate;
            target.ModifiedDate = source.ModifiedDate;
            target.ProductSubCategoryID = source.ProductSubCategoryID;
            target.ProductModelID = source.ProductModelID;
            target.PhotoID = source.PhotoID;
        }
    }
}
