using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RSA
{
    public partial class UserApp : Form
    {
        RSA r;
        public UserApp()
        {
            InitializeComponent();
            buttonGen.Click += new System.EventHandler(this.buttonGen_Click);
            buttonEnter.Click += new EventHandler(this.buttonEnter_Click);
            buttonShifr.Click += new EventHandler(this.buttonShifr_Click);
            radioButton1.Click += new System.EventHandler(this.radioButton1_Click);
            radioButton2.Click += new System.EventHandler(this.radioButton2_Click);
            textBoxP.Click += new System.EventHandler(this.textBoxP_Click);
            textBoxQ.Click += new System.EventHandler(this.textBoxQ_Click);

            textBoxP.ReadOnly = true;
            textBoxQ.ReadOnly = true;
            buttonGen.Enabled = false;
            buttonEnter.Enabled = false;
            textBox.ReadOnly = true;
            buttonShifr.Enabled = false;
        }

        private void radioButton2_Click(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                textBoxP.ReadOnly = false;
                textBoxQ.ReadOnly = false;
                buttonGen.Enabled = false;
                buttonEnter.Enabled = true;
            }
        }
        private void radioButton1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                textBoxP.ReadOnly = true;
                textBoxQ.ReadOnly = true;
                buttonGen.Enabled = true;
                buttonEnter.Enabled = false;
            }
        }

        
        private void textBoxP_Click(object sender, EventArgs e)
        {
            textBoxP.SelectionStart = 0;
            textBoxP.SelectionLength = textBoxP.Text.Length;
        }

        private void textBoxQ_Click(object sender, EventArgs e)
        {
            textBoxQ.SelectionStart = 0;
            textBoxQ.SelectionLength = textBoxQ.Text.Length;
        }


        private void buttonGen_Click(object sender, EventArgs e)
        {
            BigInteger p = BigInteger.Generate(new BigInteger("2000"));

            while (!BigInteger.IsPrimeMillerRabin(p, 100))
                p = BigInteger.Generate(new BigInteger("2000"));

            BigInteger q = BigInteger.Generate(new BigInteger("2000"));

            while (!BigInteger.IsPrimeMillerRabin(q, 100))
                q = BigInteger.Generate(new BigInteger("2000"));

            textBoxP.Text = p.ToString();
            textBoxQ.Text = q.ToString();
            textBox.ReadOnly = false;
            buttonShifr.Enabled = true;
        }

        private void buttonEnter_Click(object sender, EventArgs e)
        {
            int num;
            bool isNumP = int.TryParse(textBoxP.Text, out num);
            bool isNumQ = int.TryParse(textBoxQ.Text, out num);
            if (isNumP & isNumQ)
            {
                BigInteger p = new BigInteger(textBoxP.Text);
                BigInteger q = new BigInteger(textBoxQ.Text);
                if (!p.Equals(new BigInteger("1")) && !q.Equals(new BigInteger("1")) && BigInteger.IsPrimeMillerRabin(p, 100) && BigInteger.IsPrimeMillerRabin(q, 100) && new BigInteger("2000").CompareTo(p) == 1 && new BigInteger("2000").CompareTo(q) == 1)
                {
                    labelStatusPQ.Text = "Числа введены верно, можно шифровать.";
                    textBox.ReadOnly = false;
                    buttonShifr.Enabled = true;
                }
                else 
                {
                    labelStatusPQ.Text = "Неудачные p и q.";
                    textBox.ReadOnly = true;
                    buttonShifr.Enabled = false;
                }
            }
            else
            {
                labelStatusPQ.Text = "Числа введены НЕ верно.";
                textBox.ReadOnly = true;
                buttonShifr.Enabled = false;
            }
        }
        private void buttonShifr_Click(object sender, EventArgs e)
        {
            if (textBox.Text == "")
                labelInform.Text = "Шифровать нечего. Поле пустое.";
            else
            {
                BigInteger p = new BigInteger(textBoxP.Text);
                BigInteger q = new BigInteger(textBoxQ.Text);

                //Cоздаем класс RSA
                RSA rsa = new RSA(p, q);

                //Cчитываем сообщение, которое необходимо зашифровать
                string s = textBox.Text;

                //Генерируем алфавит
                RSA.GenerateInform();

                //Преобразовываем строку в длинное число для шифрования
                BigInteger num = RSA.GetBigInt(s);

                //Выводим получившееся длинное число в строку информации
                labelInform.Text = "Сообщение преобразовано в длинное число:\n" + num.ToString() + "\n";

                try
                {
                    int i = 1;//с какой позиции начинается очередной блок
                    int len = 1;//размер блока
                    BigInteger crypt = new BigInteger("0");
                    List<BigInteger> cr = new List<BigInteger>();
                    string nn = num.ToString();
                    while (i < nn.Length)//пока не весь текст разбит на блоки 
                    {
                        ++len;
                        string temp = nn.Substring(i, len);

                        if (new BigInteger(temp).CompareTo(rsa.n) >= 0)
                        {
                            temp = nn.Substring(i, len - 1);
                            labelInform.Text = labelInform.Text + temp + " 1\n";
                            cr.Add(new BigInteger(temp));
                            i += len;
                            --i;
                            len = 0;
                        }
                        else if (i + len >= nn.Length)
                        {
                            temp = nn.Substring(i, len);
                            cr.Add(new BigInteger(temp));
                            labelInform.Text = labelInform.Text + temp + " 2\n";
                            break;
                        }
                    }

                    labelInform.Text = labelInform.Text + " Я вышел из цикла\n";

                    List<BigInteger> shifr = new List<BigInteger>();
                    foreach (BigInteger a in cr)
                    {
                        shifr.Add(rsa.Crypt(a));
                    }
                    
                    labelInform.Text = labelInform.Text + " Я вышел из шифровки\n";
                    List<BigInteger> shifr2 = new List<BigInteger>();
                    foreach (BigInteger a in shifr)
                    {
                        shifr2.Add(rsa.Decrypt(a));
                    }
                    
                    labelInform.Text = labelInform.Text + " Я вышел из дешифровки\n";

                    string tmp = "1";
                    foreach (BigInteger a in shifr2)
                    {
                        labelInform.Text = labelInform.Text + a.ToString() +  " Я взял число\n";
                        tmp = tmp + RSA.GetText(a);
                        labelInform.Text = labelInform.Text + tmp + " У меня получилось\n";
                    }
                    labelInform.Text = labelInform.Text + " Я составил строку\n";

                    //Расшифрованное сообщение
                    labelInform.Text = labelInform.Text + "Зашифрованное сообщение:\n" + tmp + "\n";
                }
                catch (Exception ex)
                {
                    labelInform.Text = labelInform.Text + ex.Message;
                }
                
                

                
                
                
                

                /*
                BigInteger crypt;
                
                //Шифруем и получаем криптограмму
                crypt = rsa.Crypt(num);

                //Выводим получившуюся криптограмму в строку информации
                labelInform.Text = labelInform.Text + "Криптограмма:\n" + crypt.ToString() + "\n";

                //Дешифруем
                BigInteger decrypt = rsa.Decrypt(crypt);

                //Выводим получившуюсяя дешифрованную криптограмму в строку информации
                labelInform.Text = labelInform.Text + "Дешифрованная криптограмма:\n" + decrypt.ToString() + "\n";
                
                //Расшифрованное сообщение
                //labelInform.Text = labelInform.Text + "Зашифрованное сообщение:\n" + RSA.GetText(decrypt) + "\n";
                */
            }
        }

    }
}
