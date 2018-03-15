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
        public int AbsCompareTo(BigInteger other)
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
        /// Сравнивает 2 числа с учетом их знака
        /// </summary>
        /// <param name="other">сравнимое число</param>
        /// <returns>-1 - больше другое (сравниваемое) число, 0 - равны, 1 - больше число, 
        /// к которому применяется сравнение</returns>
        public int CompareTo(BigInteger other)
        {
            if (this.sign != other.sign)
            {
                if (this.sign == true)
                    return 1;
                else
                    return -1;
            }

            if (this.arr.Count == other.arr.Count)
            {
                for (int i = 0; i < this.arr.Count; i++)
                {
                    if (this.arr[i] > other.arr[i] && this.sign == true || this.arr[i] < other.arr[i] && this.sign == false)
                        return 1;
                    if (this.arr[i] < other.arr[i] && this.sign == true || this.arr[i] > other.arr[i] && this.sign == false)
                        return -1;
                }
                return 0;
            }
            else if (this.arr.Count > other.arr.Count && this.sign == true || this.arr.Count < other.arr.Count && this.sign == false)
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
        public BigInteger AbsAdd(BigInteger value)
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
        /// Сложение с учетом знака
        /// </summary>
        /// <param name="value">С чем складываем</param>
        /// <returns>Результат</returns>
        public BigInteger Add(BigInteger value)
        {
            if (this.sign && value.sign)
            {
                return this.AbsAdd(value);
            }
            if (!this.sign && value.sign)
            {
                return value.AbsSubstract(this);
            }
            if (this.sign && !value.sign)
            {
                return this.AbsSubstract(value);
            }
            BigInteger ans = this.AbsAdd(value);
            ans.ChangeSign(false);
            return ans;
        }

        /// <summary>
        /// Вычитание
        /// </summary>
        /// <param name="value">Вычитаемое</param>
        /// <returns>Разность</returns>
        public BigInteger AbsSubstract(BigInteger value)
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
        /// Вычитание с учетом знака
        /// </summary>
        /// <param name="value">Вычитаемое</param>
        /// <returns>Разность</returns>
        public BigInteger Substract(BigInteger value)
        {
            if (this.sign && value.sign)
            {
                return this.AbsSubstract(value);
            }
            if (!this.sign && value.sign)
            {
                BigInteger ans = this.AbsAdd(value);
                ans.ChangeSign(false);
                return ans;
            }
            if (this.sign && !value.sign)
            {
                return this.AbsAdd(value);
            }
            return value.AbsSubstract(this);
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
        public BigInteger AbsMultiply(int value)
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
        /// Умножение с учетом знака
        /// </summary>
        /// <param name="value">На что умножаем</param>
        /// <returns>Произведение</returns>
        public BigInteger Multiply(int value)
        {
            BigInteger ans = this.AbsMultiply(Math.Abs(value));
            if (this.sign && value >= 0 || !this.sign && value < 0)
                return ans;

            ans.ChangeSign(false);
            return ans;
        }

        /// <summary>
        /// Умножение большого числа на большое
        /// </summary>
        /// <param name="value">Множитель</param>
        /// <returns>Произведение</returns>
        public BigInteger AbsMultiply(BigInteger value)
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
        /// Умножение больших чисел с учетом знака
        /// </summary>
        /// <param name="value">На что умножаем</param>
        /// <returns>Произведение</returns>
        public BigInteger Multiply(BigInteger value)
        {
            BigInteger ans = this.AbsMultiply(value);
            if ((this.sign && value.sign) || (!this.sign && !value.sign))
                return ans;
            ans.ChangeSign(false);
            return ans;
        }
        /// <summary>
        /// Деление большого числа на короткое
        /// </summary>
        /// <param name="v">Делитель</param>
        /// <param name="r">Остаток от деления</param>
        /// <returns></returns>
        public BigInteger AbsDivide(int v, out int r)
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
        /// Деление большого числа на маленькое с учетом знака
        /// </summary>
        /// <param name="v">Делитель</param>
        /// <param name="r">Остаток</param>
        /// <returns>Частное</returns>
        public BigInteger Divide(int v, out int r)
        {
            if (v == 0)
                throw new Exception("divide by zero");
            BigInteger ans = this.AbsDivide(Math.Abs(v), out r);

            if ((this.sign && v > 0) || (!this.sign && v < 0))
                return ans;
            ans.ChangeSign(false);
            r = -r;
            return ans;
        }

        /// <summary>
        /// Алгоритм Кнута деления длинных чисел
        /// </summary>
        /// <param name="q">Частное q[m]..q[0]</param>
        /// <param name="r">Остаток r[n - 1]..r[0]</param>
        /// <param name="u">u[m + n - 1]..u[0] — делимое по основанию b</param>
        /// <param name="v">v[n - 1]..v[0] — делитель по основанию b</param>
        public static int AbsDivide(out BigInteger q, out BigInteger r, BigInteger u, BigInteger v)
        {
            //Начальная инициализация
            int n = v.arr.Count;
            int m = u.arr.Count - v.arr.Count;
            int[] tempArray = new int[m + 1];
            tempArray[m] = 1;
            q = new BigInteger(tempArray, true);

            /* 1. НОРМАЛИЗАЦИЯ
             * Нам необходимо преобразовать делимое и делитель, 
             * умножив на коэффициент d. d = [b/(v[n-1]+1)] и умножаем. 
             * Если d=1, то добавляем нулевой разряд u[m+n]=0 */
            int d = (mybase / (v.arr[n - 1] + 1));
            u = u.Multiply(d);
            v = v.Multiply(d);

            //Проверка на d==1
            if (u.arr.Count == n + m)
                u.arr.Add(0);

            //2. Начальная установка j. j = m
            int j = m;

            //Цикл по j
            while (j >= 0)
            {
                /*3. Вычислить временное q. tempq=[(u[j+n]*b+u[j+n-1])/v[n-1]], 
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
                while (tempr < mybase);

                /*4. Умножить и вычесть. u[j+n]..u[j] = u[j+n]..u[j] — tempq * v[n-1].. v[0]. Значение разрядов должно быть 
                 * всегда положительным, поэтому если мы получаем отрицательный разряд, то должны 
                 * прибавить b^(n+1). Причем следует запомнить заимствование слева из старшего разряда.*/
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
                }
                /*5.Проверка остатка
                 q[j] = tempq. Если результат 4 — го шага был отрицательным, то перейти к шагу 6, иначе к шагу 7.*/
                q.arr[j] = tempq;
                if (flag)
                { /*6.Компенсировать сложение. Вероятность данного шага очень мала, а именно r/b, 
                    т.е при большой базе вы очень редко будете попадать в отрицательные числа. 
                    q[j] = q[j] — 1. u[j+n]..u[j] = u[j+n]..u[j] + v[n-1]..v[0], при этом произойдет
                    перенос в разряд слева от u[j+n], но им следует пренебречь, так как перенос погашается
                    заимствованием из того же разряда произведенном на шаге 4.*/
                    q.arr[j]--;
                    u2 = u2.Add(v);
                    if (u2.arr.Count > n + j)
                        u2.arr.RemoveAt(n + j);

                }
                //меняем u, так как все вычисления происходят с его разрядами
                /*7.Цикл по j. j уменьшаем на 1. Если j>=0, то вернуться к шагу 3.*/
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
            //8.Денормализация
            /*q[m]..q[0] — искомое частное, а для получения искомого остатка достаточно u[n-1]..u[0]/d*/
            int unusedR = 0;
            r = new BigInteger(u.arr.GetRange(0, n), true).Divide(d, out unusedR);
            return 0;
        }

        /// <summary>
        /// Делим большое число на большое с учетом знака
        /// </summary>
        /// <param name="q">Частное q[m]..q[0]</param>
        /// <param name="r">Остаток r[n - 1]..r[0]</param>
        /// <param name="u">u[m + n - 1]..u[0] — делимое по основанию b</param>
        /// <param name="v">v[n - 1]..v[0] — делитель по основанию b</param>
        /// <returns></returns>
        private static int Divide(out BigInteger q, out BigInteger r, BigInteger u, BigInteger v)
        {
            int ans = AbsDivide(out q, out r, u, v);
            if ((u.sign && v.sign) || (!u.sign && !v.sign))
            {
                return ans;
            }
            q.ChangeSign(!q.sign);
            r.ChangeSign(!r.sign);
            return ans;

        }

        /// <summary>
        /// Берем остаток от деления большого числа на большое
        /// </summary>
        /// <param name="v">Делитель</param>
        /// <returns>Остаток от деления</returns>
        public BigInteger Mod(BigInteger v)
        {
            BigInteger q;
            BigInteger r;
            if (this.AbsCompareTo(v) > -1)
            {
                //Если число маленькое, то делим большое на малое
                if (v.arr.Count == 1)
                {
                    int tempr = 0;
                    this.Divide(v.arr[0], out tempr);
                    return new BigInteger(tempr.ToString());
                }
                Divide(out q, out r, this, v);
                return r;
            }
            else
            {
                return this;
            }
        }

        /// <summary>
        /// Деление нацело
        /// </summary>
        /// <param name="v">Второе число</param>
        /// <returns>Результат</returns>
        public BigInteger Div(BigInteger v)
        {
            BigInteger q;
            BigInteger r;
            if (this.CompareTo(v) > -1)
            {
                if (v.arr.Count == 1)
                {
                    int tempr = 0;
                    return this.Divide(v.arr[0], out tempr);
                }
                Divide(out q, out r, this, v);
            }
            else
            {
                return new BigInteger("0");
            }
            return q;
        }

        /// <summary>
        /// Бинарное возведение в степень
        /// </summary>
        /// <param name="k"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public BigInteger Pow(BigInteger k, BigInteger n)
        {
            BigInteger a = new BigInteger(arr, sign);
            BigInteger b = new BigInteger("1");
            /*Заметим, что для любого числа a и чётного числа n выполнимо очевидное тождество 
             * (следующее из ассоциативности операции умножения): a^n = (a^{n/2})^2 = a^{n/2} * a^{n/2} 
             * Оно и является основным в методе бинарного возведения в степень. Действительно, для чётного n 
             * мы показали, как, потратив всего одну операцию умножения, можно свести задачу к вдвое меньшей степени. 
             * Осталось понять, что делать, если степень n нечётна. Здесь мы поступаем очень просто: перейдём к степени
             * n-1, которая будет уже чётной: a^n = a^{n-1} * a Итак, мы фактически нашли рекуррентную формулу: 
             * от степени n мы переходим, если она чётна, к n/2, а иначе — к n-1. Понятно, что всего будет не более
             * 2 log n переходов, прежде чем мы придём к n = 0. Таким образом, мы получили 
             * алгоритм, работающий за O (log n) умножений.*/
            while (k.CompareTo(new BigInteger("0")) > 0)
            {

                int r = 0;
                BigInteger q = k.Divide(2, out r); //Делим степень k на 2
                if (r == 0)
                {
                    k = q; //присваеваем новую степень
                    a = a.Multiply(a).Mod(n);// [ a = (a*a)%n; ]
                }
                else
                {
                    k = k.Substract(new BigInteger("1"));
                    b = b.Multiply(a).Mod(n);// [ b = (b*a)%n; ]
                }
            }
            return b;
        }
    }
}



