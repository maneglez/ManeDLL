using System;

namespace Mane.Sap.ServiceLayer.Interfaces
{
    public class Pruebita : IRelatedDocumentsSap
    {


        public RelatedDocumentTypeEnum DocType { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int AbsEntry { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public string UUID => throw new NotImplementedException();
    }
}