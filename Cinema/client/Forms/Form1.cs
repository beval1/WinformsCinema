using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using IronBarCode;
using System.Drawing;

namespace Cinema
{
    public partial class Form1 : Form
    {
        private readonly ApiClient _apiClient;
        private List<Projection> _projections;

        public Form1()
        {
            InitializeComponent();
            _apiClient = ApiClient.Instance;
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            ProjectionList projectionList = await _apiClient.GetProjections();
            _projections = projectionList.Data;
            ProjectionTemplate();
        }
        
        private void ProjectionTemplate()
        {
            foreach (Projection projection in _projections) {
                ProjectionView projectionView = new ProjectionView(projection, flowLayoutPanel2);
                projectionView.Visualize();
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.tif;";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string selectedFileName = openFileDialog1.FileName;
                BarcodeResult Result = BarcodeReader.QuicklyReadOneBarcode(selectedFileName);
                if (Result != null)
                {
                    Ticket ticket = await _apiClient.GetTicket(Result.Text);
                    Console.WriteLine(ticket.Uuid);
                    Image barcodeImg = Image.FromFile(openFileDialog1.FileName);
                    GeneratedQRForm qrForm = new GeneratedQRForm(barcodeImg, ticket.Projection, ticket);
                    qrForm.ShowDialog();
                } else
                {
                    string message = "Error reading QR code";
                    string caption = "Error";
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    MessageBox.Show(message, caption, buttons);
                }

            }
        }
    }
}
