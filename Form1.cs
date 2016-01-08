using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace gomoku
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public int[,] pole = new int[15, 15];
        public Graphics pole1;
        private int x;
        private int y;
        private bool hod = false;
        double[,] ozenkac = new double[15, 15];  //оценка ходов для пк
        double[,] ocenka = new double[15, 15];    //оценка ходов для человека
        int[,] massivsum = new int[15, 15];
        int[,] massivsumc = new int[15, 15];
        double[,] f = new double[15, 15];
        
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            
            pictureBox1.BackColor = Color.White;
            
            for (int i1 = 0; i1 < 16; i1++)
            {
                pole1.DrawLine(Pens.Black, 0, 0 + i1 * 22, 330, 0 + i1 * 22);
                pole1.DrawLine(Pens.Black, 0 + i1 * 22, 0, 0 + i1 * 22, 330);
            }
            if (Class1.pc == true)
            {
                if (Class1.znak)
                {
                    pole[7, 7] = 2;
                    pole1.DrawEllipse(Pens.Red, 7 * 22, 7 * 22, 22, 22);
                    hod = false;
                }
                else
                {
                    pole[7, 7] = 2;
                    pole1.FillEllipse(Brushes.Red, 7 * 22, 7 * 22, 22, 22);
                    hod = false;
                }
            }
            int i = 0;
            int j = 0;
            if ((e.X > 0) && (e.X < 330) && (e.Y > 0) && (e.Y < 330))
            {
                x = ((e.X) / 22) * 22;
                y = ((e.Y) / 22) * 22;
                j = (e.X) / 22;
                i = (e.Y) / 22;
                win();
                if ((pole[i, j] == 0) && (hod == false) && (winer == false))
                {
                    Class1.kolvohod++;
                    if (Class1.znak)
                    {
                        pole1.DrawLine(Pens.Blue, x, y, x + 22, y + 22);
                        pole1.DrawLine(Pens.Blue, x + 22, y, x, y + 22);
                        pole[i, j] = 1;
                        hod = true;
                        toolStripStatusLabel1.Text = max1.ToString();
                        comp();
                        if (winer == false) { win(); }
                        toolStripStatusLabel1.Text = max1.ToString();
                    }
                    else {
                        pole1.FillEllipse(Brushes.Black,x,y,22,22);
                       
                        pole[i, j] = 1;
                        hod = true;
                        toolStripStatusLabel1.Text = max1.ToString();
                        comp();
                        if (winer == false) { win(); }
                        toolStripStatusLabel1.Text = max1.ToString();
                     }
                }
                else
                {
                    MessageBox.Show("Cюда ходить нельзя");
                }
               
            }
            string str1 = "";
            string str2 = "";
            if (winer) {
                timer1.Stop();
                if (Class1.kolvohod1) { str1 = Class1.kolvohod.ToString()+"ходов"; }
                if (Class1.time1) { str2 = Class1.time.ToString()+"секунд"; }
                if (Class1.time1||Class1.kolvohod1){
                    MessageBox.Show("Статистика игры" + " " + str1 + " " + str2);
                }
                
            }
   
        }
        int kolvoodinak = 0;
        double max1;
        int j0, i0;
        int jr, ir;
        //Ход компьютера.Назначение весов и выбор клетки для хода.
        public void comp()
        {
            int[] koordodinaki = new int[225];
            int[] koordodinakj = new int[225];
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    massivsum[i, j] = 0;
                    massivsumc[i, j] = 0;
                    ocenka[i, j] = 0;
                    ozenkac[i, j] = 0;
                }
            }
            summa();
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    ocenka[i, j] = (double)massivsum[i, j];
                    ozenkac[i, j] = (double)massivsumc[i, j];
                    f[i, j] = ocenka[i, j] + ozenkac[i, j];
                }
            }
            max1 = 0;
            if (hod == true)
            {
                for (int i = 0; i < 15; i++)
                {
                    for (int j = 0; j < 15; j++)
                    {
                        if (pole[i, j] == 0)
                        {
                            if ((f[i, j] > max1))
                            {
                                max1 = f[i, j];
                            }
                        }
                    }
                }
                kolvoodinak = 0;
                for (int i = 0; i < 15; i++)
                {
                    for (int j = 0; j < 15; j++)
                    {
                        if (f[i, j] == max1)
                        {
                            koordodinaki[kolvoodinak] = i;
                            koordodinakj[kolvoodinak] = j;
                            kolvoodinak = kolvoodinak + 1;
                        }
                    }
                }
                Random r = new Random();
                int c = r.Next(0, kolvoodinak);
                i0 = koordodinaki[c];
                j0 = koordodinakj[c];
                jr = j0;
                ir = i0;
                win();
                if ((pole[ir, jr] == 0) && (!winer)) //Рисуем круг в нужной клетке.
                {
                    if (Class1.znak)
                    {
                        pole1.DrawEllipse(Pens.Red, jr * 22, ir * 22, 22, 22);
                        pole[ir, jr] = 2;
                    }
                    else {
                        pole1.FillEllipse(Brushes.Red,jr * 22, ir * 22, 22, 22);
                        pole[ir, jr] = 2;
                    
                    }
                }
                hod = false;
            }
        }
        int i1, j1;
        int g;
        int sum1;
        int sum2;
        int sum3;
        int sum4;
        int sum5;
        int sum6;
        int sum7;
        int sum8;
        int sum1c;
        int sum2c;
        int sum3c;
        int sum4c;
        int sum5c;
        int sum6c;
        int sum7c;
        int sum8c;
        int[] massivi = new int[225];
        int[] massivj = new int[225];
        public void summa() //Кол-во крестиков и ноликов
        {
            g = 0;
            for (int i = 0; i < 225; i++)
            {
                massivi[i] = 0;
                massivj[i] = 0;
            }
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    if (pole[i, j] == 0)
                    {
                        g++;
                        i1 = i;
                        j1 = j;
                        massivi[g - 1] = i;
                        massivj[g - 1] = j;
                    }
                }
            }
            sum1 = 0;
            sum1c = 0;
            sum2 = 0;
            sum3 = 0;
            sum3c = 0;
            sum2c = 0;
            sum4 = 0;
            sum4c = 0;
            sum5 = 0;
            sum5c = 0;
            sum6 = 0;
            sum6c = 0;
            sum7c = 0;
            sum7 = 0;
            sum8 = 0;
            sum8c = 0;
            for (int i = 0; i < 224; i++)
            {
                i1 = massivi[i];
                j1 = massivj[i];
                int k1 = 0;
                int k2 = 0;
                int m1 = 0;
                int m2 = 0;
                int l1 = 0;
                int l2 = 0;
                int s1 = 0;
                int s2 = 0;
                int k11 = 0;
                int k21 = 0;
                int m11 = 0;
                int m21 = 0;
                int l11 = 0;
                int l21 = 0;
                int s11 = 0;
                int s21 = 0;
                int i2;
                for (i2 = 1; i2 < 5; i2++)
                {
                    if (i1 + i2 < 15)
                    {
                        if (pole[i1 + i2, j1] == 2)
                        {
                            k1++;
                        }
                        if (pole[i1 + i2, j1] == 1)
                        {
                            k2++;
                        }
                        if ((k1 >= 3) || (k2 >= 3))
                        {
                            if (k1 == 3) sum1c = 1000;
                            if (k2 == 3)
                            {
                                sum1 = 1500;
                                if (pole[i1 + 1, j1] == 2)
                                {
                                    sum1 = 0;
                                    sum1c = 10;
                                }
                            }
                            if ((k2 == 3) && (k1 == 0))
                            {
                                if (pole[i1 + 1, j1] == 1)
                                {
                                    sum1 = 4000;
                                }
                                if (pole[i1 + 1, j1] == 0)
                                {
                                    sum1 = 2000;
                                }
                            }
                            if ((k1 == 3) && (k2 == 0))
                            {
                                if (pole[i1 + 1, j1] == 2)
                                {
                                    sum1c = 3500;
                                }
                                if (pole[i1 + 1, j1] == 0)
                                {
                                    sum1c = 1500;
                                }
                            }

                            if (k1 == 4) sum1c = 10000;
                            if (k2 == 4) sum1 = 7000;
                        }
                        else
                        {
                            sum1c = k1 * 20;
                            sum1 = k2 * 10;
                        }
                    }
                    if (i1 - i2 >= 0)
                    {
                        if (pole[i1 - i2, j1] == 2)
                        {
                            k11++;
                        }
                        if (pole[i1 - i2, j1] == 1)
                        {
                            k21++;
                        }
                        if ((k11 >= 3) || (k21 >= 3))
                        {
                            if (k11 == 3) sum5c = 1000;
                            if (k21 == 3)
                            {
                                sum5 = 1500;
                                if (pole[i1 - 1, j1] == 2)
                                {
                                    sum5 = 0;
                                    sum5c = 10;
                                }
                            }
                            if ((k21 == 3) && (k11 == 0))
                            {
                                if (pole[i1 - 1, j1] == 1)
                                {
                                    sum5 = 4000;
                                }
                                if (pole[i1 - 1, j1] == 0)
                                {
                                    sum5 = 2000;
                                }

                            }
                            if ((k11 == 3) && (k21 == 0))
                            {
                                if (pole[i1 - 1, j1] == 2)
                                {
                                    sum5c = 3500;
                                }
                                if (pole[i1 - 1, j1] == 0)
                                {
                                    sum5c = 1500;
                                }
                            }

                            if (k11 == 4) sum5c = 10000;
                            if (k21 == 4) sum5 = 7000;
                        }
                        else
                        {
                            sum5c = k11 * 20;
                            sum5 = k21 * 10;
                        }
                    }
                    if (j1 + i2 < 15)
                    {
                        if (pole[i1, j1 + i2] == 2)
                        {
                            m1++;
                        }
                        if (pole[i1, j1 + i2] == 1)
                        {
                            m2++;
                        }
                        if ((m1 >= 3) || (m2 >= 3))
                        {
                            if (m1 == 3) sum2c = 1000;
                            if (m2 == 3)
                            {
                                sum2 = 1500;
                                if (pole[i1, j1 + 1] == 2)
                                {
                                    sum2 = 0;
                                    sum2c = 10;
                                }
                            }
                            if ((m2 == 3) && (m1 == 0))
                            {
                                if (pole[i1, j1 + 1] == 1)
                                {
                                    sum2 = 4000;
                                }
                                if (pole[i1, j1 + 1] == 0)
                                {
                                    sum2 = 2000;
                                }
                            }
                            if ((m1 == 3) && (m2 == 0))
                            {
                                if (pole[i1, j1 + 1] == 2)
                                {
                                    sum2c = 3500;
                                }
                                if (pole[i1, j1 + 1] == 0)
                                {
                                    sum2c = 1500;
                                }
                            }
                            if (m1 == 4) sum2c = 10000;
                            if (m2 == 4) sum2 = 7000;
                        }
                        else
                        {
                            sum2c = m1 * 20;
                            sum2 = m2 * 10;
                        }
                    }
                    if (j1 - i2 >= 0)
                    {
                        if (pole[i1, j1 - i2] == 2)
                        {
                            m11++;
                        }
                        if (pole[i1, j1 - i2] == 1)
                        {
                            m21++;
                        }
                        if ((m11 >= 3) || (m21 >= 3))
                        {
                            if (m11 == 3) sum6c = 1000;
                            if (m21 == 3)
                            {
                                sum6 = 1500;
                                if (pole[i1, j1 - 1] == 2)
                                {
                                    sum6 = 0;
                                    sum6c = 10;
                                }
                            }
                            if ((m21 == 3) && (m11 == 0))
                            {
                                if (pole[i1, j1 - 1] == 1)
                                {
                                    sum6 = 4000;
                                }
                                if (pole[i1, j1 - 1] == 0)
                                {
                                    sum6 = 2000;
                                }
                            }
                            if ((m11 == 3) && (m21 == 0))
                            {
                                if (pole[i1, j1 - 1] == 2)
                                {
                                    sum6c = 3500;
                                }
                                if (pole[i1, j1 - 1] == 0)
                                {
                                    sum6c = 1500;
                                }
                            }
                            if (m11 == 4) sum6c = 10000;
                            if (m21 == 4) sum6 = 7000;
                        }
                        else
                        {
                            sum6c = m11 * 20;
                            sum6 = m21 * 10;
                        }
                    }
                    if ((i1 + i2 < 15) && (j1 + i2 < 15))
                    {
                        if (pole[i1 + i2, j1 + i2] == 2)
                        {
                            l1++;
                        }
                        if (pole[i1 + i2, j1 + i2] == 1)
                        {
                            l2++;
                        }
                        if ((l1 >= 3) || (l2 >= 3))
                        {
                            if (l1 == 3) sum3c = 1000;
                            if (l2 == 3)
                            {
                                sum3 = 1500;
                                if (pole[i1 + 1, j1 + 1] == 2)
                                {
                                    sum3 = 0;
                                    sum3c = 10;
                                }
                            }
                            if ((l2 == 3) && (l1 == 0))
                            {
                                if (pole[i1 + 1, j1 + 1] == 1)
                                {
                                    sum3 = 4000;
                                }
                                if (pole[i1 + 1, j1 + 1] == 0)
                                {
                                    sum3 = 2000;
                                }
                            }
                            if ((l1 == 3) && (l2 == 0))
                            {
                                if (pole[i1 + 1, j1 + 1] == 2)
                                {
                                    sum3c = 3500;
                                }
                                if (pole[i1 + 1, j1 + 1] == 0)
                                {
                                    sum3c = 1500;
                                }
                            }
                            if (l1 == 4) sum3c = 10000;
                            if (l2 == 4) sum3 = 7000;
                        }
                        else
                        {
                            sum3c = l1 * 20;
                            sum3 = l2 * 10;
                        }
                    }
                    if ((i1 - i2 >= 0) && (j1 - i2 >= 0))
                    {
                        if (pole[i1 - i2, j1 - i2] == 2)
                        {
                            l11++;
                        }
                        if (pole[i1 - i2, j1 - i2] == 1)
                        {
                            l21++;
                        }
                        if ((l11 >= 3) || (l21 >= 3))
                        {
                            if (l11 == 3) sum7c = 1000;
                            if (l21 == 3)
                            {
                                sum7 = 1500;
                                if (pole[i1 - 1, j1 - 1] == 2)
                                {
                                    sum7 = 0;
                                    sum7c = 10;
                                }
                            }
                            if ((l21 == 3) && (l11 == 0))
                            {
                                if (pole[i1 - 1, j1 - 1] == 1)
                                {
                                    sum7 = 4000;
                                }
                                if (pole[i1 - 1, j1 - 1] == 0)
                                {
                                    sum7 = 2000;
                                }
                            }
                            if ((l11 == 3) && (l21 == 0))
                            {
                                if (pole[i1 - 1, j1 - 1] == 2)
                                {
                                    sum7c = 3500;
                                }
                                if (pole[i1 - 1, j1 - 1] == 0)
                                {
                                    sum7c = 1500;
                                }
                            }
                            if (l11 == 4) sum7c = 10000;
                            if (l21 == 4) sum7 = 7000;
                        }
                        else
                        {
                            sum7c = l11 * 20;
                            sum7 = l21 * 10;
                        }
                    }
                    if ((i1 + i2 < 15) && (j1 - i2 >= 0))
                    {
                        if (pole[i1 + i2, j1 - i2] == 2)
                        {
                            s1++;
                        }
                        if (pole[i1 + i2, j1 - i2] == 1)
                        {
                            s2++;
                        }
                        if ((s1 >= 3) || (s2 >= 3))
                        {
                            if (s1 == 3) sum4c = 1000;
                            if (s2 == 3)
                            {
                                sum4 = 1500;
                                if (pole[i1 + 1, j1 - 1] == 2)
                                {
                                    sum4 = 0;
                                    sum4c = 10;
                                }
                            }
                            if ((s2 == 3) && (s1 == 0))
                            {
                                if (pole[i1 + 1, j1 - 1] == 1)
                                {
                                    sum4 = 4000;
                                }
                                if (pole[i1 + 1, j1 - 1] == 0)
                                {
                                    sum4 = 2000;
                                }
                            }
                            if ((s1 == 3) && (s2 == 0))
                            {
                                if (pole[i1 + 1, j1 - 1] == 2)
                                {
                                    sum4c = 3500;
                                }
                                if (pole[i1 + 1, j1 - 1] == 0)
                                {
                                    sum4c = 1500;
                                }
                            }
                            if (s1 == 4) sum4c = 10000;
                            if (s2 == 4) sum4 = 7000;
                        }
                        else
                        {
                            sum4c = s1 * 20;
                            sum4 = s2 * 10;
                        }
                    }
                    if ((i1 - i2 >= 0) && (j1 + i2 < 15))
                    {
                        if (pole[i1 - i2, j1 + i2] == 2)
                        {
                            s11++;
                        }
                        if (pole[i1 - i2, j1 + i2] == 1)
                        {
                            s21++;
                        }
                        if ((s11 >= 3) || (s21 >= 3))
                        {
                            if (s11 == 3) sum8c = 1000;
                            if (s21 == 3)
                            {
                                sum8 = 1500;
                                if (pole[i1 - 1, j1 + 1] == 2)
                                {
                                    sum8 = 0;
                                    sum8c = 10;
                                }
                            }
                            if ((s21 == 3) && (s11 == 0))
                            {
                                if (pole[i1 - 1, j1 + 1] == 1)
                                {
                                    sum8 = 4000;
                                }
                                if (pole[i1 - 1, j1 + 1] == 0)
                                {
                                    sum8 = 2000;
                                }
                            }
                            if ((s11 == 3) && (s21 == 0))
                            {
                                if (pole[i1 - 1, j1 + 1] == 2)
                                {
                                    sum8c = 3500;
                                }
                                if (pole[i1 - 1, j1 + 1] == 0)
                                {
                                    sum8c = 1500;
                                }
                            }
                            if (s11 == 4) sum8c = 10000;
                            if (s21 == 4) sum8 = 7000;
                        }
                        else
                        {
                            sum8c = s11 * 20;
                            sum8 = s21 * 10;
                        }
                    }
                }
                if ((i1 - 2 >= 0) && (i1 + 2 < 15))
                {
                    if ((pole[i1 - 1, j1] == 1) && (pole[i1 + 1, j1] == 1) && (pole[i1 + 2, j1] == 1) && (pole[i1 - 2, j1] == 1))
                    {
                        sum5c = 6000;
                    }
                    if ((pole[i1 - 1, j1] == 2) && (pole[i1 + 1, j1] == 2) && (pole[i1 - 2, j1] == 2) && (pole[i1 + 2, j1] == 2))
                    {
                        sum5 = 4500;
                    }
                }
                if ((j1 - 2 >= 0) && (j1 + 2 < 15))
                {
                    if ((pole[i1, j1 - 1] == 1) && (pole[i1, j1 + 1] == 1) && (pole[i1, j1 + 2] == 1) && (pole[i1, j1 - 2] == 1))
                    {
                        sum6c = 6000;
                    }
                    if ((pole[i1, j1 - 1] == 2) && (pole[i1, j1 + 1] == 2) && (pole[i1, j1 + 2] == 2) && (pole[i1, j1 - 2] == 2))
                    {
                        sum6 = 4500;
                    }
                }
                if ((i1 - 2 >= 0) && (i1 + 2 < 15) && (j1 - 2 >= 0) && (j1 + 2 < 15))
                {
                    if ((pole[i1 - 1, j1 + 1] == 1) && (pole[i1 + 1, j1 - 1] == 1) && (pole[i1 + 2, j1 - 2] == 1) && (pole[i1 - 2, j1 + 2] == 1))
                    {
                        sum8c = 6000;
                    }
                    if ((pole[i1 - 1, j1 + 1] == 2) && (pole[i1 + 1, j1 - 1] == 2) && (pole[i1 + 2, j1 - 2] == 2) && (pole[i1 - 2, j1 + 2] == 2))
                    {
                        sum8 = 4500;
                    }
                    if ((pole[i1 - 1, j1 - 1] == 2) && (pole[i1 + 1, j1 + 1] == 2) && (pole[i1 + 2, j1 + 2] == 2) && (pole[i1 - 2, j1 - 2] == 2))
                    {
                        sum7 = 4500;
                    }
                    if ((pole[i1 - 1, j1 - 1] == 1) && (pole[i1 + 1, j1 + 1] == 1) && (pole[i1 + 2, j1 + 2] == 1) && (pole[i1 - 2, j1 - 2] == 1))
                    {
                        sum7c = 6000;
                    }
                }
                if ((i1 - 1 >= 0) && (i1 + 3 < 15))
                {
                    if ((pole[i1 - 1, j1] == 1) && (pole[i1 + 1, j1] == 1) && (pole[i1 + 2, j1] == 1) && (pole[i1 + 3, j1] == 1))
                    {
                        sum5c = 6000;
                    }
                    if ((pole[i1 - 1, j1] == 2) && (pole[i1 + 1, j1] == 2) && (pole[i1 + 2, j1] == 2) && (pole[i1 + 3, j1] == 2))
                    {
                        sum5 = 4500;
                    }
                }
                if ((j1 - 1 >= 0) && (j1 + 3 < 15))
                {
                    if ((pole[i1, j1 - 1] == 1) && (pole[i1, j1 + 1] == 1) && (pole[i1, j1 + 2] == 1) && (pole[i1, j1 + 3] == 1))
                    {
                        sum6c = 6000;
                    }
                    if ((pole[i1, j1 - 1] == 2) && (pole[i1, j1 + 1] == 2) && (pole[i1, j1 + 2] == 2) && (pole[i1, j1 + 3] == 2))
                    {
                        sum6 = 4500;
                    }
                }
                if (((i1 - 1 >= 0) && (i1 + 3 < 15) && (j1 - 1 >= 0) && (j1 + 3 < 15)))
                {
                    if ((pole[i1 - 1, j1 - 1] == 2) && (pole[i1 + 1, j1 + 1] == 2) && (pole[i1 + 2, j1 + 2] == 2) && (pole[i1 + 3, j1 + 3] == 2))
                    {
                        sum7 = 4500;
                    }
                    if ((pole[i1 - 1, j1 - 1] == 1) && (pole[i1 + 1, j1 + 1] == 1) && (pole[i1 + 2, j1 + 2] == 1) && (pole[i1 + 3, j1 + 3] == 1))
                    {
                        sum7c = 6000;
                    }
                }
                if ((i1 - 3 >= 0) && (i1 + 1 < 15))
                {
                    if ((pole[i1 - 3, j1] == 1) && (pole[i1 - 2, j1] == 1) && (pole[i1 - 1, j1] == 1) && (pole[i1 + 1, j1] == 1))
                    {
                        sum5c = 6000;
                    }
                    if ((pole[i1 - 3, j1] == 2) && (pole[i1 - 2, j1] == 2) && (pole[i1 - 1, j1] == 2) && (pole[i1 + 1, j1] == 2))
                    {
                        sum5 = 4500;
                    }
                }
                if ((j1 - 3 >= 0) && (j1 + 1 < 15))
                {
                    if ((pole[i1, j1 - 3] == 1) && (pole[i1, j1 - 2] == 1) && (pole[i1, j1 - 1] == 1) && (pole[i1, j1 + 1] == 1))
                    {
                        sum6c = 6000;
                    }
                    if ((pole[i1, j1 - 3] == 2) && (pole[i1, j1 - 2] == 2) && (pole[i1, j1 - 1] == 2) && (pole[i1, j1 + 1] == 2))
                    {
                        sum6 = 4500;
                    }
                }
                if ((i1 - 3 >= 0) && (i1 + 1 < 15) && (j1 - 3 >= 0) && (j1 + 1 < 15))
                {
                    if ((pole[i1 - 3, j1 - 3] == 2) && (pole[i1 - 2, j1 - 2] == 2) && (pole[i1 - 1, j1 - 1] == 2) && (pole[i1 + 1, j1 + 1] == 2))
                    {
                        sum7 = 4500;
                    }
                    if ((pole[i1 - 3, j1 - 3] == 1) && (pole[i1 - 2, j1 - 2] == 1) && (pole[i1 - 1, j1 - 1] == 1) && (pole[i1 + 1, j1 + 1] == 1))
                    {
                        sum7c = 6000;
                    }
                }
                if (((i1 - 1 >= 0) && (i1 + 3 < 15) && (j1 - 3 >= 0) && (j1 + 1 < 15)))
                {
                    if ((pole[i1 - 1, j1 + 1] == 1) && (pole[i1 + 1, j1 - 1] == 1) && (pole[i1 + 2, j1 - 2] == 1) && (pole[i1 + 3, j1 - 3] == 1))
                    {
                        sum8c = 6000;
                    }
                    if ((pole[i1 - 1, j1 + 1] == 2) && (pole[i1 + 1, j1 - 1] == 2) && (pole[i1 + 2, j1 - 2] == 2) && (pole[i1 + 3, j1 - 3] == 2))
                    {
                        sum8 = 4500;
                    }
                }
                if (((i1 - 3 >= 0) && (i1 + 1 < 15) && (j1 - 1 >= 0) && (j1 + 3 < 15)))
                {
                    if ((pole[i1 - 1, j1 + 1] == 1) && (pole[i1 - 2, j1 + 2] == 1) && (pole[i1 - 3, j1 + 3] == 1) && (pole[i1 + 1, j1 - 1] == 1))
                    {
                        sum8c = 6000;
                    }
                    if ((pole[i1 - 1, j1 + 1] == 2) && (pole[i1 - 2, j1 + 2] == 2) && (pole[i1 - 3, j1 + 3] == 2) && (pole[i1 + 1, j1 - 1] == 2))
                    {
                        sum8 = 4500;
                    }
                }
                if ((i1 - 1 >= 0) && (i1 + 2 < 15))
                {
                    if ((pole[i1 - 1, j1] == 1) && (pole[i1 + 1, j1] == 1) && (pole[i1 + 2, j1] == 1) && ((k1 == 0) && (k11 == 0)))
                    {
                        sum5c = 4000;
                    }
                    if ((pole[i1 - 1, j1] == 2) && (pole[i1 + 1, j1] == 2) && (pole[i1 + 2, j1] == 2) && ((k2 == 0) && (k21 == 0)))
                    {
                        sum5 = 3000;
                    }
                }
                if ((i1 - 2 >= 0) && (i1 + 1 < 15))
                {
                    if ((pole[i1 - 2, j1] == 1) && (pole[i1 - 1, j1] == 1) && (pole[i1 + 1, j1] == 1) && ((k1 == 0) && (k11 == 0)))
                    {
                        sum5c = 4000;
                    }
                    if ((pole[i1 - 2, j1] == 2) && (pole[i1 - 1, j1] == 2) && (pole[i1 + 1, j1] == 2) && ((k2 == 0) && (k21 == 0)))
                    {
                        sum5 = 3000;
                    }
                }
                if ((j1 - 1 >= 0) && (j1 + 2 < 15))
                {
                    if ((pole[i1, j1 - 1] == 1) && (pole[i1, j1 + 1] == 1) && (pole[i1, j1 + 2] == 1) && ((m1 == 0) && (m11 == 0)))
                    {
                        sum6c = 4000;
                    }
                    if ((pole[i1, j1 - 1] == 2) && (pole[i1, j1 + 1] == 2) && (pole[i1, j1 + 2] == 2) && ((m2 == 0) && (m21 == 0)))
                    {
                        sum6 = 3000;
                    }
                }
                if ((j1 - 2 >= 0) && (j1 + 1 < 15))
                {
                    if ((pole[i1, j1 - 2] == 1) && (pole[i1, j1 - 1] == 1) && (pole[i1, j1 + 1] == 1) && ((m1 == 0) && (m11 == 0)))
                    {
                        sum6c = 4000;
                    }
                    if ((pole[i1, j1 - 2] == 2) && (pole[i1, j1 - 1] == 2) && (pole[i1, j1 + 1] == 2) && ((m2 == 0) && (m21 == 0)))
                    {
                        sum6 = 3000;
                    }
                }
                if ((i1 - 2 >= 0) && (i1 + 1 < 15) && (j1 - 2 >= 0) && (j1 + 1 < 15))
                {
                    if ((pole[i1 - 2, j1 - 2] == 2) && (pole[i1 - 1, j1 - 1] == 2) && (pole[i1 + 1, j1 + 1] == 2) && ((l2 == 0) && (l21 == 0)))
                    {
                        sum7 = 3000;
                    }
                    if ((pole[i1 - 2, j1 - 2] == 1) && (pole[i1 - 1, j1 - 1] == 1) && (pole[i1 + 1, j1 + 1] == 1) && ((l1 == 0) && (l11 == 0)))
                    {
                        sum7c = 4000;
                    }
                }
                if ((i1 - 1 >= 0) && (i1 + 2 < 15) && (j1 - 1 >= 0) && (j1 + 2 < 15))
                {
                    if ((pole[i1 - 1, j1 - 1] == 2) && (pole[i1 + 1, j1 + 1] == 2) && (pole[i1 + 2, j1 + 2] == 2) && ((l2 == 0) && (l21 == 0)))
                    {
                        sum7 = 3000;
                    }
                    if ((pole[i1 - 1, j1 - 1] == 1) && (pole[i1 + 1, j1 + 1] == 1) && (pole[i1 + 2, j1 + 2] == 1) && ((l1 == 0) && (l11 == 0)))
                    {
                        sum7c = 4000;
                    }
                }
                if (((i1 - 1 >= 0) && (i1 + 2 < 15) && (j1 - 2 >= 0) && (j1 + 1 < 15)))
                {
                    if ((pole[i1 - 1, j1 + 1] == 1) && (pole[i1 + 1, j1 - 1] == 1) && (pole[i1 + 2, j1 - 2] == 1) && ((s1 == 0) && (s11 == 0)))
                    {
                        sum8c = 4000;
                    }
                    if ((pole[i1 - 1, j1 + 1] == 2) && (pole[i1 + 1, j1 - 1] == 2) && (pole[i1 + 2, j1 - 2] == 2) && ((s2 == 0) && (s21 == 0)))
                    {
                        sum8 = 3000;
                    }
                }
                if (((i1 - 2 >= 0) && (i1 + 1 < 15) && (j1 - 1 >= 0) && (j1 + 2 < 15)))
                {
                    if ((pole[i1 - 1, j1 + 1] == 1) && (pole[i1 - 2, j1 + 2] == 1) && (pole[i1 + 1, j1 - 1] == 1) && ((s1 == 0) && (s11 == 0)))
                    {
                        sum8c = 4000;
                    }
                    if ((pole[i1 - 1, j1 + 1] == 2) && (pole[i1 - 2, j1 + 2] == 2) && (pole[i1 + 1, j1 - 1] == 2) && ((s2 == 0) && (s21 == 0)))
                    {
                        sum8 = 3000;
                    }
                }
                //Назначение веса.Вес для клетки равен сумме кол-ва клеток в 8-ми направлениях
                massivsum[i1, j1] = sum1 + sum2 + sum3 + sum4 + sum5 + sum6 + sum7 + sum8;
                massivsumc[i1, j1] = sum1c + sum2c + sum3c + sum4c + sum5c + sum6c + sum7c + sum8c;
            }
        }
        //Дописать проверку на выигрыш и ничью 
        bool winer = false;
        void win()
        {
            for (int i1 = 0; i1 < 15; i1++)
            {
                for (int j1 = 0; j1 < 15; j1++)
                {
                    int k1 = 0;
                    int k2 = 0;
                    int m1 = 0;
                    int m2 = 0;
                    int l1 = 0;
                    int l2 = 0;
                    int s1 = 0;
                    int s2 = 0;
                    int k11 = 0;
                    int k21 = 0;
                    int m11 = 0;
                    int m21 = 0;
                    int l11 = 0;
                    int l21 = 0;
                    int s11 = 0;
                    int s21 = 0;
                    int i2;
                    for (i2 = 1; i2 < 6; i2++)
                    {
                        if (i1 + i2 < 15)
                        {
                            if (pole[i1 + i2, j1] == 2)
                            {
                                k1++;
                            }
                            if (pole[i1 + i2, j1] == 1)
                            {
                                k2++;
                            }
                            if (k1 == 5)
                            {
                                MessageBox.Show("ПК выиграл");
                                winer = true;
                                break;
                            }
                            if (k2 == 5)
                            {
                                MessageBox.Show("ИГРОК выиграл");
                                winer = true;
                                break;
                            }
                        }
                    }
                    for (i2 = 1; i2 < 6; i2++)
                    {
                        if (i1 - i2 >= 0)
                        {
                            if (pole[i1 - i2, j1] == 2)
                            {
                                k11++;
                            }
                            if (pole[i1 - i2, j1] == 1)
                            {
                                k21++;
                            }
                            if (k11 == 5)
                            {
                                MessageBox.Show("ПК выиграл");
                                winer = true;
                                break;
                            }
                            if (k21 == 5)
                            {
                                MessageBox.Show("ИГРОК выиграл");
                                winer = true;
                                break;
                            }
                        }
                    }
                    for (i2 = 1; i2 < 6; i2++)
                    {
                        if (j1 + i2 < 15)
                        {
                            if (pole[i1, j1 + i2] == 2)
                            {
                                m1++;
                            }
                            if (pole[i1, j1 + i2] == 1)
                            {
                                m2++;
                            }
                            if (m1 == 5)
                            {
                                MessageBox.Show("ПК выиграл");
                                winer = true;
                                break;
                            }
                            if (m2 == 5)
                            {
                                MessageBox.Show("ИГРОК выиграл");
                                winer = true;
                                break;
                            }
                        }
                    }
                    for (i2 = 1; i2 < 6; i2++)
                    {
                        if (j1 - i2 >= 0)
                        {
                            if (pole[i1, j1 - i2] == 2)
                            {
                                m11++;
                            }
                            if (pole[i1, j1 - i2] == 1)
                            {
                                m21++;
                            }
                            if (m11 == 5)
                            {
                                MessageBox.Show("ПК выиграл");
                                winer = true;
                                break;
                            }
                            if (m21 == 5)
                            {
                                MessageBox.Show("ИГРОК выиграл");
                                winer = true;
                                break;
                            }
                        }
                    }
                    for (i2 = 1; i2 < 6; i2++)
                    {
                        if ((i1 + i2 < 15) && (j1 + i2 < 15))
                        {
                            if (pole[i1 + i2, j1 + i2] == 2)
                            {
                                l1++;
                            }
                            if (pole[i1 + i2, j1 + i2] == 1)
                            {
                                l2++;
                            }

                            if (l1 == 5)
                            {
                                MessageBox.Show("ПК выиграл");
                                winer = true;
                                break;
                            }
                            if (l2 == 5)
                            {
                                MessageBox.Show("ИГРОК выиграл");
                                winer = true;
                                break;
                            }
                        }
                    }
                    for (i2 = 1; i2 < 6; i2++)
                    {
                        if ((i1 - i2 >= 0) && (j1 - i2 >= 0))
                        {
                            if (pole[i1 - i2, j1 - i2] == 2)
                            {
                                l11++;
                            }
                            if (pole[i1 - i2, j1 - i2] == 1)
                            {
                                l21++;
                            }
                            if (l11 == 5)
                            {
                                MessageBox.Show("ПК выиграл");
                                winer = true;
                                break;
                            }
                            if (l21 == 5)
                            {
                                MessageBox.Show("ИГРОК выиграл");
                                winer = true;
                                break;
                            }
                        }
                    }
                    for (i2 = 1; i2 < 6; i2++)
                    {
                        if ((i1 + i2 < 15) && (j1 - i2 >= 0))
                        {
                            if (pole[i1 + i2, j1 - i2] == 2)
                            {
                                s1++;
                            }
                            if (pole[i1 + i2, j1 - i2] == 1)
                            {
                                s2++;
                            }
                            if (s1 == 5)
                            {
                                MessageBox.Show("ПК выиграл");
                                winer = true;
                                break;
                            }
                            if (s2 == 5)
                            {
                                MessageBox.Show("ИГРОК выиграл");
                                winer = true;
                                break;
                            }
                        }
                    }
                    for (i2 = 1; i2 < 6; i2++)
                    {
                        if ((i1 - i2 >= 0) && (j1 + i2 < 15))
                        {
                            if (pole[i1 - i2, j1 + i2] == 2)
                            {
                                s11++;
                            }
                            if (pole[i1 - i2, j1 + i2] == 1)
                            {
                                s21++;
                            }
                            if (s11 == 5)
                            {
                                MessageBox.Show("ПК выиграл");
                                winer = true;
                                break;
                            }
                            if (s21 == 5)
                            {
                                MessageBox.Show("ИГРОК выиграл");
                                winer = true;
                                break;
                            }
                        }
                    }
                }
            }
        }
        private void новаяИграToolStripMenuItem_Click(object sender, EventArgs e)
        {
            winer = false;
            Form2 f = new Form2();
               this.Hide();
            f.ShowDialog();
             
             for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    pole[i, j] = 0;
                }
            }
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    ocenka[i, j] = 0;
                }
            }
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    ozenkac[i, j] = 0;
                }
            }
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    massivsum[i, j] = 0;
                }
            }
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    massivsumc[i, j] = 0;
                }
            }
        }
        private void справкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Цель игры-построить ряд из 5 крестиков либо по горизонтали, либо по вертикали, либо по диагонали");
        }
        private void Form1_Activated(object sender, EventArgs e)
        {
            
            pictureBox1.BackColor = Color.White;
            for (int i = 0; i < 16; i++)
            {
                pole1.DrawLine(Pens.Black, 0, 0 + i * 22, 330, 0 + i * 22);
                pole1.DrawLine(Pens.Black, 0 + i * 22, 0, 0 + i * 22, 330);
            }
            if (Class1.pc == true)
            {
                if (Class1.znak)
                {
                    pole[7, 7] = 2;
                    pole1.DrawEllipse(Pens.Red, 7 * 22, 7 * 22, 22, 22);
                    hod = false;
                }
                else
                {
                    pole[7, 7] = 2;
                    pole1.FillEllipse(Brushes.Red, 7 * 22, 7 * 22, 22, 22);
                    hod = false;
                }
            }
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();
            timer1.Interval = 1000;
            pole1 = pictureBox1.CreateGraphics();
            
            for (int i = 0; i < 16; i++)
            {
                pole1.DrawLine(Pens.Black, 0, 0 + i * 22, 330, 0 + i * 22);
                pole1.DrawLine(Pens.Black, 0 + i * 22, 0, 0 + i * 22, 330);
            }
            if (Class1.pc == true)
            {
                if (Class1.znak) {
                    pole[7, 7] = 2;
                pole1.DrawEllipse(Pens.Red, 7 * 22, 7 * 22, 22, 22);
                hod = false; }
                else
                {
                    pole[7, 7] = 2;
                    pole1.FillEllipse(Brushes.Red,7*22,7*22,22,22);
                    hod = false;
                }
                
            } 
            pictureBox1.BackColor = Color.White;
           
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Class1.time++;
        }

       
   

        
    }
}
