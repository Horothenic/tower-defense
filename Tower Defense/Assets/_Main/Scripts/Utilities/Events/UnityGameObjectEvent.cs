using UnityEngine;
using UnityEngine.Events;
using System;

namespace Utilities.Events
{
    [Serializable]
    public class UnityGameObjectEvent : UnityEvent<GameObject>
    { }
}