using System;
using System.Windows.Forms;
using IronBarCode;
using System.Drawing.Printing;

namespace Cinema
{
    public partial class GeneratedQRForm : Form
    {
        private readonly Seat _seat = null;
        private readonly GeneratedBarcode _barcode = null;
        private readonly Projection _projection = null;

        public GeneratedQRForm(GeneratedBarcode barcode, Projection projection, Seat seat)
        {
            InitializeComponent();
            _barcode = barcode;
            _projection = projection;
            _seat = seat;
        }

        private void GeneratedQRForm_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = _barcode.Image;
            label1.Text = $"Movie: {_projection.Movie.Name}\nTicket Price: {_projection.TicketPrice} BGN\n" +
                $"Seat: row-{_seat.Row}; column-{_seat.Col}";
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
                    _barcode.SaveAsJpeg("");
                }
            }
            */
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "JPeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif";
            saveFileDialog1.Title = "Save an Image File";
            saveFileDialog1.ShowDialog();
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                _barcode.SaveAsJpeg(saveFileDialog1.FileName);
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
