namespace CSTest
{
    partial class TagViewer
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
            this.textBoxSearch = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.listViewAllHeader = new System.Windows.Forms.ListView();
            this.TagName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.VRValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ValueLength = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TagValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxSearch
            // 
            this.textBoxSearch.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxSearch.Location = new System.Drawing.Point(12, 12);
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Size = new System.Drawing.Size(246, 22);
            this.textBoxSearch.TabIndex = 0;
            this.textBoxSearch.TextChanged += new System.EventHandler(this.textBoxSearch_TextChanged);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.listViewAllHeader);
            this.panel1.Location = new System.Drawing.Point(12, 39);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(760, 460);
            this.panel1.TabIndex = 1;
            // 
            // listViewAllHeader
            // 
            this.listViewAllHeader.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewAllHeader.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.TagName,
            this.VRValue,
            this.ValueLength,
            this.TagValue});
            this.listViewAllHeader.Location = new System.Drawing.Point(3, 3);
            this.listViewAllHeader.MultiSelect = false;
            this.listViewAllHeader.Name = "listViewAllHeader";
            this.listViewAllHeader.Size = new System.Drawing.Size(754, 454);
            this.listViewAllHeader.TabIndex = 0;
            this.listViewAllHeader.UseCompatibleStateImageBehavior = false;
            this.listViewAllHeader.View = System.Windows.Forms.View.Details;
            // 
            // TagName
            // 
            this.TagName.Text = "Tag Name";
            this.TagName.Width = 200;
            // 
            // VRValue
            // 
            this.VRValue.Text = "VR";
            this.VRValue.Width = 40;
            // 
            // ValueLength
            // 
            this.ValueLength.Text = "Length";
            // 
            // TagValue
            // 
            this.TagValue.Text = "Tag Value";
            this.TagValue.Width = 290;
            // 
            // TagViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 511);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.textBoxSearch);
            this.MinimumSize = new System.Drawing.Size(800, 550);
            this.Name = "TagViewer";
            this.Text = "TagViewer";
            this.Load += new System.EventHandler(this.AllHeaderView_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxSearch;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListView listViewAllHeader;
        private System.Windows.Forms.ColumnHeader TagName;
        private System.Windows.Forms.ColumnHeader VRValue;
        private System.Windows.Forms.ColumnHeader ValueLength;
        private System.Windows.Forms.ColumnHeader TagValue;
    }
}