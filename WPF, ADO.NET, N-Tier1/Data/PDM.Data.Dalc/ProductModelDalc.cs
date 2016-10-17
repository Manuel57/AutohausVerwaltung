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
    public class ProductModelDalc : IDalcBase<ProductModelDto>
    {
        public int Update(ProductModelDto item)
        {
            int result = 0;
            InputParameters(item);
            using (IDbConnection connection = new SqlConnection(PDMDatabase.DatabaseConnectionString))
            {
                result = (int)DataAccessHelper.ExecuteNonQuery(Constants.UpdateProductModel, connection);
            }
            return result;
        }

        public int Delete(int id)
        {
            int result = 0;
            #region
            DataAccessHelper.AddInputParameters("@productModelID", id);
            DataAccessHelper.AddOutputParameters("@rowseffected ", SqlDbType.Int);
            #endregion
            using (IDbConnection connection = new SqlConnection(PDMDatabase.DatabaseConnectionString))
            {
                result = (int)DataAccessHelper.ExecuteNonQuery(Constants.DeleteProductModel, connection);
            }
            return result;
        }

        public int Create(ProductModelDto item)
        {
            int result = 0;
            InputParameters(item);
            using (IDbConnection connection = new SqlConnection(PDMDatabase.DatabaseConnectionString))
            {
                result = (int)DataAccessHelper.ExecuteNonQuery(Constants.InsertProductModel, connection);
            }
            return result;
        }

        public IEnumerable<ProductModelDto> GetAll()
        {
            List<ProductModelDto> targetList = new List<ProductModelDto>();
            using (IDbConnection connection = new SqlConnection(PDMDatabase.DatabaseConnectionString))
            {
                using (IDataReader reader = DataAccessHelper.ExecuteReader(Constants.GetAllProductModel, connection))
                {
                    while (reader.Read())
                    {
                        ProductModelDto target = new ProductModelDto();
                        ProductModelMapper.MapDataToDto(reader, target);
                        targetList.Add(target);
                    }
                }
            }
            return targetList;
        }

        public ProductModelDto GetByID(int id)
        {
            ProductModelDto target = null;
            DataAccessHelper.AddInputParameters("@productModelID", id);
            using (IDbConnection connection = new SqlConnection(PDMDatabase.DatabaseConnectionString))
            {
                using (IDataReader reader = DataAccessHelper.ExecuteReader(Constants.GetProductModelByID, connection))
                {
                    if (reader.Read())
                    {
                        target = new ProductModelDto();
                        ProductModelMapper.MapDataToDto(reader, target);
                    }
                }
            }
            return target;
        }


        #region Private Methods
        private void InputParameters(ProductModelDto input)
        {
            DataAccessHelper.AddInputParameters("@name", input.Name);
            DataAccessHelper.AddInputParameters("@modifiedDate", input.ModifiedDate);

            if (input.ProductModelID > 0)
            {
                DataAccessHelper.AddInputParameters("@productModelID", input.ProductModelID);
                DataAccessHelper.AddOutputParameters("@rowseffected ", input.ProductModelID);
            }
            else
            {
                DataAccessHelper.AddOutputParameters("@productModelID", input.ProductModelID);
            }
        }
        #endregion
    }
}
