namespace AAPD7112_ST10076452_MveloKhumalo
{
    public partial class Form1 : System.Windows.Forms.Form
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Button btnReportIssues;
        private System.Windows.Forms.Button btnLocalEvents;
        private System.Windows.Forms.Button btnServiceStatus;

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
            this.btnReportIssues = new System.Windows.Forms.Button();
            this.btnLocalEvents = new System.Windows.Forms.Button();
            this.btnServiceStatus = new System.Windows.Forms.Button();
            this.SuspendLayout();

            //
            // btnReportIssues
            //
            this.btnReportIssues.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnReportIssues.Location = new System.Drawing.Point(80, 50);
            this.btnReportIssues.Name = "btnReportIssues";
            this.btnReportIssues.Size = new System.Drawing.Size(250, 50);
            this.btnReportIssues.TabIndex = 0;
            this.btnReportIssues.Text = "Report Issues";
            this.btnReportIssues.UseVisualStyleBackColor = true;
            this.btnReportIssues.Click += new System.EventHandler(this.btnReportIssues_Click);

            //
            // btnLocalEvents
            //
            this.btnLocalEvents.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnLocalEvents.Location = new System.Drawing.Point(80, 120);
            this.btnLocalEvents.Name = "btnLocalEvents";
            this.btnLocalEvents.Size = new System.Drawing.Size(250, 50);
            this.btnLocalEvents.TabIndex = 1;
            this.btnLocalEvents.Text = "Local Events and Announcements";
            this.btnLocalEvents.UseVisualStyleBackColor = true;
            this.btnLocalEvents.Click += new System.EventHandler(this.btnLocalEvents_Click);

            //
            // btnServiceStatus
            //
            this.btnServiceStatus.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnServiceStatus.Location = new System.Drawing.Point(80, 190);
            this.btnServiceStatus.Name = "btnServiceStatus";
            this.btnServiceStatus.Size = new System.Drawing.Size(250, 50);
            this.btnServiceStatus.TabIndex = 2;
            this.btnServiceStatus.Text = "Service Request Status";
            this.btnServiceStatus.UseVisualStyleBackColor = true;
            this.btnServiceStatus.Click += new System.EventHandler(this.btnServiceStatus_Click);

            //
            // MainForm
            //
            this.ClientSize = new System.Drawing.Size(420, 300);
            this.Controls.Add(this.btnServiceStatus);
            this.Controls.Add(this.btnLocalEvents);
            this.Controls.Add(this.btnReportIssues);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Municipality - Main Menu";
            this.ResumeLayout(false);
        }


        private void btnReportIssues_Click(object sender, System.EventArgs e)
        {
            ReportIssuesForm reportForm = new ReportIssuesForm();

            reportForm.Show();
        }

        private void btnLocalEvents_Click(object sender, System.EventArgs e)
        {
            EventsForm eventForm = new EventsForm();
            eventForm.Show();
        }

        private void btnServiceStatus_Click(object sender, System.EventArgs e)
        {
            ServiceRequest serviceRequest = new ServiceRequest();
            serviceRequest.Show();
        }
    }
}