namespace WinformsMVPPhonebookApp.Views
{
    partial class MainForm
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
            splitContainer = new SplitContainer();
            buttonDeleteEntry = new Button();
            buttonEditEntry = new Button();
            buttonAddEntry = new Button();
            dataGridView = new DataGridView();
            EntryName = new DataGridViewTextBoxColumn();
            EntryPhoneNumber = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)splitContainer).BeginInit();
            splitContainer.Panel1.SuspendLayout();
            splitContainer.Panel2.SuspendLayout();
            splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView).BeginInit();
            SuspendLayout();
            // 
            // splitContainer
            // 
            splitContainer.Dock = DockStyle.Fill;
            splitContainer.Location = new Point(0, 0);
            splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            splitContainer.Panel1.BackColor = SystemColors.Menu;
            splitContainer.Panel1.Controls.Add(buttonDeleteEntry);
            splitContainer.Panel1.Controls.Add(buttonEditEntry);
            splitContainer.Panel1.Controls.Add(buttonAddEntry);
            // 
            // splitContainer.Panel2
            // 
            splitContainer.Panel2.BackColor = SystemColors.Control;
            splitContainer.Panel2.Controls.Add(dataGridView);
            splitContainer.Panel2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            splitContainer.Size = new Size(800, 450);
            splitContainer.SplitterDistance = 224;
            splitContainer.TabIndex = 0;
            // 
            // buttonDeleteEntry
            // 
            buttonDeleteEntry.Enabled = false;
            buttonDeleteEntry.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            buttonDeleteEntry.Location = new Point(12, 84);
            buttonDeleteEntry.Name = "buttonDeleteEntry";
            buttonDeleteEntry.Size = new Size(209, 30);
            buttonDeleteEntry.TabIndex = 2;
            buttonDeleteEntry.Text = "Delete selected";
            buttonDeleteEntry.UseVisualStyleBackColor = true;
            buttonDeleteEntry.Click += ButtonDeleteEntry_Click;
            // 
            // buttonEditEntry
            // 
            buttonEditEntry.Enabled = false;
            buttonEditEntry.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            buttonEditEntry.Location = new Point(12, 48);
            buttonEditEntry.Name = "buttonEditEntry";
            buttonEditEntry.Size = new Size(209, 30);
            buttonEditEntry.TabIndex = 1;
            buttonEditEntry.Text = "Edit selected";
            buttonEditEntry.UseVisualStyleBackColor = true;
            // 
            // buttonAddEntry
            // 
            buttonAddEntry.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            buttonAddEntry.Location = new Point(12, 12);
            buttonAddEntry.Name = "buttonAddEntry";
            buttonAddEntry.Size = new Size(209, 30);
            buttonAddEntry.TabIndex = 0;
            buttonAddEntry.Text = "Add new";
            buttonAddEntry.UseVisualStyleBackColor = true;
            // 
            // dataGridView
            // 
            dataGridView.AllowUserToAddRows = false;
            dataGridView.AllowUserToDeleteRows = false;
            dataGridView.BackgroundColor = SystemColors.Control;
            dataGridView.ColumnHeadersHeight = 45;
            dataGridView.Columns.AddRange(new DataGridViewColumn[] { EntryName, EntryPhoneNumber });
            dataGridView.EditMode = DataGridViewEditMode.EditProgrammatically;
            dataGridView.Location = new Point(3, 12);
            dataGridView.MultiSelect = false;
            dataGridView.Name = "dataGridView";
            dataGridView.ScrollBars = ScrollBars.Vertical;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.Size = new Size(557, 426);
            dataGridView.TabIndex = 0;
            dataGridView.SelectionChanged += DataGridView_SelectionChanged;
            // 
            // EntryName
            // 
            EntryName.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            EntryName.DataPropertyName = "Name";
            EntryName.FillWeight = 200F;
            EntryName.HeaderText = "Name";
            EntryName.Name = "EntryName";
            // 
            // EntryPhoneNumber
            // 
            EntryPhoneNumber.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            EntryPhoneNumber.DataPropertyName = "PhoneNumber";
            EntryPhoneNumber.HeaderText = "Phone number";
            EntryPhoneNumber.Name = "EntryPhoneNumber";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(splitContainer);
            MaximizeBox = false;
            Name = "MainForm";
            Text = "Phonebook App";
            splitContainer.Panel1.ResumeLayout(false);
            splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer).EndInit();
            splitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private SplitContainer splitContainer;
        private Button buttonDeleteEntry;
        private Button buttonEditEntry;
        private Button buttonAddEntry;
        private DataGridView dataGridView;
        private DataGridViewTextBoxColumn EntryName;
        private DataGridViewTextBoxColumn EntryPhoneNumber;
    }
}
