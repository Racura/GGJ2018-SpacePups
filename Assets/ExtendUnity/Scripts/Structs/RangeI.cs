using UnityEngine;
using System.Collections;

[System.Serializable]
public struct RangeI {

	public int min;
	public int max;

	public RangeI(int min, int max) {
		this.min = min;
		this.max = max;
	}
	
	public float Clamp(float value){
		
		return Mathf.Clamp(value, min, max);
	}

	public float Lerp(float value){

		return Mathf.Lerp(min, max, value);
	}

	public float LerpInt(float value){

		return Mathf.FloorToInt(Mathf.Lerp(min, max + 1, value));
	}
	
	public int Random(){
		
		return UnityEngine.Random.Range(min, max + 1);
	}

	public bool Inside (int value)
	{
		return value >= min && value <= max;
	}

	public float Difference (float value)
	{
		return Clamp(value) - value;
	}

	
	public static implicit operator RangeI(int i)
	{
		return new RangeI(i, i);
	}

	public static RangeI operator *(RangeI p1, int other) {
		return new RangeI(p1.min * other, p1.max * other);
	}

	public static RangeF operator *(RangeI p1, float other) {
		return new RangeF(p1.min * other, p1.max * other);
	}
	
	
	public static readonly RangeI Range01 = new RangeI (0, 1);
}