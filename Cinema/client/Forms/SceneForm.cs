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
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace Cinema
{
    public partial class SceneForm : Form
    {
        private readonly Projection _projection;
        private readonly List<Seat> _seats = new List<Seat>();
        private Seat _clickedSeat = null;
        private readonly ApiClient _apiClient;
        private SceneSeats _scene = null;
        private string _ownerName = string.Empty;

        public SceneForm(Projection projection)
        {
            InitializeComponent();
            _projection = projection;
            _apiClient = ApiClient.Instance;
            NameForm.NameChanged += name => _ownerName = name;
            CenterToScreen();
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
            if (_projection.SceneSeats != null)
            {
                _scene = _projection.SceneSeats;
            } else
            {
                _scene = _projection.Scene.SceneSeats;
            }

            Console.WriteLine(_projection.Id);
            int sceneRows = _scene.Scene.Count;
            int sceneCols = _scene.Scene[1].Count;
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
                //label for row
                this.Controls.Add(
                new Label()
                {
                    Text = row.ToString(),
                    Location = new Point(xStart - 50, y + 5),
                    Font = new Font("Arial", 20),
                    Size = new Size(30, 30),
                }); 
                for (int col = 1; col <= sceneCols; col++)
                {
                    Seat seat;
                    if (_scene.Scene[row][col] == 1)
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
                    //label for column
                    if (row == sceneRows)
                    {
                        this.Controls.Add(new Label()
                        {
                            Text = col.ToString(),
                            Location = new Point(x + 5, yStart - 50),
                            Font = new Font("Arial", 20),
                            Size = new Size(30, 30),
                        });
                    }
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
                        Form nameForm = new NameForm();
                        nameForm.ShowDialog();
                        Hide();
                        if (_ownerName != null && _ownerName != "")
                        {
                            GenerateBarcode();
                        }
                        else { RefreshSelected(); }
                    }
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

        private async void GenerateBarcode()
        {
            //set the clicked seat as taken
            _scene.Scene[_clickedSeat.Row][_clickedSeat.Col] = 1;

            //wrap up everything in a ticketwrapper object
            TicketWrapper ticket = new TicketWrapper
            {
                Projection = _projection,
                SeatRow = _clickedSeat.Row,
                SeatCol = _clickedSeat.Col,
                OwnerFullName = _ownerName,
                Scene = _scene,
            };

            //convert projection object to json
            //string json = JsonConvert.SerializeObject(_projection);
            //string json = Regex.Unescape(JsonConvert.SerializeObject(_projection, Formatting.Indented));
            //no need to manually convert, RestSharp does it for us

            //send request to the backend for generating ticket
            TicketRoot generatedTicket = await _apiClient.CreateTicket(ticket);
            if (generatedTicket == null)
            {
                string message = "Error creating a ticket";
                string caption = "Error!";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                MessageBox.Show(message, caption, buttons);
                return;
            }

            //generate barcode with ticketID
            Console.WriteLine(generatedTicket.Ticket.Uuid);
            var barcode = IronBarCode.QRCodeWriter.CreateBarcode(generatedTicket.Ticket.Uuid);

            //show QR form
            GeneratedQRForm qrForm = new GeneratedQRForm(barcode.Image, _projection, generatedTicket.Ticket);
            qrForm.ShowDialog();
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

