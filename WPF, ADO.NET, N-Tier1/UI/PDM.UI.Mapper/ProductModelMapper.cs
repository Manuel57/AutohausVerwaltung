using BlEntity = PDM.Business.Entities;
using UIEntity = PDM.UI.Entities;

namespace PDM.UI.Mapper
{
    public static class ProductModelMapper
    {
        public static void MapBusinessToUI(BlEntity.ProductModelEntity source, UIEntity.ProductModelEntity target)
        {
            target.ProductModelID = source.ProductModelID;
            target.Name = source.Name;
            target.ModifiedDate = source.ModifiedDate;
        }

        public static void MapUIToBusiness(UIEntity.ProductModelEntity source, BlEntity.ProductModelEntity target)
        {
            target.ProductModelID = source.ProductModelID;
            target.Name = source.Name;
            target.ModifiedDate = source.ModifiedDate;
        }
    }
}
