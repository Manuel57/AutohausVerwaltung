﻿using PDM.Data.Dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDM.Data.Mapper
{
    public static class ProductModelMapper
    {
        public static void MapDataToDto(IDataReader source, ProductModelDto target)
        {
            target.ProductModelID = source.GetValue<int>("ProductModelID");
            target.Name = source.GetValue<string>("Name");
            target.ModifiedDate = source.GetValue<DateTime>("ModifiedDate");
        }
    }
}
