using PDM.Business.Entities;
using PDM.Data.Dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDM.Business.Mapper
{
    public static class ProductSubCategoryMapper
    {
        public static void MapDtoToBusiness(ProductSubCategoryDto source, ProductSubCategoryEntity target)
        {
            target.ProductCategoryID = source.ProductCategoryID;
            target.ProductSubCategoryID = source.ProductSubCategoryID;
            target.Name = source.Name;
            target.ModifiedDate = source.ModifiedDate;
        }

        public static void MapBusinessToDto(ProductSubCategoryEntity source, ProductSubCategoryDto target)
        {
            target.ProductCategoryID = source.ProductCategoryID;
            target.ProductSubCategoryID = source.ProductSubCategoryID;
            target.Name = source.Name;
            target.ModifiedDate = source.ModifiedDate;
        }
    }
}
