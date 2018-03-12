using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSA
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                BigInteger a = new BigInteger("1234567892");
                BigInteger b = new BigInteger("987654321");
                int r = 0;
                Console.WriteLine(a.Divide(2, out r).ToString());
                Console.ReadKey();
            }
            catch(Exception s)
            {
                Console.WriteLine(s);
            }
        }
    }
}
