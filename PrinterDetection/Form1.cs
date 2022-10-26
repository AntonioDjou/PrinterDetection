using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace PrinterDetection
{
    public partial class Form1 : Form
    {
        [DllImport("winspool.drv", SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern bool SetDefaultPrinter(string Name);
        public Form1()
        {
            InitializeComponent();
            GetImpressoras();
            lblImpressoraPadrao.Text = GetImpressoraPadrao();
        }

        private void GetImpressoras()
        {
            foreach (string impressora in PrinterSettings.InstalledPrinters)
            {
                listBox1.Items.Add(impressora);
            }
            if (listBox1.Items == null)
            {
                MessageBox.Show("Não há impressoras disponíveis");
            }
        }

        private string GetImpressoraPadrao()
        {
            var impressora = new PrinterSettings();
            return impressora.PrinterName;
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            if(SetDefaultPrinter(listBox1.Text))
            {
                lblImpressoraPadrao.Text = listBox1.Text;
            }
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            e.Graphics.DrawString("Cliente", new Font("Arial", 20, FontStyle.Bold), Brushes.Black, new Point(5, 20));
            e.Graphics.DrawString("____________________________________________________________________________________________", new Font("Arial", 10, FontStyle.Bold), Brushes.Black, new Point(5, 35));

            e.Graphics.DrawString($"Telefone:      (85) 98549.6894", new Font("Arial", 10), Brushes.Black, new Point(5, 60));
            e.Graphics.DrawString($"Nome:           Antonio Paz", new Font("Arial", 10), Brushes.Black, new Point(5, 80));

            e.Graphics.DrawString($"Endereço:    Rua Dom Timóteo, 76", new Font("Arial", 10), Brushes.Black, new Point(5, 120));
            e.Graphics.DrawString($"Cidade:        Tianguá", new Font("Arial", 10), Brushes.Black, new Point(5, 140));
            e.Graphics.DrawString($"CEP:              62320480", new Font("Arial", 10), Brushes.Black, new Point(5, 160));
            e.Graphics.DrawString($"UF:  CE", new Font("Arial", 10), Brushes.Black, new Point(170, 160));
            e.Graphics.DrawString("========================================================================================", new Font("Arial", 10), Brushes.Black, new Point(0, 185));

            e.Graphics.DrawString($"Itens", new Font("Arial", 20, FontStyle.Bold), Brushes.Black, new Point(0, 205));
            e.Graphics.DrawString("____________________________________________________________________________________________", new Font("Arial", 10), Brushes.Black, new Point(0, 230));
            e.Graphics.DrawString($"Descrição", new Font("Arial", 10), Brushes.Black, new Point(5, 257));
            e.Graphics.DrawString($"Qtde", new Font("Arial", 10), Brushes.Black, new Point(230, 257));
            e.Graphics.DrawString($"Valor Total", new Font("Arial", 10), Brushes.Black, new Point(330, 257));
            e.Graphics.DrawString("____________________________________________________________________________________________", new Font("Arial", 10, FontStyle.Bold), Brushes.Black, new Point(0, 270));
            e.Graphics.DrawString($"Django Clássico", new Font("Arial", 10), Brushes.Black, new Point(5, 300));
            e.Graphics.DrawString($"1", new Font("Arial", 10), Brushes.Black, new Point(242, 300));
            e.Graphics.DrawString($"R$24,00", new Font("Arial", 10), Brushes.Black, new Point(340, 300));
            //e.Graphics.DrawString($"{orderJson.description}", new Font("Arial", 10), Brushes.Black, new Point(0, 170));
            
            e.Graphics.DrawString("____________________________________________________________________________________________", new Font("Arial", 10, FontStyle.Bold), Brushes.Black, new Point(0, 470));
            e.Graphics.DrawString($"Valor do pedido:                 R$ 24.00", new Font("Arial", 10), Brushes.Black, new Point(5, 490));
            e.Graphics.DrawString($"Taxa de Entrega:                 R$ 7.00", new Font("Arial", 10), Brushes.Black, new Point(5, 510));
            e.Graphics.DrawString($"Total:                                   R$ 31.00", new Font("Arial", 10), Brushes.Black, new Point(5, 530));
            e.Graphics.DrawString($"Valor pago pelo cliente:    R$ 31.00", new Font("Arial", 10), Brushes.Black, new Point(5, 550));
            e.Graphics.DrawString($"R$ 31,00 - PGTO COM DINHEIRO", new Font("Arial", 10, FontStyle.Bold), Brushes.Black, new Point(5, 580));
        }

        private void printButton_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
        }
    }
}
