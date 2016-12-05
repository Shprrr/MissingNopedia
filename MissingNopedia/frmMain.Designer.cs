namespace MissingNopedia
{
	partial class frmMain
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
				if (client != null) client.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Code généré par le Concepteur Windows Form

		/// <summary>
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
			this.webBrowser = new System.Windows.Forms.WebBrowser();
			this.tabControlEx = new MissingNopedia.TabControlEx();
			this.tabPagePokemon = new System.Windows.Forms.TabPage();
			this.cboPokemon = new System.Windows.Forms.ComboBox();
			this.btnSearchPokemon = new System.Windows.Forms.Button();
			this.tabPageType = new System.Windows.Forms.TabPage();
			this.tabPageMove = new System.Windows.Forms.TabPage();
			this.cboMove = new System.Windows.Forms.ComboBox();
			this.btnSearchMove = new System.Windows.Forms.Button();
			this.tabPageAbility = new System.Windows.Forms.TabPage();
			this.cboAbility = new System.Windows.Forms.ComboBox();
			this.btnSearchAbility = new System.Windows.Forms.Button();
			this.tabControlEx.SuspendLayout();
			this.tabPagePokemon.SuspendLayout();
			this.tabPageMove.SuspendLayout();
			this.tabPageAbility.SuspendLayout();
			this.SuspendLayout();
			// 
			// webBrowser
			// 
			this.webBrowser.AllowWebBrowserDrop = false;
			this.webBrowser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.webBrowser.Location = new System.Drawing.Point(12, 66);
			this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
			this.webBrowser.Name = "webBrowser";
			this.webBrowser.Size = new System.Drawing.Size(822, 683);
			this.webBrowser.TabIndex = 1;
			this.webBrowser.WebBrowserShortcutsEnabled = false;
			this.webBrowser.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.webBrowser_Navigating);
			// 
			// tabControlEx
			// 
			this.tabControlEx.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabControlEx.Appearance = System.Windows.Forms.TabAppearance.Buttons;
			this.tabControlEx.Controls.Add(this.tabPagePokemon);
			this.tabControlEx.Controls.Add(this.tabPageType);
			this.tabControlEx.Controls.Add(this.tabPageMove);
			this.tabControlEx.Controls.Add(this.tabPageAbility);
			this.tabControlEx.ItemSize = new System.Drawing.Size(103, 21);
			this.tabControlEx.Location = new System.Drawing.Point(12, 12);
			this.tabControlEx.Name = "tabControlEx";
			this.tabControlEx.SelectedIndex = 0;
			this.tabControlEx.Size = new System.Drawing.Size(822, 48);
			this.tabControlEx.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
			this.tabControlEx.TabIndex = 0;
			this.tabControlEx.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabControlEx_Selected);
			// 
			// tabPagePokemon
			// 
			this.tabPagePokemon.Controls.Add(this.cboPokemon);
			this.tabPagePokemon.Controls.Add(this.btnSearchPokemon);
			this.tabPagePokemon.Location = new System.Drawing.Point(0, 25);
			this.tabPagePokemon.Name = "tabPagePokemon";
			this.tabPagePokemon.Size = new System.Drawing.Size(822, 22);
			this.tabPagePokemon.TabIndex = 0;
			this.tabPagePokemon.Text = "Pokémon";
			// 
			// cboPokemon
			// 
			this.cboPokemon.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.cboPokemon.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this.cboPokemon.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.cboPokemon.FormattingEnabled = true;
			this.cboPokemon.Location = new System.Drawing.Point(0, 1);
			this.cboPokemon.Name = "cboPokemon";
			this.cboPokemon.Size = new System.Drawing.Size(742, 21);
			this.cboPokemon.TabIndex = 0;
			this.cboPokemon.SelectedIndexChanged += new System.EventHandler(this.btnSearchPokemon_Click);
			// 
			// btnSearchPokemon
			// 
			this.btnSearchPokemon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSearchPokemon.Location = new System.Drawing.Point(748, -1);
			this.btnSearchPokemon.Name = "btnSearchPokemon";
			this.btnSearchPokemon.Size = new System.Drawing.Size(75, 23);
			this.btnSearchPokemon.TabIndex = 1;
			this.btnSearchPokemon.Text = "&Search";
			this.btnSearchPokemon.UseVisualStyleBackColor = true;
			this.btnSearchPokemon.Click += new System.EventHandler(this.btnSearchPokemon_Click);
			// 
			// tabPageType
			// 
			this.tabPageType.Location = new System.Drawing.Point(0, 25);
			this.tabPageType.Name = "tabPageType";
			this.tabPageType.Size = new System.Drawing.Size(822, 22);
			this.tabPageType.TabIndex = 1;
			this.tabPageType.Text = "Type Effectiveness";
			// 
			// tabPageMove
			// 
			this.tabPageMove.Controls.Add(this.cboMove);
			this.tabPageMove.Controls.Add(this.btnSearchMove);
			this.tabPageMove.Location = new System.Drawing.Point(0, 25);
			this.tabPageMove.Name = "tabPageMove";
			this.tabPageMove.Size = new System.Drawing.Size(822, 22);
			this.tabPageMove.TabIndex = 2;
			this.tabPageMove.Text = "Move";
			this.tabPageMove.UseVisualStyleBackColor = true;
			// 
			// cboMove
			// 
			this.cboMove.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.cboMove.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this.cboMove.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.cboMove.FormattingEnabled = true;
			this.cboMove.Location = new System.Drawing.Point(0, 1);
			this.cboMove.Name = "cboMove";
			this.cboMove.Size = new System.Drawing.Size(742, 21);
			this.cboMove.TabIndex = 2;
			this.cboMove.SelectedIndexChanged += new System.EventHandler(this.btnSearchMove_Click);
			// 
			// btnSearchMove
			// 
			this.btnSearchMove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSearchMove.Location = new System.Drawing.Point(748, -1);
			this.btnSearchMove.Name = "btnSearchMove";
			this.btnSearchMove.Size = new System.Drawing.Size(75, 23);
			this.btnSearchMove.TabIndex = 3;
			this.btnSearchMove.Text = "&Search";
			this.btnSearchMove.UseVisualStyleBackColor = true;
			this.btnSearchMove.Click += new System.EventHandler(this.btnSearchMove_Click);
			// 
			// tabPageAbility
			// 
			this.tabPageAbility.Controls.Add(this.cboAbility);
			this.tabPageAbility.Controls.Add(this.btnSearchAbility);
			this.tabPageAbility.Location = new System.Drawing.Point(0, 25);
			this.tabPageAbility.Name = "tabPageAbility";
			this.tabPageAbility.Size = new System.Drawing.Size(822, 22);
			this.tabPageAbility.TabIndex = 3;
			this.tabPageAbility.Text = "Ability";
			this.tabPageAbility.UseVisualStyleBackColor = true;
			// 
			// cboAbility
			// 
			this.cboAbility.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.cboAbility.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this.cboAbility.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.cboAbility.FormattingEnabled = true;
			this.cboAbility.Location = new System.Drawing.Point(0, 1);
			this.cboAbility.Name = "cboAbility";
			this.cboAbility.Size = new System.Drawing.Size(742, 21);
			this.cboAbility.TabIndex = 2;
			this.cboAbility.SelectedIndexChanged += new System.EventHandler(this.btnSearchAbility_Click);
			// 
			// btnSearchAbility
			// 
			this.btnSearchAbility.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSearchAbility.Location = new System.Drawing.Point(748, -1);
			this.btnSearchAbility.Name = "btnSearchAbility";
			this.btnSearchAbility.Size = new System.Drawing.Size(75, 23);
			this.btnSearchAbility.TabIndex = 3;
			this.btnSearchAbility.Text = "&Search";
			this.btnSearchAbility.UseVisualStyleBackColor = true;
			this.btnSearchAbility.Click += new System.EventHandler(this.btnSearchAbility_Click);
			// 
			// frmMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(846, 761);
			this.Controls.Add(this.tabControlEx);
			this.Controls.Add(this.webBrowser);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "frmMain";
			this.Text = "MissingNopedia";
			this.Load += new System.EventHandler(this.frmMain_Load);
			this.tabControlEx.ResumeLayout(false);
			this.tabPagePokemon.ResumeLayout(false);
			this.tabPageMove.ResumeLayout(false);
			this.tabPageAbility.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ComboBox cboPokemon;
		private System.Windows.Forms.Button btnSearchPokemon;
		private System.Windows.Forms.WebBrowser webBrowser;
		private TabControlEx tabControlEx;
		private System.Windows.Forms.TabPage tabPagePokemon;
		private System.Windows.Forms.TabPage tabPageType;
		private System.Windows.Forms.TabPage tabPageMove;
		private System.Windows.Forms.ComboBox cboMove;
		private System.Windows.Forms.Button btnSearchMove;
		private System.Windows.Forms.TabPage tabPageAbility;
		private System.Windows.Forms.ComboBox cboAbility;
		private System.Windows.Forms.Button btnSearchAbility;
	}
}

