namespace Mane.Helpers.Common
{
    public interface ITraspaso : IDocumento
    {
        string FromWhs { get; set; }
        string ToWhs { get; set; }
    }
}
