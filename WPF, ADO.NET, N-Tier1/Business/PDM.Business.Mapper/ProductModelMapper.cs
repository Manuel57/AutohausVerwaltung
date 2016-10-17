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
    public static class ProductModelMapper
    {
        public static void MapDtoToBusiness(ProductModelDto source, ProductModelEntity target)
        {
            target.ProductModelID = source.ProductModelID;
            target.Name = source.Name;
            target.ModifiedDate = source.ModifiedDate;
        }

        public static void MapBusinessToDto(ProductModelEntity source, ProductModelDto target)
        {
            target.ProductModelID = source.ProductModelID;
            target.Name = source.Name;
            target.ModifiedDate = source.ModifiedDate;
        }
    }
}
