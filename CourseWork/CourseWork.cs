using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static System.Math;

namespace CourseWork
{
    public partial class CourseWork : Form
    {
        public static List<TextBox> EquationTBox = new List<TextBox>();
        public static List<TextBox> ConditionTBox = new List<TextBox>();
        List<Label> ConditionLabel = new List<Label>();

        bool saveFlag = false;
        int roundCount = 4;
        public static int iter = 0, amountSections = 0;
        public static double leftBorder = 0, step = 0;
        double rightBorder = 0;
        public static string letters = "yzcdefghijklmnopqrstuvwx";
        string letters1 = "klmnopqrstuvwyzcdefghij";

        public CourseWork()
        {
            InitializeComponent();
        }

        private void CourseWork_Load(object sender, EventArgs e)
        {
            AboutProgram About = new AboutProgram();
            About.ShowDialog();
        }

        //Додавання полів для введення
        private void AddTextBox_Click(object sender, EventArgs e)
        {
            //Динамічне створення полів TextBox, Label для введення
            try
            {
                if (iter == 24)
                    throw new Exception();

                ConditionTBox.Add(new TextBox());
                ConditionTBox[iter].Location = new Point(82, 25 + iter * 32);
                ConditionTBox[iter].Name = "Condition" + iter.ToString();
                ConditionTBox[iter].Size = new Size(65, 26);
                ConditionTBox[iter].TabIndex = iter;
                ConditionTBox[iter].KeyPress += InputData_KeyPress;
                ConditionTBox[iter].MaxLength = 6;
                ConditionPanel.Controls.Add(ConditionTBox[iter]);

                ConditionLabel.Add(new Label());
                ConditionLabel[iter].Location = new Point(6, 28 + iter * 32);
                ConditionLabel[iter].Name = "Label" + iter.ToString();
                ConditionLabel[iter].Text = letters[iter].ToString() + '(' + LeftBorder.Text + ") =";
                ConditionLabel[iter].Size = new Size(100, 19);
                ConditionLabel[iter].TabIndex = iter;
                ConditionPanel.Controls.Add(ConditionLabel[iter]);

                EquationTBox.Add(new TextBox());
                EquationTBox[iter].Location = new Point(6, 25 + iter * 32);
                EquationTBox[iter].Name = "Equation" + iter.ToString();
                EquationTBox[iter].Size = new Size(EquationPanel.Width - 15, 26);
                EquationTBox[iter].TabIndex = iter;
                EquationTBox[iter].Anchor = (AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top);
                EquationTBox[iter].KeyPress += TextBox_KeyPress;
                EquationPanel.Controls.Add(EquationTBox[iter]);

                if (iter > 5)
                    Height += 32;
                iter++;
            }
            catch (Exception)
            {
                MessageBox.Show("Забагато рівнянь. Програма не може містити більше 24 рівнянь", 
                    "Помилка в роботі програми", MessageBoxButtons.OK, MessageBoxIcon.Error);
                iter--;
                return;
            }    
        }

        //Видалення полів для введення
        private void DeleteTextBox_Click(object sender, EventArgs e)
        {
            //Динамічне видалення створеного поля TextBox
            try
            {
                iter--;
                EquationPanel.Controls.Remove(EquationTBox[iter]);
                ConditionPanel.Controls.Remove(ConditionTBox[iter]);
                ConditionPanel.Controls.Remove(ConditionLabel[iter]);

                EquationTBox.RemoveAt(iter);
                ConditionTBox.RemoveAt(iter);
                ConditionLabel.RemoveAt(iter);

                Height -= 32;
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Ви не можете видалити обєкт, який ще не створений", 
                    "Помилка в роботі програми", MessageBoxButtons.OK, MessageBoxIcon.Error);

                iter++;
                return;
            }
            
        }

        //Перевірка введеного тексту
        private void InputData_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8 && e.KeyChar != 44 && e.KeyChar != 45)
                e.Handled = true;
        }

        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char n = e.KeyChar;
            if (!char.IsLetterOrDigit(n) && n != 47 && n != 8 && n != 61 && n != 94 && !(n >= 40 && n <= 45))
                e.Handled = true;
        }

        //Очистити всі поля для введення
        private void ClearAll_Click(object sender, EventArgs e)
        {
            EquationPanel.Controls.Clear();
            ConditionPanel.Controls.Clear();
            ConditionPanel.Controls.Clear();

            EquationTBox.Clear();
            ConditionTBox.Clear();
            ConditionLabel.Clear();
            iter = 0;

            Width = MinimumSize.Width;
            Height = MinimumSize.Height;

            ResultsGrid.DataSource = new object();
            CoeffsGrid.DataSource = new object();

            for (int i = 0; i < Chart.Series.Count;)
                Chart.Series.RemoveAt(i);
        }

        //Налаштування програми
        private void Settings_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings(MenuStrip, ToolStrip, StatusStrip, roundCount, saveFlag);
            settings.ShowDialog();
            roundCount = settings.precisionCount;
            saveFlag = settings.saveFlag;
        }

        //Довідкове меню
        private void AboutProgram_Click(object sender, EventArgs e)
        {
            AboutProgram About = new AboutProgram();
            About.ShowDialog();
        }

        //Виклик методу обрахунку диф. рівнянь за методом Рунге-Кутта
        private void RungeKuttaMethod_Click(object sender, EventArgs e)
        {
            ResultsGrid.DataSource = new object();
            CoeffsGrid.DataSource = new object();

            try
            {
                leftBorder = Double.Parse(LeftBorder.Text);
                rightBorder = Double.Parse(RightBorder.Text);
                amountSections = Int32.Parse(AmountSections.Text);

                step = (rightBorder - leftBorder) / amountSections;
                StepBreak.Text = step.ToString();


                //Перехват помилки якщо немає рівнянь
                if (iter == 0)
                    throw new Exception();

                RungeKuttaMethod rkm = new RungeKuttaMethod();

                BuildTables(rkm.GetSolve(), rkm.GetCoeffs());
                BuildChart(rkm.GetSolve());
                AutoSave(saveFlag);
            }
            catch (Exception)
            {
                MessageBox.Show("Помилка! Немає даних для обрахунку", "Помилка в ході роботи",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            
        }

        //Вийти з програми
        private void Exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        //Побудова інтегральних кривих
        private void BuildChart(double[,] solve)
        {
            //Видалення решти графіків щоб запобігти помилок
            for (int i = 0; i < Chart.Series.Count;)
                Chart.Series.RemoveAt(i);

            //Дані для графіків
            Random r = new Random();
            double[] Xarr = new double[amountSections + 1];
            double[][] Yarr = new double[iter][];

            //Розбивка solve на підмасивив
            for (int i = 0; i < iter; i++)
                Yarr[i] = new double[amountSections + 1];

            for (int i = 0; i < amountSections + 1; i++)
            {
                Xarr[i] = solve[i, 0];

                for (int j = 1; j < iter + 1; j++)
                    Yarr[j - 1][i] = solve[i, j];
            }

            //Налаштування графіку
            Chart.ChartAreas[0].AxisX.Title = "Xi";
            Chart.ChartAreas[0].AxisX.TitleFont = new Font("Cambria", 14, FontStyle.Bold);
            Chart.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.LightGray;
            Chart.ChartAreas[0].AxisY.Title = "Значення розв'язків";
            Chart.ChartAreas[0].AxisY.TitleFont = new Font("Cambria", 14, FontStyle.Bold);
            Chart.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.LightGray;


            //Створення безпосередньо графіків
            for (int i = 0; i < iter; i++)
            {
                Chart.Series.Add(letters[i].ToString() + 'i');
                Chart.Series[i].Points.DataBindXY(Xarr, Yarr[i]);
                Chart.Series[i].ChartType = SeriesChartType.Line;
                Chart.Series[i].LegendText = letters[i].ToString() + 'i';
                Chart.Series[i].Color = Color.FromArgb(r.Next(0, 255), r.Next(0, 255), r.Next(0, 255));
                Chart.Series[i].ToolTip = "X = #VALX, Y = #VALY";
                Chart.Series[i].MarkerSize = 7;
                Chart.Series[i].MarkerStyle = MarkerStyle.Circle;
            }
        }

        //Побудова таблиць для виведення результатів
        private void BuildTables(double[,] solve, double[,] coeffs)
        {
            DataTable resultTable = new DataTable();
            DataTable coeffTable = new DataTable();
            DataColumn column;
            DataRow row;

            //Окремі стовпці для ітератора та xi
            resultTable.Columns.Add(new DataColumn("i"));
            resultTable.Columns.Add(new DataColumn("xi"));

            coeffTable.Columns.Add(new DataColumn("i"));
            coeffTable.Columns.Add(new DataColumn("xi"));

            //Створення усіх необхідних стовпців
            for (int i = 0; i < iter; i++)
            {
                column = new DataColumn(letters[i].ToString() + 'i');
                resultTable.Columns.Add(column);

                column = new DataColumn(letters1[i].ToString() + "1");
                coeffTable.Columns.Add(column);

                column = new DataColumn(letters1[i].ToString() + "2");
                coeffTable.Columns.Add(column);

                column = new DataColumn(letters1[i].ToString() + "3");
                coeffTable.Columns.Add(column);

                column = new DataColumn(letters1[i].ToString() + "4");
                coeffTable.Columns.Add(column);

                column = new DataColumn('Δ' + letters[i].ToString() + 'i');
                coeffTable.Columns.Add(column);
            }

            //Заповнення рядків
            for (int i = 0; i < amountSections + 1; i++)
            {
                //Заповнення результуючими значеннями
                row = resultTable.NewRow();
                row["i"] = i;
                row["xi"] = Round(solve[i, 0], roundCount);
                for (int j = 0; j < iter; j++)
                {
                    row[letters[j].ToString() + 'i'] = Round(solve[i, j + 1], roundCount);
                }
                resultTable.Rows.Add(row);

                //Заповення коефіцієнтів
                row = coeffTable.NewRow();
                row["i"] = i;
                row["xi"] = Round(solve[i, 0], roundCount);
                for (int j = 0; j < iter; j++)
                {
                    row[letters1[j].ToString() + "1"] = Round(coeffs[i, j], roundCount);
                    row[letters1[j].ToString() + "2"] = Round(coeffs[i, j + 1], roundCount);
                    row[letters1[j].ToString() + "3"] = Round(coeffs[i, j + 2], roundCount);
                    row[letters1[j].ToString() + "4"] = Round(coeffs[i, j + 3], roundCount);
                    row['Δ' + letters[j].ToString() + 'i'] = Round(coeffs[i, j + 4], roundCount);
                }
                coeffTable.Rows.Add(row);
            }

            ResultsGrid.DataSource = resultTable;
            CoeffsGrid.DataSource = coeffTable;

            for (int i = 0; i < iter + 2; i++)
                ResultsGrid.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            for (int i = 0; i < 5 * iter + 2; i++)
                CoeffsGrid.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        //Вікно інструкціїї по використанню програми
        private void Instruction_Click(object sender, EventArgs e)
        {
            Instruction instruction = new Instruction();
            instruction.Show();
        }

        //Зчитування даних з інших файлів
        private void OpenFileTool_Click(object sender, EventArgs e)
        {
            ClearAll_Click(null, null);
            OpenDialog.Title = "Відкрити файл";
            OpenDialog.Filter = "Файл TXT|*.txt|Файл RTF|*.rtf|Файл Excel|*.xls|Файл Word|*.doc|Всі файли|*.*";
            OpenDialog.FileName = "";

            if (OpenDialog.ShowDialog() == DialogResult.Cancel)
                return;

            FileStream fStream = new FileStream(OpenDialog.FileName, FileMode.Open);
            StreamReader streamReader = new StreamReader(fStream);

            try
            {
                string[] str = streamReader.ReadToEnd().Split('\n');
                string[] str1;

                //Видалення символу повертання каретки '\r'
                for (int i = 0; i < str.Length; i++)
                    str[i] = str[i].Replace("\r", "");

                //Ділення рядку по пробілу для зчитування парметрів відрізку
                str1 = str[1].Split(' ');
                LeftBorder.Text = str1[2];
                str1 = str[2].Split(' ');
                RightBorder.Text = str1[2];
                str1 = str[3].Split(' ');
                AmountSections.Text = str1[2];
                str1 = str[4].Split(' ');
                StepBreak.Text = str1[2];

                //Зчитування початкових умов та самих рівнянь
                for (int i = 7; !(str[i] == ""); i++)
                {
                    str1 = str[i].Split(' ');
                    AddTextBox_Click(null, null);
                    ConditionTBox[iter - 1].Text = str1[0];
                    EquationTBox[iter - 1].Text = str1[1];
                }
                RungeKuttaMethod_Click(null, null);
            }
            catch (Exception)
            {
                MessageBox.Show("Виникла помилка під час відкриванні файлу!", "Помилка в ході програми", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Запис результатів у файл
        private void SaveFileTool_Click(object sender, EventArgs e)
        {
            SaveDialog.Title = "Зберегти файл";
            SaveDialog.Filter = "Файл TXT|*.txt|Файл RTF|*.rtf|Файл Excel|*.xls|Файл Word|*.doc|Всі файли|*.*";
            SaveDialog.FileName = "";

            if (SaveDialog.ShowDialog() == DialogResult.Cancel)
                return;

            FileStream fs = new FileStream(SaveDialog.FileName, FileMode.Create);
            StreamWriter streamWriter = new StreamWriter(fs);

            try
            {
                //Записуємо початкові умови
                streamWriter.WriteLine("Вихідні параметри відрізку:");
                streamWriter.WriteLine("a = " + leftBorder);
                streamWriter.WriteLine("b = " + rightBorder);
                streamWriter.WriteLine("n = " + amountSections);
                streamWriter.WriteLine("h = " + step);
                streamWriter.WriteLine();

                //Рівняння
                streamWriter.WriteLine("Задана система:");
                for (int i = 0; i < iter; i++)
                {
                    streamWriter.WriteLine(ConditionTBox[i].Text + " " + EquationTBox[i].Text);
                }
                streamWriter.WriteLine();

                //Результати
                streamWriter.WriteLine("Таблиця розрахунку системи:");
                streamWriter.Write(string.Format("{0,15}", "i"));
                streamWriter.Write(string.Format("{0,15}", "xi"));
                for (int i = 0; i < iter; i++)
                {
                    streamWriter.Write(string.Format("{0,15}", letters[i] + "i"));
                }
                streamWriter.WriteLine();
                for (int i = 0; i < amountSections + 1; i++)
                {
                    for (int j = 0; j < iter + 2; j++)
                    {
                        streamWriter.Write(string.Format("{0,15}", ResultsGrid.Rows[i].Cells[j].Value));
                    }
                    streamWriter.WriteLine();
                }
                streamWriter.WriteLine();

                //Коефіцієнти
                streamWriter.WriteLine("Таблиця розрахунку коефіцієнтів:");
                streamWriter.Write(string.Format("{0,15}", "i"));
                streamWriter.Write(string.Format("{0,15}", "xi"));
                for (int i = 0; i < iter; i++)
                {
                    streamWriter.Write(string.Format("{0,15}", letters1[i] + "1"));
                    streamWriter.Write(string.Format("{0,15}", letters1[i] + "2"));
                    streamWriter.Write(string.Format("{0,15}", letters1[i] + "3"));
                    streamWriter.Write(string.Format("{0,15}", letters1[i] + "4"));
                    streamWriter.Write(string.Format("{0,15}", 'Δ' + letters[i].ToString() + 'i'));
                }
                streamWriter.WriteLine();
                for (int i = 0; i < amountSections + 1; i++)
                {
                    for (int j = 0; j < 5 * iter + 2; j++)
                    {
                        streamWriter.Write(string.Format("{0,15}", CoeffsGrid.Rows[i].Cells[j].Value));
                    }
                    streamWriter.WriteLine();
                }

                streamWriter.Close();
                fs.Close();

                MessageBox.Show("Файл успішно записано!", "Процес запису файлу", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Виникла помилка під час запису файлу!", "Помилка в ході програми", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AutoSave(bool flag)
        {
            if (!flag)
                return;

            FileStream fs = new FileStream("AutosaveFile.txt", FileMode.Append);
            StreamWriter streamWriter = new StreamWriter(fs);

            try
            {
                streamWriter.WriteLine("///Запис: " + DateTime.Now + "///");

                //Записуємо початкові умови
                streamWriter.WriteLine("Вихідні параметри відрізку:");
                streamWriter.WriteLine("a = " + leftBorder);
                streamWriter.WriteLine("b = " + rightBorder);
                streamWriter.WriteLine("n = " + amountSections);
                streamWriter.WriteLine("h = " + step);
                streamWriter.WriteLine();

                //Рівняння
                streamWriter.WriteLine("Задана система:");
                for (int i = 0; i < iter; i++)
                {
                    streamWriter.WriteLine(ConditionTBox[i].Text + " " + EquationTBox[i].Text);
                }
                streamWriter.WriteLine();

                //Результати
                streamWriter.WriteLine("Таблиця розрахунку системи:");
                streamWriter.Write(string.Format("{0,15}", "i"));
                streamWriter.Write(string.Format("{0,15}", "xi"));
                for (int i = 0; i < iter; i++)
                {
                    streamWriter.Write(string.Format("{0,15}", letters[i] + "i"));
                }
                streamWriter.WriteLine();
                for (int i = 0; i < amountSections + 1; i++)
                {
                    for (int j = 0; j < iter + 2; j++)
                    {
                        streamWriter.Write(string.Format("{0,15}", ResultsGrid.Rows[i].Cells[j].Value));
                    }
                    streamWriter.WriteLine();
                }
                streamWriter.WriteLine();

                //Коефіцієнти
                streamWriter.WriteLine("Таблиця розрахунку коефіцієнтів:");
                streamWriter.Write(string.Format("{0,15}", "i"));
                streamWriter.Write(string.Format("{0,15}", "xi"));
                for (int i = 0; i < iter; i++)
                {
                    streamWriter.Write(string.Format("{0,15}", letters1[i] + "1"));
                    streamWriter.Write(string.Format("{0,15}", letters1[i] + "2"));
                    streamWriter.Write(string.Format("{0,15}", letters1[i] + "3"));
                    streamWriter.Write(string.Format("{0,15}", letters1[i] + "4"));
                    streamWriter.Write(string.Format("{0,15}", 'Δ' + letters[i].ToString() + 'i'));
                }
                streamWriter.WriteLine();
                for (int i = 0; i < amountSections + 1; i++)
                {
                    for (int j = 0; j < 5 * iter + 2; j++)
                    {
                        streamWriter.Write(string.Format("{0,15}", CoeffsGrid.Rows[i].Cells[j].Value));
                    }
                    streamWriter.WriteLine();
                }

                streamWriter.WriteLine("///Файл успішно записано!///");
                streamWriter.Close();
                fs.Close();
            }
            catch (Exception)
            {
                streamWriter.WriteLine("///Помилка! Файл не записано///");
                streamWriter.Close();
                fs.Close();
            }

        }

        #region //Тестові значення для роботи з програмою
        private void Test1_Click(object sender, EventArgs e)
        {
            ClearAll_Click(null, null);
            for (int i = 0; i < 1; i++)
            {
                AddTextBox_Click(null, null);
            }

            ConditionTBox[0].Text = "0";
            EquationTBox[0].Text = "0,05*exp(-0,05*x)-0,0065*y";

            RungeKuttaMethod_Click(null, null);
        }

        private void Test2_Click(object sender, EventArgs e)
        {
            ClearAll_Click(null, null);
            for (int i = 0; i < 2; i++)
            {
                AddTextBox_Click(null, null);
            }

            ConditionTBox[0].Text = "0,8";
            EquationTBox[0].Text = "0,1*x*y+0,4*(y^2)+x";

            ConditionTBox[1].Text = "0,1";
            EquationTBox[1].Text = "0,1*x+2,8*(y^2)-2";

            RungeKuttaMethod_Click(null, null);
        }

        private void Test3_Click(object sender, EventArgs e)
        {
            ClearAll_Click(null, null);
            for (int i = 0; i < 3; i++)
            {
                AddTextBox_Click(null, null);
            }

            ConditionTBox[0].Text = "0,1";
            EquationTBox[0].Text = "0,3*x+1,2*(y^2)+0,5*z+0,1*c";

            ConditionTBox[1].Text = "0,5";
            EquationTBox[1].Text = "0,9*(x^2)+1,2*y+0,3*z+0,2*c";

            ConditionTBox[2].Text = "0,4";
            EquationTBox[2].Text = "2,3*x+0,1*(y^2)+0,2*z+0,12*c";

            RungeKuttaMethod_Click(null, null);
        }

        private void Test4_Click(object sender, EventArgs e)
        {
            ClearAll_Click(null, null);
            for (int i = 0; i < 4; i++)
            {
                AddTextBox_Click(null, null);
            }

            ConditionTBox[0].Text = "0,6";
            EquationTBox[0].Text = "sin(y)+cos(x)-tan(x)";

            ConditionTBox[1].Text = "0,75";
            EquationTBox[1].Text = "tan(z)-exp(y)*sin(x)/atan(y)";

            ConditionTBox[2].Text = "0,9";
            EquationTBox[2].Text = "sinh(y)+cos(c)+sqrt(0,0001)";

            ConditionTBox[3].Text = "0,115";
            EquationTBox[3].Text = "sin(d)+lg(0,78)*c-ln(d)";

            RungeKuttaMethod_Click(null, null);
        }

        private void Test5_Click(object sender, EventArgs e)
        {
            ClearAll_Click(null, null);
            for (int i = 0; i < 5; i++)
            {
                AddTextBox_Click(null, null);
            }

            ConditionTBox[0].Text = "1,3";
            EquationTBox[0].Text = "sin(y*x*z*c)/lg(c^z)*0,14*y+2*e";

            ConditionTBox[1].Text = "0,1";
            EquationTBox[1].Text = "atan(y-d)-cosh(0,005*x)/sqrt(4)";

            ConditionTBox[2].Text = "0,8";
            EquationTBox[2].Text = "0,1*x*y+0,4*(y^2)+exp(x)";

            ConditionTBox[3].Text = "0,3";
            EquationTBox[3].Text = "(y^2)*(z^2)+2*x*y-(z^2)+1";

            ConditionTBox[4].Text = "0,2";
            EquationTBox[4].Text = "0,1*acos(0,05)*y+0,4*(lg(d)^2)+x";

            RungeKuttaMethod_Click(null, null);
        }

        private void Test6_Click(object sender, EventArgs e)
        {
            ClearAll_Click(null, null);
            for (int i = 0; i < 5; i++)
            {
                AddTextBox_Click(null, null);
            }

            ConditionTBox[0].Text = "1";
            EquationTBox[0].Text = "-1*y*z+0,25*c*d";

            ConditionTBox[1].Text = "1";
            EquationTBox[1].Text = "-1*y*z+0,25*c*d-0,5*z*c";

            ConditionTBox[2].Text = "0";
            EquationTBox[2].Text = "y*z-0,25*c*d-0,5*z*c";

            ConditionTBox[3].Text = "0";
            EquationTBox[3].Text = "y*z-0,25*c*d+0,5*z*c";

            ConditionTBox[4].Text = "0";
            EquationTBox[4].Text = "0,5*z*c";

            RungeKuttaMethod_Click(null, null);
        }
        #endregion

        #region //Обробники подій для StatusStrip
        private void LeftBorder_MouseMove(object sender, MouseEventArgs e)
        {
            StatusLabel.Text = "Ліва границя відрізку";
        }

        private void RightBorder_MouseMove(object sender, MouseEventArgs e)
        {
            StatusLabel.Text = "Права границя відрізку";
        }

        private void AmountSections_MouseMove(object sender, MouseEventArgs e)
        {
            StatusLabel.Text = "На скільки частин потрібно розбити відрізок";
        }

        private void StepBreak_MouseMove(object sender, MouseEventArgs e)
        {
            StatusLabel.Text = "Крок розбиття відрізка";
        }

        private void RungeKuttaMethod_MouseMove(object sender, MouseEventArgs e)
        {
            StatusLabel.Text = "Порахувати систему за методом Рунге-Кутта";
        }

        private void AddTextBox_MouseMove(object sender, MouseEventArgs e)
        {
            StatusLabel.Text = "Додати поле для введення рівняння";
        }

        private void DeleteTextBox_MouseMove(object sender, MouseEventArgs e)
        {
            StatusLabel.Text = "Видалити останнє створене поле для введення";
        }

        private void Exit_MouseMove(object sender, MouseEventArgs e)
        {
            StatusLabel.Text = "Вийти з програми";
        }

        private void Setting_MouseMove(object sender, MouseEventArgs e)
        {
            StatusLabel.Text = "Налаштування програми";
        }

        private void ClearAll_MouseMove(object sender, MouseEventArgs e)
        {
            StatusLabel.Text = "Очистити всі поля для введення";
        }

        private void OpenFileTool_MouseMove(object sender, MouseEventArgs e)
        {
            StatusLabel.Text = "Відкрити збережений файл";
        }

        private void TestButton_MouseMove(object sender, MouseEventArgs e)
        {
            StatusLabel.Text = "Доступні тестові приклади для демонстрації";
        }

        private void Instruction_MouseMove(object sender, MouseEventArgs e)
        {
            StatusLabel.Text = "Коротка інструкція по використанню програми";
        }

        private void AboutProgram_MouseMove(object sender, MouseEventArgs e)
        {
            StatusLabel.Text = "Відомості про програму";
        }

        private void InputPage_MouseMove(object sender, MouseEventArgs e)
        {
            StatusLabel.Text = "Вкладка для введення даних";
        }

        private void ResultsPage_MouseMove(object sender, MouseEventArgs e)
        {
            StatusLabel.Text = "Вкладка для виведення результатів розрахунку";
        }

        private void CoeffPage_MouseMove(object sender, MouseEventArgs e)
        {
            StatusLabel.Text = "Вкладка для виведення коефіцієнтів системи";
        }

        private void Graphic_MouseMove(object sender, MouseEventArgs e)
        {
            StatusLabel.Text = "Результуючий графік для усіх рівнянь";
        }

        private void SaveFileTool_MouseMove(object sender, MouseEventArgs e)
        {
            StatusLabel.Text = "Зберегти результати розрахунків у файл";
        }

        private void LeaveBorder_MouseLeave(object sender, EventArgs e)
        {
            StatusLabel.Text = "Готово";
        }
        #endregion
    }
}
