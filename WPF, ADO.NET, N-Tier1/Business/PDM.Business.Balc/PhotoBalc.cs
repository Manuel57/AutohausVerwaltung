using PDM.Business.Entities;
using PDM.Business.IBalc;
using PDM.Business.Mapper;
using PDM.Data.Dalc;
using PDM.Data.Dto;
using PDM.Data.IDalc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDM.Business.Balc
{
    public class PhotoBalc : IBalcBase<PhotoEntity>, IDisposable
    {
        private IDalcBase<PhotoDto> database;
        public PhotoBalc()
        {
            this.database = new PhotoDalc();
        }

        public int Update(PhotoEntity item)
        {
            PhotoDto target = new PhotoDto();
            PhotoMapper.MapBusinessToDto(item, target);
            return database.Update(target);
        }

        public int Delete(int id)
        {
            return database.Delete(id);
        }

        public int Create(PhotoEntity item)
        {
            PhotoDto target = new PhotoDto();
            PhotoMapper.MapBusinessToDto(item, target);
            return database.Create(target);
        }

        public IEnumerable<PhotoEntity> GetAll()
        {
            List<PhotoEntity> targetList = new List<PhotoEntity>();
            foreach (PhotoDto source in database.GetAll())
            {
                PhotoEntity target = new PhotoEntity();
                PhotoMapper.MapDtoToBusiness(source, target);
                targetList.Add(target);
            }
            return targetList;
        }

        public PhotoEntity GetByID(int id)
        {
            PhotoEntity target = new PhotoEntity();
            PhotoDto source = database.GetByID(id);
            PhotoMapper.MapDtoToBusiness(source, target);
            return target;
        }

          #region Implement IDisposable

        private bool disposed;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        private void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    if (database != null)

                        database = null;
                }
                disposed = true;
            }
        }

        ~PhotoBalc()
        {
            Dispose(false);
        }

        #endregion
    }
}
