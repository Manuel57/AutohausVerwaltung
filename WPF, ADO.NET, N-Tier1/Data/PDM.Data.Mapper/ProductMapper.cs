using PDM.Data.Dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDM.Data.Mapper
{
    public static class ProductMapper
    {
        public static void MapDataToDto(IDataReader source, ProductDto target)
        {
            target.ProductID = source.GetValue<int>("ProductID");
            target.Name = source.GetValue<string>("Name");
            target.ProductNumber = source.GetValue<string>("ProductNumber");
            target.MakeFlag = source.GetValue<bool>("MakeFlag");
            target.Color = source.GetValue<string>("Color");
            target.StandardCost = source.GetValue<decimal>("StandardCost");
            target.ListPrice = source.GetValue<decimal>("ListPrice");
            target.Size = source.GetValue<string>("Size");
            target.Weight = source.GetValue<decimal>("Weight");
            target.Style = source.GetValue<string>("Style");
            target.SellStartDate = source.GetValue<DateTime>("SellStartDate");
            target.ModifiedDate = source.GetValue<DateTime>("ModifiedDate");
            target.ProductSubCategoryID = source.GetValue<int>("ProductSubCategoryID");
            target.ProductModelID = source.GetValue<int>("ProductModelID");
            target.PhotoID = source.GetValue<int>("PhotoID");
        }
    }
}
