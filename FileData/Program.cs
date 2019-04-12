using System;
using System.Collections.Generic;
using System.Linq;
using ThirdPartyTools;

namespace FileData
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var fileProcessor = new FileDataProcessor(args, new FileDetails());
            var str = fileProcessor.GetFileData();
            Console.WriteLine(str);
            Console.ReadLine();

        }
    }
}
