namespace IsStrongOrSigned
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.kryptonSplitContainer1 = new Krypton.Toolkit.KryptonSplitContainer();
            this.dgvFileProperties = new Krypton.Toolkit.KryptonDataGridView();
            this.btnRemovelAll = new Krypton.Toolkit.KryptonButton();
            this.lstLog = new Krypton.Toolkit.KryptonListBox();
            this.btnRemoveSelected = new Krypton.Toolkit.KryptonButton();
            this.btnAdd = new Krypton.Toolkit.KryptonButton();
            this.progressDialog1 = new Ookii.Dialogs.WinForms.ProgressDialog(this.components);
            this.FileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Version = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StrongNamed = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StrongVerified = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CopyRight = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BinVersion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DigStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FullPath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainer1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainer1.Panel1)).BeginInit();
            this.kryptonSplitContainer1.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainer1.Panel2)).BeginInit();
            this.kryptonSplitContainer1.Panel2.SuspendLayout();
            this.kryptonSplitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFileProperties)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonSplitContainer1
            // 
            this.kryptonSplitContainer1.Cursor = System.Windows.Forms.Cursors.Default;
            this.kryptonSplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonSplitContainer1.Location = new System.Drawing.Point(0, 0);
            this.kryptonSplitContainer1.Name = "kryptonSplitContainer1";
            this.kryptonSplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // kryptonSplitContainer1.Panel1
            // 
            this.kryptonSplitContainer1.Panel1.Controls.Add(this.dgvFileProperties);
            // 
            // kryptonSplitContainer1.Panel2
            // 
            this.kryptonSplitContainer1.Panel2.Controls.Add(this.btnRemovelAll);
            this.kryptonSplitContainer1.Panel2.Controls.Add(this.lstLog);
            this.kryptonSplitContainer1.Panel2.Controls.Add(this.btnRemoveSelected);
            this.kryptonSplitContainer1.Panel2.Controls.Add(this.btnAdd);
            this.kryptonSplitContainer1.Size = new System.Drawing.Size(1192, 453);
            this.kryptonSplitContainer1.SplitterDistance = 267;
            this.kryptonSplitContainer1.TabIndex = 0;
            // 
            // dgvFileProperties
            // 
            this.dgvFileProperties.AllowDrop = true;
            this.dgvFileProperties.AllowUserToAddRows = false;
            this.dgvFileProperties.AllowUserToResizeRows = false;
            this.dgvFileProperties.ColumnHeadersHeight = 36;
            this.dgvFileProperties.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvFileProperties.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FileName,
            this.Version,
            this.Type,
            this.StrongNamed,
            this.StrongVerified,
            this.CopyRight,
            this.BinVersion,
            this.DigStatus,
            this.FullPath});
            this.dgvFileProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvFileProperties.Location = new System.Drawing.Point(0, 0);
            this.dgvFileProperties.Name = "dgvFileProperties";
            this.dgvFileProperties.ReadOnly = true;
            this.dgvFileProperties.RowHeadersVisible = false;
            this.dgvFileProperties.RowHeadersWidth = 51;
            this.dgvFileProperties.RowTemplate.Height = 24;
            this.dgvFileProperties.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvFileProperties.Size = new System.Drawing.Size(1192, 267);
            this.dgvFileProperties.TabIndex = 0;
            this.dgvFileProperties.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvFileProperties_CellMouseDoubleClick);
            this.dgvFileProperties.SelectionChanged += new System.EventHandler(this.dgvFileProperties_SelectionChanged);
            // 
            // btnRemovelAll
            // 
            this.btnRemovelAll.CornerRoundingRadius = -1F;
            this.btnRemovelAll.Enabled = false;
            this.btnRemovelAll.Location = new System.Drawing.Point(1038, 5);
            this.btnRemovelAll.Name = "btnRemovelAll";
            this.btnRemovelAll.Size = new System.Drawing.Size(154, 29);
            this.btnRemovelAll.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemovelAll.TabIndex = 2;
            this.btnRemovelAll.Values.Text = "Remove &All";
            this.btnRemovelAll.Click += new System.EventHandler(this.btnRemovelAll_Click);
            // 
            // lstLog
            // 
            this.lstLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstLog.Location = new System.Drawing.Point(0, 36);
            this.lstLog.Name = "lstLog";
            this.lstLog.Size = new System.Drawing.Size(1192, 133);
            this.lstLog.TabIndex = 3;
            // 
            // btnRemoveSelected
            // 
            this.btnRemoveSelected.CornerRoundingRadius = -1F;
            this.btnRemoveSelected.Enabled = false;
            this.btnRemoveSelected.Location = new System.Drawing.Point(847, 5);
            this.btnRemoveSelected.Name = "btnRemoveSelected";
            this.btnRemoveSelected.Size = new System.Drawing.Size(154, 29);
            this.btnRemoveSelected.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemoveSelected.TabIndex = 1;
            this.btnRemoveSelected.Values.Text = "Remove &Selected";
            this.btnRemoveSelected.Click += new System.EventHandler(this.btnRemoveSelected_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.CornerRoundingRadius = -1F;
            this.btnAdd.Location = new System.Drawing.Point(0, 3);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(90, 29);
            this.btnAdd.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Values.Text = "&Add ...";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // progressDialog1
            // 
            this.progressDialog1.MinimizeBox = false;
            this.progressDialog1.ShowCancelButton = false;
            this.progressDialog1.ShowTimeRemaining = true;
            this.progressDialog1.Text = "Progress:";
            this.progressDialog1.WindowTitle = "Processing Files:";
            this.progressDialog1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.ProgressDialog_DoWork);
            this.progressDialog1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.progressDialog1_RunWorkerCompleted);
            // 
            // FileName
            // 
            this.FileName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.FileName.Frozen = true;
            this.FileName.HeaderText = "File Name";
            this.FileName.MinimumWidth = 6;
            this.FileName.Name = "FileName";
            this.FileName.ReadOnly = true;
            this.FileName.Width = 109;
            // 
            // Version
            // 
            this.Version.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Version.HeaderText = "CRT / .Net";
            this.Version.MinimumWidth = 108;
            this.Version.Name = "Version";
            this.Version.ReadOnly = true;
            this.Version.Width = 108;
            // 
            // Type
            // 
            this.Type.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Type.HeaderText = "Type";
            this.Type.MinimumWidth = 73;
            this.Type.Name = "Type";
            this.Type.ReadOnly = true;
            this.Type.Width = 73;
            // 
            // StrongNamed
            // 
            this.StrongNamed.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.StrongNamed.HeaderText = "Strong Named";
            this.StrongNamed.MinimumWidth = 139;
            this.StrongNamed.Name = "StrongNamed";
            this.StrongNamed.ReadOnly = true;
            this.StrongNamed.Width = 139;
            // 
            // StrongVerified
            // 
            this.StrongVerified.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.StrongVerified.HeaderText = "Strong Verified";
            this.StrongVerified.MinimumWidth = 141;
            this.StrongVerified.Name = "StrongVerified";
            this.StrongVerified.ReadOnly = true;
            this.StrongVerified.Width = 141;
            // 
            // CopyRight
            // 
            this.CopyRight.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.CopyRight.HeaderText = "© Legal CopyRight";
            this.CopyRight.MinimumWidth = 168;
            this.CopyRight.Name = "CopyRight";
            this.CopyRight.ReadOnly = true;
            this.CopyRight.Width = 168;
            // 
            // BinVersion
            // 
            this.BinVersion.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.BinVersion.HeaderText = "Version";
            this.BinVersion.MinimumWidth = 90;
            this.BinVersion.Name = "BinVersion";
            this.BinVersion.ReadOnly = true;
            this.BinVersion.Width = 90;
            // 
            // DigStatus
            // 
            this.DigStatus.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.DigStatus.HeaderText = "Digital Status";
            this.DigStatus.MinimumWidth = 131;
            this.DigStatus.Name = "DigStatus";
            this.DigStatus.ReadOnly = true;
            this.DigStatus.ToolTipText = "Time is formatted in \"Locale\" offset";
            this.DigStatus.Width = 131;
            // 
            // FullPath
            // 
            this.FullPath.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.FullPath.HeaderText = "Full Path";
            this.FullPath.MinimumWidth = 500;
            this.FullPath.Name = "FullPath";
            this.FullPath.ReadOnly = true;
            this.FullPath.Width = 500;
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1192, 453);
            this.Controls.Add(this.kryptonSplitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1210, 500);
            this.Name = "Form1";
            this.Text = "Display Strong Naming and Digital Signing Status";
            this.Shown += new System.EventHandler(this.Form1_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainer1.Panel1)).EndInit();
            this.kryptonSplitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainer1.Panel2)).EndInit();
            this.kryptonSplitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainer1)).EndInit();
            this.kryptonSplitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFileProperties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Krypton.Toolkit.KryptonSplitContainer kryptonSplitContainer1;
        private Krypton.Toolkit.KryptonDataGridView dgvFileProperties;
        private Krypton.Toolkit.KryptonListBox lstLog;
        private Krypton.Toolkit.KryptonButton btnRemoveSelected;
        private Krypton.Toolkit.KryptonButton btnAdd;
        private Ookii.Dialogs.WinForms.ProgressDialog progressDialog1;
        private Krypton.Toolkit.KryptonButton btnRemovelAll;
        private System.Windows.Forms.DataGridViewTextBoxColumn FileName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Version;
        private System.Windows.Forms.DataGridViewTextBoxColumn Type;
        private System.Windows.Forms.DataGridViewTextBoxColumn StrongNamed;
        private System.Windows.Forms.DataGridViewTextBoxColumn StrongVerified;
        private System.Windows.Forms.DataGridViewTextBoxColumn CopyRight;
        private System.Windows.Forms.DataGridViewTextBoxColumn BinVersion;
        private System.Windows.Forms.DataGridViewTextBoxColumn DigStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn FullPath;
    }
}