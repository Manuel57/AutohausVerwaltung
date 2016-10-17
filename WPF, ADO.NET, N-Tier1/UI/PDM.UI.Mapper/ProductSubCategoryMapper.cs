using BlEntity = PDM.Business.Entities;
using UIEntity = PDM.UI.Entities;

namespace PDM.UI.Mapper
{
    public static class ProductSubCategoryMapper
    {
        public static void MapBusinessToUI(BlEntity.ProductSubCategoryEntity source, UIEntity.ProductSubCategoryEntity target)
        {
            target.ProductCategoryID = source.ProductCategoryID;
            target.ProductSubCategoryID = source.ProductSubCategoryID;
            target.Name = source.Name;
            target.ModifiedDate = source.ModifiedDate;
        }

        public static void MapUIToBusiness(UIEntity.ProductSubCategoryEntity source, BlEntity.ProductSubCategoryEntity target)
        {
            target.ProductCategoryID = source.ProductCategoryID;
            target.ProductSubCategoryID = source.ProductSubCategoryID;
            target.Name = source.Name;
            target.ModifiedDate = source.ModifiedDate;
        }
    }
}
