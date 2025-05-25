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
            label1 = new Label();
            buttonDeletePhonenumber = new Button();
            buttonEditPhonenumber = new Button();
            buttonAddPhonenumber = new Button();
            listView = new ListView();
            columnHeaderName = new ColumnHeader();
            columnHeaderPhoneNumber = new ColumnHeader();
            ((System.ComponentModel.ISupportInitialize)splitContainer).BeginInit();
            splitContainer.Panel1.SuspendLayout();
            splitContainer.Panel2.SuspendLayout();
            splitContainer.SuspendLayout();
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
            splitContainer.Panel1.Controls.Add(label1);
            splitContainer.Panel1.Controls.Add(buttonDeletePhonenumber);
            splitContainer.Panel1.Controls.Add(buttonEditPhonenumber);
            splitContainer.Panel1.Controls.Add(buttonAddPhonenumber);
            // 
            // splitContainer.Panel2
            // 
            splitContainer.Panel2.BackColor = SystemColors.Control;
            splitContainer.Panel2.Controls.Add(listView);
            splitContainer.Size = new Size(800, 450);
            splitContainer.SplitterDistance = 224;
            splitContainer.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(35, 254);
            label1.Name = "label1";
            label1.Size = new Size(38, 15);
            label1.TabIndex = 3;
            label1.Text = "label1";
            // 
            // buttonDeletePhonenumber
            // 
            buttonDeletePhonenumber.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            buttonDeletePhonenumber.Location = new Point(12, 84);
            buttonDeletePhonenumber.Name = "buttonDeletePhonenumber";
            buttonDeletePhonenumber.Size = new Size(209, 30);
            buttonDeletePhonenumber.TabIndex = 2;
            buttonDeletePhonenumber.Text = "Delete selected";
            buttonDeletePhonenumber.UseVisualStyleBackColor = true;
            // 
            // buttonEditPhonenumber
            // 
            buttonEditPhonenumber.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            buttonEditPhonenumber.Location = new Point(12, 48);
            buttonEditPhonenumber.Name = "buttonEditPhonenumber";
            buttonEditPhonenumber.Size = new Size(209, 30);
            buttonEditPhonenumber.TabIndex = 1;
            buttonEditPhonenumber.Text = "Edit selected";
            buttonEditPhonenumber.UseVisualStyleBackColor = true;
            // 
            // buttonAddPhonenumber
            // 
            buttonAddPhonenumber.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            buttonAddPhonenumber.Location = new Point(12, 12);
            buttonAddPhonenumber.Name = "buttonAddPhonenumber";
            buttonAddPhonenumber.Size = new Size(209, 30);
            buttonAddPhonenumber.TabIndex = 0;
            buttonAddPhonenumber.Text = "Add new";
            buttonAddPhonenumber.UseVisualStyleBackColor = true;
            // 
            // listView
            // 
            listView.Columns.AddRange(new ColumnHeader[] { columnHeaderName, columnHeaderPhoneNumber });
            listView.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            listView.FullRowSelect = true;
            listView.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            listView.Location = new Point(3, 12);
            listView.MultiSelect = false;
            listView.Name = "listView";
            listView.ShowGroups = false;
            listView.Size = new Size(557, 426);
            listView.TabIndex = 0;
            listView.UseCompatibleStateImageBehavior = false;
            listView.View = View.Details;
            listView.ItemSelectionChanged += ListView_ItemSelectionChanged;
            // 
            // columnHeaderName
            // 
            columnHeaderName.Text = "Name";
            columnHeaderName.Width = 272;
            // 
            // columnHeaderPhoneNumber
            // 
            columnHeaderPhoneNumber.Text = "Phone Number";
            columnHeaderPhoneNumber.Width = 272;
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
            splitContainer.Panel1.PerformLayout();
            splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer).EndInit();
            splitContainer.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private SplitContainer splitContainer;
        private ListView listView;
        private Button buttonDeletePhonenumber;
        private Button buttonEditPhonenumber;
        private Button buttonAddPhonenumber;
        private ColumnHeader columnHeaderName;
        private ColumnHeader columnHeaderPhoneNumber;
        private Label label1;
    }
}
