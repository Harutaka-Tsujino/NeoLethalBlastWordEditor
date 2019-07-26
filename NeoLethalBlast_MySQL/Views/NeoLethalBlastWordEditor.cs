using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using NeoLethalBlast_MySQL.Models.Services;

namespace NeoLethalBlast_MySQL.Views
{
	public partial class NeoLethalBlastWordEditor : Form
	{
		private DataTable bindingDataTable;
		private string currentTableName;
		private int tableAutoIncrement;

		public NeoLethalBlastWordEditor()
		{
			InitializeComponent();
		}

		private bool MeansEnumuration(string typeString)
		{
			return typeString.Contains("enum");
		}

		private DataGridViewComboBoxColumn ExtractEnumComponent(DataRow dataRow)
		{
			DataGridViewComboBoxColumn dGVComboBoxColumn = new DataGridViewComboBoxColumn();
			dGVComboBoxColumn.HeaderText = dataRow[0].ToString();

			var enumComponents = Regex.Matches(GetTypeString(dataRow), @"(\')(?<enumComponent>.+?)(\')");

			BindingSource bindingSource = new BindingSource();

			foreach (var enumComponent in enumComponents.Cast<Match>())
			{
				bindingSource.Add(enumComponent.Value.Trim('\''));
			}

			dGVComboBoxColumn.DataSource = bindingSource;

			return dGVComboBoxColumn;
		}

		private string GetTypeString(DataRow dataRow)
		{
			const int TYPE_INFORMATION_INDEX = 1;

			return dataRow[TYPE_INFORMATION_INDEX].ToString();
		}

		private int GetHaeaderTextRowIndex(DataGridViewComboBoxColumn dGVComboBoxColumn)
		{
			foreach (var dGVColumn in WordDataGrid.Columns.Cast<DataGridViewColumn>())
			{
				if (dGVColumn.HeaderCell.Value.ToString() == dGVComboBoxColumn.HeaderText)
				{
					return dGVColumn.Index;
				}
			}

			throw new Exception("インデックスが見つかりませんでした");
		}
		
		private void ReplaceColumnWithComboBox(DataRow dataRow)
		{
			var dGVComboBoxColumn = ExtractEnumComponent(dataRow);

			var index = GetHaeaderTextRowIndex(dGVComboBoxColumn);

			WordDataGrid.Columns.Insert(index, dGVComboBoxColumn);

			foreach (var dataGridRow in WordDataGrid.Rows.Cast<DataGridViewRow>())
			{
				dataGridRow.Cells[index].Value = dataGridRow.Cells[index + 1].Value;
			}

			WordDataGrid.Columns.Remove(WordDataGrid.Columns[index + 1]);
		}

		private void CreateEnumComboBox(DataRow dataRow)
		{
			if (!MeansEnumuration(GetTypeString(dataRow))) return;

			ReplaceColumnWithComboBox(dataRow);
		}

		private void LoadDataBase()
		{
			MySqlConnection connection = MySQLConnector.Connect();

			MySqlDataAdapter tableSource = new MySqlDataAdapter("SELECT * FROM " + currentTableName, connection);
			bindingDataTable = new DataTable();

			tableSource.Fill(bindingDataTable);

			MySqlDataAdapter tableInformation = new MySqlDataAdapter("DESCRIBE " + currentTableName, connection);

			//コンボボックスを挿入する際に以前のものを消さなければ挿入される場所がおかしくなる
			WordDataGrid.Columns.Clear();

			WordDataGrid.DataSource = bindingDataTable;

			DataTable tableInformationTable = new DataTable();
			tableInformation.Fill(tableInformationTable);

			foreach (var dataRow in tableInformationTable.Rows.Cast<DataRow>())
			{
				CreateEnumComboBox(dataRow);
			}

			WordDataGrid.Columns[0].ReadOnly = true;

			FetchTableAutoIncrement();
		}

		private void NeoLethalBlastWordEditor_Load(object sender, EventArgs e)
		{
			try
			{
				currentTableName = "word_datas";

				LoadDataBase();

				bindingDataTable.TableNewRow += new DataTableNewRowEventHandler(SetID);
				bindingDataTable.RowDeleting += new DataRowChangeEventHandler(StoreDeletedID);

				WordDataGrid.AllowUserToDeleteRows = false;
			}

			catch (MySqlException me)
			{
				Console.WriteLine("ERROR: " + me.Message);
			}
		}

		private void FetchTableAutoIncrement()
		{
			MySqlConnection connection = MySQLConnector.Connect();

			MySqlDataAdapter autoIncrementData = new MySqlDataAdapter(
				"SELECT auto_increment FROM information_schema.TABLES WHERE table_name = " + "\'" + currentTableName + "\';",
				connection);

			DataTable autoIncrementTable = new DataTable();
			autoIncrementData.Fill(autoIncrementTable);

			if (autoIncrementTable.Rows[0][0].GetType() == typeof(DBNull))
			{
				tableAutoIncrement = 1;

				return;
			}

			 tableAutoIncrement = int.Parse(autoIncrementTable.Rows[0][0].ToString());
		}

		private void SetID(object sender, EventArgs e)
		{
			int LastRowIndex = bindingDataTable.Rows.Count - 1;

			if (LastRowIndex < 0) return;

			if (bindingDataTable.Rows[LastRowIndex].RowState != DataRowState.Added) return;

			const int PRIMARY_KEY_COLUMN_INDEX = 0;

			int prevIndex = 0;

			for (int i = LastRowIndex - 1; i >= 0; --i)
			{
				if (bindingDataTable.Rows[i].RowState == DataRowState.Deleted) continue;

				prevIndex = int.Parse(bindingDataTable.Rows[i][PRIMARY_KEY_COLUMN_INDEX].ToString()) + 1;

				break;
			}

			if (tableAutoIncrement > prevIndex) prevIndex = tableAutoIncrement;

			var objectValue = bindingDataTable.Rows[LastRowIndex][PRIMARY_KEY_COLUMN_INDEX];

			if (objectValue.GetType() != typeof(DBNull)) return;

			bindingDataTable.Rows[LastRowIndex][PRIMARY_KEY_COLUMN_INDEX] = prevIndex;

			tableAutoIncrement = prevIndex + 1;
		}

		private void StoreDeletedID(object sender, EventArgs e)
		{
			
		}

		private void WordDataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{

		}

		private void ExecuteDeleteCmd()
		{

		}

		private void Write_Click(object sender, EventArgs e)
		{
			for (var rowIndex = 0; rowIndex < bindingDataTable.Rows.Count; ++rowIndex)
			{
				for (var columnIndex = 1; columnIndex < bindingDataTable.Columns.Count; ++columnIndex)
				{
					bindingDataTable.Rows[rowIndex][columnIndex] = WordDataGrid.Rows[rowIndex].Cells[columnIndex].Value;
				}
			}

			MySqlConnection connection = MySQLConnector.Connect();

			foreach (var dataRow in bindingDataTable.Rows.Cast<DataRow>())
			{
				MySqlCommand cmd = null;

				switch (dataRow.RowState)
				{
					case DataRowState.Unchanged:

						break;

					case DataRowState.Deleted:

						break;

					case DataRowState.Modified:
						cmd = new MySqlCommand(ModifyRow(dataRow), connection);

						break;

					case DataRowState.Added:
						cmd = new MySqlCommand(AddRow(dataRow), connection);

						break;

					default:
						break;
				}

				if (cmd == null) continue;

				cmd.ExecuteNonQuery();
			}

			LoadDataBase();
		}

		private string NormalizeValueString(string value, Type valueType)
		{
			if (valueType == typeof(bool))
			{
				if (value == "" || value == "False") value = "FALSE";

				if (value == "True") value = "TRUE";
			}

			if(valueType == typeof(DBNull))
			{
				value = "FALSE";
			}

			if (valueType == typeof(string))
			{
				if(value == "")
				{
					value = "FALSE";

					return value;
				}

				value = "'" + value + "'";
			}

			return value;
		}

		private string ModifyRow(DataRow dataRow)
		{
			var commandTextBuider = new StringBuilder();
			commandTextBuider.Append("UPDATE ");
			commandTextBuider.Append(currentTableName);
			commandTextBuider.Append(" SET ");

			for (int columnIndex = 1; columnIndex < bindingDataTable.Columns.Count; ++columnIndex)
			{
				var value = dataRow[columnIndex].ToString();
				var valueType = dataRow[columnIndex].GetType();

				commandTextBuider.Append("`");
				commandTextBuider.Append(WordDataGrid.Columns[columnIndex].HeaderText);
				commandTextBuider.Append("` = ");

				commandTextBuider.Append(NormalizeValueString(value, valueType));

				commandTextBuider.Append(", ");
			}

			commandTextBuider.Remove(commandTextBuider.Length - 2, 2);

			commandTextBuider.Append(" WHERE (");
			commandTextBuider.Append("`id` = '");
			commandTextBuider.Append(dataRow[0].ToString());
			commandTextBuider.Append("');");

			return commandTextBuider.ToString();
		}

		private string AddRow(DataRow dataRow)
		{
			var commandTextBuider = new StringBuilder();
			commandTextBuider.Append("INSERT INTO ");
			commandTextBuider.Append(currentTableName);
			commandTextBuider.Append(" VALUES (");

			for (int columnIndex = 0; columnIndex < bindingDataTable.Columns.Count; ++columnIndex)
			{
				var value = dataRow[columnIndex].ToString();

				var valueType = dataRow[columnIndex].GetType();

				commandTextBuider.Append(NormalizeValueString(value, valueType));

				commandTextBuider.Append(", ");
			}

			commandTextBuider.Remove(commandTextBuider.Length - 2, 2);

			commandTextBuider.Append(")");
			commandTextBuider.Append(";");

			return commandTextBuider.ToString();
		}
	}
}
