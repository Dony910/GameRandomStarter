﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GameRandomStarter
{
    public partial class GameRandomStarter : Form
    {
        string listPath = Application.StartupPath + @"\GameList.json";

        JObject gameListData = new JObject();
        public GameRandomStarter()
        {
            InitializeComponent();
        }
        private void GameRandomStarter_Load(object sender, EventArgs e)
        {
            this.Icon = Icon.FromHandle(Properties.Resources.Icon.GetHicon());
            LoadJson();
        }

        private void Start_Click(object sender, EventArgs e)
        {
            LoadJson();
            Random random = new Random();
            int randomIndex;
            JToken tag;
            do {
                randomIndex = random.Next(((JObject)gameListData["Games"]).Count);
                tag = ((JObject)gameListData["AllTags"])[gameListData["Games"][((JObject)gameListData["Games"]).Properties().ToList()[randomIndex].Name.ToString()]["Tag"].ToString()];
            }
            while (!Convert.ToBoolean(tag));

            Debug.WriteLine(randomIndex);
            Process.Start(gameListData["Games"][((JObject)gameListData["Games"]).Properties().ToList()[randomIndex].Name.ToString()]["Path"].ToString());
        }

        private void Modify_Click(object sender, EventArgs e)
        {
            GameList gameList = new GameList();
            gameList.ShowDialog();

            LoadJson();
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
                    }
                }
            }
            catch (System.IO.FileNotFoundException) { }
        }

        private void Tag_Click(object sender, EventArgs e)
        {
            TagAdder tagAdder = new TagAdder();
            tagAdder.ShowDialog();

            LoadJson();
        }
    }
}