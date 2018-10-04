namespace MainSystem
{
    partial class Product_Record
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Product_Record));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comFactory = new System.Windows.Forms.ComboBox();
            this.comProduct = new System.Windows.Forms.ComboBox();
            this.comGroup = new System.Windows.Forms.ComboBox();
            this.comType = new System.Windows.Forms.ComboBox();
            this.txtProduct = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtGroup = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtFactory = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtType = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnAdd = new Bunifu.Framework.UI.BunifuTileButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtSort = new System.Windows.Forms.TextBox();
            this.txtSize = new System.Windows.Forms.TextBox();
            this.txtColor = new System.Windows.Forms.TextBox();
            this.txtCarton = new System.Windows.Forms.TextBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.txtClassification = new System.Windows.Forms.TextBox();
            this.comSort = new System.Windows.Forms.ComboBox();
            this.comSize = new System.Windows.Forms.ComboBox();
            this.comColour = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.ImageProduct = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ImageProduct)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.comFactory);
            this.groupBox1.Controls.Add(this.comProduct);
            this.groupBox1.Controls.Add(this.comGroup);
            this.groupBox1.Controls.Add(this.comType);
            this.groupBox1.Controls.Add(this.txtProduct);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtGroup);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtFactory);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtType);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // comFactory
            // 
            resources.ApplyResources(this.comFactory, "comFactory");
            this.comFactory.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.comFactory.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comFactory.FormattingEnabled = true;
            this.comFactory.Name = "comFactory";
            this.comFactory.SelectedValueChanged += new System.EventHandler(this.comBox_SelectedValueChanged);
            this.comFactory.TextChanged += new System.EventHandler(this.txtBox_TextChanged);
            // 
            // comProduct
            // 
            resources.ApplyResources(this.comProduct, "comProduct");
            this.comProduct.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.comProduct.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comProduct.BackColor = System.Drawing.Color.White;
            this.comProduct.FormattingEnabled = true;
            this.comProduct.Name = "comProduct";
            this.comProduct.SelectedValueChanged += new System.EventHandler(this.comBox_SelectedValueChanged);
            this.comProduct.TextChanged += new System.EventHandler(this.txtBox_TextChanged);
            // 
            // comGroup
            // 
            resources.ApplyResources(this.comGroup, "comGroup");
            this.comGroup.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.comGroup.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comGroup.FormattingEnabled = true;
            this.comGroup.Name = "comGroup";
            this.comGroup.SelectedValueChanged += new System.EventHandler(this.comBox_SelectedValueChanged);
            this.comGroup.TextChanged += new System.EventHandler(this.txtBox_TextChanged);
            // 
            // comType
            // 
            resources.ApplyResources(this.comType, "comType");
            this.comType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.comType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comType.FormattingEnabled = true;
            this.comType.Name = "comType";
            this.comType.SelectedValueChanged += new System.EventHandler(this.comBox_SelectedValueChanged);
            this.comType.TextChanged += new System.EventHandler(this.txtBox_TextChanged);
            // 
            // txtProduct
            // 
            resources.ApplyResources(this.txtProduct, "txtProduct");
            this.txtProduct.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtProduct.Name = "txtProduct";
            this.txtProduct.TextChanged += new System.EventHandler(this.txtBox_TextChanged);
            this.txtProduct.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBox_KeyDown);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(68)))), ((int)(((byte)(53)))));
            this.label4.Name = "label4";
            // 
            // txtGroup
            // 
            resources.ApplyResources(this.txtGroup, "txtGroup");
            this.txtGroup.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtGroup.Name = "txtGroup";
            this.txtGroup.TextChanged += new System.EventHandler(this.txtBox_TextChanged);
            this.txtGroup.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBox_KeyDown);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(68)))), ((int)(((byte)(53)))));
            this.label3.Name = "label3";
            // 
            // txtFactory
            // 
            resources.ApplyResources(this.txtFactory, "txtFactory");
            this.txtFactory.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFactory.Name = "txtFactory";
            this.txtFactory.TextChanged += new System.EventHandler(this.txtBox_TextChanged);
            this.txtFactory.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBox_KeyDown);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(68)))), ((int)(((byte)(53)))));
            this.label2.Name = "label2";
            // 
            // txtType
            // 
            resources.ApplyResources(this.txtType, "txtType");
            this.txtType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtType.Name = "txtType";
            this.txtType.TextChanged += new System.EventHandler(this.txtBox_TextChanged);
            this.txtType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBox_KeyDown);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(68)))), ((int)(((byte)(53)))));
            this.label1.Name = "label1";
            // 
            // panel3
            // 
            resources.ApplyResources(this.panel3, "panel3");
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.tableLayoutPanel2);
            this.panel3.Controls.Add(this.panel1);
            this.panel3.Name = "panel3";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            resources.ApplyResources(this.tableLayoutPanel2, "tableLayoutPanel2");
            this.tableLayoutPanel2.Controls.Add(this.btnAdd, 1, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.btnAdd.color = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.btnAdd.colorActive = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(65)))), ((int)(((byte)(146)))));
            this.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.btnAdd, "btnAdd");
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Image = global::MainSystem.Properties.Resources.Save_321;
            this.btnAdd.ImagePosition = 1;
            this.btnAdd.ImageZoom = 25;
            this.btnAdd.LabelPosition = 18;
            this.btnAdd.LabelText = "حفظ";
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Click += new System.EventHandler(this.btnAddProduct_Click);
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.ImageProduct);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Name = "panel1";
            // 
            // groupBox2
            // 
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Controls.Add(this.txtSort);
            this.groupBox2.Controls.Add(this.txtSize);
            this.groupBox2.Controls.Add(this.txtColor);
            this.groupBox2.Controls.Add(this.txtCarton);
            this.groupBox2.Controls.Add(this.txtDescription);
            this.groupBox2.Controls.Add(this.txtClassification);
            this.groupBox2.Controls.Add(this.comSort);
            this.groupBox2.Controls.Add(this.comSize);
            this.groupBox2.Controls.Add(this.comColour);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // txtSort
            // 
            resources.ApplyResources(this.txtSort, "txtSort");
            this.txtSort.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSort.Name = "txtSort";
            this.txtSort.TextChanged += new System.EventHandler(this.txtBox_TextChanged);
            this.txtSort.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBox_KeyDown);
            // 
            // txtSize
            // 
            resources.ApplyResources(this.txtSize, "txtSize");
            this.txtSize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSize.Name = "txtSize";
            this.txtSize.TextChanged += new System.EventHandler(this.txtBox_TextChanged);
            this.txtSize.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBox_KeyDown);
            // 
            // txtColor
            // 
            resources.ApplyResources(this.txtColor, "txtColor");
            this.txtColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtColor.Name = "txtColor";
            this.txtColor.TextChanged += new System.EventHandler(this.txtBox_TextChanged);
            this.txtColor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBox_KeyDown);
            // 
            // txtCarton
            // 
            resources.ApplyResources(this.txtCarton, "txtCarton");
            this.txtCarton.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCarton.Name = "txtCarton";
            // 
            // txtDescription
            // 
            resources.ApplyResources(this.txtDescription, "txtDescription");
            this.txtDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBox_KeyDown);
            // 
            // txtClassification
            // 
            resources.ApplyResources(this.txtClassification, "txtClassification");
            this.txtClassification.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtClassification.Name = "txtClassification";
            this.txtClassification.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBox_KeyDown);
            // 
            // comSort
            // 
            resources.ApplyResources(this.comSort, "comSort");
            this.comSort.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.comSort.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comSort.FormattingEnabled = true;
            this.comSort.Name = "comSort";
            this.comSort.SelectedIndexChanged += new System.EventHandler(this.comBox_SelectedValueChanged);
            // 
            // comSize
            // 
            resources.ApplyResources(this.comSize, "comSize");
            this.comSize.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.comSize.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comSize.FormattingEnabled = true;
            this.comSize.Name = "comSize";
            this.comSize.SelectedIndexChanged += new System.EventHandler(this.comBox_SelectedValueChanged);
            // 
            // comColour
            // 
            resources.ApplyResources(this.comColour, "comColour");
            this.comColour.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.comColour.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comColour.FormattingEnabled = true;
            this.comColour.Name = "comColour";
            this.comColour.SelectedIndexChanged += new System.EventHandler(this.comBox_SelectedValueChanged);
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(68)))), ((int)(((byte)(53)))));
            this.label10.Name = "label10";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(68)))), ((int)(((byte)(53)))));
            this.label9.Name = "label9";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(68)))), ((int)(((byte)(53)))));
            this.label5.Name = "label5";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(68)))), ((int)(((byte)(53)))));
            this.label6.Name = "label6";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(68)))), ((int)(((byte)(53)))));
            this.label7.Name = "label7";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(68)))), ((int)(((byte)(53)))));
            this.label8.Name = "label8";
            // 
            // ImageProduct
            // 
            this.ImageProduct.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(122)))), ((int)(((byte)(190)))));
            this.ImageProduct.BackgroundImage = global::MainSystem.Properties.Resources.camara1;
            resources.ApplyResources(this.ImageProduct, "ImageProduct");
            this.ImageProduct.Name = "ImageProduct";
            this.ImageProduct.TabStop = false;
            this.ImageProduct.Click += new System.EventHandler(this.ImageProduct_Click);
            // 
            // Product_Record
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel3);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(182)))), ((int)(((byte)(92)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Product_Record";
            this.Load += new System.EventHandler(this.Product_Record_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ImageProduct)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtGroup;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtFactory;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtProduct;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox ImageProduct;
        private System.Windows.Forms.ComboBox comProduct;
        private System.Windows.Forms.ComboBox comGroup;
        private System.Windows.Forms.ComboBox comType;
        private System.Windows.Forms.ComboBox comFactory;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private Bunifu.Framework.UI.BunifuTileButton btnAdd;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtCarton;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.TextBox txtClassification;
        private System.Windows.Forms.ComboBox comSort;
        private System.Windows.Forms.ComboBox comSize;
        private System.Windows.Forms.ComboBox comColour;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtSort;
        private System.Windows.Forms.TextBox txtSize;
        private System.Windows.Forms.TextBox txtColor;
    }
}

