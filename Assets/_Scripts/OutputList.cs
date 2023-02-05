using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectHaufe
{
    public class OutputList : MonoBehaviour
    {
        [SerializeField] private TableSelectList tableSelectList;
        [SerializeField] private Transform m_content;
        [SerializeField] private GameObject m_outputOptionPrefab;

        private List<OutputOption> m_outputOptions = new List<OutputOption>();

        private void Start()
        {
            tableSelectList.onOptionsChanged += OnOptionsChanged;
        }

        private void OnOptionsChanged(TableSelectList list, TableSelectionField option, Operation operation)
        {
            switch (operation) {
                case Operation.Added:
                    CreateNewOutputOption();
                    m_outputOptions[^1].SetSelectionField(option);
                    break;
                case Operation.Removed:
                    for(int i = 0; i < m_outputOptions.Count; i++) {
                        if (m_outputOptions[i].AssociatedSelectionField == option) {
                            Destroy(m_outputOptions[i].gameObject);
                            m_outputOptions.RemoveAt(i);
                            return;
                        }
                    }
                    break;
            }
        }

        private void CreateNewOutputOption()
        {
            OutputOption newOption = Instantiate(m_outputOptionPrefab, m_content).GetComponent<OutputOption>();
            m_outputOptions.Add(newOption);
        }
    } 
}
