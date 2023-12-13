using SAPbobsCOM;
using System;
using System.Diagnostics;

namespace Mane.Sap
{
    public class SapExecutor : IDisposable
    {
        private ComDisposer Disposer;
        private Company _company;
        private ConexionSap conexionSap;
        public Company Company => _company;
        public SapExecutor(ConexionSap conexion)
        {
            if (conexion == null)
                throw new ArgumentNullException(nameof(conexion));
            conexionSap = conexion.Copy();
            Construct();
        }
        public int Connect()
        {
            return _company.Conectar(conexionSap.Nombre);
        }
        public string GetLastErrorDescription()
        {
            return _company.GetLastErrorDescription();
        }
        public dynamic GetBusinessObject(BoObjectTypes objType)
        {
            var obj = _company.GetBusinessObject(objType);
            return Disposer.Add(obj);
        }
        public string GetNewObjectKey()
        {
            return _company.GetNewObjectKey();
        }

        public SapExecutor(string nombreConexion)
        {
            var con = Sap.Conexiones.Find(nombreConexion) ?? throw new Exception($"La conexion \"{nombreConexion}\" no existe");
            conexionSap = con.Copy();
            Construct();
        }
        private void Construct()
        {
            Disposer = new ComDisposer();
            _company = Disposer.Add(new Company());
            ConfigureCompany();
        }
        private void ConfigureCompany()
        {
            _company.Server = conexionSap.Server;
            if (!string.IsNullOrEmpty(conexionSap.LicenseServer))
                _company.LicenseServer = conexionSap.LicenseServer;
            if (!string.IsNullOrEmpty(conexionSap.SLDServer))
                _company.SLDServer = conexionSap.SLDServer;
            _company.CompanyDB = conexionSap.DbCompany;
            _company.DbUserName = conexionSap.DbUser;
            _company.DbPassword = conexionSap.DbPassword;
            _company.UserName = conexionSap.User;
            _company.Password = conexionSap.Password;
            _company.DbServerType = (BoDataServerTypes)conexionSap.TipoServidor;
            _company.language = BoSuppLangs.ln_Spanish_La;
            _company.UseTrusted = false;
        }
        public void Dispose()
        {
            if(_company != null)
            if (_company.Connected)
                {
                    _company.Disconnect();
                }
            _company = null;
            Disposer.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
