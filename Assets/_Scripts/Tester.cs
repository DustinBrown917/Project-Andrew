using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

namespace ProjectHaufe
{
    public class Tester : MonoBehaviour, IInitializable
    {
        public int Priority { get; } = 100;

        private void Awake()
        {
            Initializer.Register(this);
        }

        public void Initialize()
        {
            Table table = new Table();

            table.TableName = "Shoulder";
            table.Ailments.Add("Apley's Scratch");
            table.Ailments.Add("Drop Arm");
            table.Ailments.Add("Hawkin's Kennedy");
            table.Ailments.Add("Empty Can");
            table.Ailments.Add("Doofus' Dink");
            table.Ailments.Add("Full Can");
            table.Ailments.Add("Do The Can Can");
            table.Ailments.Add("Speed's");
            table.Ailments.Add("Neer's Impingement");

            Table.Loaded.Add(table.TableName, table);

            Table table2 = new Table();

            table2.TableName = "Knee";
            table2.Ailments.Add("Osgood Shitter");
            table2.Ailments.Add("Bursitis");
            table2.Ailments.Add("Booboo");
            table2.Ailments.Add("ACL Sprain");
            table2.Ailments.Add("Emmett's Gambit");
            table2.Ailments.Add("Zorzit's Wingnut");
            table2.Ailments.Add("Knobby Knees");
            table2.Ailments.Add("Goonism");

            Table.Loaded.Add(table2.TableName, table2);
        }
    } 
}
