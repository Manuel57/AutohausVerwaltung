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
    public class PhotoDalc : IDalcBase<PhotoDto>
    {
        public int Update(PhotoDto item)
        {
            int result = 0;
            InputParameters(item);
            using (IDbConnection connection = new SqlConnection(PDMDatabase.DatabaseConnectionString))
            {
                result = (int)DataAccessHelper.ExecuteNonQuery(Constants.UpdateProductPhoto, connection);
            }
            return result;
        }

        public int Delete(int id)
        {
            int result = 0;
            DataAccessHelper.AddInputParameters("@photoID", id);
            DataAccessHelper.AddOutputParameters("@rowseffected", SqlDbType.Int);
            using (IDbConnection connection = new SqlConnection(PDMDatabase.DatabaseConnectionString))
            {
                result = (int)DataAccessHelper.ExecuteNonQuery(Constants.DeleteProductPhoto, connection);
            }
            return result;
        }

        public int Create(PhotoDto item)
        {
            int result = 0;
            InputParameters(item);
            using (IDbConnection connection = new SqlConnection(PDMDatabase.DatabaseConnectionString))
            {
                result = (int)DataAccessHelper.ExecuteNonQuery(Constants.InsertProductPhoto, connection);
            }
            return result;
        }

        public IEnumerable<PhotoDto> GetAll()
        {
            List<PhotoDto> targetList = new List<PhotoDto>();
            using (IDbConnection connection = new SqlConnection(PDMDatabase.DatabaseConnectionString))
            {
                using (IDataReader reader = DataAccessHelper.ExecuteReader(Constants.GetAllProductPhoto, connection))
                {
                    while (reader.Read())
                    {
                        PhotoDto target = new PhotoDto();
                        PhotoMapper.MapDataToDto(reader, target);
                        targetList.Add(target);
                    }
                }
            }
            return targetList;
        }

        public PhotoDto GetByID(int id)
        {
            PhotoDto target = null;
            DataAccessHelper.AddInputParameters("@photoID", id);
            using (IDbConnection connection = new SqlConnection(PDMDatabase.DatabaseConnectionString))
            {
                using (IDataReader reader = DataAccessHelper.ExecuteReader(Constants.GetProductPhotoByID, connection))
                {
                    if (reader.Read())
                    {
                        target = new PhotoDto();
                        PhotoMapper.MapDataToDto(reader, target);
                    }
                }
            }
            return target;
        }

        #region Private Methods
        private void InputParameters(PhotoDto dto)
        {
            DataAccessHelper.AddInputParameters("@name", dto.Name);
            DataAccessHelper.AddInputParameters("@image", dto.Image);
            DataAccessHelper.AddInputParameters("@modifiedDate", dto.ModifiedDate);
            if (dto.PhotoID > 0)
            {
                DataAccessHelper.AddInputParameters("@photoID", dto.PhotoID);
                DataAccessHelper.AddOutputParameters("@rowseffected", SqlDbType.Int);
            }
            else
            {
                DataAccessHelper.AddOutputParameters("@photoID", dto.PhotoID);
            }

        }
        #endregion
    }
}
