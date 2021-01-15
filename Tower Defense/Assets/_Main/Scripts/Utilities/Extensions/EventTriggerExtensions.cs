using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Utilities.Extensions
{
    public static class EventTriggerExtensions
    {
        #region BEHAVIORS

        public static void AddListener(this EventTrigger eventTrigger, EventTriggerType type, UnityAction action)
        {
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = type;
            entry.callback.AddListener((eventData) => { action(); });
            eventTrigger.triggers.Add(entry);
        }

        #endregion
    }
}
