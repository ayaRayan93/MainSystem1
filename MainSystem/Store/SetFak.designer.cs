namespace MainSystem
{
    partial class SetFak
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.comStore = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtStoreID = new System.Windows.Forms.TextBox();
            this.txtFactory = new System.Windows.Forms.TextBox();
            this.comFactory = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtSetsID = new System.Windows.Forms.TextBox();
            this.comSets = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSetQuantity = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.comType = new System.Windows.Forms.ComboBox();
            this.txtType = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.txtGroup = new System.Windows.Forms.TextBox();
            this.comGroup = new System.Windows.Forms.ComboBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ProductCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Group_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Factory_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Type_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductColor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.productSize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductSort = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button1 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.txtTotalQuantitySet = new System.Windows.Forms.TextBox();
            this.panTopPart = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.bunifuTileButton1 = new Bunifu.Framework.UI.BunifuTileButton();
            this.btnNewChooes = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panTopPart.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // comStore
            // 
            this.comStore.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.comStore.Font = new System.Drawing.Font("Tahoma", 10F);
            this.comStore.FormattingEnabled = true;
            this.comStore.Location = new System.Drawing.Point(352, 24);
            this.comStore.Name = "comStore";
            this.comStore.Size = new System.Drawing.Size(142, 24);
            this.comStore.TabIndex = 127;
            this.comStore.SelectedValueChanged += new System.EventHandler(this.comBox_SelectedValueChanged);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Neo Sans Arabic", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(500, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 16);
            this.label1.TabIndex = 128;
            this.label1.Text = "اسم المخزن";
            // 
            // txtStoreID
            // 
            this.txtStoreID.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtStoreID.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtStoreID.Location = new System.Drawing.Point(291, 24);
            this.txtStoreID.Name = "txtStoreID";
            this.txtStoreID.Size = new System.Drawing.Size(55, 24);
            this.txtStoreID.TabIndex = 129;
            this.txtStoreID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBox_KeyDown);
            // 
            // txtFactory
            // 
            this.txtFactory.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtFactory.Location = new System.Drawing.Point(61, 31);
            this.txtFactory.Name = "txtFactory";
            this.txtFactory.Size = new System.Drawing.Size(55, 24);
            this.txtFactory.TabIndex = 131;
            this.txtFactory.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBox_KeyDown);
            // 
            // comFactory
            // 
            this.comFactory.Font = new System.Drawing.Font("Tahoma", 10F);
            this.comFactory.FormattingEnabled = true;
            this.comFactory.Location = new System.Drawing.Point(122, 31);
            this.comFactory.Name = "comFactory";
            this.comFactory.Size = new System.Drawing.Size(142, 24);
            this.comFactory.TabIndex = 130;
            this.comFactory.SelectedValueChanged += new System.EventHandler(this.comBox_SelectedValueChanged);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Neo Sans Arabic", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(270, 34);
            this.label15.Name = "label15";
            this.label15.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label15.Size = new System.Drawing.Size(82, 16);
            this.label15.TabIndex = 132;
            this.label15.Text = "المصنع/المورد";
            // 
            // txtSetsID
            // 
            this.txtSetsID.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtSetsID.Location = new System.Drawing.Point(13, 14);
            this.txtSetsID.Name = "txtSetsID";
            this.txtSetsID.Size = new System.Drawing.Size(55, 24);
            this.txtSetsID.TabIndex = 134;
            this.txtSetsID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBox_KeyDown);
            // 
            // comSets
            // 
            this.comSets.Font = new System.Drawing.Font("Tahoma", 10F);
            this.comSets.FormattingEnabled = true;
            this.comSets.Location = new System.Drawing.Point(74, 14);
            this.comSets.Name = "comSets";
            this.comSets.Size = new System.Drawing.Size(142, 24);
            this.comSets.TabIndex = 133;
            this.comSets.SelectedValueChanged += new System.EventHandler(this.comBox_SelectedValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Neo Sans Arabic", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(222, 17);
            this.label2.Name = "label2";
            this.label2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label2.Size = new System.Drawing.Size(40, 16);
            this.label2.TabIndex = 135;
            this.label2.Text = "الطقم";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.groupBox1.Controls.Add(this.comSets);
            this.groupBox1.Controls.Add(this.txtSetsID);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox1.Location = new System.Drawing.Point(305, 215);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(275, 48);
            this.groupBox1.TabIndex = 136;
            this.groupBox1.TabStop = false;
            this.groupBox1.Visible = false;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Neo Sans Arabic", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(670, 313);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(148, 16);
            this.label3.TabIndex = 137;
            this.label3.Text = "كمية الطقم المراد تفككها ";
            // 
            // txtSetQuantity
            // 
            this.txtSetQuantity.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtSetQuantity.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtSetQuantity.Location = new System.Drawing.Point(535, 310);
            this.txtSetQuantity.Name = "txtSetQuantity";
            this.txtSetQuantity.Size = new System.Drawing.Size(130, 24);
            this.txtSetQuantity.TabIndex = 138;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.groupBox2.Controls.Add(this.comType);
            this.groupBox2.Controls.Add(this.txtType);
            this.groupBox2.Controls.Add(this.comFactory);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.txtFactory);
            this.groupBox2.Controls.Add(this.txtGroup);
            this.groupBox2.Controls.Add(this.comGroup);
            this.groupBox2.Font = new System.Drawing.Font("Tahoma", 10F);
            this.groupBox2.Location = new System.Drawing.Point(48, 67);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.groupBox2.Size = new System.Drawing.Size(784, 126);
            this.groupBox2.TabIndex = 140;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "فلتر";
            // 
            // comType
            // 
            this.comType.Font = new System.Drawing.Font("Tahoma", 10F);
            this.comType.FormattingEnabled = true;
            this.comType.Location = new System.Drawing.Point(516, 31);
            this.comType.Name = "comType";
            this.comType.Size = new System.Drawing.Size(131, 24);
            this.comType.TabIndex = 141;
            this.comType.SelectedValueChanged += new System.EventHandler(this.comBox_SelectedValueChanged);
            // 
            // txtType
            // 
            this.txtType.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtType.Location = new System.Drawing.Point(455, 31);
            this.txtType.Name = "txtType";
            this.txtType.Size = new System.Drawing.Size(55, 24);
            this.txtType.TabIndex = 142;
            this.txtType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBox_KeyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Neo Sans Arabic", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(653, 37);
            this.label4.Name = "label4";
            this.label4.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label4.Size = new System.Drawing.Size(26, 16);
            this.label4.TabIndex = 143;
            this.label4.Text = "نوع";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Neo Sans Arabic", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(452, 88);
            this.label14.Name = "label14";
            this.label14.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label14.Size = new System.Drawing.Size(59, 16);
            this.label14.TabIndex = 146;
            this.label14.Text = "المجموعة";
            // 
            // txtGroup
            // 
            this.txtGroup.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtGroup.Location = new System.Drawing.Point(254, 82);
            this.txtGroup.Name = "txtGroup";
            this.txtGroup.Size = new System.Drawing.Size(55, 24);
            this.txtGroup.TabIndex = 145;
            this.txtGroup.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBox_KeyDown);
            // 
            // comGroup
            // 
            this.comGroup.Font = new System.Drawing.Font("Tahoma", 10F);
            this.comGroup.FormattingEnabled = true;
            this.comGroup.Location = new System.Drawing.Point(315, 82);
            this.comGroup.Name = "comGroup";
            this.comGroup.Size = new System.Drawing.Size(131, 24);
            this.comGroup.TabIndex = 144;
            this.comGroup.SelectedValueChanged += new System.EventHandler(this.comBox_SelectedValueChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Neo Sans Arabic", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.ColumnHeadersHeight = 30;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ProductCode,
            this.ProductQuantity,
            this.Group_Name,
            this.Factory_Name,
            this.Type_Name,
            this.ProductName,
            this.ProductColor,
            this.productSize,
            this.ProductSort});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 383);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.dataGridView1.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridView1.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Neo Sans Arabic", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridView1.Size = new System.Drawing.Size(854, 215);
            this.dataGridView1.TabIndex = 141;
            // 
            // ProductCode
            // 
            this.ProductCode.HeaderText = "الكود";
            this.ProductCode.Name = "ProductCode";
            this.ProductCode.Width = 200;
            // 
            // ProductQuantity
            // 
            this.ProductQuantity.HeaderText = "الكمية";
            this.ProductQuantity.Name = "ProductQuantity";
            // 
            // Group_Name
            // 
            this.Group_Name.HeaderText = "المجموعة";
            this.Group_Name.Name = "Group_Name";
            // 
            // Factory_Name
            // 
            this.Factory_Name.HeaderText = "المصنع";
            this.Factory_Name.Name = "Factory_Name";
            // 
            // Type_Name
            // 
            this.Type_Name.HeaderText = "النوع";
            this.Type_Name.Name = "Type_Name";
            // 
            // ProductName
            // 
            this.ProductName.HeaderText = "اسم الصنف";
            this.ProductName.Name = "ProductName";
            // 
            // ProductColor
            // 
            this.ProductColor.HeaderText = "اللون";
            this.ProductColor.Name = "ProductColor";
            // 
            // productSize
            // 
            this.productSize.HeaderText = "المقاس";
            this.productSize.Name = "productSize";
            // 
            // ProductSort
            // 
            this.ProductSort.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ProductSort.HeaderText = "الفرز";
            this.ProductSort.Name = "ProductSort";
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Neo Sans Arabic", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.button1.Location = new System.Drawing.Point(184, 305);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(99, 33);
            this.button1.TabIndex = 148;
            this.button1.Text = "فك";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.btnFak_Click);
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Neo Sans Arabic", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(704, 282);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(112, 16);
            this.label5.TabIndex = 149;
            this.label5.Text = "اجمالي كمية الطقم";
            // 
            // txtTotalQuantitySet
            // 
            this.txtTotalQuantitySet.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtTotalQuantitySet.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtTotalQuantitySet.Location = new System.Drawing.Point(535, 280);
            this.txtTotalQuantitySet.Name = "txtTotalQuantitySet";
            this.txtTotalQuantitySet.ReadOnly = true;
            this.txtTotalQuantitySet.Size = new System.Drawing.Size(130, 24);
            this.txtTotalQuantitySet.TabIndex = 150;
            // 
            // panTopPart
            // 
            this.panTopPart.Controls.Add(this.btnNewChooes);
            this.panTopPart.Controls.Add(this.groupBox1);
            this.panTopPart.Controls.Add(this.txtTotalQuantitySet);
            this.panTopPart.Controls.Add(this.comStore);
            this.panTopPart.Controls.Add(this.label5);
            this.panTopPart.Controls.Add(this.label1);
            this.panTopPart.Controls.Add(this.button1);
            this.panTopPart.Controls.Add(this.txtStoreID);
            this.panTopPart.Controls.Add(this.label3);
            this.panTopPart.Controls.Add(this.groupBox2);
            this.panTopPart.Controls.Add(this.txtSetQuantity);
            this.panTopPart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panTopPart.Location = new System.Drawing.Point(3, 3);
            this.panTopPart.Name = "panTopPart";
            this.panTopPart.Size = new System.Drawing.Size(854, 374);
            this.panTopPart.TabIndex = 151;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panTopPart, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.dataGridView1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 380F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(860, 661);
            this.tableLayoutPanel1.TabIndex = 152;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 43.75F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 43.75F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Controls.Add(this.bunifuTileButton1, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 604);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(854, 54);
            this.tableLayoutPanel2.TabIndex = 152;
            // 
            // bunifuTileButton1
            // 
            this.bunifuTileButton1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.bunifuTileButton1.color = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.bunifuTileButton1.colorActive = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.bunifuTileButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bunifuTileButton1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bunifuTileButton1.Font = new System.Drawing.Font("Neo Sans Arabic", 9.75F);
            this.bunifuTileButton1.ForeColor = System.Drawing.Color.White;
            this.bunifuTileButton1.Image = global::MainSystem.Properties.Resources.Save_321;
            this.bunifuTileButton1.ImagePosition = 1;
            this.bunifuTileButton1.ImageZoom = 25;
            this.bunifuTileButton1.LabelPosition = 12;
            this.bunifuTileButton1.LabelText = "حفظ";
            this.bunifuTileButton1.Location = new System.Drawing.Point(378, 4);
            this.bunifuTileButton1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.bunifuTileButton1.Name = "bunifuTileButton1";
            this.bunifuTileButton1.Size = new System.Drawing.Size(100, 46);
            this.bunifuTileButton1.TabIndex = 0;
            this.bunifuTileButton1.Click += new System.EventHandler(this.btnDone_Click);
            // 
            // btnNewChooes
            // 
            this.btnNewChooes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.btnNewChooes.FlatAppearance.BorderSize = 0;
            this.btnNewChooes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNewChooes.Font = new System.Drawing.Font("Neo Sans Arabic", 11F);
            this.btnNewChooes.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnNewChooes.Location = new System.Drawing.Point(22, 16);
            this.btnNewChooes.Name = "btnNewChooes";
            this.btnNewChooes.Size = new System.Drawing.Size(103, 36);
            this.btnNewChooes.TabIndex = 151;
            this.btnNewChooes.Text = "اختيار اخر";
            this.btnNewChooes.UseVisualStyleBackColor = false;
            this.btnNewChooes.Click += new System.EventHandler(this.btnNewChooes_Click);
            // 
            // SetFak
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(860, 661);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "SetFak";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panTopPart.ResumeLayout(false);
            this.panTopPart.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comStore;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtStoreID;
        private System.Windows.Forms.TextBox txtFactory;
        private System.Windows.Forms.ComboBox comFactory;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtSetsID;
        private System.Windows.Forms.ComboBox comSets;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtSetQuantity;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox comType;
        private System.Windows.Forms.TextBox txtType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtGroup;
        private System.Windows.Forms.ComboBox comGroup;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtTotalQuantitySet;
        private System.Windows.Forms.Panel panTopPart;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductQuantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn Group_Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn Factory_Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn Type_Name;
        private new System.Windows.Forms.DataGridViewTextBoxColumn ProductName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductColor;
        private System.Windows.Forms.DataGridViewTextBoxColumn productSize;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductSort;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private Bunifu.Framework.UI.BunifuTileButton bunifuTileButton1;
        private System.Windows.Forms.Button btnNewChooes;
    }
}

