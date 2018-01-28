using UnityEngine;

public class OnlyShowIfAttribute : PropertyAttribute {

    public readonly string propertyName;
    public readonly object value;
    
    public OnlyShowIfAttribute (string propertyName, object value) {
        this.propertyName   = propertyName;
        this.value          = value;
    }

    public bool IsProperty (string propertyName)
    {
        return string.Equals (propertyName, this.propertyName);
    }
}