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
    public class ProductSubCategoryDalc : IDalcBase<ProductSubCategoryDto>
    {
        public int Update(ProductSubCategoryDto item)
        {
            int result = 0;
            InputParameters(item);
            using (IDbConnection connection = new SqlConnection(PDMDatabase.DatabaseConnectionString))
            {
                result = (int)DataAccessHelper.ExecuteNonQuery(Constants.UpdateProductSubCategory, connection);
            }
            return result;
        }

        public int Delete(int id)
        {
            int result = 0;
            DataAccessHelper.AddInputParameters("@productSubCategoryID", id);
            DataAccessHelper.AddOutputParameters("@rowseffected ", SqlDbType.Int);
            using (IDbConnection connection = new SqlConnection(PDMDatabase.DatabaseConnectionString))
            {
                result = (int)DataAccessHelper.ExecuteNonQuery(Constants.DeleteProductSubCategory, connection);
            }
            return result;
        }

        public int Create(ProductSubCategoryDto item)
        {
            int result = 0;
            InputParameters(item);
            using (IDbConnection connection = new SqlConnection(PDMDatabase.DatabaseConnectionString))
            {
                result = (int)DataAccessHelper.ExecuteNonQuery(Constants.InsertProductSubCategory, connection);
            }
            return result;
        }

        public IEnumerable<ProductSubCategoryDto> GetAll()
        {
            List<ProductSubCategoryDto> targetList = new List<ProductSubCategoryDto>();
            using (IDbConnection connection = new SqlConnection(PDMDatabase.DatabaseConnectionString))
            {
                using (IDataReader reader = DataAccessHelper.ExecuteReader(Constants.GetAllProductSubCategory, connection))
                {
                    while (reader.Read())
                    {
                        ProductSubCategoryDto target = new ProductSubCategoryDto();
                        ProductSubCategoryMapper.MapDataToDto(reader, target);
                        targetList.Add(target);
                    }
                }
            }
            return targetList;
        }

        public ProductSubCategoryDto GetByID(int id)
        {
            ProductSubCategoryDto target = null;
            DataAccessHelper.AddInputParameters("@productSubCategoryID", id);
            using (IDbConnection connection = new SqlConnection(PDMDatabase.DatabaseConnectionString))
            {
                using (IDataReader reader = DataAccessHelper.ExecuteReader(Constants.GetProductSubCategoryByID, connection))
                {
                    if (reader.Read())
                    {
                        target = new ProductSubCategoryDto();
                        ProductSubCategoryMapper.MapDataToDto(reader, target);
                    }
                }
            }
            return target;
        }

        #region Private Methods
        private void InputParameters(ProductSubCategoryDto input)
        {
            DataAccessHelper.AddInputParameters("@name", input.Name);
            DataAccessHelper.AddInputParameters("@modifiedDate", input.ModifiedDate);
            
            if (input.ProductSubCategoryID > 0)
            {
                DataAccessHelper.AddInputParameters("@productSubCategoryID", input.ProductSubCategoryID);
                DataAccessHelper.AddOutputParameters("@rowseffected ", input.ProductSubCategoryID);
            }
            else
            {
                DataAccessHelper.AddInputParameters("@productCategoryID", input.ProductCategoryID);
                DataAccessHelper.AddOutputParameters("@productSubCategoryID", input.ProductSubCategoryID);
            }
        }
        #endregion
    }
}
