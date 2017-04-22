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
			this.cboType = new System.Windows.Forms.ComboBox();
			this.cboOperator = new System.Windows.Forms.ComboBox();
			this.txtValue = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			//
			// cboType
			//
			this.cboType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboType.FormattingEnabled = true;
			this.cboType.Location = new System.Drawing.Point(0, 0);
			this.cboType.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
			this.cboType.Name = "cboType";
			this.cboType.Size = new System.Drawing.Size(161, 21);
			this.cboType.TabIndex = 0;
			//
			// cboOperator
			//
			this.cboOperator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboOperator.FormattingEnabled = true;
			this.cboOperator.Location = new System.Drawing.Point(167, 0);
			this.cboOperator.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
			this.cboOperator.Name = "cboOperator";
			this.cboOperator.Size = new System.Drawing.Size(60, 21);
			this.cboOperator.TabIndex = 1;
			//
			// txtValue
			//
			this.txtValue.Location = new System.Drawing.Point(233, 0);
			this.txtValue.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
			this.txtValue.Name = "txtValue";
			this.txtValue.Size = new System.Drawing.Size(250, 20);
			this.txtValue.TabIndex = 2;
			//
			// Criterion
			//
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.Controls.Add(this.txtValue);
			this.Controls.Add(this.cboOperator);
			this.Controls.Add(this.cboType);
			this.Name = "Criterion";
			this.Size = new System.Drawing.Size(483, 21);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ComboBox cboType;
		private System.Windows.Forms.ComboBox cboOperator;
		private System.Windows.Forms.TextBox txtValue;
	}
}
