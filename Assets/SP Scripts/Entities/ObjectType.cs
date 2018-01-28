
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObjectType
{
    None            = 0,

    Doggo           = 1 << 0,
    Ball            = 1 << 1,
    Debres          = 1 << 2,

    All             = ~0,

}
