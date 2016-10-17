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
    public class ProductCategoryBalc : IBalcBase<ProductCategoryEntity>, IDisposable
    {
        private IDalcBase<ProductCategoryDto> database;
        public ProductCategoryBalc()
        {
            this.database = new ProductCategoryDalc();
        }
        public int Update(ProductCategoryEntity item)
        {
            ProductCategoryDto target = new ProductCategoryDto();
            ProductCategoryMapper.MapBusinessToDto(item, target);
            return database.Update(target);
        }

        public int Delete(int id)
        {
            return database.Delete(id);
        }

        public int Create(ProductCategoryEntity item)
        {
            ProductCategoryDto target = new ProductCategoryDto();
            ProductCategoryMapper.MapBusinessToDto(item, target);
            return database.Create(target);
        }

        public IEnumerable<ProductCategoryEntity> GetAll()
        {
            List<ProductCategoryEntity> targetList = new List<ProductCategoryEntity>();
            foreach (ProductCategoryDto source in database.GetAll())
            {
                ProductCategoryEntity target = new ProductCategoryEntity();
                ProductCategoryMapper.MapDtoToBusiness(source, target);
                targetList.Add(target);
            }
            return targetList;
        }

        public ProductCategoryEntity GetByID(int id)
        {
            ProductCategoryEntity target = new ProductCategoryEntity();
            ProductCategoryDto source = database.GetByID(id);
            ProductCategoryMapper.MapDtoToBusiness(source, target);
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

        ~ProductCategoryBalc()
        {
            Dispose(false);
        }

        #endregion
    }
}
