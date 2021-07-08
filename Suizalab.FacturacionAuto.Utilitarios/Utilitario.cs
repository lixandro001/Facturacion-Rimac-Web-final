using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;

namespace Suizalab.FacturacionAuto.Utilitarios
{
    public class Utilitario
    {
        public static void LogServiceFacturacion(string strMessage)
        {
            try
            {
                if (!string.IsNullOrEmpty(strMessage))
                {
                    string strFile = string.Empty;
                    string strPath = @ConfigurationManager.AppSettings["strPath"] + @"Log";
                    string Date = System.DateTime.Now.ToString("dd-MM-yyyy");

                    if (!Directory.Exists(strPath))
                        Directory.CreateDirectory(strPath);

                    strFile = strPath + @"\ServiceFacturacion_" + Date + ".txt";
                    strMessage = "Fecha: " + DateTime.Now.ToString("dd/MM/yyyy h:mm:ss tt") + ": " + strMessage;

                    using (StreamWriter oWriter = File.AppendText(strFile))
                    {
                        oWriter.WriteLine(strMessage);
                        oWriter.Flush();
                    }
                }
            }
            catch (Exception ex)
            {
                new Exception(ex.Message, ex);
            }
        }

        public static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
