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
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Panel panBtnCriteria;
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.btnAddCriterion = new System.Windows.Forms.Button();
            this.btnRemoveCriterion = new System.Windows.Forms.Button();
            this.tlpCriteria = new System.Windows.Forms.TableLayoutPanel();
            this.flpCriteria = new System.Windows.Forms.FlowLayoutPanel();
            this.webBrowser = new System.Windows.Forms.WebBrowser();
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
            this.ColAbility1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColAbility2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColHiddenAbility = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.splitSearch = new System.Windows.Forms.SplitContainer();
            this.lblFound = new System.Windows.Forms.Label();
            this.tabControlEx = new MissingNopedia.TabControlEx();
            this.tabPagePokemon = new System.Windows.Forms.TabPage();
            this.btnBackPokemon = new System.Windows.Forms.Button();
            this.cboPokemon = new System.Windows.Forms.ComboBox();
            this.btnSearchPokemon = new System.Windows.Forms.Button();
            this.tabPageType = new System.Windows.Forms.TabPage();
            this.tabPageMove = new System.Windows.Forms.TabPage();
            this.btnBackMove = new System.Windows.Forms.Button();
            this.cboMove = new System.Windows.Forms.ComboBox();
            this.btnSearchMove = new System.Windows.Forms.Button();
            this.tabPageAbility = new System.Windows.Forms.TabPage();
            this.btnBackAbility = new System.Windows.Forms.Button();
            this.cboAbility = new System.Windows.Forms.ComboBox();
            this.btnSearchAbility = new System.Windows.Forms.Button();
            this.tabPageSearch = new System.Windows.Forms.TabPage();
            this.btnOptions = new System.Windows.Forms.Button();
            label1 = new System.Windows.Forms.Label();
            panBtnCriteria = new System.Windows.Forms.Panel();
            panBtnCriteria.SuspendLayout();
            this.tlpCriteria.SuspendLayout();
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
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point);
            label1.Location = new System.Drawing.Point(4, 3);
            label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(59, 16);
            label1.TabIndex = 1;
            label1.Text = "Results";
            // 
            // panBtnCriteria
            // 
            panBtnCriteria.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            panBtnCriteria.AutoSize = true;
            panBtnCriteria.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            panBtnCriteria.Controls.Add(this.btnAddCriterion);
            panBtnCriteria.Controls.Add(this.btnRemoveCriterion);
            panBtnCriteria.Location = new System.Drawing.Point(0, 115);
            panBtnCriteria.Margin = new System.Windows.Forms.Padding(0);
            panBtnCriteria.Name = "panBtnCriteria";
            panBtnCriteria.Size = new System.Drawing.Size(215, 30);
            panBtnCriteria.TabIndex = 5;
            // 
            // btnAddCriterion
            // 
            this.btnAddCriterion.Location = new System.Drawing.Point(0, 0);
            this.btnAddCriterion.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnAddCriterion.Name = "btnAddCriterion";
            this.btnAddCriterion.Size = new System.Drawing.Size(88, 27);
            this.btnAddCriterion.TabIndex = 3;
            this.btnAddCriterion.Text = "&Add Criterion";
            this.btnAddCriterion.UseVisualStyleBackColor = true;
            this.btnAddCriterion.Click += new System.EventHandler(this.btnAddCriterion_Click);
            // 
            // btnRemoveCriterion
            // 
            this.btnRemoveCriterion.Location = new System.Drawing.Point(94, 0);
            this.btnRemoveCriterion.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnRemoveCriterion.Name = "btnRemoveCriterion";
            this.btnRemoveCriterion.Size = new System.Drawing.Size(117, 27);
            this.btnRemoveCriterion.TabIndex = 4;
            this.btnRemoveCriterion.Text = "&Remove Criterion";
            this.btnRemoveCriterion.UseVisualStyleBackColor = true;
            this.btnRemoveCriterion.Click += new System.EventHandler(this.btnRemoveCriterion_Click);
            // 
            // tlpCriteria
            // 
            this.tlpCriteria.AutoScroll = true;
            this.tlpCriteria.AutoSize = true;
            this.tlpCriteria.ColumnCount = 1;
            this.tlpCriteria.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpCriteria.Controls.Add(this.flpCriteria, 0, 0);
            this.tlpCriteria.Controls.Add(panBtnCriteria, 0, 1);
            this.tlpCriteria.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpCriteria.Location = new System.Drawing.Point(0, 0);
            this.tlpCriteria.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tlpCriteria.Name = "tlpCriteria";
            this.tlpCriteria.RowCount = 2;
            this.tlpCriteria.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpCriteria.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpCriteria.Size = new System.Drawing.Size(1026, 145);
            this.tlpCriteria.TabIndex = 6;
            // 
            // flpCriteria
            // 
            this.flpCriteria.AutoSize = true;
            this.flpCriteria.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpCriteria.Location = new System.Drawing.Point(4, 3);
            this.flpCriteria.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.flpCriteria.Name = "flpCriteria";
            this.flpCriteria.Size = new System.Drawing.Size(0, 0);
            this.flpCriteria.TabIndex = 0;
            // 
            // webBrowser
            // 
            this.webBrowser.AllowWebBrowserDrop = false;
            this.webBrowser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.webBrowser.Location = new System.Drawing.Point(14, 76);
            this.webBrowser.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.webBrowser.MinimumSize = new System.Drawing.Size(23, 23);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.Size = new System.Drawing.Size(1026, 791);
            this.webBrowser.TabIndex = 1;
            this.webBrowser.WebBrowserShortcutsEnabled = false;
            this.webBrowser.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.webBrowser_Navigating);
            // 
            // btnAdvancedSearch
            // 
            this.btnAdvancedSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdvancedSearch.Location = new System.Drawing.Point(938, 0);
            this.btnAdvancedSearch.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnAdvancedSearch.Name = "btnAdvancedSearch";
            this.btnAdvancedSearch.Size = new System.Drawing.Size(88, 27);
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
            this.ColSpecialTank,
            this.ColAbility1,
            this.ColAbility2,
            this.ColHiddenAbility});
            this.dgvResult.Location = new System.Drawing.Point(0, 33);
            this.dgvResult.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.dgvResult.Name = "dgvResult";
            this.dgvResult.ReadOnly = true;
            this.dgvResult.Size = new System.Drawing.Size(1026, 605);
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
            this.ColTotalStat.Width = 61;
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
            this.ColSpecialTank.DividerWidth = 4;
            this.ColSpecialTank.HeaderText = "Special Tank";
            this.ColSpecialTank.Name = "ColSpecialTank";
            this.ColSpecialTank.ReadOnly = true;
            this.ColSpecialTank.Width = 5;
            // 
            // ColAbility1
            // 
            this.ColAbility1.HeaderText = "Ability";
            this.ColAbility1.Name = "ColAbility1";
            this.ColAbility1.ReadOnly = true;
            this.ColAbility1.Width = 5;
            // 
            // ColAbility2
            // 
            this.ColAbility2.HeaderText = "Ability";
            this.ColAbility2.Name = "ColAbility2";
            this.ColAbility2.ReadOnly = true;
            this.ColAbility2.Width = 5;
            // 
            // ColHiddenAbility
            // 
            this.ColHiddenAbility.HeaderText = "Hidden Ability";
            this.ColHiddenAbility.Name = "ColHiddenAbility";
            this.ColHiddenAbility.ReadOnly = true;
            this.ColHiddenAbility.Width = 5;
            // 
            // splitSearch
            // 
            this.splitSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitSearch.BackColor = System.Drawing.SystemColors.ControlDark;
            this.splitSearch.Location = new System.Drawing.Point(14, 76);
            this.splitSearch.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.splitSearch.Name = "splitSearch";
            this.splitSearch.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitSearch.Panel1
            // 
            this.splitSearch.Panel1.AutoScroll = true;
            this.splitSearch.Panel1.BackColor = System.Drawing.SystemColors.Control;
            this.splitSearch.Panel1.Controls.Add(this.tlpCriteria);
            // 
            // splitSearch.Panel2
            // 
            this.splitSearch.Panel2.BackColor = System.Drawing.SystemColors.Control;
            this.splitSearch.Panel2.Controls.Add(this.lblFound);
            this.splitSearch.Panel2.Controls.Add(this.btnAdvancedSearch);
            this.splitSearch.Panel2.Controls.Add(this.dgvResult);
            this.splitSearch.Panel2.Controls.Add(label1);
            this.splitSearch.Size = new System.Drawing.Size(1026, 791);
            this.splitSearch.SplitterDistance = 145;
            this.splitSearch.SplitterWidth = 5;
            this.splitSearch.TabIndex = 3;
            // 
            // lblFound
            // 
            this.lblFound.AutoSize = true;
            this.lblFound.Location = new System.Drawing.Point(91, 6);
            this.lblFound.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFound.Name = "lblFound";
            this.lblFound.Size = new System.Drawing.Size(48, 15);
            this.lblFound.TabIndex = 4;
            this.lblFound.Text = "0 found";
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
            this.tabControlEx.Location = new System.Drawing.Point(14, 14);
            this.tabControlEx.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tabControlEx.Name = "tabControlEx";
            this.tabControlEx.SelectedIndex = 0;
            this.tabControlEx.Size = new System.Drawing.Size(1026, 55);
            this.tabControlEx.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControlEx.TabIndex = 0;
            this.tabControlEx.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabControlEx_Selected);
            // 
            // tabPagePokemon
            // 
            this.tabPagePokemon.Controls.Add(this.btnBackPokemon);
            this.tabPagePokemon.Controls.Add(this.cboPokemon);
            this.tabPagePokemon.Controls.Add(this.btnSearchPokemon);
            this.tabPagePokemon.Location = new System.Drawing.Point(0, 25);
            this.tabPagePokemon.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tabPagePokemon.Name = "tabPagePokemon";
            this.tabPagePokemon.Size = new System.Drawing.Size(1026, 29);
            this.tabPagePokemon.TabIndex = 0;
            this.tabPagePokemon.Text = "Pokémon";
            this.tabPagePokemon.UseVisualStyleBackColor = true;
            // 
            // btnBackPokemon
            // 
            this.btnBackPokemon.Enabled = false;
            this.btnBackPokemon.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnBackPokemon.Location = new System.Drawing.Point(0, -1);
            this.btnBackPokemon.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnBackPokemon.Name = "btnBackPokemon";
            this.btnBackPokemon.Size = new System.Drawing.Size(27, 27);
            this.btnBackPokemon.TabIndex = 2;
            this.btnBackPokemon.Text = "&<";
            this.btnBackPokemon.UseVisualStyleBackColor = true;
            this.btnBackPokemon.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // cboPokemon
            // 
            this.cboPokemon.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboPokemon.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboPokemon.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboPokemon.FormattingEnabled = true;
            this.cboPokemon.Location = new System.Drawing.Point(34, 1);
            this.cboPokemon.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cboPokemon.Name = "cboPokemon";
            this.cboPokemon.Size = new System.Drawing.Size(902, 23);
            this.cboPokemon.TabIndex = 0;
            this.cboPokemon.SelectedIndexChanged += new System.EventHandler(this.btnSearchPokemon_Click);
            this.cboPokemon.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cboPokemon_KeyUp);
            // 
            // btnSearchPokemon
            // 
            this.btnSearchPokemon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearchPokemon.Location = new System.Drawing.Point(938, -1);
            this.btnSearchPokemon.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnSearchPokemon.Name = "btnSearchPokemon";
            this.btnSearchPokemon.Size = new System.Drawing.Size(88, 27);
            this.btnSearchPokemon.TabIndex = 1;
            this.btnSearchPokemon.Text = "&Search";
            this.btnSearchPokemon.UseVisualStyleBackColor = true;
            this.btnSearchPokemon.Click += new System.EventHandler(this.btnSearchPokemon_Click);
            // 
            // tabPageType
            // 
            this.tabPageType.Location = new System.Drawing.Point(0, 25);
            this.tabPageType.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tabPageType.Name = "tabPageType";
            this.tabPageType.Size = new System.Drawing.Size(1026, 29);
            this.tabPageType.TabIndex = 1;
            this.tabPageType.Text = "Type Effectiveness";
            this.tabPageType.UseVisualStyleBackColor = true;
            // 
            // tabPageMove
            // 
            this.tabPageMove.Controls.Add(this.btnBackMove);
            this.tabPageMove.Controls.Add(this.cboMove);
            this.tabPageMove.Controls.Add(this.btnSearchMove);
            this.tabPageMove.Location = new System.Drawing.Point(0, 25);
            this.tabPageMove.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tabPageMove.Name = "tabPageMove";
            this.tabPageMove.Size = new System.Drawing.Size(1026, 29);
            this.tabPageMove.TabIndex = 2;
            this.tabPageMove.Text = "Move";
            this.tabPageMove.UseVisualStyleBackColor = true;
            // 
            // btnBackMove
            // 
            this.btnBackMove.Enabled = false;
            this.btnBackMove.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnBackMove.Location = new System.Drawing.Point(0, -1);
            this.btnBackMove.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnBackMove.Name = "btnBackMove";
            this.btnBackMove.Size = new System.Drawing.Size(27, 27);
            this.btnBackMove.TabIndex = 2;
            this.btnBackMove.Text = "&<";
            this.btnBackMove.UseVisualStyleBackColor = true;
            this.btnBackMove.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // cboMove
            // 
            this.cboMove.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboMove.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboMove.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboMove.FormattingEnabled = true;
            this.cboMove.Location = new System.Drawing.Point(34, 1);
            this.cboMove.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cboMove.Name = "cboMove";
            this.cboMove.Size = new System.Drawing.Size(835, 23);
            this.cboMove.TabIndex = 0;
            this.cboMove.SelectedIndexChanged += new System.EventHandler(this.btnSearchMove_Click);
            this.cboMove.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cboMove_KeyUp);
            // 
            // btnSearchMove
            // 
            this.btnSearchMove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearchMove.Location = new System.Drawing.Point(871, -1);
            this.btnSearchMove.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnSearchMove.Name = "btnSearchMove";
            this.btnSearchMove.Size = new System.Drawing.Size(88, 27);
            this.btnSearchMove.TabIndex = 1;
            this.btnSearchMove.Text = "&Search";
            this.btnSearchMove.UseVisualStyleBackColor = true;
            this.btnSearchMove.Click += new System.EventHandler(this.btnSearchMove_Click);
            // 
            // tabPageAbility
            // 
            this.tabPageAbility.Controls.Add(this.btnBackAbility);
            this.tabPageAbility.Controls.Add(this.cboAbility);
            this.tabPageAbility.Controls.Add(this.btnSearchAbility);
            this.tabPageAbility.Location = new System.Drawing.Point(0, 25);
            this.tabPageAbility.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tabPageAbility.Name = "tabPageAbility";
            this.tabPageAbility.Size = new System.Drawing.Size(1026, 29);
            this.tabPageAbility.TabIndex = 3;
            this.tabPageAbility.Text = "Ability";
            this.tabPageAbility.UseVisualStyleBackColor = true;
            // 
            // btnBackAbility
            // 
            this.btnBackAbility.Enabled = false;
            this.btnBackAbility.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnBackAbility.Location = new System.Drawing.Point(0, -1);
            this.btnBackAbility.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnBackAbility.Name = "btnBackAbility";
            this.btnBackAbility.Size = new System.Drawing.Size(27, 27);
            this.btnBackAbility.TabIndex = 2;
            this.btnBackAbility.Text = "&<";
            this.btnBackAbility.UseVisualStyleBackColor = true;
            this.btnBackAbility.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // cboAbility
            // 
            this.cboAbility.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboAbility.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboAbility.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboAbility.FormattingEnabled = true;
            this.cboAbility.Location = new System.Drawing.Point(34, 1);
            this.cboAbility.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cboAbility.Name = "cboAbility";
            this.cboAbility.Size = new System.Drawing.Size(835, 23);
            this.cboAbility.TabIndex = 0;
            this.cboAbility.SelectedIndexChanged += new System.EventHandler(this.btnSearchAbility_Click);
            this.cboAbility.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cboAbility_KeyUp);
            // 
            // btnSearchAbility
            // 
            this.btnSearchAbility.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearchAbility.Location = new System.Drawing.Point(871, -1);
            this.btnSearchAbility.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnSearchAbility.Name = "btnSearchAbility";
            this.btnSearchAbility.Size = new System.Drawing.Size(88, 27);
            this.btnSearchAbility.TabIndex = 1;
            this.btnSearchAbility.Text = "&Search";
            this.btnSearchAbility.UseVisualStyleBackColor = true;
            this.btnSearchAbility.Click += new System.EventHandler(this.btnSearchAbility_Click);
            // 
            // tabPageSearch
            // 
            this.tabPageSearch.Location = new System.Drawing.Point(0, 25);
            this.tabPageSearch.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tabPageSearch.Name = "tabPageSearch";
            this.tabPageSearch.Size = new System.Drawing.Size(1026, 29);
            this.tabPageSearch.TabIndex = 4;
            this.tabPageSearch.Text = "Advanced Search";
            this.tabPageSearch.UseVisualStyleBackColor = true;
            // 
            // btnOptions
            // 
            this.btnOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOptions.Location = new System.Drawing.Point(965, 9);
            this.btnOptions.Name = "btnOptions";
            this.btnOptions.Size = new System.Drawing.Size(75, 23);
            this.btnOptions.TabIndex = 4;
            this.btnOptions.Text = "&Options";
            this.btnOptions.UseVisualStyleBackColor = true;
            this.btnOptions.Click += new System.EventHandler(this.btnOptions_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1054, 881);
            this.Controls.Add(this.btnOptions);
            this.Controls.Add(this.splitSearch);
            this.Controls.Add(this.tabControlEx);
            this.Controls.Add(this.webBrowser);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "frmMain";
            this.Text = "MissingNopedia";
            this.Load += new System.EventHandler(this.frmMain_Load);
            panBtnCriteria.ResumeLayout(false);
            this.tlpCriteria.ResumeLayout(false);
            this.tlpCriteria.PerformLayout();
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
		private System.Windows.Forms.FlowLayoutPanel flpCriteria;
		private System.Windows.Forms.Button btnAdvancedSearch;
		private System.Windows.Forms.DataGridView dgvResult;
		private System.Windows.Forms.SplitContainer splitSearch;
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
		private System.Windows.Forms.DataGridViewTextBoxColumn ColAbility1;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColAbility2;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColHiddenAbility;
		private System.Windows.Forms.Button btnBackPokemon;
		private System.Windows.Forms.Button btnBackMove;
		private System.Windows.Forms.Button btnBackAbility;
		private System.Windows.Forms.Button btnRemoveCriterion;
		private System.Windows.Forms.Button btnAddCriterion;
		private System.Windows.Forms.Label lblFound;
		private System.Windows.Forms.TableLayoutPanel tlpCriteria;
		private System.Windows.Forms.Button btnOptions;
	}
}

