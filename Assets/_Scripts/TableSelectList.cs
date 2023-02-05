using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectHaufe
{
    public enum Operation : byte
    {
        Added, Removed
    }


    public class TableSelectList : MonoBehaviour, IInitializable
    {
        public delegate void OptionsChangedEvent(TableSelectList list, TableSelectionField option, Operation operation);

        public int Priority { get; } = 10;
        [SerializeField] private Transform m_content;
        [SerializeField] private GameObject m_prefab;
        [SerializeField] private GameObject m_addButtonContainer;
        private List<TableSelectionField> m_options = new List<TableSelectionField>();
        public IReadOnlyList<TableSelectionField> Options => m_options;

        public event OptionsChangedEvent onOptionsChanged;

        private void Awake()
        {
            Initializer.Register(this);
        }

        public void Initialize()
        {
            CreateNewOption();
            TableSelectionField tsf = m_options[0];
            tsf.RefreshTables(); 
            tsf.SetDeleteButtonInteractable(false);
        }


        public void CreateNewOption()
        {
            TableSelectionField tsf = Instantiate(m_prefab, m_content).GetComponent<TableSelectionField>();
            m_options.Add(tsf);
            tsf.RefreshTables();
            tsf.onCancelButtonPressed += DeleteOption;
            onOptionsChanged?.Invoke(this, tsf, Operation.Added);
            m_addButtonContainer.transform.SetAsLastSibling();
        }

        public void DeleteOption(TableSelectionField tsf)
        {
            if (m_options.Count == 1)
                return;

            if (m_options.Remove(tsf)) {
                Destroy(tsf.gameObject);
                onOptionsChanged?.Invoke(this, tsf, Operation.Removed);
            }
        }
    } 
}
