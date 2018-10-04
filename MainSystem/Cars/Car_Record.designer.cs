namespace MainSystem
{
    partial class Car_Record
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Car_Record));
            this.txtCarNumber = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCarValue = new System.Windows.Forms.TextBox();
            this.panContent = new System.Windows.Forms.Panel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnAdd = new Bunifu.Framework.UI.BunifuTileButton();
            this.label7 = new System.Windows.Forms.Label();
            this.txtPremiumDepreciation = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtDepreciationPeriod = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCarCapacity = new System.Windows.Forms.TextBox();
            this.txtOpenning_Account = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtMeterReading = new System.Windows.Forms.TextBox();
            this.bunifuDragControl1 = new Bunifu.Framework.UI.BunifuDragControl(this.components);
            this.panContent.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtCarNumber
            // 
            this.txtCarNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCarNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCarNumber.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtCarNumber.Location = new System.Drawing.Point(439, 95);
            this.txtCarNumber.Name = "txtCarNumber";
            this.txtCarNumber.Size = new System.Drawing.Size(176, 24);
            this.txtCarNumber.TabIndex = 0;
            this.txtCarNumber.TextChanged += new System.EventHandler(this.txtBox_TextChanged);
            this.txtCarNumber.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBox_KeyDown);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Neo Sans Arabic", 10F);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
            this.label1.Location = new System.Drawing.Point(619, 98);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "رقم السيارة";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Neo Sans Arabic", 10F);
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
            this.label3.Location = new System.Drawing.Point(616, 176);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(125, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "القيمة الحالية للسيارة";
            // 
            // txtCarValue
            // 
            this.txtCarValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCarValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCarValue.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtCarValue.Location = new System.Drawing.Point(439, 172);
            this.txtCarValue.Name = "txtCarValue";
            this.txtCarValue.Size = new System.Drawing.Size(176, 24);
            this.txtCarValue.TabIndex = 2;
            this.txtCarValue.TextChanged += new System.EventHandler(this.txtBox_TextChanged);
            this.txtCarValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBox_KeyDown);
            // 
            // panContent
            // 
            this.panContent.BackColor = System.Drawing.Color.White;
            this.panContent.Controls.Add(this.tableLayoutPanel2);
            this.panContent.Controls.Add(this.label7);
            this.panContent.Controls.Add(this.txtPremiumDepreciation);
            this.panContent.Controls.Add(this.label8);
            this.panContent.Controls.Add(this.txtDepreciationPeriod);
            this.panContent.Controls.Add(this.label4);
            this.panContent.Controls.Add(this.txtCarCapacity);
            this.panContent.Controls.Add(this.txtOpenning_Account);
            this.panContent.Controls.Add(this.label5);
            this.panContent.Controls.Add(this.label6);
            this.panContent.Controls.Add(this.txtMeterReading);
            this.panContent.Controls.Add(this.label3);
            this.panContent.Controls.Add(this.txtCarNumber);
            this.panContent.Controls.Add(this.txtCarValue);
            this.panContent.Controls.Add(this.label1);
            this.panContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panContent.Location = new System.Drawing.Point(0, 0);
            this.panContent.Name = "panContent";
            this.panContent.Size = new System.Drawing.Size(823, 410);
            this.panContent.TabIndex = 7;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 43.75F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 43.75F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Controls.Add(this.btnAdd, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 362);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(823, 48);
            this.tableLayoutPanel2.TabIndex = 39;
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
            this.btnAdd.ImageZoom = 20;
            this.btnAdd.LabelPosition = 18;
            this.btnAdd.LabelText = "حفظ";
            this.btnAdd.Location = new System.Drawing.Point(364, 4);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(96, 40);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Click += new System.EventHandler(this.button1_Click);
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Neo Sans Arabic", 10F);
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
            this.label7.Location = new System.Drawing.Point(326, 215);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(89, 17);
            this.label7.TabIndex = 29;
            this.label7.Text = "قيسط الاهلاك";
            // 
            // txtPremiumDepreciation
            // 
            this.txtPremiumDepreciation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPremiumDepreciation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPremiumDepreciation.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtPremiumDepreciation.Location = new System.Drawing.Point(149, 212);
            this.txtPremiumDepreciation.Name = "txtPremiumDepreciation";
            this.txtPremiumDepreciation.Size = new System.Drawing.Size(176, 24);
            this.txtPremiumDepreciation.TabIndex = 6;
            this.txtPremiumDepreciation.TextChanged += new System.EventHandler(this.txtBox_TextChanged);
            this.txtPremiumDepreciation.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBox_KeyDown);
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Neo Sans Arabic", 10F);
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
            this.label8.Location = new System.Drawing.Point(616, 213);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(78, 17);
            this.label8.TabIndex = 27;
            this.label8.Text = "فترة الاهلاك";
            // 
            // txtDepreciationPeriod
            // 
            this.txtDepreciationPeriod.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDepreciationPeriod.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDepreciationPeriod.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtDepreciationPeriod.Location = new System.Drawing.Point(439, 208);
            this.txtDepreciationPeriod.Name = "txtDepreciationPeriod";
            this.txtDepreciationPeriod.Size = new System.Drawing.Size(176, 24);
            this.txtDepreciationPeriod.TabIndex = 3;
            this.txtDepreciationPeriod.TextChanged += new System.EventHandler(this.txtBox_TextChanged);
            this.txtDepreciationPeriod.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBox_KeyDown);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Neo Sans Arabic", 10F);
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
            this.label4.Location = new System.Drawing.Point(616, 137);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(97, 17);
            this.label4.TabIndex = 24;
            this.label4.Text = "رصيد بداية المدة";
            // 
            // txtCarCapacity
            // 
            this.txtCarCapacity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCarCapacity.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCarCapacity.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtCarCapacity.Location = new System.Drawing.Point(149, 95);
            this.txtCarCapacity.Name = "txtCarCapacity";
            this.txtCarCapacity.Size = new System.Drawing.Size(176, 24);
            this.txtCarCapacity.TabIndex = 4;
            this.txtCarCapacity.TextChanged += new System.EventHandler(this.txtBox_TextChanged);
            this.txtCarCapacity.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBox_KeyDown);
            // 
            // txtOpenning_Account
            // 
            this.txtOpenning_Account.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOpenning_Account.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtOpenning_Account.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtOpenning_Account.Location = new System.Drawing.Point(439, 134);
            this.txtOpenning_Account.Name = "txtOpenning_Account";
            this.txtOpenning_Account.Size = new System.Drawing.Size(176, 24);
            this.txtOpenning_Account.TabIndex = 1;
            this.txtOpenning_Account.TextChanged += new System.EventHandler(this.txtBox_TextChanged);
            this.txtOpenning_Account.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBox_KeyDown);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Neo Sans Arabic", 10F);
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
            this.label5.Location = new System.Drawing.Point(329, 98);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(75, 17);
            this.label5.TabIndex = 20;
            this.label5.Text = "سعة التحمل";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Neo Sans Arabic", 10F);
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
            this.label6.Location = new System.Drawing.Point(328, 137);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(75, 17);
            this.label6.TabIndex = 23;
            this.label6.Text = "قراءة العداد";
            // 
            // txtMeterReading
            // 
            this.txtMeterReading.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMeterReading.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMeterReading.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtMeterReading.Location = new System.Drawing.Point(149, 134);
            this.txtMeterReading.Name = "txtMeterReading";
            this.txtMeterReading.Size = new System.Drawing.Size(176, 24);
            this.txtMeterReading.TabIndex = 5;
            this.txtMeterReading.TextChanged += new System.EventHandler(this.txtBox_TextChanged);
            this.txtMeterReading.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBox_KeyDown);
            // 
            // bunifuDragControl1
            // 
            this.bunifuDragControl1.Fixed = true;
            this.bunifuDragControl1.Horizontal = true;
            this.bunifuDragControl1.TargetControl = null;
            this.bunifuDragControl1.Vertical = true;
            // 
            // Car_Record
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(823, 410);
            this.Controls.Add(this.panContent);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(68)))), ((int)(((byte)(154)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Car_Record";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "تسجيل مخزن";
            this.panContent.ResumeLayout(false);
            this.panContent.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtCarNumber;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCarValue;
        private System.Windows.Forms.Panel panContent;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtCarCapacity;
        private System.Windows.Forms.TextBox txtOpenning_Account;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtMeterReading;
        private Bunifu.Framework.UI.BunifuDragControl bunifuDragControl1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtPremiumDepreciation;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtDepreciationPeriod;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private Bunifu.Framework.UI.BunifuTileButton btnAdd;
    }
}

