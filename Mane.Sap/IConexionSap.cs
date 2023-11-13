namespace Mane.Sap
{
    public interface IConexionSap
    {
        string DbCompany { get; set; }
        string DbPassword { get; set; }
        string DbUser { get; set; }
        string LicenseServer { get; set; }
        string Nombre { get; set; }
        string Password { get; set; }
        string Server { get; set; }
        string SLDServer { get; set; }
        string User { get; set; }
        TipoServidorSap TipoServidor { get; set; }
    }
}