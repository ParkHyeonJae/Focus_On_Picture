using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

public class XRTriggerDetector : MonoBehaviour
{
    [System.Serializable]
    public class XRTrigger
    {
        public GenEnums.Tags[] targetTags;

        private HashSet<string> _tagSet;
        public HashSet<string> tagSet
        {
            get
            {
                if (_tagSet == null)
                {
                    _tagSet = new HashSet<string>();

                    foreach (var tag in targetTags)
                        _tagSet.Add(tag.ToString());
                }

                return _tagSet;
            }
        }

        [System.Serializable]
        public class TriggerEvent : UnityEvent<Collider> { }
        public TriggerEvent onTriggerEnterCallback;
        public TriggerEvent onTriggerStayCallback;
        public TriggerEvent onTriggerExitCallback;


    }


    public List<XRTrigger> xRTriggers;

    

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        foreach (var trigger in xRTriggers)
        {
            if (!trigger.tagSet.Contains(other.tag))
                continue;
            trigger.onTriggerEnterCallback.Invoke(other);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        foreach (var trigger in xRTriggers)
        {
            if (!trigger.tagSet.Contains(other.tag))
                continue;
            trigger.onTriggerStayCallback.Invoke(other);
        }
    }


    private void OnTriggerExit(Collider other)
    {
        foreach (var trigger in xRTriggers)
        {
            if (!trigger.tagSet.Contains(other.tag))
                continue;
            trigger.onTriggerExitCallback.Invoke(other);
        }
    }
}
