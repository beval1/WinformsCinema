using System;
using System.Windows.Forms;
using IronBarCode;
using System.Drawing.Printing;
using System.Drawing;
using System.Drawing.Imaging;

namespace Cinema
{
    public partial class GeneratedQRForm : Form
    {
        private readonly Ticket _ticket = null;
        private readonly Image _barcodeImage = null;
        private readonly Projection _projection = null;

        public GeneratedQRForm(Image barcode, Projection projection, Ticket ticket)
        {
            InitializeComponent();
            _barcodeImage = barcode;
            _projection = projection;
            _ticket = ticket;
        }

        private void GeneratedQRForm_Load(object sender, EventArgs e)
        {
            CenterToScreen();
            pictureBox1.Image = _barcodeImage;
            label1.Text = $"Client Name: {_ticket.OwnerFullName}\nMovie: {_projection.Movie.Name}\nTicket Price: {_projection.TicketPrice} BGN\n" +
                $"Seat: row-{_ticket.SeatRow}; column-{_ticket.SeatCol}\nProjection Date: { _projection.ProjectionTime.ToShortDateString()}\n" +
                $"Projection Time: { _projection.ProjectionTime.ToShortTimeString()}";
            label1.Font = new System.Drawing.Font("Arial", 14);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            /*
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    _barcodeImage.SaveAsJpeg("");
                }
            }
            */
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "JPeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif";
            saveFileDialog1.Title = "Save an Image File";
            saveFileDialog1.ShowDialog();
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //_barcodeImage.SaveAsJpeg(saveFileDialog1.FileName);
                _barcodeImage.Save(saveFileDialog1.FileName, ImageFormat.Jpeg);
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PrintDialog PrintDialog1 = new PrintDialog();
            PrintDialog1.Document = new PrintDocument();
            PrintDialog1.ShowDialog();
        }
    }
}
