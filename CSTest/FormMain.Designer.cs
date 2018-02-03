namespace CSTest
{
    partial class FormMain
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonOpen = new System.Windows.Forms.Button();
            this.panelMain = new System.Windows.Forms.Panel();
            this.myView = new CSCustomDisplay.CustomView();
            this.buttonRotateCW = new System.Windows.Forms.Button();
            this.buttonRotateCCW = new System.Windows.Forms.Button();
            this.buttonRotate180 = new System.Windows.Forms.Button();
            this.buttonFlipTB = new System.Windows.Forms.Button();
            this.buttonFlipLR = new System.Windows.Forms.Button();
            this.buttonHeader = new System.Windows.Forms.Button();
            this.buttonReset = new System.Windows.Forms.Button();
            this.panelMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOpen
            // 
            this.buttonOpen.Location = new System.Drawing.Point(12, 12);
            this.buttonOpen.Name = "buttonOpen";
            this.buttonOpen.Size = new System.Drawing.Size(90, 39);
            this.buttonOpen.TabIndex = 0;
            this.buttonOpen.Text = "Open";
            this.buttonOpen.UseVisualStyleBackColor = true;
            this.buttonOpen.Click += new System.EventHandler(this.buttonOpen_Click);
            // 
            // panelMain
            // 
            this.panelMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelMain.Controls.Add(this.myView);
            this.panelMain.Location = new System.Drawing.Point(12, 57);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(760, 442);
            this.panelMain.TabIndex = 2;
            // 
            // myView
            // 
            this.myView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.myView.BackColor = System.Drawing.Color.Black;
            this.myView.Location = new System.Drawing.Point(3, 3);
            this.myView.Name = "myView";
            this.myView.Size = new System.Drawing.Size(754, 436);
            this.myView.TabIndex = 0;
            // 
            // buttonRotateCW
            // 
            this.buttonRotateCW.Location = new System.Drawing.Point(120, 12);
            this.buttonRotateCW.Name = "buttonRotateCW";
            this.buttonRotateCW.Size = new System.Drawing.Size(80, 39);
            this.buttonRotateCW.TabIndex = 3;
            this.buttonRotateCW.Text = "RotateCW";
            this.buttonRotateCW.UseVisualStyleBackColor = true;
            this.buttonRotateCW.Click += new System.EventHandler(this.buttonRotateCW_Click);
            // 
            // buttonRotateCCW
            // 
            this.buttonRotateCCW.Location = new System.Drawing.Point(206, 12);
            this.buttonRotateCCW.Name = "buttonRotateCCW";
            this.buttonRotateCCW.Size = new System.Drawing.Size(80, 39);
            this.buttonRotateCCW.TabIndex = 4;
            this.buttonRotateCCW.Text = "RotateCCW";
            this.buttonRotateCCW.UseVisualStyleBackColor = true;
            this.buttonRotateCCW.Click += new System.EventHandler(this.buttonRotateCCW_Click);
            // 
            // buttonRotate180
            // 
            this.buttonRotate180.Location = new System.Drawing.Point(292, 12);
            this.buttonRotate180.Name = "buttonRotate180";
            this.buttonRotate180.Size = new System.Drawing.Size(80, 39);
            this.buttonRotate180.TabIndex = 5;
            this.buttonRotate180.Text = "Rotate180";
            this.buttonRotate180.UseVisualStyleBackColor = true;
            this.buttonRotate180.Click += new System.EventHandler(this.buttonRotate180_Click);
            // 
            // buttonFlipTB
            // 
            this.buttonFlipTB.Location = new System.Drawing.Point(378, 12);
            this.buttonFlipTB.Name = "buttonFlipTB";
            this.buttonFlipTB.Size = new System.Drawing.Size(80, 39);
            this.buttonFlipTB.TabIndex = 6;
            this.buttonFlipTB.Text = "FlipTB";
            this.buttonFlipTB.UseVisualStyleBackColor = true;
            this.buttonFlipTB.Click += new System.EventHandler(this.buttonFlipTB_Click);
            // 
            // buttonFlipLR
            // 
            this.buttonFlipLR.Location = new System.Drawing.Point(464, 12);
            this.buttonFlipLR.Name = "buttonFlipLR";
            this.buttonFlipLR.Size = new System.Drawing.Size(80, 39);
            this.buttonFlipLR.TabIndex = 7;
            this.buttonFlipLR.Text = "FlipLR";
            this.buttonFlipLR.UseVisualStyleBackColor = true;
            this.buttonFlipLR.Click += new System.EventHandler(this.buttonFlipLR_Click);
            // 
            // buttonHeader
            // 
            this.buttonHeader.Location = new System.Drawing.Point(642, 12);
            this.buttonHeader.Name = "buttonHeader";
            this.buttonHeader.Size = new System.Drawing.Size(80, 39);
            this.buttonHeader.TabIndex = 8;
            this.buttonHeader.Text = "Header";
            this.buttonHeader.UseVisualStyleBackColor = true;
            this.buttonHeader.Click += new System.EventHandler(this.buttonHeader_Click);
            // 
            // buttonReset
            // 
            this.buttonReset.Location = new System.Drawing.Point(550, 12);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(80, 39);
            this.buttonReset.TabIndex = 9;
            this.buttonReset.Text = "Reset";
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 511);
            this.Controls.Add(this.buttonReset);
            this.Controls.Add(this.buttonHeader);
            this.Controls.Add(this.buttonFlipLR);
            this.Controls.Add(this.buttonFlipTB);
            this.Controls.Add(this.buttonRotate180);
            this.Controls.Add(this.buttonRotateCCW);
            this.Controls.Add(this.buttonRotateCW);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.buttonOpen);
            this.MinimumSize = new System.Drawing.Size(800, 550);
            this.Name = "FormMain";
            this.Text = "DicomTest";
            this.panelMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonOpen;
        private System.Windows.Forms.Panel panelMain;
        private CSCustomDisplay.CustomView myView;
        private System.Windows.Forms.Button buttonRotateCW;
        private System.Windows.Forms.Button buttonRotateCCW;
        private System.Windows.Forms.Button buttonRotate180;
        private System.Windows.Forms.Button buttonFlipTB;
        private System.Windows.Forms.Button buttonFlipLR;
        private System.Windows.Forms.Button buttonHeader;
        private System.Windows.Forms.Button buttonReset;
    }
}

