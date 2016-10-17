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
    public static class ProductCategoryMapper
    {
        public static void MapDtoToBusiness(ProductCategoryDto source, ProductCategoryEntity target)
        {
            target.ProductCategoryID = source.ProductCategoryID;
            target.Name = source.Name;
            target.ModifiedDate = source.ModifiedDate;
        }

        public static void MapBusinessToDto(ProductCategoryEntity source, ProductCategoryDto target)
        {
            target.ProductCategoryID = source.ProductCategoryID;
            target.Name = source.Name;
            target.ModifiedDate = source.ModifiedDate;
        }
    }
}
