using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RSA
{
    /// <summary>
    /// Класс для работы с большим числом
    /// </summary>
    class BigInteger
    {
        //Массив, который хранит большое число
        private List<int> arr = new List<int>();

        //База
        private static int base_int = 8;

        //Положительное или отрицательное
        private bool sign = true;

        private static int mybase = (int)Math.Pow(10, base_int);

        /// <summary>
        /// Смена знака
        /// </summary>
        /// <param name="a">true +, false -</param>
        public void ChangeSign(bool a)
        {
            sign = a;
        }

        /// <summary>
        /// Конструктор для большого числа
        /// </summary>
        /// <param name="s">Большое число в строковом формате для преобразования в необходимый вид</param>
        public BigInteger(string s)
        {
            int tmpvalue = 0, tmpbase = 0; //временное значение и база



            for (int i = s.Length - 1; i >= 0; i--)
            {
                if (i == 0 && s[0] == '-')
                {
                    sign = false;
                }
                else
                {
                    //Если число уже нужной длины
                    if (tmpbase == base_int)
                    {
                        arr.Add(tmpvalue);
                        tmpvalue = int.Parse(s[i].ToString());
                        tmpbase = 1;
                    }
                    else
                    {
                        tmpvalue = tmpvalue + (int)Math.Pow(10, tmpbase) * int.Parse(s[i].ToString());
                        tmpbase++;
                    }
                }
            }

            //Если мы получили не пустую строку
            if (tmpbase != 0)
            {
                //Докладываем остаток
                arr.Add(tmpvalue);
            }
        }

        /// <summary>
        /// Конструктор из списка
        /// </summary>
        /// <param name="arr">Список, содержащий число</param>
        public BigInteger(List<int> arr, bool s)
        {
            this.arr = arr;
            sign = s;
        }

        /// <summary>
        /// Конструктор из массива
        /// </summary>
        /// <param name="arr">Массив</param>
        /// <param name="sign">Знак</param>
        public BigInteger(int[] arr, bool s)
        {
            for (int i = 0; i < arr.Length; i++)
                this.arr.Add(arr[i]);
            this.arr = Normalize(this.arr);
            this.sign = s;
        }

        /// <summary>
        /// Сравнение двух больших чисел
        /// </summary>
        /// <param name="other">Сравниваемое число</param>
        /// <returns>-1 - больше другое (сравниваемое) число, 0 - равны, 1 - больше число, 
        /// к которому применяется сравнение</returns>
        public int CompareTo(BigInteger other)
        {
            if (this.arr.Count == other.arr.Count)
            {
                for (int i = 0; i < this.arr.Count; i++)
                {
                    if (this.arr[i] > other.arr[i])
                        return 1;
                    if (this.arr[i] < other.arr[i])
                        return -1;
                }
                return 0;
            }
            else if (this.arr.Count > other.arr.Count)
                return 1;
            else
                return -1;
        }

        /// <summary>
        /// Преобразование большого числа в строку
        /// </summary>
        /// <returns>Большое число в строковом виде</returns>
        public string ToString()
        {
            StringBuilder ans = new StringBuilder();

            //Проверка на знак
            if (!sign)
                ans.Append('-');

            ans.Append(arr[arr.Count - 1].ToString());
            string temp;

            //Число нулей
            int zeroCount;

            for (int i = arr.Count - 2; i >= 0; i--)
            {
                temp = arr[i].ToString();
                zeroCount = base_int - temp.Length; //Подсчитываем количество не хватающих нулей
                for (int j = 0; j < zeroCount; j++)
                    ans.Append('0');
                ans.Append(temp);
            }
            return ans.ToString();
        }

        /// <summary>
        /// Сложение больших чисел
        /// </summary>
        /// <param name="value">Прибавляемое большое число</param>
        /// <returns>Результирующее большое число</returns>
        public BigInteger Add(BigInteger value)
        {
            //Значение переноса
            int k = 0;

            //Максимальная длина числа
            int n = Math.Max(arr.Count, value.arr.Count);

            //Результирующий массив
            List<int> ans = new List<int>();

            //Временное значение i-го разряда из первого и второго числа
            int tmp1, tmp2;

            for (int i = 0; i < n; i++)
            {
                tmp1 = (arr.Count > i) ? arr[i] : 0;
                tmp2 = (value.arr.Count > i) ? value.arr[i] : 0;

                ans.Add(tmp1 + tmp2 + k);

                //Если результат получился больше базы
                if (ans[i] >= mybase)
                {
                    ans[i] -= mybase;
                    k = 1;
                }
                else
                    k = 0;
            }

            //Если остался еще один перенос разряда
            if (k == 1)
                ans.Add(k);

            return new BigInteger(ans, true);
        }

        /// <summary>
        /// Вычитание
        /// </summary>
        /// <param name="value">Вычитаемое</param>
        /// <returns>Разность</returns>
        public BigInteger Substract(BigInteger value)
        {
            //Если второе число больше первого
            if (this.CompareTo(value) == -1)
            {
                BigInteger tmp = value.Substract(this);
                tmp.ChangeSign(false);
                return tmp;
            }

            //Значение переноса
            int k = 0;

            //Максимальная длина числа
            int n = Math.Max(arr.Count, value.arr.Count);

            //Результирующий массив
            List<int> ans = new List<int>();

            //Временное значение i-го разряда из первого и второго числа
            int tmp1, tmp2;

            for (int i = 0; i < n; i++)
            {
                tmp1 = (arr.Count > i) ? arr[i] : 0;
                tmp2 = (value.arr.Count > i) ? value.arr[i] : 0;

                ans.Add(tmp1 - tmp2 - k);

                if (ans[i] < 0)
                {
                    ans[i] += mybase;
                    k = 1;
                }
                else
                    k = 0;
            }

            return new BigInteger(Normalize(ans), true);
        }

        /// <summary>
        /// Удаление лидирующих нулей
        /// </summary>
        /// <param name="arr">Список для удаления</param>
        /// <returns>Нормализованный список</returns>
        private List<int> Normalize(List<int> arr)
        {
            //Удаляем лидирующие нули
            while (arr.Count > 0 && arr[arr.Count - 1] == 0)
                arr.RemoveAt(arr.Count - 1);
            return arr;
        }

        /// <summary>
        /// Умножение большого числа на маленькое (в рамках базы)
        /// </summary>
        /// <param name="value">Множитель, меньший базы</param>
        /// <returns>Произведение</returns>
        public BigInteger Multiply(int value)
        {
            //Проверка на число, меньшее базы
            if (value >= mybase)
                throw new Exception("This value is bigger than base");

            //Значение переноса
            int k = 0;

            //Результирующий массив
            List<int> ans = new List<int>();

            long tmp;

            for (int i = 0; i < arr.Count; i++)
            {
                tmp = (long)arr[i] * (long)value + k;
                ans.Add((int)(tmp % mybase));
                k = (int)(tmp / mybase);
            }

            ans.Add(k);

            return new BigInteger(Normalize(ans), true);
        }

        /// <summary>
        /// Умножение большого числа на большое
        /// </summary>
        /// <param name="value">Множитель</param>
        /// <returns>Произведение</returns>
        public BigInteger Multiply(BigInteger value)
        {
            BigInteger ans = new BigInteger("0");

            BigInteger tmp;

            for (int i = 0; i < arr.Count; i++)
            {
                //Умножаем текущий разряд числа на другое длинное число
                tmp = value.Multiply(arr[i]);

                //Сдвигаем его
                for (int j = 0; j < i; j++)
                    tmp.arr.Insert(0, 0);

                //Складываем с другими
                ans = ans.Add(tmp);
            }
            return ans;
        }

        /// <summary>
        /// Деление большого числа на короткое
        /// </summary>
        /// <param name="v">Делитель</param>
        /// <param name="r">Остаток от деления</param>
        /// <returns></returns>
        public BigInteger Divide(int v, out int r)
        {
            //Результат деления
            int[] ans = new int[arr.Count];

            //Остаток
            r = 0;

            int j = arr.Count - 1;
            long c;
            while (j >= 0)
            {
                c = (long)(r) * (long)(mybase) + arr[j];
                ans[j] = (int)(c / v);
                r = (int)(c % v);
                j--;
            }

            return new BigInteger(ans, sign);
        }

        /// <summary>
        /// Алгоритм Кнута деления длинных чисел
        /// </summary>
        /// <param name="q">Частное q[m]..q[0]</param>
        /// <param name="r">Остаток r[n - 1]..r[0]</param>
        /// <param name="u">u[m + n - 1]..u[0] — делимое по основанию b</param>
        /// <param name="v">v[n - 1]..v[0] — делитель по основанию b</param>
        /// <returns></returns>
        public static int Divide(out BigInteger q, out BigInteger r, BigInteger u, BigInteger v)
        {
            //Начальная инициализация
            int n = v.arr.Count;
            int m = u.arr.Count - v.arr.Count;
            int[] tempArray = new int[m + 1];
            tempArray[m] = 1;
            q = new BigInteger(tempArray, true);

            /* НОРМАЛИЗАЦИЯ
             * Нам необходимо преобразовать делимое и делитель, 
             * умножив на коэффициент d. d = [b/(v[n-1]+1)] и умножаем. 
             * Если d=1, то добавляем нулевой разряд u[m+n]=0 */
            int d = (mybase / (v.arr[n - 1] + 1));
            u = u.Multiply(d);
            v = v.Multiply(d);

            //Проверка на d==1
            if (u.arr.Count == n + m)
                u.arr.Add(0);

            //Начальная установка j. j = m
            int j = m;

            //Цикл по j
            while (j >= 0)
            {
                /*Вычислить временное q. tempq=[(u[j+n]*b+u[j+n-1])/v[n-1]], 
                 * tempr — остаток от такого деления. Если tempq=b или q*v[n-2]>b*tempr + u[j+n-2],
                 * то tempq = tempq — 1 и tempr = tempr + v[n-1]. Необходимо повторять данную проверку 
                 * пока temr < b*/
                long cur = (long)(u.arr[j + n]) * (long)(mybase) + u.arr[j + n - 1];
                int tempq = (int)(cur / v.arr[n - 1]);//нормализация помогает не выпрыгнуть за границу типа
                int tempr = (int)(cur % v.arr[n - 1]);
                do
                {
                    if (tempq == mybase || (long)tempq * (long)v.arr[n - 2] > (long)mybase * (long)tempr + u.arr[j + n - 2])
                    {
                        tempq--;
                        tempr += v.arr[n - 1];
                    }
                    else
                    {
                        break;
                    }
                }
                while (tempr < mybase); //Умножить и вычесть 
                BigInteger u2 = new BigInteger(u.arr.GetRange(j, n + 1), true);
                u2 = u2.Substract(v.Multiply(tempq));
                bool flag = false;
                if (!u2.sign) //если отрицательные
                {
                    flag = true;
                    List<int> bn = new List<int>();
                    for (int i = 0; i <= n; i++)
                        bn.Add(0);
                    bn.Add(1);
                    u2.ChangeSign(true);
                    u2 = new BigInteger(bn, true).Substract(u2);
                } //Проверка остатка
                q.arr[j] = tempq;
                if (flag)
                { //Компенсировать сложение
                    q.arr[j]--;
                    u2 = u2.Add(v);
                    if (u2.arr.Count > n + j)
                        u2.arr.RemoveAt(n + j);

                }
                //меняем u, так как все вычисления происходят с его разрядами
                for (int h = j; h < j + n; h++)
                {
                    if (h - j >= u2.arr.Count)
                    {
                        u.arr[h] = 0;
                    }
                    else
                    {
                        u.arr[h] = u2.arr[h - j];
                    }
                }
                j--;
            }
            q.arr = q.Normalize(q.arr);
            //Денормализация
            int unusedR = 0;
            r = new BigInteger(u.arr.GetRange(0, n), true).Divide(d, out unusedR);
            return 0;
        }
    }
}



