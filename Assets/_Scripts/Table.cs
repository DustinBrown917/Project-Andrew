using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectHaufe
{
    public class Table 
    {
        public static Dictionary<string, Table> Loaded = new Dictionary<string, Table>();

        public string TableName { get; set; } = "New Table";

        public List<string> Ailments { get; set; } = new List<string>();
    } 
}
