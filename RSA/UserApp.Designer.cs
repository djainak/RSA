using System;
using System.Windows.Forms;

namespace RSA
{
    partial class UserApp
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxP = new System.Windows.Forms.TextBox();
            this.textBoxQ = new System.Windows.Forms.TextBox();
            this.buttonGen = new System.Windows.Forms.Button();
            this.buttonEnter = new System.Windows.Forms.Button();
            this.labelStatusPQ = new System.Windows.Forms.Label();
            this.textBox = new System.Windows.Forms.TextBox();
            this.labelText = new System.Windows.Forms.Label();
            this.buttonShifr = new System.Windows.Forms.Button();
            this.labelInform = new System.Windows.Forms.Label();
            this.radioButton = new System.Windows.Forms.GroupBox();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(25, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "p = ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 104);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(25, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "q = ";
            // 
            // textBoxP
            // 
            this.textBoxP.ForeColor = System.Drawing.SystemColors.GrayText;
            this.textBoxP.Location = new System.Drawing.Point(55, 79);
            this.textBoxP.Name = "textBoxP";
            this.textBoxP.Size = new System.Drawing.Size(664, 20);
            this.textBoxP.TabIndex = 5;
            this.textBoxP.Text = "Введите простое число";
            // 
            // textBoxQ
            // 
            this.textBoxQ.ForeColor = System.Drawing.SystemColors.GrayText;
            this.textBoxQ.Location = new System.Drawing.Point(55, 101);
            this.textBoxQ.Name = "textBoxQ";
            this.textBoxQ.Size = new System.Drawing.Size(664, 20);
            this.textBoxQ.TabIndex = 6;
            this.textBoxQ.Text = "Введите простое число";
            // 
            // buttonGen
            // 
            this.buttonGen.Location = new System.Drawing.Point(26, 52);
            this.buttonGen.Name = "buttonGen";
            this.buttonGen.Size = new System.Drawing.Size(101, 23);
            this.buttonGen.TabIndex = 7;
            this.buttonGen.Text = "Сгенерировать";
            this.buttonGen.UseVisualStyleBackColor = true;
            // 
            // buttonEnter
            // 
            this.buttonEnter.Location = new System.Drawing.Point(26, 130);
            this.buttonEnter.Name = "buttonEnter";
            this.buttonEnter.Size = new System.Drawing.Size(75, 23);
            this.buttonEnter.TabIndex = 8;
            this.buttonEnter.Text = "Ввести";
            this.buttonEnter.UseVisualStyleBackColor = true;
            // 
            // labelStatusPQ
            // 
            this.labelStatusPQ.AutoSize = true;
            this.labelStatusPQ.Location = new System.Drawing.Point(107, 135);
            this.labelStatusPQ.Name = "labelStatusPQ";
            this.labelStatusPQ.Size = new System.Drawing.Size(0, 13);
            this.labelStatusPQ.TabIndex = 9;
            // 
            // textBox
            // 
            this.textBox.Location = new System.Drawing.Point(26, 178);
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(689, 20);
            this.textBox.TabIndex = 10;
            // 
            // labelText
            // 
            this.labelText.AutoSize = true;
            this.labelText.Location = new System.Drawing.Point(26, 160);
            this.labelText.Name = "labelText";
            this.labelText.Size = new System.Drawing.Size(203, 13);
            this.labelText.TabIndex = 11;
            this.labelText.Text = "Введите сообщение для шифрования: ";
            // 
            // buttonShifr
            // 
            this.buttonShifr.Location = new System.Drawing.Point(26, 205);
            this.buttonShifr.Name = "buttonShifr";
            this.buttonShifr.Size = new System.Drawing.Size(203, 23);
            this.buttonShifr.TabIndex = 12;
            this.buttonShifr.Text = "Зашифровать и дешифровать";
            this.buttonShifr.UseVisualStyleBackColor = true;
            // 
            // labelInform
            // 
            this.labelInform.AutoSize = true;
            this.labelInform.Location = new System.Drawing.Point(26, 235);
            this.labelInform.Name = "labelInform";
            this.labelInform.Size = new System.Drawing.Size(300, 13);
            this.labelInform.TabIndex = 13;
            this.labelInform.Text = "Здесь будет информация о шифровании и дешифровании";
            // 
            // radioButton
            // 
            this.radioButton.Controls.Add(this.radioButton2);
            this.radioButton.Controls.Add(this.radioButton1);
            this.radioButton.Location = new System.Drawing.Point(26, 12);
            this.radioButton.Name = "radioButton";
            this.radioButton.Size = new System.Drawing.Size(415, 38);
            this.radioButton.TabIndex = 14;
            this.radioButton.TabStop = false;
            this.radioButton.Text = "Выбор способа ввода простых чисел:";
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(7, 15);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(79, 17);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Генерация";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(93, 15);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(87, 17);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Ручной ввод";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // UserApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(727, 399);
            this.Controls.Add(this.radioButton);
            this.Controls.Add(this.labelInform);
            this.Controls.Add(this.buttonShifr);
            this.Controls.Add(this.labelText);
            this.Controls.Add(this.textBox);
            this.Controls.Add(this.labelStatusPQ);
            this.Controls.Add(this.buttonEnter);
            this.Controls.Add(this.buttonGen);
            this.Controls.Add(this.textBoxQ);
            this.Controls.Add(this.textBoxP);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "UserApp";
            this.Text = "RSA";
            this.radioButton.ResumeLayout(false);
            this.radioButton.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxP;
        private System.Windows.Forms.TextBox textBoxQ;
        private System.Windows.Forms.Button buttonGen;
        private System.Windows.Forms.Button buttonEnter;
        private System.Windows.Forms.Label labelStatusPQ;
        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.Label labelText;
        private System.Windows.Forms.Button buttonShifr;
        private System.Windows.Forms.Label labelInform;
        private GroupBox radioButton;
        private RadioButton radioButton2;
        private RadioButton radioButton1;
    }
    
    }