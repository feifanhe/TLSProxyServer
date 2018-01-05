using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace SVTServerService
{
    class IniReader
    {
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(
            string section, string key, string def, StringBuilder retVal, int size, string filePath);

        private string FilePath;

        public IniReader(string FilePath)
        {
            Log.Write("Set INI file path: {0}", FilePath);
            this.FilePath = FilePath;
        }

        public string GetValue(string Section, string Key)
        {
            Log.Write("Get from section [{0}] and key={1}", Section, Key);
            StringBuilder sb = new StringBuilder(255);
            int i = GetPrivateProfileString(Section, Key, "", sb, 255, this.FilePath);
            return sb.ToString();
        }
    }
}
