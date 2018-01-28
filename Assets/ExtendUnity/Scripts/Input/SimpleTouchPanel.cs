using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class SimpleTouchPanel : MonoBehaviour,
    IPointerDownHandler, IPointerUpHandler, IDragHandler {

    Image image;
    bool createdImage;

    List<SimpleTouchInput.Touch> inputs;

    protected void Awake () {
        
        createdImage    = false;
        image           = GetComponent<Image>();

        if (image == null) {
            image               = gameObject.AddComponent<Image>();
            image.hideFlags     = HideFlags.DontSave;
            image.color         = Color.clear;
            createdImage        = true;
        }
    }

    protected void OnEnable () {
        if (createdImage && image != null) {
            image.enabled = true;
        }
    }

    protected void OnDisable () {
        if (createdImage && image != null) {
            image.enabled = false;
        }

        if (inputs != null && inputs.Count > 0) {
            for (int i = 0; i < inputs.Count; ++i) {
                if (inputs[i].IsDown) {
                    inputs[i].UpdateState (SimpleTouchInput.State.Cancelled);
                }
            }

            WriteInput ();
        }
    }

    protected void Update () {
        WriteInput ();
    }

    protected void WriteInput () {
        if (inputs == null || inputs.Count < 1) {
            SimpleTouchInput.SetTouches (new SimpleTouchInput.Touch[0]);
            return;
        }
        var output = new SimpleTouchInput.Touch[inputs.Count];

        for (int i = 0; i < inputs.Count; ++i)
            output[i] = inputs[i];

        SimpleTouchInput.SetTouches (output);


        for (int i = 0; i < inputs.Count; ++i) {
            var input = inputs[i];

            if (!input.IsDown) {
                inputs.RemoveAt(i);
                --i;
                continue;
            }

            if (input.state == SimpleTouchInput.State.Pressed) {
                input.UpdateState (SimpleTouchInput.State.Down);
            }

            input.deltaPosition = Vector2.zero;
            inputs[i] = input;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (inputs == null) inputs = new List <SimpleTouchInput.Touch>();

        inputs.Add (
            SimpleTouchInput.CreateTouch (
                eventData.pointerId, eventData.position
            )
        );
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (inputs == null) return;

        for (int i = 0; i < inputs.Count; ++i) {
            var input = inputs[i];
            if (inputs[i].IsDown && inputs[i].id == eventData.pointerId) {
            
                input.state             = SimpleTouchInput.State.Down;

                input.deltaPosition     = eventData.position - input.currentPosition;
                input.currentPosition   = eventData.position;

                inputs[i]       = input;
                return;
            }
        }
    }

    public void OnPointerUp (PointerEventData eventData)
    {
        if (inputs == null) return;

        for (int i = 0; i < inputs.Count; ++i) {
            var input = inputs[i];

            if (input.IsDown && input.id == eventData.pointerId) {
            
                input.state     = SimpleTouchInput.State.Released;
                inputs[i]       = input;
                return;
            }
        }
    }
}
