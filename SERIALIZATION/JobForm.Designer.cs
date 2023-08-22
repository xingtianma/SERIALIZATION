namespace SERIALIZATION
{
    partial class JobForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.table = new System.Windows.Forms.DataGridView();
            this.revText = new System.Windows.Forms.TextBox();
            this.poText = new System.Windows.Forms.TextBox();
            this.advancedSearchButton = new System.Windows.Forms.Button();
            this.revBox = new System.Windows.Forms.CheckBox();
            this.poBox = new System.Windows.Forms.CheckBox();
            this.jobText = new System.Windows.Forms.TextBox();
            this.jobBox = new System.Windows.Forms.CheckBox();
            this.partText = new System.Windows.Forms.TextBox();
            this.partBox = new System.Windows.Forms.CheckBox();
            this.addPartJobButton = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.table)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.table);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(985, 426);
            this.panel1.TabIndex = 0;
            // 
            // table
            // 
            this.table.AllowUserToAddRows = false;
            this.table.AllowUserToDeleteRows = false;
            this.table.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.table.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.table.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.table.Location = new System.Drawing.Point(0, 0);
            this.table.Name = "table";
            this.table.ReadOnly = true;
            this.table.Size = new System.Drawing.Size(985, 426);
            this.table.TabIndex = 0;
            this.table.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.table_CellClick);
            // 
            // revText
            // 
            this.revText.Location = new System.Drawing.Point(289, 477);
            this.revText.Name = "revText";
            this.revText.Size = new System.Drawing.Size(172, 20);
            this.revText.TabIndex = 22;
            // 
            // poText
            // 
            this.poText.Location = new System.Drawing.Point(59, 478);
            this.poText.Name = "poText";
            this.poText.Size = new System.Drawing.Size(170, 20);
            this.poText.TabIndex = 21;
            // 
            // advancedSearchButton
            // 
            this.advancedSearchButton.Location = new System.Drawing.Point(903, 512);
            this.advancedSearchButton.Name = "advancedSearchButton";
            this.advancedSearchButton.Size = new System.Drawing.Size(75, 23);
            this.advancedSearchButton.TabIndex = 20;
            this.advancedSearchButton.Text = "SEARCH";
            this.advancedSearchButton.UseVisualStyleBackColor = true;
            this.advancedSearchButton.Click += new System.EventHandler(this.advancedSearchButton_Click);
            // 
            // revBox
            // 
            this.revBox.AutoSize = true;
            this.revBox.Location = new System.Drawing.Point(235, 480);
            this.revBox.Name = "revBox";
            this.revBox.Size = new System.Drawing.Size(48, 17);
            this.revBox.TabIndex = 19;
            this.revBox.Text = "REV";
            this.revBox.UseVisualStyleBackColor = true;
            // 
            // poBox
            // 
            this.poBox.AutoSize = true;
            this.poBox.Location = new System.Drawing.Point(2, 481);
            this.poBox.Name = "poBox";
            this.poBox.Size = new System.Drawing.Size(51, 17);
            this.poBox.TabIndex = 18;
            this.poBox.Text = "PO #";
            this.poBox.UseVisualStyleBackColor = true;
            // 
            // jobText
            // 
            this.jobText.Location = new System.Drawing.Point(529, 476);
            this.jobText.Name = "jobText";
            this.jobText.Size = new System.Drawing.Size(181, 20);
            this.jobText.TabIndex = 24;
            // 
            // jobBox
            // 
            this.jobBox.AutoSize = true;
            this.jobBox.Location = new System.Drawing.Point(467, 479);
            this.jobBox.Name = "jobBox";
            this.jobBox.Size = new System.Drawing.Size(56, 17);
            this.jobBox.TabIndex = 23;
            this.jobBox.Text = "JOB #";
            this.jobBox.UseVisualStyleBackColor = true;
            // 
            // partText
            // 
            this.partText.Location = new System.Drawing.Point(785, 476);
            this.partText.Name = "partText";
            this.partText.Size = new System.Drawing.Size(193, 20);
            this.partText.TabIndex = 27;
            // 
            // partBox
            // 
            this.partBox.AutoSize = true;
            this.partBox.Location = new System.Drawing.Point(714, 478);
            this.partBox.Name = "partBox";
            this.partBox.Size = new System.Drawing.Size(65, 17);
            this.partBox.TabIndex = 26;
            this.partBox.Text = "PART #";
            this.partBox.UseVisualStyleBackColor = true;
            // 
            // addPartJobButton
            // 
            this.addPartJobButton.Location = new System.Drawing.Point(785, 512);
            this.addPartJobButton.Name = "addPartJobButton";
            this.addPartJobButton.Size = new System.Drawing.Size(110, 23);
            this.addPartJobButton.TabIndex = 28;
            this.addPartJobButton.Text = "ADD PART JOB";
            this.addPartJobButton.UseVisualStyleBackColor = true;
            this.addPartJobButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // JobForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1005, 539);
            this.Controls.Add(this.addPartJobButton);
            this.Controls.Add(this.partText);
            this.Controls.Add(this.partBox);
            this.Controls.Add(this.jobText);
            this.Controls.Add(this.jobBox);
            this.Controls.Add(this.revText);
            this.Controls.Add(this.poText);
            this.Controls.Add(this.advancedSearchButton);
            this.Controls.Add(this.revBox);
            this.Controls.Add(this.poBox);
            this.Controls.Add(this.panel1);
            this.Name = "JobForm";
            this.Text = "Jobs";
            this.Load += new System.EventHandler(this.JobForm_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.table)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
            this.FormClosed += MyClosedHandler;

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView table;
        private System.Windows.Forms.TextBox revText;
        private System.Windows.Forms.TextBox poText;
        private System.Windows.Forms.Button advancedSearchButton;
        private System.Windows.Forms.CheckBox revBox;
        private System.Windows.Forms.CheckBox poBox;
        private System.Windows.Forms.TextBox jobText;
        private System.Windows.Forms.CheckBox jobBox;
        private System.Windows.Forms.TextBox partText;
        private System.Windows.Forms.CheckBox partBox;
        private System.Windows.Forms.Button addPartJobButton;
    }
}