namespace Scut
{
    partial class ContainsTextFilterControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnRemove = new System.Windows.Forms.Button();
            this.cbColor = new System.Windows.Forms.ComboBox();
            this.rbNone = new System.Windows.Forms.RadioButton();
            this.rbColor = new System.Windows.Forms.RadioButton();
            this.rbHide = new System.Windows.Forms.RadioButton();
            this.cbIgnoreCase = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbText = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSize = true;
            this.groupBox1.Controls.Add(this.btnRemove);
            this.groupBox1.Controls.Add(this.cbColor);
            this.groupBox1.Controls.Add(this.rbNone);
            this.groupBox1.Controls.Add(this.rbColor);
            this.groupBox1.Controls.Add(this.rbHide);
            this.groupBox1.Controls.Add(this.cbIgnoreCase);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.tbText);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(504, 104);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filter";
            // 
            // btnRemove
            // 
            this.btnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemove.Location = new System.Drawing.Point(421, 74);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(75, 23);
            this.btnRemove.TabIndex = 2;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.BtnRemoveClick);
            // 
            // cbColor
            // 
            this.cbColor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbColor.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbColor.FormattingEnabled = true;
            this.cbColor.Location = new System.Drawing.Point(323, 47);
            this.cbColor.Name = "cbColor";
            this.cbColor.Size = new System.Drawing.Size(173, 21);
            this.cbColor.TabIndex = 4;
            this.cbColor.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.ComboBox1DrawItem);
            // 
            // rbNone
            // 
            this.rbNone.AutoSize = true;
            this.rbNone.Checked = true;
            this.rbNone.Location = new System.Drawing.Point(43, 48);
            this.rbNone.Name = "rbNone";
            this.rbNone.Size = new System.Drawing.Size(77, 17);
            this.rbNone.TabIndex = 3;
            this.rbNone.TabStop = true;
            this.rbNone.Text = "Do nothing";
            this.rbNone.UseVisualStyleBackColor = true;
            // 
            // rbColor
            // 
            this.rbColor.AutoSize = true;
            this.rbColor.Location = new System.Drawing.Point(245, 48);
            this.rbColor.Name = "rbColor";
            this.rbColor.Size = new System.Drawing.Size(72, 17);
            this.rbColor.TabIndex = 3;
            this.rbColor.Text = "Color row:";
            this.rbColor.UseVisualStyleBackColor = true;
            this.rbColor.CheckedChanged += new System.EventHandler(this.RbColorCheckedChanged);
            // 
            // rbHide
            // 
            this.rbHide.AutoSize = true;
            this.rbHide.Location = new System.Drawing.Point(148, 48);
            this.rbHide.Name = "rbHide";
            this.rbHide.Size = new System.Drawing.Size(67, 17);
            this.rbHide.TabIndex = 3;
            this.rbHide.Text = "Hide row";
            this.rbHide.UseVisualStyleBackColor = true;
            // 
            // cbIgnoreCase
            // 
            this.cbIgnoreCase.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbIgnoreCase.AutoSize = true;
            this.cbIgnoreCase.Location = new System.Drawing.Point(419, 24);
            this.cbIgnoreCase.Name = "cbIgnoreCase";
            this.cbIgnoreCase.Size = new System.Drawing.Size(82, 17);
            this.cbIgnoreCase.TabIndex = 2;
            this.cbIgnoreCase.Text = "Ignore case";
            this.cbIgnoreCase.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "then:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "If row contains text:";
            // 
            // tbText
            // 
            this.tbText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbText.Location = new System.Drawing.Point(111, 22);
            this.tbText.Name = "tbText";
            this.tbText.Size = new System.Drawing.Size(302, 20);
            this.tbText.TabIndex = 0;
            // 
            // ContainsTextFilterControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "ContainsTextFilterControl";
            this.Size = new System.Drawing.Size(504, 104);
            this.Load += new System.EventHandler(this.FilterControlLoad);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbText;
        private System.Windows.Forms.RadioButton rbColor;
        private System.Windows.Forms.RadioButton rbHide;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbColor;
        private System.Windows.Forms.CheckBox cbIgnoreCase;
        private System.Windows.Forms.RadioButton rbNone;
        private System.Windows.Forms.Button btnRemove;

    }
}
