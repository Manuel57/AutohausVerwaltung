using BlEntity = PDM.Business.Entities;
using UIEntity = PDM.UI.Entities;
namespace PDM.UI.Mapper
{
    public static class ProductCategoryMapper
    {
        public static void MapBusinessToUI(BlEntity.ProductCategoryEntity source, UIEntity.ProductCategoryEntity target)
        {
            target.ProductCategoryID = source.ProductCategoryID;
            target.Name = source.Name;
            target.RowGuid = source.RowGuid;
            target.ModifiedDate = source.ModifiedDate;
        }

        public static void MapUIToBusiness(UIEntity.ProductCategoryEntity source, BlEntity.ProductCategoryEntity target)
        {
            target.ProductCategoryID = source.ProductCategoryID;
            target.Name = source.Name;
            target.RowGuid = source.RowGuid;
            target.ModifiedDate = source.ModifiedDate;
        }
    }
}
