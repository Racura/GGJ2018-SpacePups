using UnityEngine;
using System.Collections;
using System;

public interface IDotListener
{
    void DotStateChanged (ResetDot.States state);
}
