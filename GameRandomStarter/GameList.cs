using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GameRandomStarter
{
    public partial class GameList : Form
    {
        string listPath = Application.StartupPath + @"\GameList.json";

        JObject gameListData = new JObject();
        public GameList()
        {
            InitializeComponent();
        }

        private void GameList_Load(object sender, EventArgs e)
        {
            this.Icon = Icon.FromHandle(Properties.Resources.Icon.GetHicon());
            DataGrid.Size = new Size(Width - 140, Height - 90);
            try
            {
                using (StreamReader file = File.OpenText(listPath))
                {
                    using (JsonTextReader reader = new JsonTextReader(file))
                    {
                        gameListData = (JObject)JToken.ReadFrom(reader);
                        TagUpdate();
                        foreach (JProperty gameName in ((JObject)gameListData["Games"]).Properties())
                        {
                            DataGridViewComboBoxColumn comboCol = DataGrid.Columns[2] as DataGridViewComboBoxColumn;
                            DataGrid.Rows.Add(gameName.Name, gameName.Value["Path"], gameName.Value["Tag"].ToString());
                        }
                    }
                }
            }
            catch (System.IO.FileNotFoundException) { }
        }

        private void Done_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Search_Click(object sender, EventArgs e)
        {
            LoadJson();
            string[] data = ShowFileOpenDialog();
            if (data.Count() == 1) return;

            JArray emptyArray = new JArray();
            JObject emptyObject = new JObject();
            if (!gameListData.ContainsKey("Games")) gameListData.Add("Games", emptyObject);

            if (((JObject)gameListData["Games"]).ContainsKey(data[0]))
            {
                MessageBox.Show("이미 등록된 게임입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if(!((JObject)gameListData["Games"]).ContainsKey(data[0])) ((JObject)gameListData["Games"]).Add(data[0], emptyObject);
            DataGrid.Rows.Add(data[0], data[1], "None");
            ((JObject)gameListData["Games"][data[0]]).Add("Path", data[1]);
            ((JObject)gameListData["Games"][data[0]]).Add("Tag", "None");
            SaveJson();
        }

        public string[] ShowFileOpenDialog()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Choose Game exe file";
            ofd.FileName = "None";
            ofd.Multiselect = false;
            ofd.DereferenceLinks = false;
            ofd.AddExtension = true;
            ofd.Filter = "모든 게임 파일 (*.exe, *.url, *.lnk)| *.exe, *.url, *.lnk |게임 실행 파일 (*.exe)| *.exe |스팀 바로가기 파일 (*.url)| *.url |바로가기 파일 (*.lnk)| *.lnk";

            DialogResult result = ofd.ShowDialog();

            if (result == DialogResult.OK)
            {
                string fileName = ofd.SafeFileName;
                string fullFileName = ofd.FileName;

                return new string[] { fileName.Replace(".exe", "").Replace(".lnk", "").Replace(".url", ""), fullFileName };
            }
            else if (result == DialogResult.Cancel) { return new string[] { "" }; }

            return new string[] { "" };
        }
        private void LoadJson()
        {
            try
            {
                using (StreamReader file = File.OpenText(listPath))
                {
                    using (JsonTextReader reader = new JsonTextReader(file))
                    {
                        gameListData = (JObject)JToken.ReadFrom(reader);
                        Debug.WriteLine("Json Loaded");
                    }
                }
            }
            catch (System.IO.FileNotFoundException) { }
        }
        private void SaveJson()
        {
            try
            {
                File.WriteAllText(listPath, gameListData.ToString());

                Debug.WriteLine($"{listPath}에 파일이 저장되었습니다.");
            }
            catch (Exception e)
            {
                Debug.WriteLine($"{e.Message}");
            }
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in DataGrid.SelectedRows)
            {
                string gameName = row.Cells[0].Value.ToString();
                DataGrid.Rows.Remove(row);

                Debug.WriteLine(gameName);
                ((JObject)gameListData["Games"]).Remove(gameName);
            }
            SaveJson();
            Delete.Enabled = false;
            DataGrid.ClearSelection();
        }


        private void GameList_Resize(object sender, EventArgs e)
        {
            DataGrid.Size = new Size(Width - 140, Height - 90);
        }

        private void DataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
        private void DataGrid_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            ComboBox combo = e.Control as ComboBox;
            Debug.WriteLine(DataGrid.SelectedRows[0].Cells[2].Value);
            combo.SelectedIndex = combo.Items.IndexOf(DataGrid.SelectedRows[0].Cells[2].Value.ToString());
            if (combo != null)
            {
                combo.SelectedIndexChanged -= new EventHandler(ComboBox_SelectedIndexChanged);
                combo.SelectedIndexChanged += new EventHandler(ComboBox_SelectedIndexChanged);
            }
        }

        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            string item = cb.Text;
            if (item != null)
            {
                gameListData["Games"][DataGrid.SelectedRows[0].Cells[0].Value]["Tag"] = item;
                SaveJson();
            }
        }
        private void OnActivated(object sender, EventArgs e)
        {
            LoadJson();
            TagUpdate();
        }
        private void Tag_Click(object sender, EventArgs e)
        {
            TagAdder tagAdder = new TagAdder();
            tagAdder.ShowDialog();
            LoadJson();
            TagUpdate();
        }

        private void TagUpdate()
        {
            foreach(DataGridViewRow row in DataGrid.Rows)
            {
                row.Cells[2].Value = gameListData["Games"][row.Cells[0].Value.ToString()]["Tag"];
            }
            DataGridViewComboBoxColumn combo = DataGrid.Columns[2] as DataGridViewComboBoxColumn;
            List<string> invalidTags = new List<string>();
            foreach(string value in combo.Items)
            {
                if (!gameListData["AllTags"].Contains(value))
                    if(!invalidTags.Contains(value))
                        invalidTags.Add(value);

            }
            foreach(string tag in invalidTags)
            {
                combo.Items.Remove(tag);
            }
            foreach (DataGridViewRow row in DataGrid.Rows)
            {
                row.Cells[2].Value = row.Cells[2].Value.ToString();
            }
            if (gameListData.ContainsKey("AllTags"))
            {
                foreach (var obj in (JObject)gameListData["AllTags"])
                {
                    string tag = obj.Key.ToString();
                    if (!combo.Items.Contains(tag))
                    {
                        combo.Items.Add(tag);
                    }
                }
            }
            Debug.WriteLine("Tag Updated");
        }

        private void DataGrid_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void DataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Delete.Enabled = true;
            if (e.ColumnIndex == 2)
            {
                DataGrid.BeginEdit(true);
                ((ComboBox)DataGrid.EditingControl).DroppedDown = true;
            }
        }
    }
}
