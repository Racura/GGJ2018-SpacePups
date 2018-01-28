using UnityEngine;

public class RangeRangeAttribute : PropertyAttribute {

    public readonly float min, max;
    
    public RangeRangeAttribute (float min, float max) {
        this.min = min;
        this.max = max;
    }
}