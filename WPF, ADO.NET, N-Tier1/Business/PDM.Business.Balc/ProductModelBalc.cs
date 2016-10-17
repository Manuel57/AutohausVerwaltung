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
    public class ProductModelBalc : IBalcBase<ProductModelEntity>, IDisposable
    {
        private IDalcBase<ProductModelDto> database;

        public ProductModelBalc()
        {
            this.database = new ProductModelDalc();
        }
        public int Update(ProductModelEntity item)
        {
            ProductModelDto target = new ProductModelDto();
            ProductModelMapper.MapBusinessToDto(item, target);
            return database.Update(target);
        }

        public int Delete(int id)
        {
            return database.Delete(id);
        }

        public int Create(ProductModelEntity item)
        {
            ProductModelDto target = new ProductModelDto();
            ProductModelMapper.MapBusinessToDto(item, target);
            return database.Create(target);
        }

        public IEnumerable<ProductModelEntity> GetAll()
        {
            List<ProductModelEntity> targetList = new List<ProductModelEntity>();
            foreach (ProductModelDto source in database.GetAll())
            {
                ProductModelEntity target = new ProductModelEntity();
                ProductModelMapper.MapDtoToBusiness(source, target);
                targetList.Add(target);
            }
            return targetList;
        }

        public ProductModelEntity GetByID(int id)
        {
            ProductModelEntity target = new ProductModelEntity();
            ProductModelDto source = database.GetByID(id);
            ProductModelMapper.MapDtoToBusiness(source, target);
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

        ~ProductModelBalc()
        {
            Dispose(false);
        }

        #endregion
    }
}
