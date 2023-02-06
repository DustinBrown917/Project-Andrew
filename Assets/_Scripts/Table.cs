using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


namespace ProjectHaufe
{
    public class Table 
    {
        private static string SAVE_PATH => Application.persistentDataPath + "/Tables.json";
        
        public static Dictionary<string, Table> Loaded = new Dictionary<string, Table>();

        public static void LoadTables()
        {
            Debug.Log("Loading from " + SAVE_PATH);

            if(!File.Exists(SAVE_PATH)) {
                Debug.Log("Tables not found. Creating new.");
                Loaded = new Dictionary<string, Table>();
                return;
            }

            string json = File.ReadAllText(SAVE_PATH);

            Dictionary<string, Table> loaded = JsonConvert.DeserializeObject<Dictionary<string, Table>>(json);

            if(loaded == null) {
                Debug.LogError("Problem loading tables.");
                Loaded= new Dictionary<string, Table>();
                return;
            }

            Loaded = loaded;
        }

        public static void SaveTables()
        {
            Debug.Log("Saving to " + SAVE_PATH);

            File.WriteAllText(SAVE_PATH, JsonConvert.SerializeObject(Loaded));
        }

        public static void AddExampleTable()
        {
            Table table = new Table();
            table.TableName = "Example Table";
            table.Ailments.Add("Haufe's Hemorrhoid");
            table.Ailments.Add("Marc's Mouth Maggots");
            table.Ailments.Add("Gini's Gutworms");
            table.Ailments.Add("Dustin's Dick Itch");
            table.Ailments.Add("Karl's Krotchrot");
            table.Ailments.Add("Cartwright's Colon Cream");
            table.Ailments.Add("Kat's Kidney Canker");
            table.Ailments.Add("Lindy's Lungpuss");
            table.Ailments.Add("Rebekah's Retinitis");
            Loaded.Add(table.TableName, table);
        }

        public string TableName { get; set; } = "New Table";

        public List<string> Ailments { get; set; } = new List<string>();
    } 
}
