using System;
using System.IO;
using System.Windows.Forms;

namespace CourseWork
{
    public partial class Settings : Form
    {
        MenuStrip MenuStrip;
        ToolStrip ToolStrip;
        StatusStrip StatusStrip;
        public int precisionCount = 4;
        public bool saveFlag = false;

        public Settings(MenuStrip MenuStrip, ToolStrip ToolStrip, StatusStrip StatusStrip, int precisionCount, bool saveFlag)
        {
            InitializeComponent();

            this.MenuStrip = MenuStrip;
            this.ToolStrip = ToolStrip;
            this.StatusStrip = StatusStrip;
            PrecisionCounter.Value = precisionCount;
            Autosave.Checked = saveFlag;

            if (!MenuStrip.Visible)
                DisableMenuStrip.Checked = false;

            if (!ToolStrip.Visible)
                DisableToolStrip.Checked = false;

            if (!StatusStrip.Visible)
                DisableStatusStrip.Checked = false;
        }

        private void DisableMenuStrip_CheckedChanged(object sender, EventArgs e)
        {
            MenuStrip.Visible = DisableMenuStrip.Checked == false ? false : true;
        }

        private void DisableToolStrip_CheckedChanged(object sender, EventArgs e)
        {
            ToolStrip.Visible = DisableToolStrip.Checked == false ? false : true;
        }

        private void DisableStatusStrip_CheckedChanged(object sender, EventArgs e)
        {
            StatusStrip.Visible = DisableStatusStrip.Checked == false ? false : true;
        }

        private void Autosave_CheckedChanged(object sender, EventArgs e)
        {
            saveFlag = Autosave.Checked ? true : false;
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void PrecisionCounter_ValueChanged(object sender, EventArgs e)
        {
            precisionCount = Convert.ToInt32(PrecisionCounter.Value);
        }

        private void ClearLogFile_Click(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists("AutosaveFile.txt"))
                    File.Delete("AutosaveFile.txt");
                else
                    throw new Exception();

                MessageBox.Show("Файл автозбереження результатів успішно видалено!", "Процес видалення файлу", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Виникла помилка під час видалення файлу!\nФайл вже видалений", "Помилка в ході програми", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
