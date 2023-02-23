
namespace Mane.Sap.ServiceLayer.Interfaces
{
    //Lista
    public interface IRelatedDocumentsSap
    {
        int AbsEntry { get; set; }
        RelatedDocumentTypeEnum DocType { get; set; }
        string UUID { get; set; }
    }
}