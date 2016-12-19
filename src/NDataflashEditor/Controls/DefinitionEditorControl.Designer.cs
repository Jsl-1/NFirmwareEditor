namespace NDataflashEditor.Windows.DataflashDefinitionEditor
{
    partial class DataflashDefinitionEditorControl
    {
        /// <summary> 
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.BinaryViewTabControl = new System.Windows.Forms.TabControl();
            this.BinaryTabPage = new System.Windows.Forms.TabPage();
            this.dataGridViewBinary = new System.Windows.Forms.DataGridView();
            this.ChangesTabPage = new System.Windows.Forms.TabPage();
            this.dataGridViewChanges = new System.Windows.Forms.DataGridView();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.propertyGrid2 = new System.Windows.Forms.PropertyGrid();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.BinaryViewTabControl.SuspendLayout();
            this.BinaryTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewBinary)).BeginInit();
            this.ChangesTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewChanges)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.BinaryViewTabControl);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(886, 526);
            this.splitContainer1.SplitterDistance = 239;
            this.splitContainer1.TabIndex = 0;
            // 
            // BinaryViewTabControl
            // 
            this.BinaryViewTabControl.Controls.Add(this.BinaryTabPage);
            this.BinaryViewTabControl.Controls.Add(this.ChangesTabPage);
            this.BinaryViewTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BinaryViewTabControl.Location = new System.Drawing.Point(0, 0);
            this.BinaryViewTabControl.Name = "BinaryViewTabControl";
            this.BinaryViewTabControl.SelectedIndex = 0;
            this.BinaryViewTabControl.Size = new System.Drawing.Size(239, 526);
            this.BinaryViewTabControl.TabIndex = 1;
            // 
            // BinaryTabPage
            // 
            this.BinaryTabPage.Controls.Add(this.dataGridViewBinary);
            this.BinaryTabPage.Location = new System.Drawing.Point(4, 22);
            this.BinaryTabPage.Name = "BinaryTabPage";
            this.BinaryTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.BinaryTabPage.Size = new System.Drawing.Size(231, 500);
            this.BinaryTabPage.TabIndex = 0;
            this.BinaryTabPage.Text = "Binary View";
            this.BinaryTabPage.UseVisualStyleBackColor = true;
            // 
            // dataGridViewBinary
            // 
            this.dataGridViewBinary.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewBinary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewBinary.Location = new System.Drawing.Point(3, 3);
            this.dataGridViewBinary.Name = "dataGridViewBinary";
            this.dataGridViewBinary.ReadOnly = true;
            this.dataGridViewBinary.Size = new System.Drawing.Size(225, 494);
            this.dataGridViewBinary.TabIndex = 0;
            // 
            // ChangesTabPage
            // 
            this.ChangesTabPage.Controls.Add(this.dataGridViewChanges);
            this.ChangesTabPage.Location = new System.Drawing.Point(4, 22);
            this.ChangesTabPage.Name = "ChangesTabPage";
            this.ChangesTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.ChangesTabPage.Size = new System.Drawing.Size(231, 500);
            this.ChangesTabPage.TabIndex = 1;
            this.ChangesTabPage.Text = "Changes";
            this.ChangesTabPage.UseVisualStyleBackColor = true;
            // 
            // dataGridViewChanges
            // 
            this.dataGridViewChanges.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewChanges.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewChanges.Location = new System.Drawing.Point(3, 3);
            this.dataGridViewChanges.Name = "dataGridViewChanges";
            this.dataGridViewChanges.ReadOnly = true;
            this.dataGridViewChanges.Size = new System.Drawing.Size(225, 494);
            this.dataGridViewChanges.TabIndex = 1;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.treeView1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.propertyGrid2);
            this.splitContainer2.Panel2.Controls.Add(this.propertyGrid1);
            this.splitContainer2.Size = new System.Drawing.Size(643, 526);
            this.splitContainer2.SplitterDistance = 397;
            this.splitContainer2.TabIndex = 1;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(397, 526);
            this.treeView1.TabIndex = 0;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // propertyGrid2
            // 
            this.propertyGrid2.Location = new System.Drawing.Point(202, 195);
            this.propertyGrid2.Name = "propertyGrid2";
            this.propertyGrid2.Size = new System.Drawing.Size(8, 8);
            this.propertyGrid2.TabIndex = 1;
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid1.Location = new System.Drawing.Point(0, 0);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(242, 526);
            this.propertyGrid1.TabIndex = 0;
            this.propertyGrid1.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGrid1_PropertyValueChanged);
            // 
            // DataflashDefinitionEditorControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "DataflashDefinitionEditorControl";
            this.Size = new System.Drawing.Size(886, 526);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.BinaryViewTabControl.ResumeLayout(false);
            this.BinaryTabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewBinary)).EndInit();
            this.ChangesTabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewChanges)).EndInit();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.DataGridView dataGridViewBinary;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.PropertyGrid propertyGrid2;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.TabControl BinaryViewTabControl;
        private System.Windows.Forms.TabPage BinaryTabPage;
        private System.Windows.Forms.TabPage ChangesTabPage;
        private System.Windows.Forms.DataGridView dataGridViewChanges;
    }
}
