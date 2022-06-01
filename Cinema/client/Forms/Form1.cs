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
        //private List<Projection> _projectionsFiltered;

        public Form1()
        {
            InitializeComponent();
            _apiClient = ApiClient.Instance;
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            ProjectionList projectionList = await _apiClient.GetProjections();
            _projections = projectionList.Data;
            ProjectionTemplate(_projections);
            LoadGenres();
        }
        
        private void ProjectionTemplate(List<Projection> projections)
        {
            foreach (Projection projection in projections) {
                ProjectionView projectionView = new ProjectionView(projection, flowLayoutPanel2);
                projectionView.Visualize();
            }
        }

        private async void LoadGenres()
        {
            GenreList genres = await _apiClient.GetGenres();
            TreeNode allNode = new TreeNode("All");
            treeView1.Nodes.Add(allNode);
            genres.Data.ForEach(genre =>
            {
                TreeNode newNode = new TreeNode(genre.GenreName);
                treeView1.Nodes.Add(newNode);
            });
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {   
            FilterChanged();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            FilterChanged();
        }

        private void FilterChanged()
        {
            //get filter values
            string genre = null;
            if (treeView1.SelectedNode != null)
            {
                genre = treeView1.SelectedNode.Text;
                label3.Text = $"Selected Genre:\n{genre}";
            }
            string searchStr = textBox1.Text.Trim();

            //clear controls
            ClearControls(flowLayoutPanel2);

            //filter projections
            List<Projection> projectionsFiltered = _projections.FindAll(projection => 
            {
                if (projection.Movie.Name.ToLower().Contains(searchStr.ToLower()) || searchStr == "")
                {
                    string[] projectionGenres = projection.Movie.Genres.Select(g => g.GenreName).ToArray();
                    if (projectionGenres.Contains(genre) || genre == null || genre == "All")
                    {
                        return true;
                    }
                }
                return false;
            });

            //visualize all found projections again
            if (projectionsFiltered.Count > 0)
            {
                ProjectionTemplate(projectionsFiltered);
            } 
            else
            {
                Label noFoundLabel = new Label()
                {
                    Text = "Nothing found",
                    Font = new System.Drawing.Font("Arial", 24),
                    Size = new Size(400, 200)
                };
                flowLayoutPanel2.Controls.Add(noFoundLabel);
            }
        }

        private void ClearControls(Control control)
        {
            /*
            foreach (Control ctrl in control.Controls)
            {
                ctrl.Dispose();
            }
            */
            control.Controls.Clear();
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
