using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RSA
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Application.Run(new UserApp()); /*
                BigInteger p = new BigInteger(Console.ReadLine());//считываем p
                BigInteger q = new BigInteger(Console.ReadLine());//считываем q
                if (p.Equals(new BigInteger("1")) || q.Equals(new BigInteger("1")) || !BigInteger.IsPrimeMillerRabin(p, 100) //проверяем
                || !BigInteger.IsPrimeMillerRabin(q, 100) || new BigInteger("200").CompareTo(p) == 1 || new BigInteger("200").CompareTo(q) == 1)
                {
                    Console.WriteLine("Неудачные p и q");
                }
                else
                {
                    //Cоздаем класс RSA
                    RSA rsa = new RSA(p, q);

                    //Cчитываем сообщение, которое необходимо зашифровать
                    string s = Console.ReadLine();

                    //Генерируем алфавит
                    RSA.GenerateInform();

                    //Преобразовываем строку в длинное число для шифрования
                    BigInteger num = RSA.GetBigInt(s);

                    /*
                    int i = 0;//с какой позиции начинается очередной блок
                    int len = 0;//размер блока
                    while (i < s.Length)//пока не весь текст разбит на блоки
                    {
                        BigInteger m = new BigInteger("0");//цифровое значение блока
                        len = 0;
                        while (m.CompareTo(rsa.n) == -1)//пока m= 0)//если оно больше или равно n
                        {
                            len--;//уменьшаем размер блока
                            break;
                        }
                        m = temp;//присваиваем хорошее длинное число


                        i = i + len;// переходим к следующей позиции */
                                                /*
                                                BigInteger crypt = rsa.Crypt(num);//шифруем

                                                                                //вот здесь данные можно передавать по сети и прочее

                                                BigInteger decrypt = rsa.Decrypt(crypt);//дешифруем

                                                Console.WriteLine(RSA.GetText(decrypt));//выводим результат на экран
                                            }
                                            Console.ReadKey();*/

            }
            catch (Exception s)
            {
                Console.WriteLine(s);
            }
        }
    }
}
