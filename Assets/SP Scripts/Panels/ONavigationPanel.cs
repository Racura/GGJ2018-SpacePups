using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class ONavigationPanel : OParentPanel
    {
        public virtual OPanel SetRoot (OPanel panel, bool animated) 
        {
            ClearPanels (animated);
            return AddPanel (panel, animated);
        }

        public virtual OPanel PushPanel (OPanel panel, bool animated) 
        {
            return AddPanel (panel, animated);
        }

        public virtual void PopPanel (bool animated) {
            RemovePanel (ActivePanel, animated);
        }
    }
