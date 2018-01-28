using UnityEngine;
using System.Collections;

public static partial class ExtendsMathf {

	public static Vector2 GetHermitePoint (Vector2 p1, Vector2 t1, Vector2 p2, Vector2 t2, float t) {

		float tt = t * t;
		float ttt = tt * t;

		float d1 = 2 * ttt + 1 - 3 * tt;         // calculate basis function 1
		float d2 = 3 * tt -2 * ttt;              // calculate basis function 2
		float d3 = ttt - 2 * tt + t;     	     // calculate basis function 3
		float d4 = ttt -  tt;         		     // calculate basis function 4

		return d1 * p1 + d2 * p2 + d3 * t1 + d4 * t2;
	}

	public static Vector3 GetHermitePoint (Vector3 p1, Vector3 t1, Vector3 p2, Vector3 t2, float t) {

		float tt = t * t;
		float ttt = tt * t;

		float d1 = 2 * ttt + 1 - 3 * tt;         // calculate basis function 1
		float d2 = 3 * tt -2 * ttt;              // calculate basis function 2
		float d3 = ttt - 2 * tt + t;     	     // calculate basis function 3
		float d4 = ttt -  tt;         		     // calculate basis function 4

		return d1 * p1 + d2 * p2 + d3 * t1 + d4 * t2;
	}

	public static Vector3 GetHermiteTangent (Vector3 p1, Vector3 t1, Vector3 p2, Vector3 t2, float t) {

		float tt = t * t;

		float d1 = 6 * tt - 6 * t;         	// calculate basis function 1
		float d2 = 6 * t -6 * tt;           // calculate basis function 2
		float d3 = 3 * tt - 4 * t + 1;     	// calculate basis function 3
		float d4 = 3 * tt - 2 * t;      	// calculate basis function 4

		return d1 * p1 + d2 * p2 + d3 * t1 + d4 * t2;
	}

	public static Vector2 GetHermiteTangent (Vector2 p1, Vector2 t1, Vector2 p2, Vector2 t2, float t) {

		float tt = t * t;

		float d1 = 6 * tt - 6 * t;         	// calculate basis function 1
		float d2 = 6 * t -6 * tt;           // calculate basis function 2
		float d3 = 3 * tt - 4 * t + 1;     	// calculate basis function 3
		float d4 = 3 * tt - 2 * t;      	// calculate basis function 4

		return d1 * p1 + d2 * p2 + d3 * t1 + d4 * t2;
	}

	public static Vector2 GetHermiteTangentTangent (Vector2 p1, Vector2 t1, Vector2 p2, Vector2 t2, float t) {

		float d1 = 12 * t - 6;         		// calculate basis function 1
		float d2 = 6 - 12 * t;           	// calculate basis function 2
		float d3 = 6 * t - 4;     			// calculate basis function 3
		float d4 = 6 * t - 2;      			// calculate basis function 4

		return d1 * p1 + d2 * p2 + d3 * t1 + d4 * t2;
	}

	public static Vector3 GetHermiteTangentTangent (Vector3 p1, Vector3 t1, Vector3 p2, Vector3 t2, float t) {

		float d1 = 12 * t - 6;         		// calculate basis function 1
		float d2 = 6 - 12 * t;           	// calculate basis function 2
		float d3 = 6 * t - 4;     			// calculate basis function 3
		float d4 = 6 * t - 2;      			// calculate basis function 4

		return d1 * p1 + d2 * p2 + d3 * t1 + d4 * t2;
	}
}