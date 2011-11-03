using System;
using System.IO;
using System.Reflection;
using HashCheckers;

namespace MD5Checker
{
	class Program
	{
		static void Main(string[] args)
		{
		    var err = ConsoleApp.Run(args, HashTypes.MD5);

            if(err == ErrorCodes.InvalidUsage)
                PrintUsage();

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
		}

	    public static void PrintUsage()
		{
		    Console.WriteLine("Usage:");
            Console.WriteLine("{0} <filename> [<expected hash>]", ExeUtil.GetFileNameForEntryExe());
		}
	}
}
