namespace IISDeploy
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
            IISListBox = new ListBox();
            splitContainer1 = new SplitContainer();
            panel2 = new Panel();
            StatusText = new TextBox();
            panel1 = new Panel();
            TargetProjectPanel = new Panel();
            label4 = new Label();
            TargetProjectTextBox = new TextBox();
            OutputPathTextBox = new TextBox();
            buildSelect = new ComboBox();
            button5 = new Button();
            button4 = new Button();
            button3 = new Button();
            button2 = new Button();
            button1 = new Button();
            FilePathTextBox = new TextBox();
            label5 = new Label();
            label1 = new Label();
            label3 = new Label();
            BranchTextBox = new TextBox();
            GitUrlTextBox = new TextBox();
            label6 = new Label();
            label2 = new Label();
            WebNameLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            panel2.SuspendLayout();
            panel1.SuspendLayout();
            TargetProjectPanel.SuspendLayout();
            SuspendLayout();
            // 
            // IISListBox
            // 
            IISListBox.Dock = DockStyle.Fill;
            IISListBox.FormattingEnabled = true;
            IISListBox.ItemHeight = 15;
            IISListBox.Location = new Point(10, 10);
            IISListBox.Margin = new Padding(10);
            IISListBox.Name = "IISListBox";
            IISListBox.Size = new Size(393, 604);
            IISListBox.TabIndex = 0;
            IISListBox.DrawItem += IISListBox_DrawItem;
            IISListBox.SelectedIndexChanged += IISListBox_SelectedIndexChanged;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(IISListBox);
            splitContainer1.Panel1.Padding = new Padding(10, 10, 3, 10);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(panel2);
            splitContainer1.Panel2.Controls.Add(panel1);
            splitContainer1.Panel2.Padding = new Padding(3, 10, 10, 10);
            splitContainer1.Size = new Size(1222, 624);
            splitContainer1.SplitterDistance = 406;
            splitContainer1.SplitterWidth = 2;
            splitContainer1.TabIndex = 1;
            // 
            // panel2
            // 
            panel2.BorderStyle = BorderStyle.FixedSingle;
            panel2.Controls.Add(StatusText);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(3, 10);
            panel2.Margin = new Padding(3, 10, 3, 3);
            panel2.Name = "panel2";
            panel2.Padding = new Padding(10);
            panel2.Size = new Size(801, 244);
            panel2.TabIndex = 2;
            // 
            // StatusText
            // 
            StatusText.BackColor = Color.White;
            StatusText.Dock = DockStyle.Fill;
            StatusText.Location = new Point(10, 10);
            StatusText.Multiline = true;
            StatusText.Name = "StatusText";
            StatusText.ReadOnly = true;
            StatusText.ScrollBars = ScrollBars.Both;
            StatusText.Size = new Size(779, 222);
            StatusText.TabIndex = 0;
            // 
            // panel1
            // 
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(TargetProjectPanel);
            panel1.Controls.Add(OutputPathTextBox);
            panel1.Controls.Add(buildSelect);
            panel1.Controls.Add(button5);
            panel1.Controls.Add(button4);
            panel1.Controls.Add(button3);
            panel1.Controls.Add(button2);
            panel1.Controls.Add(button1);
            panel1.Controls.Add(FilePathTextBox);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(BranchTextBox);
            panel1.Controls.Add(GitUrlTextBox);
            panel1.Controls.Add(label6);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(WebNameLabel);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(3, 10);
            panel1.Margin = new Padding(3, 10, 3, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(801, 604);
            panel1.TabIndex = 1;
            // 
            // TargetProjectPanel
            // 
            TargetProjectPanel.Controls.Add(label4);
            TargetProjectPanel.Controls.Add(TargetProjectTextBox);
            TargetProjectPanel.Location = new Point(358, 417);
            TargetProjectPanel.Name = "TargetProjectPanel";
            TargetProjectPanel.Size = new Size(165, 62);
            TargetProjectPanel.TabIndex = 6;
            TargetProjectPanel.Visible = false;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.Location = new Point(6, 7);
            label4.Name = "label4";
            label4.Size = new Size(101, 18);
            label4.TabIndex = 1;
            label4.Text = "Target Project";
            // 
            // TargetProjectTextBox
            // 
            TargetProjectTextBox.Location = new Point(6, 30);
            TargetProjectTextBox.Name = "TargetProjectTextBox";
            TargetProjectTextBox.Size = new Size(130, 23);
            TargetProjectTextBox.TabIndex = 5;
            // 
            // OutputPathTextBox
            // 
            OutputPathTextBox.Location = new Point(139, 447);
            OutputPathTextBox.Name = "OutputPathTextBox";
            OutputPathTextBox.Size = new Size(213, 23);
            OutputPathTextBox.TabIndex = 5;
            // 
            // buildSelect
            // 
            buildSelect.FormattingEnabled = true;
            buildSelect.Items.AddRange(new object[] { "NodeJs", ".Net" });
            buildSelect.Location = new Point(10, 447);
            buildSelect.Name = "buildSelect";
            buildSelect.Size = new Size(121, 23);
            buildSelect.TabIndex = 4;
            buildSelect.SelectedIndexChanged += buildSelect_SelectedIndexChanged;
            // 
            // button5
            // 
            button5.Location = new Point(609, 440);
            button5.Name = "button5";
            button5.Size = new Size(150, 39);
            button5.TabIndex = 3;
            button5.Text = "Save Setting";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // button4
            // 
            button4.Location = new Point(609, 395);
            button4.Name = "button4";
            button4.Size = new Size(150, 39);
            button4.TabIndex = 3;
            button4.Text = "Start";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // button3
            // 
            button3.Location = new Point(609, 350);
            button3.Name = "button3";
            button3.Size = new Size(150, 39);
            button3.TabIndex = 3;
            button3.Text = "Pause";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button2
            // 
            button2.Location = new Point(609, 305);
            button2.Name = "button2";
            button2.Size = new Size(150, 39);
            button2.TabIndex = 3;
            button2.Text = "Pause + Deploy";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button1
            // 
            button1.Location = new Point(609, 261);
            button1.Name = "button1";
            button1.Size = new Size(150, 38);
            button1.TabIndex = 3;
            button1.Text = "Deploy";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // FilePathTextBox
            // 
            FilePathTextBox.BackColor = Color.White;
            FilePathTextBox.Enabled = false;
            FilePathTextBox.Location = new Point(10, 312);
            FilePathTextBox.Name = "FilePathTextBox";
            FilePathTextBox.Size = new Size(560, 23);
            FilePathTextBox.TabIndex = 2;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.Location = new Point(137, 426);
            label5.Name = "label5";
            label5.Size = new Size(86, 18);
            label5.TabIndex = 1;
            label5.Text = "Output Path";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(13, 426);
            label1.Name = "label1";
            label1.Size = new Size(71, 18);
            label1.TabIndex = 1;
            label1.Text = "Build Use";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(13, 291);
            label3.Name = "label3";
            label3.Size = new Size(65, 18);
            label3.TabIndex = 1;
            label3.Text = "File Path";
            // 
            // BranchTextBox
            // 
            BranchTextBox.BackColor = Color.White;
            BranchTextBox.Location = new Point(420, 380);
            BranchTextBox.Name = "BranchTextBox";
            BranchTextBox.Size = new Size(130, 23);
            BranchTextBox.TabIndex = 2;
            // 
            // GitUrlTextBox
            // 
            GitUrlTextBox.BackColor = Color.White;
            GitUrlTextBox.Location = new Point(10, 380);
            GitUrlTextBox.Name = "GitUrlTextBox";
            GitUrlTextBox.Size = new Size(401, 23);
            GitUrlTextBox.TabIndex = 2;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label6.Location = new Point(420, 359);
            label6.Name = "label6";
            label6.Size = new Size(55, 18);
            label6.TabIndex = 1;
            label6.Text = "Branch";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(13, 359);
            label2.Name = "label2";
            label2.Size = new Size(61, 18);
            label2.TabIndex = 1;
            label2.Text = "Git URL";
            // 
            // WebNameLabel
            // 
            WebNameLabel.AutoSize = true;
            WebNameLabel.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            WebNameLabel.Location = new Point(10, 250);
            WebNameLabel.Name = "WebNameLabel";
            WebNameLabel.Size = new Size(121, 26);
            WebNameLabel.TabIndex = 0;
            WebNameLabel.Text = "WebName";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1222, 624);
            Controls.Add(splitContainer1);
            Name = "MainForm";
            Text = "IIS Deploy";
            Load += MainForm_Load;
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            TargetProjectPanel.ResumeLayout(false);
            TargetProjectPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private ListBox IISListBox;
        private SplitContainer splitContainer1;
        private TextBox StatusText;
        private Panel panel1;
        private Panel panel2;
        private Button button1;
        private TextBox FilePathTextBox;
        private Label label3;
        private TextBox GitUrlTextBox;
        private Label label2;
        private Label WebNameLabel;
        private Button button2;
        private Label label1;
        private ComboBox buildSelect;
        private Button button4;
        private Button button3;
        private Button button5;
        private TextBox TargetProjectTextBox;
        private Label label5;
        private Label label4;
        private TextBox OutputPathTextBox;
        private Panel TargetProjectPanel;
        private TextBox BranchTextBox;
        private Label label6;
    }
}
