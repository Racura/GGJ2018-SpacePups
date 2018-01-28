using UnityEngine;
using System.Collections;

[System.Serializable]
public struct RectInt {

	public int x;
	public int y;
	public int width;
	public int height;
	
	public RectInt (int x, int y, int width, int height) {
		this.x = x;
		this.y = y;
		this.width = width;
		this.height = height;
	}

	public override int GetHashCode ()
	{
        int hash = 23;

        unchecked {
            hash = hash * 99 ^ x.GetHashCode ();
            hash = hash * 99 ^ y.GetHashCode ();
            hash = hash * 99 ^ width.GetHashCode ();
            hash = hash * 99 ^ height.GetHashCode ();
        }

        return hash;
	}

	public override string ToString () {
		return string.Format ("({0}, {1}, {2}, {3})", x, y, width, height);
	}

	public override bool Equals (object obj) {
		return (obj is RectInt) && (RectInt)obj == this;
	}
	
	public static bool operator ==(RectInt a, RectInt b)
	{
		return a.x == b.x && a.y == b.y && a.width == b.width && a.height == b.height;
	}

	public static bool operator !=(RectInt a, RectInt b)
	{
		return a.x != b.x || a.y != b.y || a.width != b.width || a.height != b.height;
	}
}