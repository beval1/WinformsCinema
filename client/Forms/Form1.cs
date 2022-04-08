using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cinema
{
    public partial class Form1 : Form
    {
        private ApiClient _apiClient;
        private List<Projection> _projections;
        //private ProjectionList projectionList;

        public Form1()
        {
            InitializeComponent();
            _apiClient = new ApiClient();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            ProjectionList projectionList = await _apiClient.GetProjections();
            _projections = projectionList.data;
            ProjectionTemplate();
        }
        
        private void ProjectionTemplate()
        {
            var panel1 = new Panel()
            {
                Name = "panel1",
                TabIndex = 0,
                Size = new System.Drawing.Size(139, 117),
                BorderStyle = BorderStyle.FixedSingle,
            };
            var pictureBox1 = new PictureBox()
            {
                Location = new System.Drawing.Point(6, 3),
                Name = "pictureBox1",
                Size = new System.Drawing.Size(130, 98),
                TabIndex = 1,
                TabStop = false,
                ImageLocation = "",
            };
            var linkLabel1 = new LinkLabel()
            {
                AutoSize = true,
                Location = new System.Drawing.Point(3, 104),
                Name = "linkLabel1",
                Size = new System.Drawing.Size(55, 13),
                TabIndex = 0,
                TabStop = true,
                Text = "linkLabel1",
            };
            panel1.Controls.Add(pictureBox1);
            panel1.Controls.Add(linkLabel1);
            flowLayoutPanel2.Controls.Add(panel1);
            //linkLabel1.Links.Add(0, linkLabel1.Text.Length, _movies[0].imdbLink);
            linkLabel1.LinkClicked += linkLabel_LinkClicked;


            var panel2 = new Panel()
            {
                Name = "panel2",
                TabIndex = 0,
                Size = new System.Drawing.Size(139, 117),
                BorderStyle = BorderStyle.FixedSingle
            };
            var pictureBox2 = new PictureBox()
            {
                Location = new System.Drawing.Point(6, 3),
                Name = "pictureBox2",
                Size = new System.Drawing.Size(130, 98),
                TabIndex = 1,
                TabStop = false,
                ImageLocation = "",
            };
            var linkLabel2 = new LinkLabel()
            {
                AutoSize = true,
                //Location = new System.Drawing.Point(3, pictureBox2.Location.Y + pictureBox2.Size.Height - 5),
                Location = new System.Drawing.Point(3, 104),
                Name = "linkLabel2",
                Size = new System.Drawing.Size(55, 13),
                TabIndex = 0,
                TabStop = true,
                Text = "linkLabel2",
            };
            panel2.Controls.Add(pictureBox2);
            panel2.Controls.Add(linkLabel2);
            flowLayoutPanel2.Controls.Add(panel2);
            //linkLabel2.Links.Add(0, linkLabel2.Text.Length, "https://google.com");
            linkLabel2.LinkClicked += linkLabel_LinkClicked;


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
