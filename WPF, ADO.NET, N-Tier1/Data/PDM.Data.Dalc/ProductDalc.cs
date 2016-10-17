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
    public class ProductDalc : IDalcBase<ProductDto>
    {
        public int Update(ProductDto item)
        {
            int result = 0;
            InputParameters(item);
            using (IDbConnection connection = new SqlConnection(PDMDatabase.DatabaseConnectionString))
            {
                result = (int)DataAccessHelper.ExecuteNonQuery(Constants.UpdateProduct, connection);
            }
            return result;
        }

        public int Delete(int id)
        {
            int result = 0;
            DataAccessHelper.AddInputParameters("@productID", id);
            DataAccessHelper.AddOutputParameters("@rowseffected", SqlDbType.Int);
            using (IDbConnection connection = new SqlConnection(PDMDatabase.DatabaseConnectionString))
            {
                result = (int)DataAccessHelper.ExecuteNonQuery(Constants.DeleteProduct, connection);
            }
            return result;
        }

        public int Create(ProductDto item)
        {
            int result = 0;
            InputParameters(item);
            using (IDbConnection connection = new SqlConnection(PDMDatabase.DatabaseConnectionString))
            {
                result = (int)DataAccessHelper.ExecuteNonQuery(Constants.InsertProduct, connection);
            }
            return result;
        }

        public IEnumerable<ProductDto> GetAll()
        {
            List<ProductDto> targetList = new List<ProductDto>();
            using (IDbConnection connection = new SqlConnection(PDMDatabase.DatabaseConnectionString))
            {
                using (IDataReader reader = DataAccessHelper.ExecuteReader(Constants.GetAllProduct, connection))
                {
                    while (reader.Read())
                    {
                        ProductDto target = new ProductDto();
                        ProductMapper.MapDataToDto(reader, target);
                        targetList.Add(target);
                    }
                }
            }
            return targetList;
        }

        public ProductDto GetByID(int id)
        {
            ProductDto target = null;
            DataAccessHelper.AddInputParameters("@productID", id);
            using (IDbConnection connection = new SqlConnection(PDMDatabase.DatabaseConnectionString))
            {
                using (IDataReader reader = DataAccessHelper.ExecuteReader(Constants.GetProductByID, connection))
                {
                    if (reader.Read())
                    {
                        target = new ProductDto();
                        ProductMapper.MapDataToDto(reader, target);
                    }
                }
            }
            return target;
        }

        #region Private Methods
        private void InputParameters(ProductDto dto)
        {
            DataAccessHelper.AddInputParameters("@name", dto.Name);
            DataAccessHelper.AddInputParameters("@productNumber", dto.ProductNumber);
            DataAccessHelper.AddInputParameters("@makeFlag", dto.MakeFlag);
            DataAccessHelper.AddInputParameters("@color", dto.Color);
            DataAccessHelper.AddInputParameters("@standardCost", dto.StandardCost);
            DataAccessHelper.AddInputParameters("@listPrice", dto.ListPrice);
            DataAccessHelper.AddInputParameters("@size", dto.Size);
            DataAccessHelper.AddInputParameters("@weight", dto.Weight);
            DataAccessHelper.AddInputParameters("@style", dto.Style);
            DataAccessHelper.AddInputParameters("@sellStartDate", dto.SellStartDate);
            DataAccessHelper.AddInputParameters("@modifiedDate", dto.ModifiedDate);
            DataAccessHelper.AddInputParameters("@productSubCategoryID", dto.ProductSubCategoryID);
            DataAccessHelper.AddInputParameters("@productModelID", dto.ProductModelID);
            DataAccessHelper.AddInputParameters("@photoID", dto.PhotoID);
            if (dto.ProductID > 0)
            {
                DataAccessHelper.AddInputParameters("@productID", dto.ProductID);
                DataAccessHelper.AddOutputParameters("@rowseffected", SqlDbType.Int);
            }
            else
            {
                DataAccessHelper.AddOutputParameters("@productID", dto.ProductID);
            }
        }
        #endregion
    }
}
