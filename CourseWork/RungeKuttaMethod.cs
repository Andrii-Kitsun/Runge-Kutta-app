using System;

namespace CourseWork
{
    class RungeKuttaMethod : CourseWork
    {
        MathParser math = new MathParser();

        //Масив для коефіцієнтів і прирощення по змінній
        double[,] coeff = new double[amountSections + 1, 5 * iter];
        //Масив для усіх результуючих змінних
        double[,] solve = new double[amountSections + 1, iter + 1];

        double xi = leftBorder;

        public RungeKuttaMethod()
        {
            //Цикл для заповнення перших значень розв'язку
            for (int i = 0; i < iter; i++)
            {
                solve[0 , i + 1] = (double.Parse(ConditionTBox[i].Text));
            }


            //Цикл по всіх частинах відрізка
            for (int i = 0; i < amountSections + 1; i++, xi += step)
            {
                solve[i, 0] = xi;

                FirstCoeff(i);
                SecondCoeff(i);
                ThirdCoeff(i);
                FourthCoeff(i);

                DeltaVar(i);
            }
        }

        //Метод для знаходження k1 по всім рівнянням
        private void FirstCoeff(int i)
        {
            math.setVariable("x", xi);
            for (int k = 0; k < iter; k++)
                math.setVariable(letters[k].ToString(), solve[i, k + 1]);

            for (int k = 0; k < iter; k++)
                coeff[i, 5 * k] = step * math.Parse(EquationTBox[k].Text);
        }

        //Метод для знаходження k2 по всім рівнянням
        private void SecondCoeff(int i)
        {
            math.setVariable("x", xi + step / 2);
            for (int k = 0; k < iter; k++)
                math.setVariable(letters[k].ToString(), (solve[i, k + 1] + coeff[i, 5 * k] / 2));

            for (int k = 0; k < iter; k++)
                coeff[i, 5 * k + 1] = step * math.Parse(EquationTBox[k].Text);
        }

        //Метод для знаходження k3 по всім рівнянням
        private void ThirdCoeff(int i)
        {
            math.setVariable("x", xi + step / 2);
            for (int k = 0; k < iter; k++)
                math.setVariable(letters[k].ToString(), (solve[i, k + 1] + coeff[i, 5 * k + 1] / 2));

            for (int k = 0; k < iter; k++)
                coeff[i, 5 * k + 2] = step * math.Parse(EquationTBox[k].Text);
        }

        //Метод для знаходження k4 по всім рівнянням
        private void FourthCoeff(int i)
        {
            math.setVariable("x", xi + step);
            for (int k = 0; k < iter; k++)
                math.setVariable(letters[k].ToString(), (solve[i, k + 1] + coeff[i, 5 * k + 2]));

            for (int k = 0; k < iter; k++)
                coeff[i, 5 * k + 3] = step * math.Parse(EquationTBox[k].Text);
        }

        //Метод для знаходження наближень змінних
        private void DeltaVar(int i)
        {     
            for (int k = 0; k < iter; k++)
            {
                coeff[i, 5 * k + 4] = ((coeff[i, 5 * k] + 2 * coeff[i, 5 * k + 1] + 2 * coeff[i, 5 * k + 2] + coeff[i, 5 * k + 3]) / 6);
                if (i != amountSections)
                    solve[i + 1, k + 1] = (solve[i, k + 1] + coeff[i, 5 * k + 4]);
                else
                    continue;
            }
        }

        public double[,] GetSolve()
        {
            return solve;
        }

        public double[,] GetCoeffs()
        {
            return coeff;
        }
    }        
}
