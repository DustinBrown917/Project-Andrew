using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Text;

namespace ProjectHaufe
{
    public class TableEditor : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown m_loadedTableDropdown;
        [SerializeField] private Button m_deleteButton;
        [SerializeField] private TMP_InputField m_tableNameInput;
        [SerializeField] private TMP_InputField m_injuriesInput;
        [SerializeField] private Button m_saveButton;
        [SerializeField] private TextMeshProUGUI m_saveText;

        private Table m_loadedTable = new Table();

        private string m_originalTableName = "";
        private string m_originalInjuries = "";

        private void OnEnable()
        {
            m_loadedTable = new Table();
            UpdateLoadedTablesDropdown();
        }

        public void UpdateLoadedTablesDropdown()
        {
            List<string> loadedTableNames = new List<string>(Table.Loaded.Keys);
            loadedTableNames.Sort();
            loadedTableNames.Insert(0, "New Table");

            m_loadedTableDropdown.ClearOptions();
            m_loadedTableDropdown.AddOptions(loadedTableNames);
            m_loadedTableDropdown.SetValueWithoutNotify(0);

            for(int i = 0; i < m_loadedTableDropdown.options.Count; i++) {
                if(m_loadedTableDropdown.options[i].text == m_loadedTable.TableName) {
                    m_loadedTableDropdown.SetValueWithoutNotify(i);
                    break;
                }
            }
            
            OnLoadedTableDropdownValueChanged(m_loadedTableDropdown.value);
        }

        public void OnLoadedTableDropdownValueChanged(int newValue)
        {
            if(newValue == 0) {
                SetLoadedTable(new Table());
            } else {
                string tableName = m_loadedTableDropdown.options[newValue].text;
                Table table = Table.Loaded[tableName];
                SetLoadedTable(table);
            }
        }

        private void SetLoadedTable(Table table)
        {
            if (table == m_loadedTable)
                return;

            m_loadedTable = table;

            m_tableNameInput.text = table.TableName;
            m_originalTableName = table.TableName;
            m_originalInjuries = GenerateInjuriesString();
            m_injuriesInput.text = m_originalInjuries;
        }

        private string GenerateInjuriesString()
        {
            StringBuilder sb = new StringBuilder();
            foreach(string injury in m_loadedTable.Ailments) {
                sb.Append(injury);
                sb.Append(", ");
            }

            if(sb.Length > 0) {
                sb.Remove(sb.Length - 2, 2);
            }
            
            return sb.ToString();
        }

        public void OnSavedPressed()
        {
            if(m_tableNameInput.text != m_originalTableName) {
                Table.Loaded.Remove(m_loadedTable.TableName);
            }

            m_loadedTable.TableName = m_tableNameInput.text;
            m_originalTableName = m_loadedTable.TableName;

            if (!Table.Loaded.ContainsKey(m_loadedTable.TableName)) {
                Table.Loaded.Add(m_loadedTable.TableName, m_loadedTable);
            }

            ApplyInjuriesToTable();
            UpdateSaveButtonState();
        }

        private void ApplyInjuriesToTable()
        {
            string injuryText = m_injuriesInput.text;

            string[] injuries = injuryText.Split(',');
            for(int i = 0; i < injuries.Length; i++) {
                injuries[i] = injuries[i].Trim(' ', '\n', '\r', '\t');
            }

            m_loadedTable.Ailments = new List<string>(injuries);
            m_originalInjuries = injuryText;
        }

        public void OnDeletePressed()
        {
            if (Table.Loaded.Remove(m_loadedTable.TableName)) {
                //Save file
            }

            UpdateLoadedTablesDropdown();
        }

        public void UpdateSaveButtonState()
        {
            if (string.IsNullOrEmpty(m_tableNameInput.text)) {
                m_saveButton.interactable = false;
            } else {
                m_saveButton.interactable = true;
            }

            if(m_originalTableName != m_tableNameInput.text || m_originalInjuries != m_injuriesInput.text) {
                m_saveText.text = "Save*";
            } else {
                m_saveText.text = "Save";
            }
        }

        public void OnClosePressed()
        {
            
        }
    } 
}
