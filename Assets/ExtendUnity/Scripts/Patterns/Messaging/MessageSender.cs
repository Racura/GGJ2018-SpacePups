using UnityEngine;
using System;
using System.Collections.Generic;

public class SignalSender
{
    List<Component> m_cacheList;
    Transform m_root;
    bool m_sendingMessage;

    public void SendMessage (object argument)
    {
        SendMessage (MessageScope.Local, true, argument);
    }

    private void SendMessage (MessageScope messageScope, bool includeInactive, object argument)
    {
        if (m_cacheList == null) m_cacheList = new List<Component>();

        lock (m_cacheList) {
            m_sendingMessage = true;
            m_cacheList.Clear ();

            bool includeSelf = messageScope != MessageScope.Children && messageScope != MessageScope.Parents;

            switch (messageScope) {
                case MessageScope.Children:
                case MessageScope.LocalAndChildren:
                    m_root.GetComponentsInChildren<Component> (includeInactive, m_cacheList);
                    break;
                case MessageScope.Parents:
                case MessageScope.LocalAndParents:
                    m_root.GetComponentsInParent<Component> (includeInactive, m_cacheList);
                    break;
                default:
                    m_root.GetComponents (typeof(IMessageListener), m_cacheList);
                    break;
            }

            var c = m_cacheList.Count;

            for (int i = 0; i < c; ++i) {
                
                var comp = m_cacheList[i];

                if (comp == null) continue;
                if (!includeSelf && comp.transform == m_root) continue;

                if (m_cacheList[i] is IMessageListener) {
                    var listener = m_cacheList[i] as IMessageListener;
                    listener.Listen (argument);
                }
            }

            m_sendingMessage = false;
        }
    }

    public enum MessageScope {
        Local,
        Children,
        LocalAndChildren,
        Parents,
        LocalAndParents,
    }
}   
