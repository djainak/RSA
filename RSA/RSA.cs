using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSA
{
    class RSA
    {
        public BigInteger publicKey; //публичный ключ
        private BigInteger privateKey; //приватный ключ
        public BigInteger n;

        //Mассив пар, который будет содержать все символы, которые мы собираемся шифровать
        static List<Pair> sym = new List<Pair>();

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="p">Простое число</param>
        /// <param name="q">Простое число</param>
        public RSA(BigInteger p, BigInteger q)
        {
            //Считаем n
            n = p.Multiply(q);

            /*Считаем функцию Эйлера φ(n)= (p-1)(q-1). Функция Эйлера φ(n), где n — натуральное число, 
             * равна количеству натуральных чисел, меньших и взаимно простых с ним.*/
            BigInteger fi = (p.Substract(new BigInteger("1"))).Multiply(q.Substract(new BigInteger("1")));

            /*Генерируем публичный ключ. Для этого нужно взять число взаимнопростое с n, которое будет меньше,
             * чем значение функции Эйлера(фи)*/
            publicKey = BigInteger.Generate(fi);

            while (!BigInteger.IsPrimeMillerRabin(publicKey, 100))
                publicKey = BigInteger.Generate(fi);

            /*Считаем приватный ключ. Для этого берется обратное к публичному ключу по модулю n.*/
            privateKey = publicKey.Inverse(fi);
        }

        /// <summary>
        /// Шифрование. Для того, чтобы выполнить шифрование некоторого числа по алгоритму RSA необходимо возвести
        /// это число в степень публичного ключа по модулю n.
        /// </summary>
        /// <param name="m">Шифруемое число</param>
        /// <returns>Зашифрованное число - криптограмма</returns>
        public BigInteger Crypt(BigInteger m)
        {
            return m.Pow(publicKey, n);
        }

        /// <summary>
        /// Дешифрование. Для дешифрования необходимо криптограмму возвести в степень приватного ключа по модулю n.
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public BigInteger Decrypt(BigInteger c)
        {
            return c.Pow(privateKey, n);
        }

        /// <summary>
        /// Заполняем массив, который будет содержать все символы, которые мы собираемся шифровать
        /// </summary>
        public static void GenerateInform()
        {
            int i = 0;
            for (char c = 'а'; c <= 'я'; c++)
            {
                Pair p = new Pair()
                { Digit = (i < 10) ? ('0' + i.ToString()).ToString() : i.ToString(), Letter = c };

                sym.Add(p);
                i++;
            }
        }

        /// <summary>
        /// Перевод строки в число для шифрования
        /// </summary>
        /// <param name="s">Строка</param>
        /// <returns>Число</returns>
        public static BigInteger GetBigInt(string s)
        {
            StringBuilder ans = new StringBuilder();
            ans.Append('1');
            for (int i = 0; i < s.Length; i++)
            {
                for (int j = 0; j < sym.Count; j++)
                {
                    if (s[i].Equals(sym[j].Letter))
                    {
                        ans.Append(sym[j].Digit);
                        break;
                    }
                }
            }
            return new BigInteger(ans.ToString());
        }

        /// <summary>
        /// Перевод числа в строку
        /// </summary>
        /// <param name="b">Число </param>
        /// <returns>Строка</returns>
        public static string GetText(BigInteger b)
        {
            string s = b.ToString();
            StringBuilder ans = new StringBuilder();
            for (int i = 1; i < s.Length; i = i + 2)
            {
                string temp = s.Substring(i, 2);
                for (int j = 0; j < sym.Count; j++)
                {
                    if (sym[j].Digit.Equals(temp))
                    {
                        ans.Append(sym[j].Letter);
                        break;
                    }
                }
            }
            return ans.ToString();
        }
    }
}
