using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectHaufe
{
    public interface IInitializable
    {
        int Priority { get; }
        void Initialize();
    } 
}
