using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectHaufe
{
    public class Initializer : MonoBehaviour
    {
        private static List<IInitializable> m_initializables = new List<IInitializable>();

        public static void Register(IInitializable initializable)
        {
            for(int i = 0; i < m_initializables.Count; i++) {
                if(initializable.Priority > m_initializables[i].Priority) {
                    m_initializables.Insert(i, initializable);
                    return;
                }
            }

            m_initializables.Add(initializable);
        }

        // Start is called before the first frame update
        void Start()
        {
            foreach(IInitializable initializable in m_initializables) {
                initializable.Initialize();
            }
        }

    } 
}
