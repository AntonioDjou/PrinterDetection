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
using System.Data.SqlClient;

namespace PrinterDetection
{
    public partial class Form1 : Form
    {
        [DllImport("winspool.drv", SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern bool SetDefaultPrinter(string Name);

        object selectedPaperSize;
        PaperSize pkCustomSize1;
        PaperSize pkCustomSize2;

        public static SqlConnection cnn;
        
        public Form1()
        {
            InitializeComponent();
            GetImpressoras();
            lblImpressoraPadrao.Text = GetImpressoraPadrao();
            GetPaperSize();

            
        }

        private void GetImpressoras()
        {
            foreach (string impressora in PrinterSettings.InstalledPrinters)
            {
                listBox1.Items.Add(impressora);
                comboBox1.Items.Add(impressora);
                //System.Drawing.Printing.
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

        private void GetPaperSize() // Mostra todos os tamanhos disponíveis para a impressão no ComboBox comboPaperSize
        {
            // Add list of supported paper sizes found on the printer. 
            // The DisplayMember property is used to identify the property that will provide the display string.
            comboPaperSize.DisplayMember = "PaperName";
            var printDoc = new PrinterSettings();

            // Create a PaperSize and specify the custom paper size through the constructor and add to combobox.
            pkCustomSize1 = new PaperSize("80mm", 260, 500);
            comboPaperSize.Items.Add(pkCustomSize1);
            pkCustomSize2 = new PaperSize("58mm", 200, 465);
            comboPaperSize.Items.Add(pkCustomSize2);
            
            PaperSize pkSize;
            for (int i = 0; i < printDoc.PaperSizes.Count; i++)
            {
                pkSize = printDoc.PaperSizes[i];
                comboPaperSize.Items.Add(pkSize);
            }
        }

        private void printButton_Click(object sender, EventArgs e)
        {
            selectedPaperSize = comboPaperSize.SelectedItem;
            if (selectedPaperSize == null || selectedPaperSize == pkCustomSize1) printDocument1.DefaultPageSettings.PaperSize = pkCustomSize1;
            else if (selectedPaperSize == pkCustomSize2) printDocument1.DefaultPageSettings.PaperSize = pkCustomSize2;

            //MessageBox.Show(printDocument1.DefaultPageSettings.PaperSize.ToString());
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            if (selectedPaperSize == null || selectedPaperSize == pkCustomSize1) // 80mm
            {
                e.Graphics.DrawString("-----------------------------------------------------", new Font("Arial", 10), Brushes.Black, new Point(5, 5));
                e.Graphics.DrawString("iFood", new Font("Arial", 5), Brushes.Black, new Point(115,17));
                e.Graphics.DrawString("+--------------------------------------------------------------------------------------------------+", new Font("Arial", 5), Brushes.Black, new Point(12, 25));
                e.Graphics.DrawString("|                                           PEDIDO #12345                                             |", new Font("Arial", 5), Brushes.Black, new Point(13, 32));
                e.Graphics.DrawString("|                           LOCALIZADOR DO PEDIDO im2NcXDyVZ                    |", new Font("Arial", 5), Brushes.Black, new Point(13, 42));
                e.Graphics.DrawString("|                                            ENTREGA PRÓPRIA                                    |", new Font("Arial", 5), Brushes.Black, new Point(13, 52));
                e.Graphics.DrawString("+--------------------------------------------------------------------------------------------------+", new Font("Arial", 5), Brushes.Black, new Point(12, 60));
                e.Graphics.DrawString("Restaurante: Eazy Teste", new Font("Arial", 5), Brushes.Black, new Point(5, 70));
                e.Graphics.DrawString("Data: 22/12/2022 15:20:00", new Font("Arial", 5), Brushes.Black, new Point(5, 80));
                e.Graphics.DrawString("Entrega prevista: 15:50", new Font("Arial", 5), Brushes.Black, new Point(5, 90));
                e.Graphics.DrawString("Solicite o código de confirmação na hora da entrega", new Font("Arial", 5), Brushes.Black, new Point(5, 100));
                e.Graphics.DrawString("+--------------------------------------------------------------------------------------------------+", new Font("Arial", 5), Brushes.Black, new Point(12, 120));
                e.Graphics.DrawString("|                                                                                                                  |", new Font("Arial", 5), Brushes.Black, new Point(13, 130));
                e.Graphics.DrawString("|   ** DEIXAR O PEDIDO EM: ENCONTRAR COM O ENTREGADOR **     |", new Font("Arial", 5), Brushes.Black, new Point(13, 140));
                e.Graphics.DrawString("|                                                                                                                  |", new Font("Arial", 5), Brushes.Black, new Point(13, 150));
                e.Graphics.DrawString("+--------------------------------------------------------------------------------------------------+", new Font("Arial", 5), Brushes.Black, new Point(12, 160));
                e.Graphics.DrawString("Primeiro pedido na sua loja!", new Font("Arial", 5), Brushes.Black, new Point(5, 190));
                e.Graphics.DrawString("Dados do cliente", new Font("Arial", 5), Brushes.Black, new Point(5, 210));
                e.Graphics.DrawString("Nome: Samuel Vilela", new Font("Arial", 5), Brushes.Black, new Point(5, 220));
                e.Graphics.DrawString("Telefone: +5537999943911", new Font("Arial", 5), Brushes.Black, new Point(5, 230));
                e.Graphics.DrawString("ID: 11176", new Font("Arial", 5), Brushes.Black, new Point(100, 230));
                e.Graphics.DrawString("Rua: Henrique Alberto Pepin, 313", new Font("Arial", 5), Brushes.Black, new Point(5, 240));
                e.Graphics.DrawString("Bairro: Jardim Panorama II", new Font("Arial", 5), Brushes.Black, new Point(5, 250));
                e.Graphics.DrawString("Cidade: Foz do Iguacu - PR", new Font("Arial", 5), Brushes.Black, new Point(5, 260));
                e.Graphics.DrawString("CEP: 85856300", new Font("Arial", 5), Brushes.Black, new Point(5, 270));
                e.Graphics.DrawString($"Itens do pedido", new Font("Arial", 5, FontStyle.Bold), Brushes.Black, new Point(5, 290));
                e.Graphics.DrawString($"Descrição", new Font("Arial", 5, FontStyle.Italic), Brushes.Black, new Point(50, 300));
                e.Graphics.DrawString($"Qtde", new Font("Arial", 5, FontStyle.Italic), Brushes.Black, new Point(120, 300));
                e.Graphics.DrawString($"Valor Total", new Font("Arial", 5, FontStyle.Italic), Brushes.Black, new Point(190, 300));
                for (int i = 0; i < 4; i++)
                {
                    e.Graphics.DrawString($"Django Clássico", new Font("Arial", 5), Brushes.Black, new Point(5, (310 + i * 15)));
                    e.Graphics.DrawString($"1", new Font("Arial", 5), Brushes.Black, new Point(127, (310 + i * 15)));
                    e.Graphics.DrawString($"R$24,00", new Font("Arial", 5), Brushes.Black, new Point(195, (310 + i * 15)));
                }
                e.Graphics.DrawString("+--------------------------------------------------------------------------------------------------+", new Font("Arial", 5), Brushes.Black, new Point(12, 375));
                e.Graphics.DrawString("| Valor total do pedido: R$20,00                                                                 |", new Font("Arial", 5), Brushes.Black, new Point(13, 385));
                e.Graphics.DrawString("| Taxa de entrega: R$10,00                                                                        |", new Font("Arial", 5), Brushes.Black, new Point(13, 395));
                e.Graphics.DrawString("|---------------------------------------------------------------------------------------------------|", new Font("Arial", 5), Brushes.Black, new Point(13, 405));
                e.Graphics.DrawString("| Cobrar do cliente: R$30,00                                                                      |", new Font("Arial", 5), Brushes.Black, new Point(13, 415));
                e.Graphics.DrawString("+--------------------------------------------------------------------------------------------------+", new Font("Arial", 5), Brushes.Black, new Point(12, 425));
                e.Graphics.DrawString("Forma de pagamento", new Font("Arial", 5), Brushes.Black, new Point(5, 445));
                e.Graphics.DrawString("DINHEIRO", new Font("Arial", 5), Brushes.Black, new Point(5, 455));
                e.Graphics.DrawString("Observações", new Font("Arial", 5), Brushes.Black, new Point(5, 475));
                e.Graphics.DrawString("Observaçao do pedido por extenso, podendo ser um texto grande", new Font("Arial", 5), Brushes.Black, new Point(5, 485));
            }

            else if (selectedPaperSize == pkCustomSize2) // 58mm => Referência abaixo!
            {
                e.Graphics.DrawString("--------------------------------------------------------", new Font("Arial", 7), Brushes.Black, new Point(5, 5));
                e.Graphics.DrawString("iFood", new Font("Arial", 4), Brushes.Black, new Point(95, 17));
                e.Graphics.DrawString("+-------------------------------------------------------------------------------------------+", new Font("Arial", 4), Brushes.Black, new Point(6, 25));
                e.Graphics.DrawString("|                                           PEDIDO #12345                                             |", new Font("Arial", 4), Brushes.Black, new Point(7, 32));
                e.Graphics.DrawString("|                           LOCALIZADOR DO PEDIDO im2NcXDyVZ                   |", new Font("Arial", 4), Brushes.Black, new Point(7, 42));
                e.Graphics.DrawString("|                                            ENTREGA PRÓPRIA                                     |", new Font("Arial", 4), Brushes.Black, new Point(7, 52));
                e.Graphics.DrawString("+-------------------------------------------------------------------------------------------+", new Font("Arial", 4), Brushes.Black, new Point(7, 60));
                e.Graphics.DrawString("Restaurante: Eazy Teste", new Font("Arial", 4), Brushes.Black, new Point(5, 70));
                e.Graphics.DrawString("Data: 22/12/2022 15:20:00", new Font("Arial", 4), Brushes.Black, new Point(5, 80));
                e.Graphics.DrawString("Entrega prevista: 15:50", new Font("Arial", 4), Brushes.Black, new Point(5, 90));
                e.Graphics.DrawString("Solicite o código de confirmação na hora da entrega", new Font("Arial", 4), Brushes.Black, new Point(5, 100));
                e.Graphics.DrawString("+------------------------------------------------------------------------------------------+", new Font("Arial", 4), Brushes.Black, new Point(6, 120));
                e.Graphics.DrawString("|                                                                                                                  |", new Font("Arial", 4), Brushes.Black, new Point(7, 130));
                e.Graphics.DrawString("|   ** DEIXAR O PEDIDO EM: ENCONTRAR COM O ENTREGADOR **                        |", new Font("Arial", 3), Brushes.Black, new Point(7, 140));
                e.Graphics.DrawString("|                                                                                                                  |", new Font("Arial", 4), Brushes.Black, new Point(7, 150));
                e.Graphics.DrawString("+------------------------------------------------------------------------------------------+", new Font("Arial", 4), Brushes.Black, new Point(7, 160));
                e.Graphics.DrawString("Primeiro pedido na sua loja!", new Font("Arial", 4), Brushes.Black, new Point(5, 180));
                e.Graphics.DrawString("Dados do cliente", new Font("Arial", 4), Brushes.Black, new Point(5, 200));
                e.Graphics.DrawString("Nome: Samuel Vilela", new Font("Arial", 4), Brushes.Black, new Point(5, 210));
                e.Graphics.DrawString("Telefone: +5537999943911", new Font("Arial", 4), Brushes.Black, new Point(5, 220));
                e.Graphics.DrawString("ID: 11176", new Font("Arial", 4), Brushes.Black, new Point(100, 220));
                e.Graphics.DrawString("Rua: Henrique Alberto Pepin, 313", new Font("Arial", 4), Brushes.Black, new Point(5, 230));
                e.Graphics.DrawString("Bairro: Jardim Panorama II", new Font("Arial", 4), Brushes.Black, new Point(5, 240));
                e.Graphics.DrawString("Cidade: Foz do Iguacu - PR", new Font("Arial", 4), Brushes.Black, new Point(5, 250));
                e.Graphics.DrawString("CEP: 85856300", new Font("Arial", 4), Brushes.Black, new Point(5, 260));
                e.Graphics.DrawString($"Itens do pedido", new Font("Arial", 4, FontStyle.Bold), Brushes.Black, new Point(5, 280));
                e.Graphics.DrawString($"Descrição", new Font("Arial", 4, FontStyle.Italic), Brushes.Black, new Point(12, 290));
                e.Graphics.DrawString($"Qtde", new Font("Arial", 4, FontStyle.Italic), Brushes.Black, new Point(86, 290));
                e.Graphics.DrawString($"Valor Total", new Font("Arial", 4, FontStyle.Italic), Brushes.Black, new Point(143, 290));
                for (int i = 0; i < 4; i++)
                {
                    e.Graphics.DrawString($"Django Clássico", new Font("Arial", 4), Brushes.Black, new Point(5, (300 + i * 15)));
                    e.Graphics.DrawString($"1", new Font("Arial", 4), Brushes.Black, new Point(92, (300 + i * 15)));
                    e.Graphics.DrawString($"R$24,00", new Font("Arial", 4), Brushes.Black, new Point(145, (300 + i * 15)));
                }
                e.Graphics.DrawString("+------------------------------------------------------------------------------------------+", new Font("Arial", 4), Brushes.Black, new Point(6, 355));
                e.Graphics.DrawString("| Valor total do pedido: R$20,00                                                                 |", new Font("Arial", 4), Brushes.Black, new Point(7, 365));
                e.Graphics.DrawString("| Taxa de entrega: R$10,00                                                                        |", new Font("Arial", 4), Brushes.Black, new Point(7, 375));
                e.Graphics.DrawString("|--------------------------------------------------------------------------------------------|", new Font("Arial", 4), Brushes.Black, new Point(7, 385));
                e.Graphics.DrawString("| Cobrar do cliente: R$30,00                                                                       |", new Font("Arial", 4), Brushes.Black, new Point(7, 395));
                e.Graphics.DrawString("+------------------------------------------------------------------------------------------+", new Font("Arial", 4), Brushes.Black, new Point(7, 405));
                e.Graphics.DrawString("Forma de pagamento", new Font("Arial", 4), Brushes.Black, new Point(5, 415));
                e.Graphics.DrawString("DINHEIRO", new Font("Arial", 4), Brushes.Black, new Point(5, 425));
                e.Graphics.DrawString("Observações", new Font("Arial", 4), Brushes.Black, new Point(5, 445));
                e.Graphics.DrawString("Observaçao do pedido por extenso, podendo ser um texto grande", new Font("Arial", 3), Brushes.Black, new Point(5, 455));
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13) // Se a pessoa apertou "Enter"
            {
                OrdersDataGridView.Visible = true;

                //Falta conectar ao banco

                SqlCommand sql = new SqlCommand("SELECT NOME, ORDER_ID FROM DAILY_ORDERS WHERE NOME LIKE @NOME ORDER BY NOME", cnn);
                sql.Parameters.AddWithValue("@NOME", "%" + SearchTextBox.Text + "%");

                SqlDataAdapter data = new SqlDataAdapter(sql);
                DataSet tabela = new DataSet();
                data.Fill(tabela);
                OrdersDataGridView.DataSource = tabela.Tables[0];

                OrdersDataGridView.Columns[0].Width = 200;
                OrdersDataGridView.Columns[0].HeaderText = "Nome do Cliente";
                OrdersDataGridView.Columns[1].Width = 100;
                OrdersDataGridView.Columns[1].HeaderText = "Order Id";
            } 
        }
    }
}
