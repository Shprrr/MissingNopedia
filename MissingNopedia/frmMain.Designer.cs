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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
			this.webBrowser = new System.Windows.Forms.WebBrowser();
			this.label1 = new System.Windows.Forms.Label();
			this.flpCriteria = new System.Windows.Forms.FlowLayoutPanel();
			this.btnAdvancedSearch = new System.Windows.Forms.Button();
			this.dgvResult = new System.Windows.Forms.DataGridView();
			this.ColNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColType1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColType2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColHP = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColAttack = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColDefense = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColSpAttack = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColSpDefense = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColSpeed = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColTotalStat = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColPhysicalSweeper = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColSpecialSweeper = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColWall = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColPhysicalTank = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColSpecialTank = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.splitSearch = new System.Windows.Forms.SplitContainer();
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
			this.tabPageSearch = new System.Windows.Forms.TabPage();
			((System.ComponentModel.ISupportInitialize)(this.dgvResult)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.splitSearch)).BeginInit();
			this.splitSearch.Panel1.SuspendLayout();
			this.splitSearch.Panel2.SuspendLayout();
			this.splitSearch.SuspendLayout();
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
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(3, 3);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(60, 16);
			this.label1.TabIndex = 1;
			this.label1.Text = "Results";
			// 
			// flpCriteria
			// 
			this.flpCriteria.AutoSize = true;
			this.flpCriteria.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flpCriteria.Location = new System.Drawing.Point(0, 0);
			this.flpCriteria.Name = "flpCriteria";
			this.flpCriteria.Size = new System.Drawing.Size(822, 126);
			this.flpCriteria.TabIndex = 0;
			// 
			// btnAdvancedSearch
			// 
			this.btnAdvancedSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnAdvancedSearch.Location = new System.Drawing.Point(748, 0);
			this.btnAdvancedSearch.Name = "btnAdvancedSearch";
			this.btnAdvancedSearch.Size = new System.Drawing.Size(75, 23);
			this.btnAdvancedSearch.TabIndex = 2;
			this.btnAdvancedSearch.Text = "&Search";
			this.btnAdvancedSearch.UseVisualStyleBackColor = true;
			this.btnAdvancedSearch.Click += new System.EventHandler(this.btnAdvancedSearch_Click);
			// 
			// dgvResult
			// 
			this.dgvResult.AllowUserToAddRows = false;
			this.dgvResult.AllowUserToDeleteRows = false;
			this.dgvResult.AllowUserToOrderColumns = true;
			dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.dgvResult.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
			this.dgvResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dgvResult.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader;
			this.dgvResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvResult.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColNumber,
            this.ColName,
            this.ColType1,
            this.ColType2,
            this.ColHP,
            this.ColAttack,
            this.ColDefense,
            this.ColSpAttack,
            this.ColSpDefense,
            this.ColSpeed,
            this.ColTotalStat,
            this.ColPhysicalSweeper,
            this.ColSpecialSweeper,
            this.ColWall,
            this.ColPhysicalTank,
            this.ColSpecialTank});
			this.dgvResult.Location = new System.Drawing.Point(0, 29);
			this.dgvResult.Name = "dgvResult";
			this.dgvResult.ReadOnly = true;
			this.dgvResult.Size = new System.Drawing.Size(822, 654);
			this.dgvResult.TabIndex = 3;
			// 
			// ColNumber
			// 
			this.ColNumber.Frozen = true;
			this.ColNumber.HeaderText = "#";
			this.ColNumber.Name = "ColNumber";
			this.ColNumber.ReadOnly = true;
			this.ColNumber.Width = 5;
			// 
			// ColName
			// 
			this.ColName.Frozen = true;
			this.ColName.HeaderText = "Name";
			this.ColName.Name = "ColName";
			this.ColName.ReadOnly = true;
			this.ColName.Width = 5;
			// 
			// ColType1
			// 
			dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
			this.ColType1.DefaultCellStyle = dataGridViewCellStyle2;
			this.ColType1.HeaderText = "Type";
			this.ColType1.Name = "ColType1";
			this.ColType1.ReadOnly = true;
			this.ColType1.Width = 5;
			// 
			// ColType2
			// 
			dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
			this.ColType2.DefaultCellStyle = dataGridViewCellStyle3;
			this.ColType2.HeaderText = "Type";
			this.ColType2.Name = "ColType2";
			this.ColType2.ReadOnly = true;
			this.ColType2.Width = 5;
			// 
			// ColHP
			// 
			this.ColHP.HeaderText = "HP";
			this.ColHP.Name = "ColHP";
			this.ColHP.ReadOnly = true;
			this.ColHP.Width = 5;
			// 
			// ColAttack
			// 
			this.ColAttack.HeaderText = "Attack";
			this.ColAttack.Name = "ColAttack";
			this.ColAttack.ReadOnly = true;
			this.ColAttack.Width = 5;
			// 
			// ColDefense
			// 
			this.ColDefense.HeaderText = "Defense";
			this.ColDefense.Name = "ColDefense";
			this.ColDefense.ReadOnly = true;
			this.ColDefense.Width = 5;
			// 
			// ColSpAttack
			// 
			this.ColSpAttack.HeaderText = "Special Attack";
			this.ColSpAttack.Name = "ColSpAttack";
			this.ColSpAttack.ReadOnly = true;
			this.ColSpAttack.Width = 5;
			// 
			// ColSpDefense
			// 
			this.ColSpDefense.HeaderText = "Special Defense";
			this.ColSpDefense.Name = "ColSpDefense";
			this.ColSpDefense.ReadOnly = true;
			this.ColSpDefense.Width = 5;
			// 
			// ColSpeed
			// 
			this.ColSpeed.HeaderText = "Speed";
			this.ColSpeed.Name = "ColSpeed";
			this.ColSpeed.ReadOnly = true;
			this.ColSpeed.Width = 5;
			// 
			// ColTotalStat
			// 
			this.ColTotalStat.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.ColTotalStat.DividerWidth = 4;
			this.ColTotalStat.HeaderText = "Total";
			this.ColTotalStat.Name = "ColTotalStat";
			this.ColTotalStat.ReadOnly = true;
			this.ColTotalStat.Width = 60;
			// 
			// ColPhysicalSweeper
			// 
			this.ColPhysicalSweeper.HeaderText = "Physical Sweeper";
			this.ColPhysicalSweeper.Name = "ColPhysicalSweeper";
			this.ColPhysicalSweeper.ReadOnly = true;
			this.ColPhysicalSweeper.Width = 5;
			// 
			// ColSpecialSweeper
			// 
			this.ColSpecialSweeper.HeaderText = "Special Sweeper";
			this.ColSpecialSweeper.Name = "ColSpecialSweeper";
			this.ColSpecialSweeper.ReadOnly = true;
			this.ColSpecialSweeper.Width = 5;
			// 
			// ColWall
			// 
			this.ColWall.HeaderText = "Wall";
			this.ColWall.Name = "ColWall";
			this.ColWall.ReadOnly = true;
			this.ColWall.Width = 5;
			// 
			// ColPhysicalTank
			// 
			this.ColPhysicalTank.HeaderText = "Physical Tank";
			this.ColPhysicalTank.Name = "ColPhysicalTank";
			this.ColPhysicalTank.ReadOnly = true;
			this.ColPhysicalTank.Width = 5;
			// 
			// ColSpecialTank
			// 
			this.ColSpecialTank.HeaderText = "Special Tank";
			this.ColSpecialTank.Name = "ColSpecialTank";
			this.ColSpecialTank.ReadOnly = true;
			this.ColSpecialTank.Width = 5;
			// 
			// splitSearch
			// 
			this.splitSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.splitSearch.Location = new System.Drawing.Point(12, 66);
			this.splitSearch.Name = "splitSearch";
			this.splitSearch.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitSearch.Panel1
			// 
			this.splitSearch.Panel1.Controls.Add(this.flpCriteria);
			this.splitSearch.Panel1Collapsed = true;
			// 
			// splitSearch.Panel2
			// 
			this.splitSearch.Panel2.Controls.Add(this.btnAdvancedSearch);
			this.splitSearch.Panel2.Controls.Add(this.dgvResult);
			this.splitSearch.Panel2.Controls.Add(this.label1);
			this.splitSearch.Size = new System.Drawing.Size(822, 683);
			this.splitSearch.SplitterDistance = 126;
			this.splitSearch.TabIndex = 3;
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
			this.tabControlEx.Controls.Add(this.tabPageSearch);
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
			this.tabPagePokemon.UseVisualStyleBackColor = true;
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
			this.cboPokemon.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cboPokemon_KeyUp);
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
			this.tabPageType.UseVisualStyleBackColor = true;
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
			this.cboMove.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cboMove_KeyUp);
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
			this.cboAbility.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cboAbility_KeyUp);
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
			// tabPageSearch
			// 
			this.tabPageSearch.Location = new System.Drawing.Point(0, 25);
			this.tabPageSearch.Name = "tabPageSearch";
			this.tabPageSearch.Size = new System.Drawing.Size(822, 22);
			this.tabPageSearch.TabIndex = 4;
			this.tabPageSearch.Text = "Advanced Search";
			this.tabPageSearch.UseVisualStyleBackColor = true;
			// 
			// frmMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(846, 761);
			this.Controls.Add(this.splitSearch);
			this.Controls.Add(this.tabControlEx);
			this.Controls.Add(this.webBrowser);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "frmMain";
			this.Text = "MissingNopedia";
			this.Load += new System.EventHandler(this.frmMain_Load);
			((System.ComponentModel.ISupportInitialize)(this.dgvResult)).EndInit();
			this.splitSearch.Panel1.ResumeLayout(false);
			this.splitSearch.Panel1.PerformLayout();
			this.splitSearch.Panel2.ResumeLayout(false);
			this.splitSearch.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitSearch)).EndInit();
			this.splitSearch.ResumeLayout(false);
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
		private System.Windows.Forms.TabPage tabPageSearch;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.FlowLayoutPanel flpCriteria;
		private System.Windows.Forms.Button btnAdvancedSearch;
		private System.Windows.Forms.DataGridView dgvResult;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColNumber;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColName;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColType1;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColType2;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColHP;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColAttack;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColDefense;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColSpAttack;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColSpDefense;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColSpeed;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColTotalStat;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColPhysicalSweeper;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColSpecialSweeper;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColWall;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColPhysicalTank;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColSpecialTank;
		private System.Windows.Forms.SplitContainer splitSearch;
	}
}

