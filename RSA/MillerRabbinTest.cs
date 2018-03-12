using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSA
{/*
    /// <summary>
    /// Тест Миллера-Рабина, проверяющий на простоту число
    /// </summary>
    class MillerRabbinTest
    {
        //Проверяемое на простоту число
        public BigInteger a;

        private static bool Witness(BigInteger a, BigInteger n)
        {
            BigInteger u = n.Substract(One);
            int t = 0;
            while (u.Mod(new BigInteger("2")).Equals(Zero))
            {
                t++;
                u = u.Div(new BigInteger("2"));
            }
            BigInteger[] x = new BigInteger[t + 1];
            x[0] = a.Pow(u, n);
            for (int i = 1; i <= t; i++)
            {
                x[i] = x[i - 1].Pow(new BigInteger("2"), n);
                if (x[i].Equals(One) && !x[i - 1].Equals(One) && !x[i - 1].Equals(n.Substract(One)))
                    return true;
            }
            if (!x[t].Equals(One)) { return true; }
            return false;
        }
    }*/
}
