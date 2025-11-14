namespace AAPD7112_ST10076452_MveloKhumalo
{
    partial class ServiceRequest
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView requestsGridView;
        private System.Windows.Forms.TextBox txtRequestId;
        private System.Windows.Forms.Button btnTrack;
        private System.Windows.Forms.Label lblStatusOutput;
        private System.Windows.Forms.ListBox priorityRequestsList;

       
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.requestsGridView = new System.Windows.Forms.DataGridView();
            this.txtRequestId = new System.Windows.Forms.TextBox();
            this.btnTrack = new System.Windows.Forms.Button();
            this.lblStatusOutput = new System.Windows.Forms.Label();
            this.priorityRequestsList = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.requestsGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // requestsGridView
            // 
            this.requestsGridView.Location = new System.Drawing.Point(20, 100);
            this.requestsGridView.Name = "requestsGridView";
            this.requestsGridView.Size = new System.Drawing.Size(400, 300);
            this.requestsGridView.TabIndex = 0;
            // 
            // txtRequestId
            // 
            this.txtRequestId.Location = new System.Drawing.Point(440, 25);
            this.txtRequestId.Name = "txtRequestId";
            this.txtRequestId.Size = new System.Drawing.Size(120, 20);
            this.txtRequestId.TabIndex = 1;
            // 
            // btnTrack
            // 
            this.btnTrack.Location = new System.Drawing.Point(565, 23);
            this.btnTrack.Name = "btnTrack";
            this.btnTrack.Size = new System.Drawing.Size(100, 23);
            this.btnTrack.TabIndex = 2;
            this.btnTrack.Text = "Track Request";
            this.btnTrack.UseVisualStyleBackColor = true;
            this.btnTrack.Click += new System.EventHandler(this.btnTrack_Click);
            // 
            // lblStatusOutput
            // 
            this.lblStatusOutput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblStatusOutput.Location = new System.Drawing.Point(440, 60);
            this.lblStatusOutput.Name = "lblStatusOutput";
            this.lblStatusOutput.Size = new System.Drawing.Size(330, 100);
            this.lblStatusOutput.TabIndex = 3;
            this.lblStatusOutput.Text = "Enter Request ID to track status.";
            // 
            // priorityRequestsList
            // 
            this.priorityRequestsList.FormattingEnabled = true;
            this.priorityRequestsList.Location = new System.Drawing.Point(440, 180);
            this.priorityRequestsList.Name = "priorityRequestsList";
            this.priorityRequestsList.Size = new System.Drawing.Size(330, 220);
            this.priorityRequestsList.TabIndex = 4;
            // 
            // ServiceRequest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.priorityRequestsList);
            this.Controls.Add(this.lblStatusOutput);
            this.Controls.Add(this.btnTrack);
            this.Controls.Add(this.txtRequestId);
            this.Controls.Add(this.requestsGridView);
            this.Name = "ServiceRequest";
            this.Text = "Service Request Status Tracker (BST, Heap, Graph)";
            ((System.ComponentModel.ISupportInitialize)(this.requestsGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}