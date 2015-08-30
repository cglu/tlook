using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myTokenizer
{
    class Program
    {
        static void Main(string[] args)
        {
            Tokenizer t = new Tokenizer();
            var hello = t.Parser("(T(表3.3).R(1,J1,J2).A*J1)+1/J2.Code");
            foreach (string s in hello)
            {
                Console.WriteLine(s);
            }
            Console.WriteLine("press enter to exit....");
            Console.ReadKey();
        }
       
    }
    
}
