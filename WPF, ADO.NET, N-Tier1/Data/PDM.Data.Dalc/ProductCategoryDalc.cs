using PDM.Data.Common;
using PDM.Data.Dto;
using PDM.Data.IDalc;
using PDM.Data.Mapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDM.Data.Dalc
{
    public class ProductCategoryDalc : IDalcBase<ProductCategoryDto>
    {
        public int Update(ProductCategoryDto item)
        {
            int result = 0;
            InputParameters(item);
            using (IDbConnection connection = new SqlConnection(PDMDatabase.DatabaseConnectionString))
            {
                result = (int)DataAccessHelper.ExecuteNonQuery(Constants.UpdateProductCategory, connection);
            }
            return result;
        }

        public int Delete(int id)
        {
            int result = 0;
            DataAccessHelper.AddInputParameters("@productCategoryID", id);
            DataAccessHelper.AddOutputParameters("@rowseffected ", SqlDbType.Int);
            using (IDbConnection connection = new SqlConnection(PDMDatabase.DatabaseConnectionString))
            {
                result = (int)DataAccessHelper.ExecuteNonQuery(Constants.DeleteProductCategory, connection);
            }
            return result;
        }

        public int Create(ProductCategoryDto item)
        {
            int result = 0;
            InputParameters(item);
            using (IDbConnection connection = new SqlConnection(PDMDatabase.DatabaseConnectionString))
            {
                result = (int)DataAccessHelper.ExecuteNonQuery(Constants.InsertProductCategory, connection);
            }
            return result;
        }

        public IEnumerable<ProductCategoryDto> GetAll()
        {
            List<ProductCategoryDto> targetList = new List<ProductCategoryDto>();
            using (IDbConnection connection = new SqlConnection(PDMDatabase.DatabaseConnectionString))
            {
                using (IDataReader reader = DataAccessHelper.ExecuteReader(Constants.GetAllProductCategory, connection))
                {
                    while (reader.Read())
                    {
                        ProductCategoryDto target = new ProductCategoryDto();
                        ProductCategoryMapper.MapDataToDto(reader, target);
                        targetList.Add(target);
                    }
                }
            }
            return targetList;
        }

        public ProductCategoryDto GetByID(int id)
        {
            ProductCategoryDto target = null;
            DataAccessHelper.AddInputParameters("@productCategoryID", id);
            using (IDbConnection connection = new SqlConnection(PDMDatabase.DatabaseConnectionString))
            {
                using (IDataReader reader = DataAccessHelper.ExecuteReader(Constants.GetProductCategoryByID, connection))
                {
                    if (reader.Read())
                    {
                        target = new ProductCategoryDto();
                        ProductCategoryMapper.MapDataToDto(reader, target);
                    }
                }
            }
            return target;
        }


        #region Private Methods
        private void InputParameters(ProductCategoryDto input)
        {
            DataAccessHelper.AddInputParameters("@name", input.Name);
            DataAccessHelper.AddInputParameters("@modifiedDate", input.ModifiedDate);

            if (input.ProductCategoryID > 0)
            {
                DataAccessHelper.AddInputParameters("@productCategoryID", input.ProductCategoryID);
                DataAccessHelper.AddOutputParameters("@rowseffected ", input.ProductCategoryID);
            }
            else
            {
                DataAccessHelper.AddOutputParameters("@productCategoryID", input.ProductCategoryID);
            }
        }
        #endregion
    }
}
