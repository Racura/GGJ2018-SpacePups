using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorAttribute : PropertyAttribute {

	public readonly object[] values;
	public readonly string[] keys;
    
    public SelectorAttribute (string[] values) {
		this.values = values;
		this.keys = values;
    }
    public SelectorAttribute (int[] values) {

		this.values = new object[values.Length];
		this.keys = new string[values.Length];

		for (int i = 0; i < values.Length; ++i) {
			this.values[i] = values[i];
			this.keys[i] = values[i].ToString();
		}
    }
    public SelectorAttribute (float[] values) {

		this.values = new object[values.Length];
		this.keys = new string[values.Length];

		for (int i = 0; i < values.Length; ++i) {
			this.values[i] = values[i];
			this.keys[i] = values[i].ToString();
		}
    }
	
	public SelectorAttribute (string[] values, string[] keys) {
		this.values = values;
		this.keys = keys;
    }
}
