namespace NeoLethalBlast_MySQL.Views
{
	partial class NeoLethalBlastWordEditor
	{
		/// <summary>
		/// 必要なデザイナー変数です。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 使用中のリソースをすべてクリーンアップします。
		/// </summary>
		/// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows フォーム デザイナーで生成されたコード

		/// <summary>
		/// デザイナー サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディターで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
			this.WordDataGrid = new System.Windows.Forms.DataGridView();
			this.Write = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.WordDataGrid)).BeginInit();
			this.SuspendLayout();
			// 
			// WordDataGrid
			// 
			this.WordDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.WordDataGrid.Location = new System.Drawing.Point(12, 12);
			this.WordDataGrid.Name = "WordDataGrid";
			this.WordDataGrid.RowTemplate.Height = 21;
			this.WordDataGrid.Size = new System.Drawing.Size(776, 271);
			this.WordDataGrid.TabIndex = 0;
			this.WordDataGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.WordDataGrid_CellContentClick);
			// 
			// Write
			// 
			this.Write.Location = new System.Drawing.Point(713, 289);
			this.Write.Name = "Write";
			this.Write.Size = new System.Drawing.Size(75, 23);
			this.Write.TabIndex = 1;
			this.Write.Text = "保存";
			this.Write.UseVisualStyleBackColor = true;
			this.Write.Click += new System.EventHandler(this.Write_Click);
			// 
			// NeoLethalBlastWordEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.Write);
			this.Controls.Add(this.WordDataGrid);
			this.Name = "NeoLethalBlastWordEditor";
			this.Text = "NeoLethalBlastWordEditor";
			this.Load += new System.EventHandler(this.NeoLethalBlastWordEditor_Load);
			((System.ComponentModel.ISupportInitialize)(this.WordDataGrid)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataGridView WordDataGrid;
		private System.Windows.Forms.Button Write;
	}
}

