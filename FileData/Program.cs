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
            try
            {
                var fileProcessor = new FileDataProcessor(args, new FileDetails());
                var str = fileProcessor.GetFileData();
                Console.WriteLine(str);
                Console.ReadLine();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadLine();

            }
          

        }
    }
}
