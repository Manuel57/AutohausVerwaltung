using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDM.Data.Dalc
{
    internal class Constants
    {
        #region ProductModel
        internal const string InsertProductModel = "usp_InsertProductModel";
        internal const string DeleteProductModel = "usp_DeleteProductModel";
        internal const string UpdateProductModel = "usp_UpdateProductModel";
        internal const string GetAllProductModel = "usp_GetAllProductModel";
        internal const string GetProductModelByID = "usp_GetProductModelByID";
        #endregion

        #region ProductCategory
        internal const string InsertProductCategory = "usp_InsertProductCategory";
        internal const string DeleteProductCategory = "usp_DeleteProductCategory";
        internal const string UpdateProductCategory = "usp_UpdateProductCategory";
        internal const string GetAllProductCategory = "usp_GetAllProductCategory";
        internal const string GetProductCategoryByID = "usp_GetProductCategoryByID";
        #endregion

        #region ProductSubCategory
        internal const string InsertProductSubCategory = "usp_InsertProductSubCategory";
        internal const string DeleteProductSubCategory = "usp_DeleteProductSubCategory";
        internal const string UpdateProductSubCategory = "usp_UpdateProductSubCategory";
        internal const string GetAllProductSubCategory = "usp_GetAllProductSubCategory";
        internal const string GetProductSubCategoryByID = "usp_GetProductSubCategoryByID";
        #endregion

        #region Photo
        internal const string InsertProductPhoto = "usp_InsertProductPhoto";
        internal const string DeleteProductPhoto = "usp_DeleteProductPhoto";
        internal const string UpdateProductPhoto = "usp_UpdateProductPhoto";
        internal const string GetAllProductPhoto = "usp_GetAllProductPhoto";
        internal const string GetProductPhotoByID = "usp_GetProductPhotoByID";
        #endregion

        #region Product
        internal const string InsertProduct = "usp_InsertProduct";
        internal const string DeleteProduct = "usp_DeleteProduct";
        internal const string UpdateProduct = "usp_UpdateProduct";
        internal const string GetAllProduct = "usp_GetAllProduct";
        internal const string GetProductByID = "usp_GetProductByID";
        #endregion
    }
}
