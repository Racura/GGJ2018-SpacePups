using UnityEngine;

public class PerlinNoise {

	public static readonly float Sqrt2			= Mathf.Sqrt (2f);
	public static readonly float InverseSqrt2	= 1f / Sqrt2;


	const int HashSize				= 1 << 8;

	HashList hashList;

	public bool useBounds = false;
	public Rect bounds = new Rect (float.MinValue, float.MinValue, float.MaxValue, float.MaxValue);

	public PerlinNoise () {
		SetSeed (string.Empty);
	}

	public PerlinNoise (string seed) {
		SetSeed (seed);
	}

	public void SetSeed (string seed) {
		if (hashList == null) {
			hashList = new HashList (HashSize);
		}

		hashList.SetSeed (seed);
	}

	public float OctavePerlin (Vector2 point, 
		int octaves, float frequency, float persistence, Vector2 octaveOffset
	) {
		var sum = Perlin(point);

		if (octaves < 1) return sum;

		float amplitude = 1f;
		float range = 1f;

		for (int o = 1; o < octaves; o++) {
			point 		*= frequency;
			amplitude	*= persistence;
			range		+= amplitude;
			sum			+= Perlin (point + octaveOffset * o) * amplitude;
		}
		return sum / range;
	}


	public float Perlin (Vector2 point) {
	
		int ix = Mathf.FloorToInt (point.x);
		int iy = Mathf.FloorToInt (point.y);
	
		float tx0 = point.x - ix;
		float ty0 = point.y - iy;

		float tx1 = tx0 - 1f;
		float ty1 = ty0 - 1f;


		var v00 = DotGradient2D (ix + 0, iy + 0, tx0, ty0);
		var v10 = DotGradient2D (ix + 1, iy + 0, tx1, ty0);
		var v01 = DotGradient2D (ix + 0, iy + 1, tx0, ty1);
		var v11 = DotGradient2D (ix + 1, iy + 1, tx1, ty1);

		var tx = Smooth (tx0);
		var ty = Smooth (ty0);

		return Lerp (
			Lerp (v00, v10, tx),
			Lerp (v01, v11, tx),
			ty
		) * Sqrt2;
	}

	public float DotGradient2D (int x, int y, float dx, float dy) {
	
		if (useBounds) {
			if (bounds.xMin > x)	return -1;
			if (bounds.yMin > y)	return -1;

			if (bounds.xMax < x)	return -1;
			if (bounds.yMax < y)	return -1;
		}

		switch (hashList.GetHash(x, y) & 7) {
			case 0: return dx;
			case 1: return -dx;
			case 2: return dy;
			case 3: return -dy;
			case 4: return ( dx - dy) * InverseSqrt2;
			case 5: return ( dx + dy) * InverseSqrt2;
			case 6: return (-dx - dy) * InverseSqrt2;
			case 7: return (-dx + dy) * InverseSqrt2;
		}

		return 0f;
	}

	private static int GetRandomValue (int x, int y) {
		return x
			^ (y << 2)
			^ ((x ^ 7) << 3)
			^ ((y ^ 13) << 4);
	}

	private static float Lerp (float x, float y, float lerp) {
		return x + (y - x) * lerp;
	}

	private static float Smooth (float t) {
		return t * t * t * (t * (t * 6 - 15) + 10);
	}

	private static float Dot (Vector2 p, float x, float y) {
		return p.x * x + p.y * y;
	}
}
