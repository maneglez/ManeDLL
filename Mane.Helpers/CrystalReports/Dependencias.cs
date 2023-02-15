using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Mane.Helpers.CrystalReports
{
    public static class Dependencias
    {
        public static Version VersionDeCrystal;
        private static Assembly engineAssem;
        private static Assembly sharedAssem;
        private static Assembly formsAssem;
        private static string RutaEnsambladoNet4 =>
              Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.Windows),
            "Microsoft.NET", "assembly", "GAC_MSIL");
        private static string RutaEnsambladosNet =>
            Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.Windows),
             "assembly", "GAC_MSIL");

        public static Assembly EngineAssem { get {
                if (engineAssem == null)
                    Cargar();
                return engineAssem;
            }  }
        public static Assembly SharedAssem
        {
            get
            {
                if (sharedAssem == null)
                    Cargar();
                return sharedAssem;
            }
        }
        public static Assembly FormsAssem
        {
            get
            {
                if (formsAssem == null)
                    Cargar();
                return formsAssem;
            }
        }

        public static Type ReportDocumentType { get {
                if (reportDocumentType == null)
                    Cargar();
                return reportDocumentType;
            }}
        public static Type ExportFormatType { get {
                if (exportFormatType == null)
                    Cargar();
                return exportFormatType;
            }}
        public static Type ReportViewerType { get {
                if (reportViewerType == null)
                    Cargar();
                return reportViewerType;
            }}

        private static Type reportDocumentType;
        private static Type exportFormatType;
        private static Type reportViewerType;
        private static string RutaEnsamblados { get; set; } = RutaEnsambladosNet;


        /// <summary>
        /// Carga los ensamblados del runtime de crystal
        /// </summary>
        /// <exception cref="Exception">Truena cuando no esta instalado el runtime</exception>
        public static void Cargar()
        {
            if (VersionDeCrystal == null)
                throw new Exception("No ha definido una versión de runtime crystal reports");
                if (!Directory.Exists(Path.Combine(RutaEnsamblados, "CrystalDecisions.CrystalReports.Engine")))
            {
                RutaEnsamblados = RutaEnsambladoNet4;
                if (!Directory.Exists(Path.Combine(RutaEnsamblados, "CrystalDecisions.CrystalReports.Engine")))
                    throw new Exception("Parece que no se encuentra instalado el runtime de Crystal Reports");
            }
           engineAssem = Assembly.LoadFile(FindDll("CrystalDecisions.CrystalReports.Engine.dll"));
           sharedAssem = Assembly.LoadFile(FindDll("CrystalDecisions.Shared.dll"));
           formsAssem = Assembly.LoadFile(FindDll("CrystalDecisions.Windows.Forms.dll"));
            reportDocumentType = engineAssem.GetType("CrystalDecisions.CrystalReports.Engine.ReportDocument");
            exportFormatType = sharedAssem.GetType("CrystalDecisions.Shared.ExportFormatType");
            reportViewerType = formsAssem.GetType("CrystalDecisions.Windows.Forms.CrystalReportViewer");
        }
        private static string FindDll(string dllName)
        {
            var ruta = Path.Combine(RutaEnsamblados, Path.GetFileNameWithoutExtension(dllName));
            return FindDll(ruta, dllName);
        }
        private static string FindDll(string rutaEnsamblado,string dllName,bool recursivo = false)
        {
           var files = Directory.GetFiles(rutaEnsamblado);
            foreach (var item in files)
            {
                if (Path.GetFileName(item) == dllName)
                {
                    var info = FileVersionInfo.GetVersionInfo(item);
                    if(Version.Parse(info.FileVersion) >= VersionDeCrystal)
                    return item;
                }
                   
            }
            var dirs = Directory.GetDirectories(rutaEnsamblado);
            foreach (var item in dirs)
            {
                var file = FindDll(item,dllName,true);
                if (!string.IsNullOrEmpty(file))
                    return file;
            }
            if (!recursivo)
                throw new Exception("No se encuentra la DLL " + dllName + " Version minima: " + VersionDeCrystal);
            else return "";
        }
    }
}
