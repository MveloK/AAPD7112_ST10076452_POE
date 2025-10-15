using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;

namespace AAPD7112_ST10076452_MveloKhumalo
{
    public partial class EventsForm : Form
    {
        private System.ComponentModel.IContainer components = null;
        private DataGridView dgvEvents;
        private TextBox txtKeywordSearch;
        private ComboBox cmbCategory;
        private DateTimePicker dtpDateSearch;
        private Button btnSearch;
        private Button btnClear;
        private ListBox lbRecommendations;
        private Label lblPriorityQueueDemo;
        private Panel pnlSearch;
        private SplitContainer splitContainer;
        private Panel pnlBottom;
        private Label lblKeyword;
        private Label lblCategory;
        private Label lblDate;
        private Label lblRecommendationsHeader;
        private Label lblPriorityHeader;

        private EventManager _eventManager;
        private Stack<Event> _currentFilteredStack;

        public EventsForm()
        {
            InitializeComponent();

            _eventManager = new EventManager();
            _currentFilteredStack = new Stack<Event>();

            LoadInitialData();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

       
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();

            this.BackColor = Color.FromArgb(245, 247, 250);
            this.Font = new Font("Segoe UI", 10F);
            this.ClientSize = new Size(950, 700);
            this.Text = "Local Events & Announcements";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            this.pnlSearch = new Panel
            {
                Dock = DockStyle.Top,
                Height = 110,
                Padding = new Padding(20),
                BackColor = Color.FromArgb(67, 97, 238)
            };

            lblKeyword = new Label
            {
                Text = "🔍 Keyword",
                ForeColor = Color.White,
                Location = new Point(10, 15),
                AutoSize = true,
                Font = new Font("Segoe UI Semibold", 10.5F)
            };

            txtKeywordSearch = new TextBox
            {
                Location = new Point(10, 45),
                Width = 180,
                BorderStyle = BorderStyle.FixedSingle
            };

            lblCategory = new Label
            {
                Text = "📂 Category",
                ForeColor = Color.White,
                Location = new Point(210, 15),
                AutoSize = true,
                Font = new Font("Segoe UI Semibold", 10.5F)
            };

            cmbCategory = new ComboBox
            {
                Location = new Point(210, 45),
                Width = 180,
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            lblDate = new Label
            {
                Text = "📅 Date",
                ForeColor = Color.White,
                Location = new Point(410, 15),
                AutoSize = true,
                Font = new Font("Segoe UI Semibold", 10.5F)
            };

            dtpDateSearch = new DateTimePicker
            {
                Location = new Point(410, 45),
                Width = 150,
                Format = DateTimePickerFormat.Short,
                ShowCheckBox = true
            };

            btnSearch = new Button
            {
                Text = "Search",
                Location = new Point(580, 40),
                Size = new Size(100, 40),
                BackColor = Color.FromArgb(34, 166, 179),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI Semibold", 10F)
            };
            btnSearch.FlatAppearance.BorderSize = 0;
            btnSearch.Cursor = Cursors.Hand;
            btnSearch.Click += new EventHandler(this.BtnSearch_Click);

            btnClear = new Button
            {
                Text = "Clear",
                Location = new Point(690, 40),
                Size = new Size(100, 40),
                BackColor = Color.FromArgb(120, 120, 120),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI Semibold", 10F)
            };
            btnClear.FlatAppearance.BorderSize = 0;
            btnClear.Cursor = Cursors.Hand;
            btnClear.Click += new EventHandler(this.BtnClear_Click);

            pnlSearch.Controls.AddRange(new Control[]
            {
                lblKeyword, txtKeywordSearch,
                lblCategory, cmbCategory,
                lblDate, dtpDateSearch,
                btnSearch, btnClear
            });

            splitContainer = new SplitContainer
            {
                Dock = DockStyle.Fill,
                Orientation = Orientation.Horizontal,
                SplitterDistance = 420,
                BackColor = Color.Transparent
            };

            dgvEvents = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.FromArgb(67, 97, 238),
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI Semibold", 10F)
                },
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.FromArgb(250, 250, 250),
                    SelectionBackColor = Color.FromArgb(220, 230, 250),
                    Font = new Font("Segoe UI", 10F)
                },
                RowHeadersVisible = false,
                GridColor = Color.LightGray
            };
            dgvEvents.CellDoubleClick += new DataGridViewCellEventHandler(this.DgvEvents_CellDoubleClick);
            dgvEvents.DataBindingComplete += new DataGridViewBindingCompleteEventHandler(this.DgvEvents_DataBindingComplete);

            splitContainer.Panel1.Controls.Add(dgvEvents);

            pnlBottom = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(20)
            };

            lblRecommendationsHeader = new Label
            {
                Text = "🎯 Recommended Events ",
                Font = new Font("Segoe UI Semibold", 11F),
                AutoSize = true,
                Location = new Point(0, 0)
            };

            lbRecommendations = new ListBox
            {
                Location = new Point(0, 30),
                Size = new Size(300, 100),
                BackColor = Color.FromArgb(250, 250, 250),
                BorderStyle = BorderStyle.FixedSingle,
                Font = new Font("Segoe UI", 9.5F)
            };

            lblPriorityHeader = new Label
            {
                Text = "⚡ Priority Announcements",
                Font = new Font("Segoe UI Semibold", 11F),
                Location = new Point(320, 0),
                AutoSize = true
            };

            lblPriorityQueueDemo = new Label
            {
                Location = new Point(320, 30),
                Size = new Size(540, 100),
                BackColor = Color.FromArgb(255, 243, 230),
                Padding = new Padding(10),
                BorderStyle = BorderStyle.FixedSingle,
                Font = new Font("Segoe UI", 9.5F),
                Text = "Priority Announcements will be displayed here..."
            };

            pnlBottom.Controls.AddRange(new Control[]
            {
                lblRecommendationsHeader, lbRecommendations,
                lblPriorityHeader, lblPriorityQueueDemo
            });

            splitContainer.Panel2.Controls.Add(pnlBottom);

            this.Controls.Add(splitContainer);
            this.Controls.Add(pnlSearch);
        }

      

        private void LoadInitialData()
        {
            cmbCategory.Items.Add("All Categories");
            cmbCategory.Items.AddRange(_eventManager.Categories.ToArray());
            cmbCategory.SelectedIndex = 0;

            DisplayEvents(_eventManager.GetAllEvents());
            DisplayPriorityAnnouncements();
            UpdateRecommendationsDisplay();
        }

        private void DisplayEvents(List<Event> events)
        {
            _currentFilteredStack.Clear();
            foreach (var evt in events)
                _currentFilteredStack.Push(evt);

            dgvEvents.DataSource = events.Select(e => new
            {
                e.Title,
                Date = e.Date.ToShortDateString(),
                e.Category,
                e.Description
            }).ToList();
        }

        private void DgvEvents_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                dgvEvents.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

                if (dgvEvents.Columns.Contains("Title"))
                    dgvEvents.Columns["Title"].Width = 200;
                if (dgvEvents.Columns.Contains("Date"))
                    dgvEvents.Columns["Date"].Width = 120;
                if (dgvEvents.Columns.Contains("Category"))
                    dgvEvents.Columns["Category"].Width = 150;
                if (dgvEvents.Columns.Contains("Description"))
                    dgvEvents.Columns["Description"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error customizing DataGridView: {ex.Message}");
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            string keyword = txtKeywordSearch.Text;
            string category = cmbCategory.SelectedItem?.ToString();
            DateTime? date = dtpDateSearch.Checked ? (DateTime?)dtpDateSearch.Value : null;

            var results = _eventManager.SearchEvents(keyword, category, date);
            DisplayEvents(results);
            UpdateRecommendationsDisplay();
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            txtKeywordSearch.Clear();
            cmbCategory.SelectedIndex = 0;
            dtpDateSearch.Checked = false;
            DisplayEvents(_eventManager.GetAllEvents());
        }

        private void UpdateRecommendationsDisplay()
        {
            var recommendations = _eventManager.GetRecommendations();
            lbRecommendations.Items.Clear();
            if (recommendations.Any())
            {
                foreach (var rec in recommendations)
                    lbRecommendations.Items.Add(rec.ToString());
            }
            else
            {
                lbRecommendations.Items.Add("Search for an event to get recommendations!");
            }
        }

        private void DisplayPriorityAnnouncements()
        {
            var announcements = _eventManager.GetPriorityAnnouncements();
            if (announcements.Any())
            {
                string text = "Upcoming Important Announcements (Sorted by Priority):\n\n";
                foreach (var a in announcements)
                {
                    text += $"⭐ {a.Title} (Priority: {a.Priority}): {a.Description}\n";
                }
                lblPriorityQueueDemo.Text = text;
            }
            else
            {
                lblPriorityQueueDemo.Text = "No priority announcements at this time.";
            }
        }

        private void DgvEvents_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvEvents.Rows.Count > e.RowIndex)
            {
                var row = dgvEvents.Rows[e.RowIndex];

                string title = row.Cells["Title"].Value?.ToString() ?? "N/A";
                string description = row.Cells["Description"].Value?.ToString() ?? "No description available.";

                MessageBox.Show($"Title: {title}\n\nDetails:\n{description}",
                    "Event Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
