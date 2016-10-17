using BlEntity = PDM.Business.Entities;
using UIEntity = PDM.UI.Entities;

namespace PDM.UI.Mapper
{
    public class ProductMapper
    {
        public static void MapBusinessToUI(BlEntity.ProductEntity source, UIEntity.ProductEntity target)
        {
            target.ProductID = source.ProductID;
            target.Name = source.Name;
            target.ProductNumber = source.ProductNumber;
            target.MakeFlag = source.MakeFlag;
            target.Color = source.Color;
            target.StandardCost = source.StandardCost;
            target.ListPrice = source.ListPrice;
            target.Size = source.Size;
            target.Weight = source.Weight;
            target.Style = source.Style;
            target.SellStartDate = source.SellStartDate;
            target.ModifiedDate = source.ModifiedDate;
            target.ProductSubCategoryID = source.ProductSubCategoryID;
            target.ProductModelID = source.ProductModelID;
            target.PhotoID = source.PhotoID;
        }

        public static void MapUIToBusiness(UIEntity.ProductEntity source, BlEntity.ProductEntity target)
        {
            target.ProductID = source.ProductID;
            target.Name = source.Name;
            target.ProductNumber = source.ProductNumber;
            target.MakeFlag = source.MakeFlag;
            target.Color = source.Color;
            target.StandardCost = source.StandardCost;
            target.ListPrice = source.ListPrice;
            target.Size = source.Size;
            target.Weight = source.Weight;
            target.Style = source.Style;
            target.SellStartDate = source.SellStartDate;
            target.ModifiedDate = source.ModifiedDate;
            target.ProductSubCategoryID = source.ProductSubCategoryID;
            target.ProductModelID = source.ProductModelID;
            target.PhotoID = source.PhotoID;
        }
    }
}
