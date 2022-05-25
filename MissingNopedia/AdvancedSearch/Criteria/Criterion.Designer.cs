namespace MissingNopedia.AdvancedSearch.Criteria
{
	partial class Criterion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Criterion));
            this.cboType = new System.Windows.Forms.ComboBox();
            this.cboOperator = new System.Windows.Forms.ComboBox();
            this.txtValue = new System.Windows.Forms.TextBox();
            this.cboConjonction = new System.Windows.Forms.ComboBox();
            this.picHandle = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picHandle)).BeginInit();
            this.SuspendLayout();
            // 
            // cboType
            // 
            this.cboType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboType.FormattingEnabled = true;
            this.cboType.Location = new System.Drawing.Point(85, 0);
            this.cboType.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.cboType.Name = "cboType";
            this.cboType.Size = new System.Drawing.Size(187, 23);
            this.cboType.TabIndex = 1;
            // 
            // cboOperator
            // 
            this.cboOperator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboOperator.FormattingEnabled = true;
            this.cboOperator.Location = new System.Drawing.Point(278, 0);
            this.cboOperator.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.cboOperator.Name = "cboOperator";
            this.cboOperator.Size = new System.Drawing.Size(69, 23);
            this.cboOperator.TabIndex = 2;
            // 
            // txtValue
            // 
            this.txtValue.Location = new System.Drawing.Point(353, 0);
            this.txtValue.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.txtValue.Name = "txtValue";
            this.txtValue.Size = new System.Drawing.Size(250, 23);
            this.txtValue.TabIndex = 3;
            // 
            // cboConjonction
            // 
            this.cboConjonction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboConjonction.FormattingEnabled = true;
            this.cboConjonction.Items.AddRange(new object[] {
            "AND",
            "OR"});
            this.cboConjonction.Location = new System.Drawing.Point(30, 0);
            this.cboConjonction.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.cboConjonction.MaxDropDownItems = 2;
            this.cboConjonction.Name = "cboConjonction";
            this.cboConjonction.Size = new System.Drawing.Size(49, 23);
            this.cboConjonction.TabIndex = 0;
            // 
            // picHandle
            // 
            this.picHandle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picHandle.Image = ((System.Drawing.Image)(resources.GetObject("picHandle.Image")));
            this.picHandle.Location = new System.Drawing.Point(0, 0);
            this.picHandle.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.picHandle.Name = "picHandle";
            this.picHandle.Size = new System.Drawing.Size(24, 24);
            this.picHandle.TabIndex = 4;
            this.picHandle.TabStop = false;
            this.picHandle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picHandle_MouseDown);
            // 
            // Criterion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.picHandle);
            this.Controls.Add(this.cboConjonction);
            this.Controls.Add(this.txtValue);
            this.Controls.Add(this.cboOperator);
            this.Controls.Add(this.cboType);
            this.Name = "Criterion";
            this.Size = new System.Drawing.Size(603, 24);
            this.Move += new System.EventHandler(this.Criterion_ParentChanged);
            this.ParentChanged += new System.EventHandler(this.Criterion_ParentChanged);
            ((System.ComponentModel.ISupportInitialize)(this.picHandle)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ComboBox cboType;
		private System.Windows.Forms.ComboBox cboOperator;
		private System.Windows.Forms.TextBox txtValue;
		private System.Windows.Forms.ComboBox cboConjonction;
		private System.Windows.Forms.PictureBox picHandle;
	}
}
