using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

namespace ProjectHaufe
{
    public class TableSelectionField : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown m_tableDropDown;
        [SerializeField] private TMP_InputField m_itemCountInput;
        [SerializeField] private Button m_deleteButton;

        public Table SelectedTable { get; private set; }
        public int NumItemsToSelect { get; private set; }

        public event Action<TableSelectionField> onTableChanged;
        public event Action<TableSelectionField> onItemCountChanged;
        public event Action<TableSelectionField> onCancelButtonPressed;

        public void RefreshTables()
        {
            string currentSelection = m_tableDropDown.options[m_tableDropDown.value].text;

            m_tableDropDown.ClearOptions();

            if(Table.Loaded.Count == 0) {
                SelectedTable = null;
                onTableChanged?.Invoke(this);
                return;
            }

            List<string> tableNames = new List<string>(Table.Loaded.Keys);

            tableNames.Sort();

            m_tableDropDown.AddOptions(tableNames);

            int selectionIndex = tableNames.IndexOf(currentSelection);
            if (selectionIndex == -1)
                selectionIndex = 0;

            m_tableDropDown.SetValueWithoutNotify(selectionIndex);
            OnTableSelectionChanged(selectionIndex);

            OnItemCountChanged(m_itemCountInput.text);
        }

        public void OnCancelButtonPressed()
        {
            onCancelButtonPressed?.Invoke(this);
        }

        public void SetDeleteButtonInteractable(bool interactable)
        {
            m_deleteButton.interactable = interactable;
        }

        public void OnTableSelectionChanged(int selection)
        {
            string tableName = m_tableDropDown.options[selection].text;
            SelectedTable = Table.Loaded[tableName];
            onTableChanged?.Invoke(this);
        }

        public void OnItemCountChanged(string newValue)
        {
            int newVal = ValidateItemCount(newValue);

            if(newVal.ToString() != newValue) {
                m_itemCountInput.SetTextWithoutNotify(newVal.ToString());
            }
            
            if(newVal != NumItemsToSelect) {
                NumItemsToSelect = newVal;
                onItemCountChanged?.Invoke(this);
            }
        }

        public int ValidateItemCount(string value)
        {
            if(!int.TryParse(value, out int intVal)) {
                return 0;
            }

            intVal = Mathf.Clamp(intVal, 1, SelectedTable.Ailments.Count);

            return intVal;
        }
    } 
}
