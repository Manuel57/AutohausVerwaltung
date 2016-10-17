using PDM.Data.Dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDM.Data.Mapper
{
    public static class ProductSubCategoryMapper
    {
        public static void MapDataToDto(IDataReader source, ProductSubCategoryDto target)
        {
            target.ProductSubCategoryID = source.GetValue<int>("ProductSubCategoryID");
            target.ProductCategoryID = source.GetValue<int>("ProductCategoryID");
            target.Name = source.GetValue<string>("Name");
            target.ModifiedDate = source.GetValue<DateTime>("ModifiedDate");
        }
    }
}
