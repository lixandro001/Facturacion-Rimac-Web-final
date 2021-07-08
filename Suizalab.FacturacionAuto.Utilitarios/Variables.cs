﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Suizalab.FacturacionAuto.Utilitarios
{
    public class Variables
    {
        public class ParametroFacturacion
        {
            /// <summary>
            /// Ruc de Suiza Lab
            /// </summary>
            public const string SuizaRuc = "20330025213";
            /// <summary>
            /// Sucursal Miraflores
            /// </summary>
            public const string SuizaSuc = "02";

            /// <summary>
            /// Factura Detallada
            /// </summary>
            public const string FacDetallado = "D";

            /// <summary>
            /// Factura Detallada
            /// </summary>
            public const string FacResumida = "M";

        }
        public class Archivo
        {
            /// <summary>
            /// Nombre del del archivo Informe
            /// </summary>
            public const string FileNameInforme = "Informe";
            /// <summary>
            /// Nombre del del archivo Informe
            /// </summary>
            public const string FileNameFactura = "Factura";
            /// <summary>
            /// Extensión de archivos 
            /// </summary>
            public const string ExtPdf = ".pdf";
        }
        public class Rutas
        {
            /// <summary>
            /// Nombre del Informe
            /// </summary>
            public const string Informe = "Informe";
            /// <summary>
            /// Modalidad Ocupacional
            /// </summary>
            public const string InformeFirmado = "Firmado";
            /// <summary>
            /// Modalidad Seguros
            /// </summary>
            public const string InformeFinal = "InformeFinal";
        }

        public class NumAutorización
        {
            /// <summary>
            /// No cuenta con registro en la tabla Tedef
            /// </summary>
            public const string NoExiste = "N";

        }

        public class Servicio
        {
            /// <summary>
            /// Número de servicio de colonoscopia
            /// </summary>
            public const string Colonoscopia = "0003615";
        }

        public class Emails
        {
            /// <summary>
            /// Email cuando se factura Correctamente
            /// </summary>

            #region QA
            
        public static string[] EmailFacTO = { ConfigurationManager.AppSettings["CorreoMH"] };
        public static string[] NameFacTO = { ConfigurationManager.AppSettings["NombreMH"] };
        public static string[] EmailFacObsTO = { ConfigurationManager.AppSettings["CorreoMH"] };
        public static string[] NameFacObsTO = { ConfigurationManager.AppSettings["NombreMH"] };

        public static string[] EmailFacCC = {};
        public static string[] NameFacCC = {};

        public static string[] EmailFacObsCC = { };
        public static string[] NameFacObsCC = { };
        
            #endregion
/*
            public static string[] EmailFacTO = { ConfigurationManager.AppSettings["CorreoFA"] };
            public static string[] NameFacTO = { ConfigurationManager.AppSettings["NombreFA"] };

            public static string[] EmailFacCC = { ConfigurationManager.AppSettings["CorreoME"], ConfigurationManager.AppSettings["CorreoJP"], ConfigurationManager.AppSettings["CorreoMH"] };
            public static string[] NameFacCC = { ConfigurationManager.AppSettings["NombreME"], ConfigurationManager.AppSettings["NombreJP"], ConfigurationManager.AppSettings["NombreMH"] };

            /// <summary>
            /// Email cuando hay errores para facturar
            /// </summary>

            public static string[] EmailFacObsTO = { ConfigurationManager.AppSettings["CorreoFA"], ConfigurationManager.AppSettings["CorreoKC"], ConfigurationManager.AppSettings["CorreoAE"] };
            public static string[] NameFacObsTO = { ConfigurationManager.AppSettings["NombreFA"], ConfigurationManager.AppSettings["NombreKC"], ConfigurationManager.AppSettings["CorreoAE"] };

            public static string[] EmailFacObsCC = { ConfigurationManager.AppSettings["CorreoJP"], ConfigurationManager.AppSettings["CorreoMH"] };
            public static string[] NameFacObsCC = { ConfigurationManager.AppSettings["NombreJP"], ConfigurationManager.AppSettings["NombreMH"] };

            /// <summary>
            /// Email de informes no cerrados
            /// </summary>
            public static string[] EmailInformeSemTO = { ConfigurationManager.AppSettings["CorreoES"] };
            public static string[] NameInformeSemTO = { ConfigurationManager.AppSettings["NombreES"] };

            public static string[] EmailInformeSemCC = { ConfigurationManager.AppSettings["CorreoFA"], ConfigurationManager.AppSettings["CorreoSO"], ConfigurationManager.AppSettings["CorreoME"], ConfigurationManager.AppSettings["CorreoJP"], ConfigurationManager.AppSettings["CorreoMH"] };
            public static string[] NameInformeSemCC = { ConfigurationManager.AppSettings["NombreFA"], ConfigurationManager.AppSettings["NombreSO"], ConfigurationManager.AppSettings["NombreME"], ConfigurationManager.AppSettings["NombreJP"], ConfigurationManager.AppSettings["NombreMH"] };
            */
        }

        public class Compania
        {
            public const string RimacSeguro = "20100041953";
            public const string RimacEPS = "20414955020";
        }
    }
}
