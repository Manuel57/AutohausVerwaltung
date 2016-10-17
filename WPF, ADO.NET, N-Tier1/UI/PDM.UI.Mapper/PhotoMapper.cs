using BlEntity = PDM.Business.Entities;
using UIEntity = PDM.UI.Entities;
namespace PDM.UI.Mapper
{
    public static class PhotoMapper
    {
        public static void MapBusinessToUI(BlEntity.PhotoEntity source, UIEntity.PhotoEntity target)
        {
            target.PhotoID = source.PhotoID;
            target.Name = source.Name;
            target.Image = source.Image;
            target.ModifiedDate = source.ModifiedDate;
        }

        public static void MapUIToBusiness(UIEntity.PhotoEntity source, BlEntity.PhotoEntity target)
        {
            target.PhotoID = source.PhotoID;
            target.Name = source.Name;
            target.Image = source.Image;
            target.ModifiedDate = source.ModifiedDate;
        }
    }
}
