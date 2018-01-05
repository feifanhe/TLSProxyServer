using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SVTServerService
{
    class Log
    {
        public static void Write(string format, params object[] arg)
        {
            string path = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "Service.log";
            format = string.Format("[{0} {1}] {2}", DateTime.Now.ToShortDateString(), DateTime.Now.ToLongTimeString(), format);

            try
            {
                using (FileStream fs = new FileStream(path, FileMode.Append))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        sw.WriteLine(format, arg);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Log exception: {0}", e.Message);
            }
        }
    }
}
