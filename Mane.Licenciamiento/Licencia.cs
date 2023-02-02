using Mane.Helpers;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Net.NetworkInformation;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Mane.Licenciamiento
{
    public static class Licencia
    {
        public static string GetHardwareKey()
        {
            var hk = string.Empty;
            //Identificador generado al instalar windows
            hk += Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Cryptography").GetValue("MachineGuid", string.Empty);
           //Serial de la placa base
            hk += GetMotherBoardID();
            hk = Crypto.GetMD5(hk);
            var arr = hk.ToCharArray();
            hk = "";
            int c = 0;
            for (int i = 0; i < 20; i++)
            {
                hk += arr[i];
                if (c == 3)
                {
                    hk += "-";
                    c = 0;
                }
                else c++;
            }
            hk = hk.TrimEnd(new char[] { '-' });
            return hk.ToUpper();
        }
        private static string GetMotherBoardID()
        {
            string mbInfo = String.Empty;
            try
            {
                ManagementObjectSearcher mbs = new ManagementObjectSearcher("Select * From Win32_BaseBoard");
                foreach (ManagementObject mo in mbs.Get())
                {
                    mbInfo += mo["SerialNumber"].ToString();
                }
            }
            catch (Exception)
            {

            }
            return mbInfo;
        }
        /// <summary>
        /// Fecha de expiracion
        /// </summary>
        public static DateTime Expiracion { get; private set; }
        private static string HardwareKey, AppId;
        /// <summary>
        /// Dias restantes a la expiración
        /// </summary>
        public static int DiasRestantes;
        /// <summary>
        /// Clave De Licencia
        /// </summary>
        public static string ClaveDeLicencia { get; private set; }
        /// <summary>
        /// Último error de ejecución
        /// </summary>
        public static string Error;
        private static string rutaLicenciaDefault = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            ManeApp.AppId,
            "Licencia.xml"
            );

        /// <summary>
        /// Verifica el estatus actual de la licencia
        /// </summary>
        /// <returns>El estatus actual de la licencia</returns>
        public static EstatusDeLicencia Check()
        {
            return Check(RutaLicenciaDefault,ManeApp.AppId,ManeApp.ClaveCifrado);
        }
        /// <summary>
        /// Verifica una licencia expecíficada y opcionalmente muestra un mensaje de validación
        /// </summary>
        /// <param name="rutaLicencia">Runta hacia el XML de licencia</param>
        /// <param name="MostrarMensajePorDefecto">Indica si muestra un mensaje de validación o no</param>
        /// <returns>Verdadero si la licencia es válida de lo contrario retorna falso</returns>
        public static bool ValidarLicencia(string rutaLicencia,string appId,string claveEncriptado, bool MostrarMensajePorDefecto = false)
        {
            EstatusDeLicencia estatus = Check(rutaLicencia,appId,claveEncriptado);
            bool valida = true;
            string mensaje = "";
            switch (estatus)
            {
                case EstatusDeLicencia.PorCaducar:
                    if (MostrarMensajePorDefecto)
                        MessageBox.Show("Su licencia caducará en " + Licencia.DiasRestantes + " días.");
                    break;
                case EstatusDeLicencia.Vencida:
                    valida = false;
                    mensaje = "Su Licencia ha caducado";
                    break;
                case EstatusDeLicencia.Invalida:
                    valida = false;
                    mensaje = "Su licencia no es válida";
                    break;
                case EstatusDeLicencia.ErrorDeVerificacion:
                    valida = false;
                    mensaje = "Ocurrió un error al verificar la licencia: " + Licencia.Error;
                    break;
                case EstatusDeLicencia.ArchivoDeLicenciaInexistente:
                    valida = false;
                    mensaje = "No se tiene registrada ni una licencia, favor de instalar una licencia.";
                    break;
                default:
                    break;
            }
            if (MostrarMensajePorDefecto && !valida) Task.Run(() => MessageBox.Show(mensaje));
            return valida;
        }
        /// <summary>
        /// Hace lo mismo que la funcion check solo que devuelve verdadero si es válida o falso si no lo es
        /// </summary>
        /// <param name="MostrarMensajePorDefecto">Indica si deseas que muestre un mensaje dependiendo del resultado de la verificación</param>
        /// <returns>Válida o inválidad</returns>
        public static bool ValidarLicencia(bool MostrarMensajePorDefecto = false)
        {
            return ValidarLicencia(RutaLicenciaDefault,ManeApp.AppId,ManeApp.ClaveCifrado, MostrarMensajePorDefecto);
        }
        /// <summary>
        /// Verifica el estatus de la licencia especificando la ruta de la misma
        /// </summary>
        /// <param name="ruta">Ruta hacia el XML de licencia</param>
        /// <returns>El estatus actual de la licencia especificada</returns>
        public static EstatusDeLicencia Check(string ruta,string appId,string claveEncriptado)
        {
            try
            {
                string claveDeLicencia = ReadFromFile(ruta);
                if (claveDeLicencia == EstatusDeLicencia.ArchivoDeLicenciaInexistente.ToString())
                    return EstatusDeLicencia.ArchivoDeLicenciaInexistente;
                else if (claveDeLicencia == EstatusDeLicencia.ErrorDeVerificacion.ToString())
                    return EstatusDeLicencia.ErrorDeVerificacion;
                string plainLic = Crypto.Decriptar(claveDeLicencia, claveEncriptado);
                string[] datos = plainLic.Split(';');
                /*datos =>{
                 [0] Hardware Key
                 [1] App ID
                 [2] Expiración}
                 */
                ClaveDeLicencia = claveDeLicencia;
                if (datos.Length != 3)
                {
                    // Log.Add("El numero de parámetos de licencia no coinciden " + datos.Length);
                    return EstatusDeLicencia.Invalida;
                }
                HardwareKey = datos[0];
                AppId = datos[1];
                try
                {
                    Expiracion = DateTime.Parse(datos[2]);
                }
                catch (Exception ex)
                {
                    Error = ex.ToString();
                    return EstatusDeLicencia.ErrorDeVerificacion;

                }
                if (HardwareKey != GetHardwareKey() || AppId != appId)
                {
                    // Log.Add($"La clave de hardware '{HardwareKey}' o el id de aplicacion '{appId}' no coinciden");
                    return EstatusDeLicencia.Invalida;
                }

                DiasRestantes = (Expiracion - DateTime.Now).Days;
                if (DiasRestantes < 0)
                    return EstatusDeLicencia.Vencida;
                if (DiasRestantes <= 10)
                    return EstatusDeLicencia.PorCaducar;
            }
            catch (Exception ex)
            {
                Error = ex.ToString();
                return EstatusDeLicencia.ErrorDeVerificacion;
            }
            return EstatusDeLicencia.Valida;
        }

        /// <summary>
        /// Ruta por defecto donde se guardará el archivo de licencia = "C:\ProgramData\{AplicaionTie.AppID}\licencia.xml"
        /// </summary>
        public static string RutaLicenciaDefault { get => rutaLicenciaDefault; set => rutaLicenciaDefault = value; }

        /// <summary>
        /// Guarda una licencia específica en la ruta por defecto
        /// </summary>
        /// <param name="claveDeLicencia">Clave de licencia</param>
        public static void Save(string claveDeLicencia)
        {
            SaveAs(claveDeLicencia, RutaLicenciaDefault);
        }

        /// <summary>
        /// Guarda una licencia específica en una ruta específica
        /// </summary>
        /// <param name="claveDeLicencia">Clave de licencia</param>
        /// <param name="ruta">Ruta Donde desea guardarla</param>
        public static void SaveAs(string claveDeLicencia, string ruta)
        {
            try
            {

                LicenciaXML licenciaxml = new LicenciaXML() { ClaveDeLicencia = claveDeLicencia };
                string dir = Path.GetDirectoryName(ruta);
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);

                using (FileStream fs = File.Create(ruta)) { }
                XmlSerializer serializer = new XmlSerializer(typeof(LicenciaXML));
                using (StreamWriter sw = new StreamWriter(ruta))
                    serializer.Serialize(sw, licenciaxml);
            }
            catch (Exception)
            { }
        }
        /// <summary>
        /// Elimina la licencia anteriormente instalada y la reemplaza por una nueva
        /// </summary>
        /// <param name="rutaDestino">Ruta donde se guardará la nueva licencia, por defecto lo guarda en la ruta predeterminada</param>
        public static bool Import(string rutaDestino = "")
        {
            if (string.IsNullOrEmpty(rutaDestino)) rutaDestino = RutaLicenciaDefault;

            try
            {
                using (OpenFileDialog ofd = new OpenFileDialog())
                {
                    ofd.Filter = "Archivos de xml (*.xml)|*.XML";
                    ofd.Title = "Elja el archivo XML de licencia";
                    ofd.FileName = "";
                    if (ofd.ShowDialog() != DialogResult.OK) return false;
                    string dir = Path.GetDirectoryName(rutaDestino);
                    if (!Directory.Exists(dir))
                        Directory.CreateDirectory(dir);
                    if (File.Exists(rutaDestino))
                        File.Delete(rutaDestino);
                    File.Copy(ofd.FileName, rutaDestino);
                }

            }
            catch (Exception)
            {
                MessageBox.Show("No se pudo guardar la licencia, por favor ejecute la apicación como administrador y vuelva a intentar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        /// <summary>
        /// Lee la clave de licencia de un archivo XML de licencia
        /// </summary>
        /// <param name="ruta">Ruta de acceso hacia el XML de Licencia</param>
        /// <returns>Returna una cadena con la clave de licencia</returns>
        public static string ReadFromFile(string ruta)
        {
            try
            {
                LicenciaXML lic;
                if (!File.Exists(ruta))
                    return EstatusDeLicencia.ArchivoDeLicenciaInexistente.ToString();
                XmlSerializer serializer = new XmlSerializer(typeof(LicenciaXML));
                using (StreamReader sr = new StreamReader(ruta))
                    lic = (LicenciaXML)serializer.Deserialize(sr);
                return lic.ClaveDeLicencia;
            }
            catch (Exception ex)
            {
                Error = ex.ToString();
                return EstatusDeLicencia.ErrorDeVerificacion.ToString();
            }
        }

        /// <summary>
        /// Objeto de referencia para serializar y deserializar una licencia
        /// </summary>
        public class LicenciaXML
        {
            /// <summary>
            /// Clave de licencia
            /// </summary>
            public string ClaveDeLicencia { get; set; }
        }
        
    }
    /// <summary>
    /// Estatuses de licencia
    /// </summary>
    public enum EstatusDeLicencia
    {
        /// <summary>
        /// Licencia Válida
        /// </summary>
        Valida,
        /// <summary>
        /// Quedan 10 días o menos para que la licencia expire
        /// </summary>
        PorCaducar,
        /// <summary>
        /// La Licencia venció
        /// </summary>
        Vencida,
        /// <summary>
        /// La Licencia no es válida
        /// </summary>
        Invalida,
        /// <summary>
        /// Ocurrió una Excepción al verificar la licencia
        /// </summary>
        ErrorDeVerificacion,
        /// <summary>
        /// No se encontró el archivo de licencia.
        /// </summary>
        ArchivoDeLicenciaInexistente,
        /// <summary>
        /// Nungún estado
        /// </summary>
        Ninguno
    }

}
