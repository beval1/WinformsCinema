using IronBarCode;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cinema
{
    public partial class SceneForm : Form
    {
        private readonly Projection _projection;
        private readonly List<Seat> _seats = new List<Seat>();
        private Seat _clickedSeat = null;

        public SceneForm(Projection projection)
        {
            InitializeComponent();
            _projection = projection;
        }

        private void SceneForm_Load(object sender, EventArgs e)
        {
            LoadScene();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            _seats.ForEach(s => s.Paint(e.Graphics));
        }

        private void LoadScene()
        {
            Console.WriteLine(_projection.Id);
            int sceneRows = _projection.Scene.SceneSeats.Scene.Count;
            int sceneCols = _projection.Scene.SceneSeats.Scene[1].Count;
            int yIncrement = 20;
            int xIncrement = 40;
            int width = 40;
            int height = 40;

            int[] dimensions = CalculateSpace(sceneRows, sceneCols, width, height, xIncrement, yIncrement);

            int xStart = (this.Width - dimensions[0]) / 2;
            int yStart = (this.Height - dimensions[1]) / 2; ;
            int x = xStart;
            int y = yStart;
            for (int row = 1; row <= sceneRows; row++)
            {
                for (int col = 1; col <= _projection.Scene.SceneSeats.Scene[row].Count; col++)
                {
                    Seat seat;
                    if (_projection.Scene.SceneSeats.Scene[row][col] == 1)
                    {
                        seat = new TakenSeat();
                    } else
                    {
                        seat = new FreeSeat();
                    }
                    seat.Row = row;
                    seat.Col = col;
                    _seats.Add(seat);
                    seat.Visiualize(new Point(x, y), width, height);
                    x += width + xIncrement;
                }
                x -= sceneCols * (width + xIncrement);
                y += height + yIncrement;
            }
            Invalidate();
        }

        private int[] CalculateSpace(int sceneRows, int sceneCols, int width, int height, int xIncrement, int yIncrement)
        {
            int x = 0;
            int y = 0;
            for (int row = 1; row <= sceneRows; row++)
            {
                for (int col = 1; col <= sceneCols; col++)
                {
                    x += width + xIncrement;
                }
                x -= (sceneRows * (width + xIncrement));
                y += height + yIncrement;
            }
            int[] dimensions = new int[2];
            //adjust error from the last iteration
            dimensions[0] = x - xIncrement - width;
            dimensions[1] = y - yIncrement - height;
            return dimensions;
        }

        private void SceneForm_MouseDown_1(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            RefreshSelected();

            _seats.ForEach(s =>
            {
                if (s.PointInShape(e.Location)) _clickedSeat = s;
            });

            if (_clickedSeat != null)
            {
                if (_clickedSeat.GetType() == typeof(FreeSeat))
                {
                    _clickedSeat.SetColor(Color.Green);
                    Invalidate();

                    string message = $"Are you sure you want to select this seat for {_projection.TicketPrice} BGN";
                    string caption = "Selecting Seat";
                    MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                    DialogResult result;

                    // Displays the MessageBox.
                    result = MessageBox.Show(message, caption, buttons);
                    if (result == System.Windows.Forms.DialogResult.Yes)
                    {
                        var generatedBarcode = GenerateBarcode();
                        GeneratedQRForm qrForm = new GeneratedQRForm(generatedBarcode, _projection, _clickedSeat);
                        qrForm.ShowDialog();
                    } else { RefreshSelected(); }
                }
                else
                {
                    string message = "This seat is already taken";
                    string caption = "Error selecting seat";
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    DialogResult result;
                    // Displays the MessageBox.
                    result = MessageBox.Show(message, caption, buttons);
                }
            }
            Invalidate();
        }

        private GeneratedBarcode GenerateBarcode()
        {
            //send request to the backend for generating ticket


            //generate barcode with ticketID
            var barCode = IronBarCode.QRCodeWriter.CreateBarcode("https://ironsoftware.com/csharp/barcode");
            return barCode;
        }

        private void RefreshSelected()
        {
            if (_clickedSeat != null)
            {
                _seats.ForEach(s =>
                {
                    if (s.GetType() == typeof(FreeSeat))
                    {
                        s.SetColor(Color.Blue);
                    }
                });
                _clickedSeat = null;
            }
        }
    }
}

