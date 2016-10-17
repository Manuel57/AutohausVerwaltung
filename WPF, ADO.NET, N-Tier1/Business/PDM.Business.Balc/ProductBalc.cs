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
    public class ProductBalc : IBalcBase<ProductEntity>, IDisposable
    {
        IDalcBase<ProductDto> database;
        public ProductBalc()
        {
            database = new ProductDalc();
        }
        public int Update(ProductEntity item)
        {
            ProductDto target = new ProductDto();
            ProductMapper.MapBusinessToDto(item, target);
            return database.Update(target);
        }

        public int Delete(int id)
        {
            return database.Delete(id);
        }

        public int Create(ProductEntity item)
        {
            ProductDto target = new ProductDto();
            ProductMapper.MapBusinessToDto(item, target);
            return database.Create(target);
        }

        public IEnumerable<ProductEntity> GetAll()
        {

            List<ProductEntity> targetList = new List<ProductEntity>();
            foreach (ProductDto source in database.GetAll())
            {
                ProductEntity target = new ProductEntity();
                ProductMapper.MapDtoToBusiness(source, target);
                targetList.Add(target);
            }
            return targetList;
        }

        public ProductEntity GetByID(int id)
        {
            ProductEntity target = new ProductEntity();
            ProductDto source = database.GetByID(id);
            ProductMapper.MapDtoToBusiness(source, target);
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

        ~ProductBalc()
        {
            Dispose(false);
        }

        #endregion
    }
}
