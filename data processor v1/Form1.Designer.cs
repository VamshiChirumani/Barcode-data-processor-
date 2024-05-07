
namespace data_processor_v4a
{
    partial class Form1
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
            this.txtConnection = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnRunQuery = new System.Windows.Forms.Button();
            this.lbTests = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.btnFile = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.label3 = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnRunAll = new System.Windows.Forms.Button();
            this.btnRunSelected = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSpeedList = new System.Windows.Forms.TextBox();
            this.txtHeights = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnProcessData = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.button1 = new System.Windows.Forms.Button();
            this.cbAnalysis = new System.Windows.Forms.CheckBox();
            this.txtOffsets = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cboxByID = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // txtConnection
            // 
            this.txtConnection.Location = new System.Drawing.Point(117, 17);
            this.txtConnection.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtConnection.Name = "txtConnection";
            this.txtConnection.Size = new System.Drawing.Size(488, 26);
            this.txtConnection.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Connection:";
            // 
            // btnRunQuery
            // 
            this.btnRunQuery.Location = new System.Drawing.Point(274, 235);
            this.btnRunQuery.Name = "btnRunQuery";
            this.btnRunQuery.Size = new System.Drawing.Size(129, 52);
            this.btnRunQuery.TabIndex = 2;
            this.btnRunQuery.Text = "Query Tests";
            this.btnRunQuery.UseVisualStyleBackColor = true;
            this.btnRunQuery.Click += new System.EventHandler(this.btnRunQuery_Click);
            // 
            // lbTests
            // 
            this.lbTests.FormattingEnabled = true;
            this.lbTests.ItemHeight = 20;
            this.lbTests.Location = new System.Drawing.Point(426, 51);
            this.lbTests.Name = "lbTests";
            this.lbTests.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbTests.Size = new System.Drawing.Size(179, 284);
            this.lbTests.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(43, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 20);
            this.label2.TabIndex = 5;
            this.label2.Text = "Save to:";
            // 
            // txtFileName
            // 
            this.txtFileName.Location = new System.Drawing.Point(117, 60);
            this.txtFileName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(245, 26);
            this.txtFileName.TabIndex = 4;
            // 
            // btnFile
            // 
            this.btnFile.Location = new System.Drawing.Point(369, 60);
            this.btnFile.Name = "btnFile";
            this.btnFile.Size = new System.Drawing.Size(34, 26);
            this.btnFile.TabIndex = 6;
            this.btnFile.Text = "...";
            this.btnFile.UseVisualStyleBackColor = true;
            this.btnFile.Click += new System.EventHandler(this.btnFile_Click);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "csv";
            this.saveFileDialog1.FileName = "*.csv";
            this.saveFileDialog1.Filter = "CSV files|*.csv|All files|*.*";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 377);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 20);
            this.label3.TabIndex = 7;
            this.label3.Text = "Status:";
            // 
            // lblStatus
            // 
            this.lblStatus.Location = new System.Drawing.Point(78, 377);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(527, 30);
            this.lblStatus.TabIndex = 8;
            this.lblStatus.Text = "ready...";
            // 
            // btnRunAll
            // 
            this.btnRunAll.Location = new System.Drawing.Point(171, 307);
            this.btnRunAll.Name = "btnRunAll";
            this.btnRunAll.Size = new System.Drawing.Size(78, 52);
            this.btnRunAll.TabIndex = 9;
            this.btnRunAll.Text = "Run All";
            this.btnRunAll.UseVisualStyleBackColor = true;
            this.btnRunAll.Click += new System.EventHandler(this.btnRunAll_Click);
            // 
            // btnRunSelected
            // 
            this.btnRunSelected.Location = new System.Drawing.Point(274, 307);
            this.btnRunSelected.Name = "btnRunSelected";
            this.btnRunSelected.Size = new System.Drawing.Size(129, 52);
            this.btnRunSelected.TabIndex = 10;
            this.btnRunSelected.Text = "Run Selected";
            this.btnRunSelected.UseVisualStyleBackColor = true;
            this.btnRunSelected.Click += new System.EventHandler(this.btnRunSelected_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(42, 104);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 20);
            this.label4.TabIndex = 11;
            this.label4.Text = "Speeds:";
            // 
            // txtSpeedList
            // 
            this.txtSpeedList.Location = new System.Drawing.Point(117, 101);
            this.txtSpeedList.Name = "txtSpeedList";
            this.txtSpeedList.Size = new System.Drawing.Size(286, 26);
            this.txtSpeedList.TabIndex = 12;
            this.txtSpeedList.Text = "150,300,400,600,800,1200,1500,150,300,400,600,800,1200,1500";
            this.txtSpeedList.Leave += new System.EventHandler(this.txtSpeedList_Leave);
            // 
            // txtHeights
            // 
            this.txtHeights.Location = new System.Drawing.Point(117, 142);
            this.txtHeights.Name = "txtHeights";
            this.txtHeights.Size = new System.Drawing.Size(286, 26);
            this.txtHeights.TabIndex = 14;
            this.txtHeights.Text = "25,55";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(42, 145);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 20);
            this.label5.TabIndex = 13;
            this.label5.Text = "Heights:";
            // 
            // btnProcessData
            // 
            this.btnProcessData.Location = new System.Drawing.Point(20, 235);
            this.btnProcessData.Name = "btnProcessData";
            this.btnProcessData.Size = new System.Drawing.Size(111, 52);
            this.btnProcessData.TabIndex = 15;
            this.btnProcessData.Text = "Process Data";
            this.btnProcessData.UseVisualStyleBackColor = true;
            this.btnProcessData.Click += new System.EventHandler(this.button1_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(20, 307);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(111, 52);
            this.button1.TabIndex = 16;
            this.button1.Text = "find records";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // cbAnalysis
            // 
            this.cbAnalysis.AutoSize = true;
            this.cbAnalysis.Location = new System.Drawing.Point(168, 246);
            this.cbAnalysis.Name = "cbAnalysis";
            this.cbAnalysis.Size = new System.Drawing.Size(86, 24);
            this.cbAnalysis.TabIndex = 17;
            this.cbAnalysis.Text = "Analysis";
            this.cbAnalysis.UseVisualStyleBackColor = true;
            // 
            // txtOffsets
            // 
            this.txtOffsets.Location = new System.Drawing.Point(118, 183);
            this.txtOffsets.Name = "txtOffsets";
            this.txtOffsets.Size = new System.Drawing.Size(286, 26);
            this.txtOffsets.TabIndex = 19;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(43, 186);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 20);
            this.label6.TabIndex = 18;
            this.label6.Text = "Offsets";
            // 
            // cboxByID
            // 
            this.cboxByID.AutoSize = true;
            this.cboxByID.Checked = true;
            this.cboxByID.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cboxByID.Location = new System.Drawing.Point(439, 357);
            this.cboxByID.Name = "cboxByID";
            this.cboxByID.Size = new System.Drawing.Size(67, 24);
            this.cboxByID.TabIndex = 20;
            this.cboxByID.Text = "By ID";
            this.cboxByID.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 420);
            this.Controls.Add(this.cboxByID);
            this.Controls.Add(this.txtOffsets);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cbAnalysis);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnProcessData);
            this.Controls.Add(this.txtHeights);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtSpeedList);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnRunSelected);
            this.Controls.Add(this.btnRunAll);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnFile);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtFileName);
            this.Controls.Add(this.lbTests);
            this.Controls.Add(this.btnRunQuery);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtConnection);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GS1 Data Processor v4a";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtConnection;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnRunQuery;
        private System.Windows.Forms.ListBox lbTests;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtFileName;
        private System.Windows.Forms.Button btnFile;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btnRunAll;
        private System.Windows.Forms.Button btnRunSelected;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtSpeedList;
        private System.Windows.Forms.TextBox txtHeights;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnProcessData;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox cbAnalysis;
        private System.Windows.Forms.TextBox txtOffsets;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox cboxByID;
    }
}

