using UnityEngine;
using System.Collections;

[System.Serializable]
public struct RangeF {

	public float min;
	public float max;

	public RangeF(float min, float max) {
		this.min = min;
		this.max = max;
	}
	
	public float Clamp(float value){
		
		return Mathf.Clamp(value, min, max);
	}
	
	public float Lerp(float value){
		
		return Mathf.Lerp(min, max, value);
	}

	public float InverseLerp (float value)
	{
		return (value - min) / (max - min);
	}
	
	public float Random(){
		
		return UnityEngine.Random.Range(min, max);
	}

	public int RandomInt ()
	{
		return (int)Random();
	}

	public bool Inside (float value)
	{
		return value >= min && value <= max;
	}

	public float Difference (float value)
	{
		return Clamp(value) - value;
	}


	public static bool Overlaps (RangeF value1, RangeF value2)
	{
        return value1.min <= value2.max && value1.max >= value2.min;
	}
	public static bool Contains (RangeF value1, RangeF value2)
	{
        return value1.min <= value2.min && value2.max <= value1.max;
	}

	
	public static implicit operator RangeF(int i)
	{
		return new RangeF(i, i);
	}

	public static implicit operator RangeF(float i)
	{
		return new RangeF(i, i);
	}
	
	public static implicit operator RangeF(RangeI i)
	{
		return new RangeF(i.min, i.max);
	}

	public static explicit operator RangeI(RangeF i)
	{
		return new RangeI((int)i.min, (int)i.max);
	}

	public static RangeF operator *(RangeF p1, int other) {
		return new RangeF(p1.min * other, p1.max * other);
	}

	public static RangeF operator *(RangeF p1, float other) {
		return new RangeF(p1.min * other, p1.max * other);
	}
	
	public static readonly RangeF Range01 = new RangeF (0, 1);
}