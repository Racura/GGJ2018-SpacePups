using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragCommand : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

    [SerializeField] CommandableObject commandable;
    [SerializeField] float dragLength           = 1.5f;

    bool m_isDragging;
    Vector2 m_downPos;

    public bool TrySetCommand(CommandAction commandAction)
    {
        return commandable != null && commandable.TrySetCommand (commandAction);
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        m_isDragging    = commandable != null && !commandable.HasCommand;
        m_downPos       = GetWorldPosition (eventData);
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        if (m_isDragging) {
            var pos = GetWorldPosition (eventData);
            var dif = pos - m_downPos;

            if (dif.sqrMagnitude > dragLength * dragLength) {
                
                if (Mathf.Abs (dif.x) > Mathf.Abs (dif.y)) {
                    TrySetCommand (dif.x > 0 ? CommandAction.Right : CommandAction.Left);
                } else {
                    TrySetCommand (dif.y > 0 ? CommandAction.Up : CommandAction.Down);                
                }

                m_isDragging = false;
            }
        }
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        m_isDragging = false;
    }


    public static Vector2 GetWorldPosition (PointerEventData eventData)
    {
        var position = Vector2.zero;

        var cam = eventData.pressEventCamera;

        if (cam != null) {
            position = cam.ScreenToWorldPoint (eventData.position);
        }

        return position;
    }
}
