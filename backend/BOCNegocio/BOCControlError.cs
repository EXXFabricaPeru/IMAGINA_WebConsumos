using System;
using System.IO;

namespace BOCNegocio
{
    public static class BOCErrorControl
    {
        public static void RegistraError(string MsgError)
        {
            string Path = AppDomain.CurrentDomain.BaseDirectory.ToString() + "\\Log\\log.txt";
            File.SetAttributes(Path, FileAttributes.Normal);
            StreamWriter error = File.AppendText(Path);
            error.WriteLine("---------------------------------------------------------------------------------");
            error.WriteLine(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString());
            error.WriteLine(MsgError);
            error.WriteLine("---------------------------------------------------------------------------------");
            error.Close();
        }

        public static void RegistrarJson(string jSon)
        {
            string Path = AppDomain.CurrentDomain.BaseDirectory.ToString() + "\\Log\\logPedido.txt";
            File.SetAttributes(Path, FileAttributes.Normal);
            StreamWriter error = File.AppendText(Path);
            error.WriteLine("---------------------------------------------------------------------------------");
            error.WriteLine(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString());
            error.WriteLine(jSon);
            error.WriteLine("---------------------------------------------------------------------------------");
            error.Close(); 
        }
    }
}
