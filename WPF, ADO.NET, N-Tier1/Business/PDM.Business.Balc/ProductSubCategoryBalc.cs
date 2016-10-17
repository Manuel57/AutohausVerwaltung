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
    public class ProductSubCategoryBalc : IBalcBase<ProductSubCategoryEntity>, IDisposable
    {
        private IDalcBase<ProductSubCategoryDto> database;
        public ProductSubCategoryBalc()
        {
            this.database = new ProductSubCategoryDalc();
        }

        public int Update(ProductSubCategoryEntity item)
        {
            ProductSubCategoryDto target = new ProductSubCategoryDto();
            ProductSubCategoryMapper.MapBusinessToDto(item, target);
            return database.Update(target);
        }

        public int Delete(int id)
        {
            return database.Delete(id);
        }

        public int Create(ProductSubCategoryEntity item)
        {
            ProductSubCategoryDto target = new ProductSubCategoryDto();
            ProductSubCategoryMapper.MapBusinessToDto(item, target);
            return database.Create(target);
        }

        public IEnumerable<ProductSubCategoryEntity> GetAll()
        {
            List<ProductSubCategoryEntity> targetList = new List<ProductSubCategoryEntity>();
            foreach (ProductSubCategoryDto source in database.GetAll())
            {
                ProductSubCategoryEntity target = new ProductSubCategoryEntity();
                ProductSubCategoryMapper.MapDtoToBusiness(source, target);
                targetList.Add(target);
            }
            return targetList;
        }

        public ProductSubCategoryEntity GetByID(int id)
        {
            ProductSubCategoryEntity target = new ProductSubCategoryEntity();
            ProductSubCategoryDto source = database.GetByID(id);
            ProductSubCategoryMapper.MapDtoToBusiness(source, target);
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

        ~ProductSubCategoryBalc()
        {
            Dispose(false);
        }

        #endregion
    }
}
