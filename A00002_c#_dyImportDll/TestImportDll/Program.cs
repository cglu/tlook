using System;
using System.Collections.Generic;
using System.Text;
using CustomLibrary;
namespace TestImportDll
{
    class Program
    {
        static void Main(string[] args)
        {
            outPut op = new outPut();

            Console.WriteLine(op.ReturnString());
            Console.ReadKey();
        }
    }
}
