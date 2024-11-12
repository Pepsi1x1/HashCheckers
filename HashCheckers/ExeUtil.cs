
using System;
using System.IO;
using System.Reflection;

namespace HashCheckers
{
    public static class ExeUtil
    {
        public static string GetValue(string[] arg, int index, string defaultValue)
        {
            return (arg == null || index >= arg.Length || String.IsNullOrEmpty(arg[index])) ? defaultValue : arg[index];
        }

        public static string GetValue(string[] arg, int index)
        {
            return (arg == null || index >= arg.Length) ? null : arg[index];
        }

        public static string GetFileNameForEntryExe()
        {
            return Path.GetFileName(Assembly.GetEntryAssembly().Location);
        }
    }
}
