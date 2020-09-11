using System;
using System.Windows.Forms;

namespace CourseWork
{
    public partial class AboutProgram : Form
    {
        public AboutProgram()
        {
            InitializeComponent();
        }

        private void AutorEmail_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Clipboard.SetText("andrii.kitsun@gmail.com");
            StatusLabel.Text = "Адресу скопійовано у буфер обміну";
        }

        private void KvitkaEmail_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Clipboard.SetText("alkvi@ukr.net");
            StatusLabel.Text = "Адресу скопійовано у буфер обміну";
        }

        private void ShakhnovskyEmail_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Clipboard.SetText("amshakhn@gmail.com");
            StatusLabel.Text = "Адресу скопійовано у буфер обміну";
        }

        private void BendyugEmail_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Clipboard.SetText("bendyug@outlook.com");
            StatusLabel.Text = "Адресу скопійовано у буфер обміну";
        }

        #region //Обробники подій для StatusStrip
        private void LinkLeave_MouseLeave(object sender, EventArgs e)
        {
            StatusLabel.Text = "Готово";
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void KvitkaEmail_MouseMove(object sender, MouseEventArgs e)
        {
            StatusLabel.Text = "Доцент Квітка Олександр Олександрович";
        }

        private void ShakhnovskyEmail_MouseMove(object sender, MouseEventArgs e)
        {
            StatusLabel.Text = "Доцент Шахновський Аркадій Маркусович";
        }

        private void BendyugEmail_MouseMove(object sender, MouseEventArgs e)
        {
            StatusLabel.Text = "Доцент Бендюг Владислав Іванович";
        }
        #endregion
    }
}
