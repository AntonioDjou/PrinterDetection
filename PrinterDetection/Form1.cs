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
            pkCustomSize1 = new PaperSize("80mm", 260, 375);
            comboPaperSize.Items.Add(pkCustomSize1);
            pkCustomSize2 = new PaperSize("58mm", 200, 290);
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

            MessageBox.Show(printDocument1.DefaultPageSettings.PaperSize.ToString());
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            //----------------------------------//
            //              80mm                // => Trocar pelo de dentro do "If"
            //----------------------------------//
            //e.Graphics.DrawString("Cliente", new Font("Arial", 10, FontStyle.Bold), Brushes.Black, new Point(5, 20));
            //e.Graphics.DrawString("_________________________________", new Font("Arial", 10, FontStyle.Bold), Brushes.Black, new Point(5, 22));

            //e.Graphics.DrawString($"Telefone:       {orderJson.deliveryAddress.contact_phone}", new Font("Arial", 8), Brushes.Black, new Point(5, 40));
            //e.Graphics.DrawString($"Nome:           {orderJson.customer.name}", new Font("Arial", 8), Brushes.Black, new Point(5, 55));

            //e.Graphics.DrawString($"Endereço:     {orderJson.deliveryAddress.street}, {orderJson.deliveryAddress.street_number}", new Font("Arial", 8), Brushes.Black, new Point(5, 75));
            //e.Graphics.DrawString($"Cidade:         {orderJson.deliveryAddress.city}", new Font("Arial", 8), Brushes.Black, new Point(5, 90));
            //e.Graphics.DrawString($"CEP:              {orderJson.deliveryAddress.zipcode}", new Font("Arial", 8), Brushes.Black, new Point(5, 105));
            //e.Graphics.DrawString($"UF:  {orderJson.deliveryAddress.state}", new Font("Arial", 8), Brushes.Black, new Point(170, 105));
            //e.Graphics.DrawString("===============================", new Font("Arial", 10), Brushes.Black, new Point(0, 115));

            //e.Graphics.DrawString($"Itens", new Font("Arial", 9, FontStyle.Bold), Brushes.Black, new Point(5, 130));
            //e.Graphics.DrawString("________________________________", new Font("Arial", 10), Brushes.Black, new Point(5, 135));
            //e.Graphics.DrawString($"Descrição", new Font("Arial", 7, FontStyle.Italic), Brushes.Black, new Point(5, 153));
            //e.Graphics.DrawString($"Qtde", new Font("Arial", 7, FontStyle.Italic), Brushes.Black, new Point(120, 153));
            //e.Graphics.DrawString($"Valor Total", new Font("Arial", 7, FontStyle.Italic), Brushes.Black, new Point(190, 153));
            //e.Graphics.DrawString("________________________________", new Font("Arial", 10, FontStyle.Bold), Brushes.Black, new Point(5, 155));

            //for (int i = 0; i < orderJson.items.Count; i++) // Verificar se realmente é para usar o "Count" para detectar quantos pedidos tem
            //{
            //e.Graphics.DrawString($"{orderJson.items[i].name}", new Font("Arial", 7), Brushes.Black, new Point(5, 175));
            //e.Graphics.DrawString($"{orderJson.items[i].qty}", new Font("Arial", 7), Brushes.Black, new Point(132, 175));
            //e.Graphics.DrawString($"R${orderJson.items[i].normal_price}", new Font("Arial", 7), Brushes.Black, new Point(200, 175));
            //} // Verificar também no caso de um pedido com 2 itens iguais, precisa multiplicar o "normal_price" da linha anterior pelo "qty" ou se esse atributo já é passado corretamente

            //e.Graphics.DrawString("________________________________", new Font("Arial", 10, FontStyle.Bold), Brushes.Black, new Point(5, 270));
            //e.Graphics.DrawString($"Valor do pedido:                           R$ {orderJson.items[0].normal_price}", new Font("Arial", 8), Brushes.Black, new Point(5, 290));
            //e.Graphics.DrawString($"Taxa de Entrega:                          R$ {orderJson.delivery.charge}", new Font("Arial", 8), Brushes.Black, new Point(5, 305));
            //e.Graphics.DrawString($"Total:                                             {orderJson.payment.total_w_tax}", new Font("Arial", 8), Brushes.Black, new Point(5, 320));
            //e.Graphics.DrawString($"Valor pago pelo cliente:                R$ {orderJson.payment.total_w_tax}", new Font("Arial", 8), Brushes.Black, new Point(5, 335));

            //e.Graphics.DrawString($"R$ {orderJson.payment.total_w_tax} - {orderJson.payment.payment_formatted}", new Font("Arial", 8, FontStyle.Bold), Brushes.Black, new Point(5, 355));


            if (selectedPaperSize == null || selectedPaperSize == pkCustomSize1) // 80mm
            {
                e.Graphics.DrawString("Cliente", new Font("Arial", 10, FontStyle.Bold), Brushes.Black, new Point(5, 20));
                e.Graphics.DrawString("________________________________", new Font("Arial", 10, FontStyle.Bold), Brushes.Black, new Point(5, 22));

                e.Graphics.DrawString($"Telefone:       (85) 98549.6894", new Font("Arial", 8), Brushes.Black, new Point(5, 40));
                e.Graphics.DrawString($"Nome:           Antonio Paz", new Font("Arial", 8), Brushes.Black, new Point(5, 55));

                e.Graphics.DrawString($"Endereço:     Rua Dom Timóteo, 76", new Font("Arial", 8), Brushes.Black, new Point(5, 75));
                e.Graphics.DrawString($"Cidade:         Tianguá", new Font("Arial", 8), Brushes.Black, new Point(5, 90));
                e.Graphics.DrawString($"CEP:              62320480", new Font("Arial", 8), Brushes.Black, new Point(5, 105));
                e.Graphics.DrawString($"UF:  CE", new Font("Arial", 8), Brushes.Black, new Point(170, 105));
                e.Graphics.DrawString("==============================", new Font("Arial", 10), Brushes.Black, new Point(0, 115));

                e.Graphics.DrawString($"Itens", new Font("Arial", 9, FontStyle.Bold), Brushes.Black, new Point(5, 130));
                e.Graphics.DrawString("________________________________", new Font("Arial", 10), Brushes.Black, new Point(5, 135));
                e.Graphics.DrawString($"Descrição", new Font("Arial", 7, FontStyle.Italic), Brushes.Black, new Point(5, 155));
                e.Graphics.DrawString($"Qtde", new Font("Arial", 7, FontStyle.Italic), Brushes.Black, new Point(120, 155));
                e.Graphics.DrawString($"Valor Total", new Font("Arial", 7, FontStyle.Italic), Brushes.Black, new Point(190, 155));
                e.Graphics.DrawString("________________________________", new Font("Arial", 10, FontStyle.Bold), Brushes.Black, new Point(5, 156));
                for (int i = 0; i < 4; i++)
                {
                    e.Graphics.DrawString($"Django Clássico", new Font("Arial", 7), Brushes.Black, new Point(5, (175 + i * 15)));
                    e.Graphics.DrawString($"1", new Font("Arial", 7), Brushes.Black, new Point(127, (175 + i * 15)));
                    e.Graphics.DrawString($"R$24,00", new Font("Arial", 7), Brushes.Black, new Point(195, (175 + i * 15)));
                }
                e.Graphics.DrawString("________________________________", new Font("Arial", 10, FontStyle.Bold), Brushes.Black, new Point(5, 270));
                e.Graphics.DrawString($"Valor do pedido:                           R$ 24.00", new Font("Arial", 8), Brushes.Black, new Point(5, 290));
                e.Graphics.DrawString($"Taxa de Entrega:                          R$ 7.00", new Font("Arial", 8), Brushes.Black, new Point(5, 305));
                e.Graphics.DrawString($"Total:                                             R$ 31.00", new Font("Arial", 8), Brushes.Black, new Point(5, 320));
                e.Graphics.DrawString($"Valor pago pelo cliente:                R$ 31.00", new Font("Arial", 8), Brushes.Black, new Point(5, 335));

                e.Graphics.DrawString($"R$ 31,00 - PGTO COM DINHEIRO", new Font("Arial", 8, FontStyle.Bold), Brushes.Black, new Point(5, 355));
            }

            else if (selectedPaperSize == pkCustomSize2) // 58mm => Referência abaixo!
            {
                
                e.Graphics.DrawString("Cliente", new Font("Arial", 8, FontStyle.Bold), Brushes.Black, new Point(5, 15));
                e.Graphics.DrawString("_____________________", new Font("Arial", 10, FontStyle.Bold), Brushes.Black, new Point(5, 16));

                e.Graphics.DrawString($"Telefone:       (85) 98549.6894", new Font("Arial", 6), Brushes.Black, new Point(5, 33));
                e.Graphics.DrawString($"Nome:           Antonio Paz", new Font("Arial", 6), Brushes.Black, new Point(5, 43));

                e.Graphics.DrawString($"Endereço:     Rua Dom Timóteo, 76", new Font("Arial", 6), Brushes.Black, new Point(5, 58));
                e.Graphics.DrawString($"Cidade:         Tianguá", new Font("Arial", 6), Brushes.Black, new Point(5, 68));
                e.Graphics.DrawString($"CEP:              62320480", new Font("Arial", 6), Brushes.Black, new Point(5, 78));
                e.Graphics.DrawString($"UF:  CE", new Font("Arial", 6), Brushes.Black, new Point(130, 78));
                e.Graphics.DrawString("==========================", new Font("Arial", 8), Brushes.Black, new Point(0, 88));

                e.Graphics.DrawString($"Itens", new Font("Arial", 7, FontStyle.Bold), Brushes.Black, new Point(5, 98));
                e.Graphics.DrawString("_____________________", new Font("Arial", 10), Brushes.Black, new Point(5, 97));
                e.Graphics.DrawString($"Descrição", new Font("Arial", 5, FontStyle.Italic), Brushes.Black, new Point(5, 115));
                e.Graphics.DrawString($"Qtde", new Font("Arial", 5, FontStyle.Italic), Brushes.Black, new Point(100, 115));
                e.Graphics.DrawString($"Valor Total", new Font("Arial", 5, FontStyle.Italic), Brushes.Black, new Point(135, 115));
                e.Graphics.DrawString("_____________________", new Font("Arial", 10, FontStyle.Bold), Brushes.Black, new Point(5, 114));
                
                for (int i = 0; i < 4; i++)
                {
                    e.Graphics.DrawString($"Django Clássico", new Font("Arial", 5), Brushes.Black, new Point(5, (132 + i * 10)));
                    e.Graphics.DrawString($"1", new Font("Arial", 5), Brushes.Black, new Point(105, (132 + i * 10)));
                    e.Graphics.DrawString($"R$24,00", new Font("Arial", 5), Brushes.Black, new Point(140, (132 + i * 10)));
                }
                
                e.Graphics.DrawString("_____________________", new Font("Arial", 10, FontStyle.Bold), Brushes.Black, new Point(5, 200));
                e.Graphics.DrawString($"Valor do pedido:                           R$ 24.00", new Font("Arial", 6), Brushes.Black, new Point(5, 220));
                e.Graphics.DrawString($"Taxa de Entrega:                          R$ 7.00", new Font("Arial", 6), Brushes.Black, new Point(5, 230));
                e.Graphics.DrawString($"Total:                                             R$ 31.00", new Font("Arial", 6), Brushes.Black, new Point(5, 240));
                e.Graphics.DrawString($"Valor pago pelo cliente:                R$ 31.00", new Font("Arial", 6), Brushes.Black, new Point(5, 250));

                e.Graphics.DrawString($"R$ 31,00 - PGTO COM DINHEIRO", new Font("Arial", 6, FontStyle.Bold), Brushes.Black, new Point(5, 265));


                //----------------------------------//
                //              58mm                // => Trocar pelo de dentro do "Else if"
                //----------------------------------//
                //e.Graphics.DrawString("Cliente", new Font("Arial", 8, FontStyle.Bold), Brushes.Black, new Point(5, 15));
                //e.Graphics.DrawString("_____________________", new Font("Arial", 10, FontStyle.Bold), Brushes.Black, new Point(5, 16));

                //e.Graphics.DrawString($"Telefone:       {orderJson.deliveryAddress.contact_phone}", new Font("Arial", 6), Brushes.Black, new Point(5, 33));
                //e.Graphics.DrawString($"Nome:           {orderJson.customer.name}", new Font("Arial", 6), Brushes.Black, new Point(5, 43));

                //e.Graphics.DrawString($"Endereço:     {orderJson.deliveryAddress.street}, {orderJson.deliveryAddress.street_number}", new Font("Arial", 6), Brushes.Black, new Point(5, 58));
                //e.Graphics.DrawString($"Cidade:         {orderJson.deliveryAddress.city}", new Font("Arial", 6), Brushes.Black, new Point(5, 68));
                //e.Graphics.DrawString($"CEP:              {orderJson.deliveryAddress.zipcode}", new Font("Arial", 6), Brushes.Black, new Point(5, 78));
                //e.Graphics.DrawString($"UF:  {orderJson.deliveryAddress.state}", new Font("Arial", 6), Brushes.Black, new Point(130, 78));
                //e.Graphics.DrawString("===========================", new Font("Arial", 8), Brushes.Black, new Point(0, 88));

                //e.Graphics.DrawString($"Itens", new Font("Arial", 7, FontStyle.Bold), Brushes.Black, new Point(5, 98));
                //e.Graphics.DrawString("_____________________", new Font("Arial", 10), Brushes.Black, new Point(5, 97));
                //e.Graphics.DrawString($"Descrição", new Font("Arial", 5, FontStyle.Italic), Brushes.Black, new Point(5, 115));
                //e.Graphics.DrawString($"Qtde", new Font("Arial", 5, FontStyle.Italic), Brushes.Black, new Point(100, 115));
                //e.Graphics.DrawString($"Valor Total", new Font("Arial", 5, FontStyle.Italic), Brushes.Black, new Point(135, 115));
                //e.Graphics.DrawString("_____________________", new Font("Arial", 10, FontStyle.Bold), Brushes.Black, new Point(5, 114));

                //for (int i = 0; i < orderJson.items.Count; i++) // Verificar se realmente é para usar o "Count" para detectar quantos pedidos tem
                //{
                //    e.Graphics.DrawString($"{orderJson.items[i].name", new Font("Arial", 5), Brushes.Black, new Point(5, (132 + i * 10)));
                //    e.Graphics.DrawString($"{orderJson.items[i].qty}", new Font("Arial", 5), Brushes.Black, new Point(105, (132 + i * 10)));
                //    e.Graphics.DrawString($"R${orderJson.items[i].normal_price}", new Font("Arial", 5), Brushes.Black, new Point(140, (132 + i * 10)));
                //} // Verificar também no caso de um pedido com 2 itens iguais, precisa multiplicar o "normal_price" da linha anterior pelo "qty" ou se esse atributo já é passado corretamente

                //e.Graphics.DrawString("_____________________", new Font("Arial", 10, FontStyle.Bold), Brushes.Black, new Point(5, 200));
                //e.Graphics.DrawString($"Valor do pedido:                           R$ {orderJson.items[0].normal_price}", new Font("Arial", 6), Brushes.Black, new Point(5, 220));
                //e.Graphics.DrawString($"Taxa de Entrega:                          R$ {orderJson.delivery.charge}", new Font("Arial", 6), Brushes.Black, new Point(5, 230));
                //e.Graphics.DrawString($"Total:                                             R$ {orderJson.payment.total_w_tax}", new Font("Arial", 6), Brushes.Black, new Point(5, 240));
                //e.Graphics.DrawString($"Valor pago pelo cliente:                R$ {orderJson.payment.total_w_tax}", new Font("Arial", 6), Brushes.Black, new Point(5, 250));

                //e.Graphics.DrawString($"R$ {orderJson.payment.total_w_tax} - {orderJson.payment.payment_formatted}", new Font("Arial", 6, FontStyle.Bold), Brushes.Black, new Point(5, 265));
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
