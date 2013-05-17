namespace Scut
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.gridView = new System.Windows.Forms.DataGridView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonScrollLock = new System.Windows.Forms.ToolStripButton();
            this.btnRegex = new System.Windows.Forms.ToolStripButton();
            this.btnCaseSensitive = new System.Windows.Forms.ToolStripButton();
            this._textBoxSearch = new Scut.CueToolStripTextBox();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.columnsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridView
            // 
            this.gridView.AllowUserToAddRows = false;
            this.gridView.AllowUserToDeleteRows = false;
            this.gridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridView.Location = new System.Drawing.Point(0, 49);
            this.gridView.Name = "gridView";
            this.gridView.ReadOnly = true;
            this.gridView.Size = new System.Drawing.Size(748, 311);
            this.gridView.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.settingsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(748, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.openToolStripMenuItem.Text = "&Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.OpenToolStripMenuItemClick);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItemClick);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonScrollLock,
            this.btnRegex,
            this.btnCaseSensitive,
            this._textBoxSearch});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(748, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonScrollLock
            // 
            this.toolStripButtonScrollLock.Checked = true;
            this.toolStripButtonScrollLock.CheckOnClick = true;
            this.toolStripButtonScrollLock.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolStripButtonScrollLock.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonScrollLock.Image")));
            this.toolStripButtonScrollLock.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonScrollLock.Name = "toolStripButtonScrollLock";
            this.toolStripButtonScrollLock.Size = new System.Drawing.Size(81, 22);
            this.toolStripButtonScrollLock.Text = "Scroll lock";
            // 
            // btnRegex
            // 
            this.btnRegex.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnRegex.CheckOnClick = true;
            this.btnRegex.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnRegex.Image = ((System.Drawing.Image)(resources.GetObject("btnRegex.Image")));
            this.btnRegex.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRegex.Name = "btnRegex";
            this.btnRegex.Size = new System.Drawing.Size(42, 22);
            this.btnRegex.Text = "Regex";
            // 
            // btnCaseSensitive
            // 
            this.btnCaseSensitive.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnCaseSensitive.CheckOnClick = true;
            this.btnCaseSensitive.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnCaseSensitive.Image = ((System.Drawing.Image)(resources.GetObject("btnCaseSensitive.Image")));
            this.btnCaseSensitive.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCaseSensitive.Name = "btnCaseSensitive";
            this.btnCaseSensitive.Size = new System.Drawing.Size(84, 22);
            this.btnCaseSensitive.Text = "Case sensitive";
            // 
            // _textBoxSearch
            // 
            this._textBoxSearch.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this._textBoxSearch.CueText = "Search";
            this._textBoxSearch.Name = "_textBoxSearch";
            this._textBoxSearch.Size = new System.Drawing.Size(200, 25);
            this._textBoxSearch.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TextBoxSearchKeyUp);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.columnsToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // columnsToolStripMenuItem
            // 
            this.columnsToolStripMenuItem.Name = "columnsToolStripMenuItem";
            this.columnsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.columnsToolStripMenuItem.Text = "Columns";
            this.columnsToolStripMenuItem.Click += new System.EventHandler(this.ColumnsToolStripMenuItemClick);
            // 
            // MainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(748, 360);
            this.Controls.Add(this.gridView);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Scut";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainFormFormClosing);
            this.Load += new System.EventHandler(this.MainFormLoad);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainFormDragDrop);
            this.DragOver += new System.Windows.Forms.DragEventHandler(this.MainFormDragOver);
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView gridView;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButtonScrollLock;
        private CueToolStripTextBox _textBoxSearch;
        private System.Windows.Forms.ToolStripButton btnCaseSensitive;
        private System.Windows.Forms.ToolStripButton btnRegex;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem columnsToolStripMenuItem;
    }
}