using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClassToUmlConverter.Library;

namespace ClassToUmlConverterUI
{
    public partial class frmConverter : Form
    {


        #region "Main"
        ClassToUmlConverterService converterService = new ClassToUmlConverterService();

        public frmConverter()
        {
            InitializeComponent();

            converterService.ErrorEvent += ConverterService_ErrorEvent;
            converterService.ProgressEvent += ConverterService_ProgressEvent;
        }

        private void btnInBrowse_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            txbInputFolder.Text = folderBrowserDialog1.SelectedPath ?? txbInputFolder.Text;
        }

        private void btnOutBrowse_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            txbOutputFolder.Text = folderBrowserDialog1.SelectedPath ?? txbOutputFolder.Text;
        }

        private async void btnStart_Click(object sender, EventArgs e)
        {
            if (txbInputFolder.Text != "" && txbOutputFolder.Text != "")
            {
                txbProgress.Text = "";
                await converterService.Run(txbInputFolder.Text, $"{txbOutputFolder.Text}");
            }
        }

        #endregion

        #region "Events"

        private void ConverterService_ProgressEvent(string message)
        {
            txbProgress.Text += message + "\r\n";
        }

        private void ConverterService_ErrorEvent(Exception ex)
        {
            txbProgress.Text += $"{ex.Message}, detail:{ex.StackTrace}\r\n";
        }

        #endregion
    }
}
