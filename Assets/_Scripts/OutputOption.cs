using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

namespace ProjectHaufe
{
    public class OutputOption : MonoBehaviour
    {
        private StringBuilder m_stringBuilder = new StringBuilder();
        [SerializeField] private TextMeshProUGUI m_titleText;
        [SerializeField] private TextMeshProUGUI m_itemsText;

        public TableSelectionField AssociatedSelectionField { get; private set; }

        private Coroutine m_doubleClickRoutine;

        private void OnDestroy()
        {
            if(AssociatedSelectionField != null) {
                UnsubscribeEvents();
            }
        }

        public void SetSelectionField(TableSelectionField selectionField)
        {
            if(AssociatedSelectionField != null) {
                UnsubscribeEvents();
            }

            AssociatedSelectionField = selectionField;
            SubscribeEvents();

            OnTableChanged(selectionField);
        }

        private void SubscribeEvents()
        {
            AssociatedSelectionField.onItemCountChanged += OnItemCountChanged;
            AssociatedSelectionField.onTableChanged += OnTableChanged;
        }

        private void UnsubscribeEvents()
        {
            AssociatedSelectionField.onItemCountChanged -= OnItemCountChanged;
            AssociatedSelectionField.onTableChanged -= OnTableChanged;
        }

        private void OnItemCountChanged(TableSelectionField obj)
        {
            RefreshItems();
        }

        private void OnTableChanged(TableSelectionField obj)
        {
            m_titleText.text = obj.SelectedTable.TableName;
            RefreshItems();
        }

        public void RefreshItems()
        {
            int numToSelect = AssociatedSelectionField.NumItemsToSelect;
            Table table = AssociatedSelectionField.SelectedTable;

            List<string> ailments = new List<string>(table.Ailments);

            for(int i = 0; i < numToSelect && ailments.Count > 0; i++) {
                int index = UnityEngine.Random.Range(0, ailments.Count);
                m_stringBuilder.Append(ailments[index]);
                ailments.RemoveAt(index);

                m_stringBuilder.Append(", ");
            }

            if(m_stringBuilder.Length > 0) {
                m_stringBuilder.Remove(m_stringBuilder.Length - 2, 2);
            }

            m_itemsText.text = m_stringBuilder.ToString();
            m_stringBuilder.Clear();
            RefreshHeight();
        }

        public void RefreshHeight()
        {
            m_itemsText.ForceMeshUpdate();
            RectTransform rt = m_itemsText.rectTransform;

            Vector2 sizeDelta = rt.sizeDelta;
            sizeDelta.y = m_itemsText.preferredHeight;
            rt.sizeDelta = sizeDelta;

            rt = (RectTransform)transform;

            sizeDelta = rt.sizeDelta;
            sizeDelta.y = m_titleText.preferredHeight + m_itemsText.preferredHeight;

            rt.sizeDelta = sizeDelta;
        }

        public void OnClick()
        {
            if(m_doubleClickRoutine == null) {
                m_doubleClickRoutine = StartCoroutine(DoubleClickTimer());
                return;
            }

            StopCoroutine(m_doubleClickRoutine);
            m_doubleClickRoutine = null;
            RefreshItems();
        }

        public IEnumerator DoubleClickTimer()
        {
            yield return new WaitForSeconds(0.5f);
            m_doubleClickRoutine = null;
        }
    }
}
