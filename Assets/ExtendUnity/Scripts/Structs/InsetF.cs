using UnityEngine;
using System.Collections;

[System.Serializable]
public struct InsetF {

	public float top;
	public float bottom;
	public float left;
	public float right;
	
	public InsetF(float inset) {
		this.left = this.right = this.top = this.bottom = inset;
	}

	public InsetF(float horizontal, float vertical) {
		this.left	= this.right	= horizontal;
		this.top	= this.bottom	= vertical;
	}

	public InsetF(float top, float right, float bottom, float left) {
		this.top		= top;
		this.right		= right;
		this.bottom		= bottom;
		this.left		= left;
	}
	
	
	public Rect Inflate(Rect rect)
	{
		return new Rect(){
			x		= rect.x - left,
			y		= rect.y - bottom,
			width	= rect.width + (left + right),
			height	= rect.height + (top + bottom),
		};
	}
	
	public Rect Deflate(Rect rect)
	{
		return new Rect(){
			x		= rect.x + left,
			y		= rect.y + bottom,
			width	= rect.width - (left + right),
			height	= rect.height - (top + bottom),
		};
	}
	
	
	public Vector2 Inflate(Vector2 point)
	{
		return new Vector2(){
			x		= point.x + (left + right),
			y		= point.y + (top + bottom),
		};
    }

    public Vector2 InflatePosition(Vector2 point)
    {
        return new Vector2()
        {
            x = point.x - (left),
            y = point.y - (top),
        };
    }

    public Vector2 InflateCenter(Vector2 point)
    {
        return new Vector2()
        {
            x = point.x + (right - left) * 0.5f,
            y = point.y + (top - bottom) * 0.5f,
        };
    }
    public Vector2 DeflateCenter(Vector2 point)
    {
        return new Vector2()
        {
            x = point.x - (right - left) * 0.5f,
            y = point.y - (top - bottom) * 0.5f,
        };
    }
    public Vector2 GetInflateOffset()
    {
        return new Vector2((right - left) * 0.5f, (top - bottom) * 0.5f);
    }
    public Vector2 GetDeflateOffset()
    {
        return GetInflateOffset() * -1;
    }

    public Vector2 Deflate(Vector2 point)
	{
		return new Vector2(){
			x		= point.x - (left + right),
			y		= point.y - (top + bottom),
		};
	}
	
	public float VerticalInflate(float value) {
		return value + (top + bottom);
	}
	
	public float VerticalDeflate(float value) {
		return value - (top + bottom);
	}

	public float HorizontalInflate(float value) {
		return value + (left + right);
	}
	
	public float HorizontalDeflate(float value) {
		return value - (left + right);
	}


	
	public static implicit operator InsetF(int i)
	{
		return new InsetF(i);
	}
	public static implicit operator InsetF(float i)
	{
		return new InsetF(i);
	}
	public static implicit operator InsetF(Vector2 i)
	{
		return new InsetF(i.x, i.y);
	}
}