namespace MainSystem
{
    partial class SalaryUpdate
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
            this.label1 = new System.Windows.Forms.Label();
            this.comEmployee = new System.Windows.Forms.ComboBox();
            this.txtEmployee_Number = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtSalary = new System.Windows.Forms.TextBox();
            this.txtStimulus = new System.Windows.Forms.TextBox();
            this.txtDeductions = new System.Windows.Forms.TextBox();
            this.txtDeductionsSocial = new System.Windows.Forms.TextBox();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.txtTotalSalary = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.rDelegate = new System.Windows.Forms.RadioButton();
            this.rEmployee = new System.Windows.Forms.RadioButton();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.label9 = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnAdd = new Bunifu.Framework.UI.BunifuTileButton();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Neo Sans Arabic", 11F);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
            this.label1.Location = new System.Drawing.Point(374, 204);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 18);
            this.label1.TabIndex = 28;
            this.label1.Text = "اسم الموظف";
            // 
            // comEmployee
            // 
            this.comEmployee.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.comEmployee.FormattingEnabled = true;
            this.comEmployee.Location = new System.Drawing.Point(179, 204);
            this.comEmployee.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.comEmployee.Name = "comEmployee";
            this.comEmployee.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.comEmployee.Size = new System.Drawing.Size(185, 24);
            this.comEmployee.TabIndex = 2;
            this.comEmployee.SelectedIndexChanged += new System.EventHandler(this.comEmployee_SelectedIndexChanged);
            this.comEmployee.TextChanged += new System.EventHandler(this.txtBox_TextChanged);
            // 
            // txtEmployee_Number
            // 
            this.txtEmployee_Number.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtEmployee_Number.Location = new System.Drawing.Point(466, 204);
            this.txtEmployee_Number.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtEmployee_Number.Name = "txtEmployee_Number";
            this.txtEmployee_Number.Size = new System.Drawing.Size(139, 23);
            this.txtEmployee_Number.TabIndex = 1;
            this.txtEmployee_Number.TextChanged += new System.EventHandler(this.txtBox_TextChanged);
            this.txtEmployee_Number.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtEmployeeNumber_KeyDown);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Neo Sans Arabic", 11F);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
            this.label2.Location = new System.Drawing.Point(649, 275);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 18);
            this.label2.TabIndex = 31;
            this.label2.Text = "الراتب الاساسى";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Neo Sans Arabic", 11F);
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
            this.label3.Location = new System.Drawing.Point(311, 325);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 18);
            this.label3.TabIndex = 32;
            this.label3.Text = "الحوافز";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Neo Sans Arabic", 11F);
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
            this.label4.Location = new System.Drawing.Point(311, 276);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 18);
            this.label4.TabIndex = 33;
            this.label4.Text = "استقطاعات";
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Neo Sans Arabic", 11F);
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
            this.label5.Location = new System.Drawing.Point(649, 327);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(174, 18);
            this.label5.TabIndex = 34;
            this.label5.Text = "استقطاع التأمينات الأجتماعية";
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Neo Sans Arabic", 11F);
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
            this.label6.Location = new System.Drawing.Point(649, 378);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 18);
            this.label6.TabIndex = 35;
            this.label6.Text = "ملاحظات";
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Neo Sans Arabic", 11F);
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
            this.label8.Location = new System.Drawing.Point(311, 375);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(85, 18);
            this.label8.TabIndex = 37;
            this.label8.Text = "صافي الراتب";
            // 
            // txtSalary
            // 
            this.txtSalary.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtSalary.Location = new System.Drawing.Point(466, 271);
            this.txtSalary.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtSalary.Name = "txtSalary";
            this.txtSalary.Size = new System.Drawing.Size(166, 23);
            this.txtSalary.TabIndex = 3;
            this.txtSalary.TextChanged += new System.EventHandler(this.txtBox_TextChanged);
            // 
            // txtStimulus
            // 
            this.txtStimulus.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtStimulus.Location = new System.Drawing.Point(129, 322);
            this.txtStimulus.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtStimulus.Name = "txtStimulus";
            this.txtStimulus.Size = new System.Drawing.Size(166, 23);
            this.txtStimulus.TabIndex = 6;
            this.txtStimulus.Text = "0";
            this.txtStimulus.TextChanged += new System.EventHandler(this.txtBox_TextChanged);
            this.txtStimulus.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtStimulus_KeyDown);
            // 
            // txtDeductions
            // 
            this.txtDeductions.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtDeductions.Location = new System.Drawing.Point(129, 273);
            this.txtDeductions.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtDeductions.Name = "txtDeductions";
            this.txtDeductions.Size = new System.Drawing.Size(166, 23);
            this.txtDeductions.TabIndex = 4;
            this.txtDeductions.Text = "0";
            this.txtDeductions.TextChanged += new System.EventHandler(this.txtBox_TextChanged);
            // 
            // txtDeductionsSocial
            // 
            this.txtDeductionsSocial.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtDeductionsSocial.Location = new System.Drawing.Point(466, 323);
            this.txtDeductionsSocial.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtDeductionsSocial.Name = "txtDeductionsSocial";
            this.txtDeductionsSocial.Size = new System.Drawing.Size(166, 23);
            this.txtDeductionsSocial.TabIndex = 5;
            this.txtDeductionsSocial.Text = "0";
            this.txtDeductionsSocial.TextChanged += new System.EventHandler(this.txtBox_TextChanged);
            // 
            // txtNotes
            // 
            this.txtNotes.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtNotes.Location = new System.Drawing.Point(466, 375);
            this.txtNotes.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtNotes.Multiline = true;
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Size = new System.Drawing.Size(166, 52);
            this.txtNotes.TabIndex = 8;
            this.txtNotes.TextChanged += new System.EventHandler(this.txtBox_TextChanged);
            // 
            // txtTotalSalary
            // 
            this.txtTotalSalary.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtTotalSalary.Location = new System.Drawing.Point(129, 371);
            this.txtTotalSalary.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtTotalSalary.Name = "txtTotalSalary";
            this.txtTotalSalary.Size = new System.Drawing.Size(166, 23);
            this.txtTotalSalary.TabIndex = 7;
            this.txtTotalSalary.TextChanged += new System.EventHandler(this.txtBox_TextChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(97, 191);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(1, 1);
            this.dataGridView1.TabIndex = 45;
            // 
            // rDelegate
            // 
            this.rDelegate.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.rDelegate.AutoSize = true;
            this.rDelegate.Font = new System.Drawing.Font("Neo Sans Arabic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rDelegate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
            this.rDelegate.Location = new System.Drawing.Point(462, 149);
            this.rDelegate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rDelegate.Name = "rDelegate";
            this.rDelegate.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.rDelegate.Size = new System.Drawing.Size(63, 20);
            this.rDelegate.TabIndex = 62;
            this.rDelegate.Text = "مندوب";
            this.rDelegate.UseVisualStyleBackColor = true;
            this.rDelegate.CheckedChanged += new System.EventHandler(this.rDelegate_CheckedChanged);
            // 
            // rEmployee
            // 
            this.rEmployee.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.rEmployee.AutoSize = true;
            this.rEmployee.Checked = true;
            this.rEmployee.Font = new System.Drawing.Font("Neo Sans Arabic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rEmployee.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
            this.rEmployee.Location = new System.Drawing.Point(348, 149);
            this.rEmployee.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rEmployee.Name = "rEmployee";
            this.rEmployee.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.rEmployee.Size = new System.Drawing.Size(64, 20);
            this.rEmployee.TabIndex = 61;
            this.rEmployee.TabStop = true;
            this.rEmployee.Text = "موظف";
            this.rEmployee.UseVisualStyleBackColor = true;
            this.rEmployee.CheckedChanged += new System.EventHandler(this.rEmployee_CheckedChanged);
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(38, 73);
            this.dataGridView2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(1, 1);
            this.dataGridView2.TabIndex = 63;
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Neo Sans Arabic", 11F);
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
            this.label9.Location = new System.Drawing.Point(613, 204);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(78, 18);
            this.label9.TabIndex = 64;
            this.label9.Text = "رقم وظيفي";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 43.78479F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.33766F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 43.75F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Controls.Add(this.btnAdd, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 486);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(870, 53);
            this.tableLayoutPanel2.TabIndex = 70;
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.btnAdd.color = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.btnAdd.colorActive = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAdd.Font = new System.Drawing.Font("Neo Sans Arabic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Image = global::MainSystem.Properties.Resources.Save_32;
            this.btnAdd.ImagePosition = 1;
            this.btnAdd.ImageZoom = 25;
            this.btnAdd.LabelPosition = 18;
            this.btnAdd.LabelText = "حفظ";
            this.btnAdd.Location = new System.Drawing.Point(385, 4);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(101, 45);
            this.btnAdd.TabIndex = 71;
            this.btnAdd.Click += new System.EventHandler(this.btnAddSalary_Click);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.dateTimePicker1.CustomFormat = "yyyy/MMMM";
            this.dateTimePicker1.Font = new System.Drawing.Font("Neo Sans Arabic", 11F);
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(314, 104);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(147, 25);
            this.dateTimePicker1.TabIndex = 0;
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Neo Sans Arabic", 11F);
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
            this.label7.Location = new System.Drawing.Point(484, 107);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 18);
            this.label7.TabIndex = 72;
            this.label7.Text = "راتب شهر ";
            // 
            // SalaryUpdate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(870, 539);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.rDelegate);
            this.Controls.Add(this.rEmployee);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.txtTotalSalary);
            this.Controls.Add(this.txtNotes);
            this.Controls.Add(this.txtDeductionsSocial);
            this.Controls.Add(this.txtDeductions);
            this.Controls.Add(this.txtStimulus);
            this.Controls.Add(this.txtSalary);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtEmployee_Number);
            this.Controls.Add(this.comEmployee);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Neo Sans Arabic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "SalaryUpdate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form5";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SalaryRecord_FormClosed);
            this.Load += new System.EventHandler(this.SalaryRecord_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comEmployee;
        private System.Windows.Forms.TextBox txtEmployee_Number;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtSalary;
        private System.Windows.Forms.TextBox txtStimulus;
        private System.Windows.Forms.TextBox txtDeductions;
        private System.Windows.Forms.TextBox txtDeductionsSocial;
        private System.Windows.Forms.TextBox txtNotes;
        private System.Windows.Forms.TextBox txtTotalSalary;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.RadioButton rDelegate;
        private System.Windows.Forms.RadioButton rEmployee;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private Bunifu.Framework.UI.BunifuTileButton btnAdd;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label7;
    }
}