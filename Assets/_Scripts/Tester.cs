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
            Table.LoadTables();

            if(Table.Loaded.Count == 0) {
                Table.AddExampleTable();
            }
        }
    } 
}
