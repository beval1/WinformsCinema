using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq;

namespace Cinema
{
    public partial class Form1 : Form
    {
        private ApiClient _apiClient;
        private List<Projection> _projections;

        public Form1()
        {
            InitializeComponent();
            _apiClient = new ApiClient();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            ProjectionList projectionList = await _apiClient.GetProjections();
            _projections = projectionList.data;
            foreach(Projection projection in _projections) {
                Console.WriteLine(projection.Id);
                Console.WriteLine(projection.SceneId);
                Console.WriteLine(projection.SceneSeats);
                Console.WriteLine(projection.Movie.MovieName);
                Console.WriteLine(projection.ProjectionTime);
                Console.WriteLine(projection.Movie.Genres[0].genre_name);
            }
            ProjectionTemplate();
        }
        
        private void ProjectionTemplate()
        {
            foreach (Projection projection in _projections) {
                var panel1 = new Panel()
                {
                    Name = "panel1",
                    TabIndex = 0,
                    Size = new System.Drawing.Size(550, 340),
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
                    Text = projection.Movie.MovieName,
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
                    Text = $"Genres: ",
                    Font = new System.Drawing.Font("Arial", 14),
                    Size = new System.Drawing.Size(200, 20),
                    Location = new System.Drawing.Point(pictureBox1.Location.X + pictureBox1.Size.Width + 10, 80)
                };
                var projectionTimeText = new Label
                {
                    Text = $"Projection Date: {projection.ProjectionTime.ToShortDateString()}\nProjection Time: {projection.ProjectionTime.ToShortTimeString()}",
                    Font = new System.Drawing.Font("Arial", 14),
                    Size = new System.Drawing.Size(300, 60),
                    Location = new System.Drawing.Point(pictureBox1.Location.X + pictureBox1.Size.Width + 10, 120)
                };
                var buttonBuy = new Button()
                {
                    Size = new System.Drawing.Size(270, 60),
                    Location = new System.Drawing.Point(pictureBox1.Location.X + pictureBox1.Size.Width + 10, pictureBox1.Location.Y + pictureBox1.Size.Height - 60),
                    Text = "Buy Ticket",
                    Font = new System.Drawing.Font("Arial", 24),
                };
                movieNameLink.Links.Add(0, movieNameLink.Text.Length, projection.Movie.MovieName);
                movieNameLink.LinkClicked += linkLabel_LinkClicked;
                panel1.Controls.Add(pictureBox1);
                panel1.Controls.Add(movieNameLink);
                panel1.Controls.Add(premierYearText);
                panel1.Controls.Add(genresText);
                panel1.Controls.Add(projectionTimeText);
                panel1.Controls.Add(buttonBuy);
                flowLayoutPanel2.Controls.Add(panel1);
                

            }
        }

        private void linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Specify that the link was visited.
            //e.Link.Visited = true;

            var target = e.Link.LinkData as string;
            System.Diagnostics.Process.Start(target);
        }
    }
}
