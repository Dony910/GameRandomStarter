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
    public partial class TagAdder : Form
    {
        string listPath = Application.StartupPath + @"\GameList.json";

        JObject gameListData = new JObject();
        public TagAdder()
        {
            InitializeComponent();
        }
        private void TagAdder_Load(object sender, EventArgs e)
        {
            this.Icon = Icon.FromHandle(Properties.Resources.Icon.GetHicon());
            try
            {
                using (StreamReader file = File.OpenText(listPath))
                {
                    using (JsonTextReader reader = new JsonTextReader(file))
                    {
                        gameListData = (JObject)JToken.ReadFrom(reader);
                        JArray emptyArray = new JArray();
                        if (!gameListData.ContainsKey("AllTags")) gameListData.Add("AllTags", emptyArray);
                        foreach (string gameTag in gameListData["AllTags"])
                        {
                            TagList.Items.Add(gameTag);
                        }
                    }
                }
            }
            catch (System.IO.FileNotFoundException) { }
        }

        private void Add_Click(object sender, EventArgs e)
        {
            JArray emptyArray = new JArray();
            if (!gameListData.ContainsKey("AllTags")) gameListData.Add("AllTags", emptyArray);
            string tag = textBox1.Text;
            if (((JArray)gameListData["AllTags"]).Any(j => j.ToString() == tag))
            {
                MessageBox.Show("이미 등록된 태그입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            ((JArray)gameListData["AllTags"]).Add(tag);
            TagList.Items.Add(tag);
            textBox1.Text = "";

            SaveJson();
        }
        private void Delete_Click(object sender, EventArgs e)
        {
            string tag = TagList.SelectedItem.ToString();
            TagList.Items.Remove(TagList.SelectedItem);
            ((JArray)gameListData["AllTags"]).Remove(((JArray)gameListData["AllTags"]).FirstOrDefault(j => j.ToString() == tag));
            foreach (JProperty game in ((JObject)gameListData["Games"]).Properties())
            {
                if (gameListData["Games"][game.Name]["Tag"].ToString() == tag)
                    game.Value["Tag"] = "";
            }

            SaveJson();
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

        private void Done_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
