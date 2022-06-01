using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cinema
{
    public class ProjectionView : IView
    {
        private readonly Control _control = null;
        private readonly Projection _projection = null;

        public ProjectionView(Projection projection, Control control)
        {
            _projection = projection;
            _control = control;
        }

        public void Visualize()
        {
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
                ImageLocation = _projection.Movie.CoverImage,
            };
            var movieNameLink = new LinkLabel()
            {
                AutoSize = true,
                Location = new System.Drawing.Point(pictureBox1.Location.X + pictureBox1.Size.Width + 10, 10),
                Name = "linkLabel1",
                Size = new System.Drawing.Size(55, 13),
                Text = _projection.Movie.Name,
                Font = new System.Drawing.Font("Arial", 24)
            };
            var premierYearText = new Label
            {
                Text = $"Premiere Year: {_projection.Movie.PremierYear}",
                Font = new System.Drawing.Font("Arial", 14),
                Size = new System.Drawing.Size(200, 20),
                Location = new System.Drawing.Point(pictureBox1.Location.X + pictureBox1.Size.Width + 10, 60)
            };
            var genresText = new Label
            {
                Text = $"Genres: {String.Join(", ", _projection.Movie.Genres.Select(g => g.genreName).ToArray())}",
                Font = new System.Drawing.Font("Arial", 14),
                Size = new System.Drawing.Size(300, 40),
                Location = new System.Drawing.Point(pictureBox1.Location.X + pictureBox1.Size.Width + 10, 80)
            };
            var projectionTimeText = new Label
            {
                Text = $"Projection Date: {_projection.ProjectionTime.ToShortDateString()}\nProjection Time: {_projection.ProjectionTime.ToShortTimeString()}",
                Font = new System.Drawing.Font("Arial", 14),
                Size = new System.Drawing.Size(300, 50),
                Location = new System.Drawing.Point(pictureBox1.Location.X + pictureBox1.Size.Width + 10, 120)
            };
            var ticketPriceText = new Label
            {
                Text = $"Ticket price: {_projection.TicketPrice} BGN",
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
                Tag = _projection,
            };
            buttonBuy.Click += (sender, EventArgs) => { buy_clicked(sender, EventArgs); };
            movieNameLink.Links.Add(0, movieNameLink.Text.Length, _projection.Movie.ImdbLink);
            movieNameLink.LinkClicked += linkLabel_LinkClicked;
            panel1.Controls.Add(pictureBox1);
            panel1.Controls.Add(movieNameLink);
            panel1.Controls.Add(premierYearText);
            panel1.Controls.Add(genresText);
            panel1.Controls.Add(projectionTimeText);
            panel1.Controls.Add(ticketPriceText);
            panel1.Controls.Add(buttonBuy);
            _control.Controls.Add(panel1);
        }

        private void buy_clicked(object sender, EventArgs e)
        {
            Projection projection = (Projection)((Control)sender).Tag;
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
