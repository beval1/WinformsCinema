using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;

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
            //int index = 0;
            foreach (Projection projection in _projections) {
                var panel1 = new Panel()
                {
                    Name = "panel1",
                    TabIndex = 0,
                    Size = new System.Drawing.Size(560, 340),
                    BorderStyle = BorderStyle.FixedSingle,
                };
                var pictureBox1 = new PictureBox()
                {
                    Location = new System.Drawing.Point(6, 3),
                    Name = "pictureBox1",
                    Size = new System.Drawing.Size(250, 330),
                    ImageLocation = projection.Movie.CoverImage,
                };
                var movieNameLink = new LinkLabel()
                {
                    AutoSize = true,
                    Location = new System.Drawing.Point(pictureBox1.Location.X+pictureBox1.Size.Width+10, 10),
                    Name = "linkLabel1",
                    Size = new System.Drawing.Size(55, 13),
                    Text = projection.Movie.Name,
                    Font = new System.Drawing.Font("Arial", 24)
                };
                var premierYearText = new Label
                {
                    Text = $"Premiere Year: {projection.Movie.PremierYear}",
                    Font = new System.Drawing.Font("Arial", 14),
                    Size = new System.Drawing.Size(200, 20),
                    Location = new System.Drawing.Point(pictureBox1.Location.X + pictureBox1.Size.Width + 10, 60)
                };
                var genresText = new Label
                {
                    Text = $"Genres: {String.Join(", ", projection.Movie.Genres.Select(g => g.genreName).ToArray())}",
                    Font = new System.Drawing.Font("Arial", 14),
                    Size = new System.Drawing.Size(300, 40),
                    Location = new System.Drawing.Point(pictureBox1.Location.X + pictureBox1.Size.Width + 10, 80)
                };
                var projectionTimeText = new Label
                {
                    Text = $"Projection Date: {projection.ProjectionTime.ToShortDateString()}\nProjection Time: {projection.ProjectionTime.ToShortTimeString()}",
                    Font = new System.Drawing.Font("Arial", 14),
                    Size = new System.Drawing.Size(300, 50),
                    Location = new System.Drawing.Point(pictureBox1.Location.X + pictureBox1.Size.Width + 10, 120)
                };
                var ticketPriceText = new Label
                {
                    Text = $"Ticket price: {projection.TicketPrice} BGN",
                    Font = new System.Drawing.Font("Arial", 14),
                    Size = new System.Drawing.Size(300, 30),
                    Location = new System.Drawing.Point(pictureBox1.Location.X + pictureBox1.Size.Width + 10, 240)
                };
                var buttonBuy = new Button()
                {
                    Size = new System.Drawing.Size(270, 60),
                    Location = new System.Drawing.Point(pictureBox1.Location.X + pictureBox1.Size.Width + 10, pictureBox1.Location.Y + pictureBox1.Size.Height - 60),
                    Text = "Buy Ticket",
                    Font = new System.Drawing.Font("Arial", 24),
                    Tag = projection,
                };
                buttonBuy.Click += (sender, EventArgs) => { buy_clicked(sender, EventArgs); };
                movieNameLink.Links.Add(0, movieNameLink.Text.Length, projection.Movie.ImdbLink);
                movieNameLink.LinkClicked += linkLabel_LinkClicked;
                panel1.Controls.Add(pictureBox1);
                panel1.Controls.Add(movieNameLink);
                panel1.Controls.Add(premierYearText);
                panel1.Controls.Add(genresText);
                panel1.Controls.Add(projectionTimeText);
                panel1.Controls.Add(ticketPriceText);
                panel1.Controls.Add(buttonBuy);
                flowLayoutPanel2.Controls.Add(panel1);

                //index++;
            }
        }

        private void buy_clicked(object sender, EventArgs e)
        {
            Projection projection = (Projection) ((Control)sender).Tag;
            SceneForm frm = new SceneForm(projection);
            frm.ShowDialog();
            //Hide();
        }

        private void linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Specify that the link was visited.
            //e.Link.Visited = true;

            var target = e.Link.LinkData.ToString();
            System.Diagnostics.Process.Start(target);
        }
    }
}
