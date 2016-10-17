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
    public static class PhotoMapper
    {
        public static void MapDtoToBusiness(PhotoDto source, PhotoEntity target)
        {
            target.PhotoID = source.PhotoID;
            target.Name = source.Name;
            target.Image = source.Image;
            target.ModifiedDate = source.ModifiedDate;
        }

        public static void MapBusinessToDto(PhotoEntity source, PhotoDto target)
        {
            target.PhotoID = source.PhotoID;
            target.Name = source.Name;
            target.Image = source.Image;
            target.ModifiedDate = source.ModifiedDate;
        }
    }
}
