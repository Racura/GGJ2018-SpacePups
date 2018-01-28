using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public interface IPanelManager 
    {

        void SetRoot (OPanel panel, bool animated);

        void PushPanel (OPanel panel, bool animated);
        void PopPanel (bool animated);

        void Snap ();


    }
