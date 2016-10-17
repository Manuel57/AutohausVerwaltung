using PDM.Data.Dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDM.Data.Mapper
{
    public static class PhotoMapper
    {
        public static void MapDataToDto(IDataReader source, PhotoDto target)
        {
            target.PhotoID = source.GetValue<int>("PhotoID");
            target.Name = source.GetValue<string>("Name");
            target.Image = source.GetValue<byte[]>("Image");
            target.ModifiedDate = source.GetValue<DateTime>("ModifiedDate");
        }
    }
}
