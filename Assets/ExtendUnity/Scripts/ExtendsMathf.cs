using UnityEngine;
using System.Collections;

public static partial class ExtendsMathf {

	public static bool InsideVectors (Vector2 dir, Vector2 a, Vector2 b)
	{
		int i = 0;
		
		if(dir.x * a.y - dir.y * a.x < 0) ++i;

		if(dir.y * b.x - dir.x * b.y < 0) ++i;
		
		if(i == 1){
			if(b.x * a.y - b.y * a.x > 0) ++i;
		}

		return i > 1;
	}

	public static Vector2 Vector2FromDegrees (float angle) {
		return Vector2FromRad (angle * Mathf.Deg2Rad);
	}
	public static Vector2 Vector2FromRad (float radians) {
		return new Vector2(
			Mathf.Sin(radians),
			Mathf.Cos(radians)
		);
	}

	public static Vector2 RotateBy(this Vector2 bas, Vector2 up){
		return new Vector2(
			bas.x * up.y + bas.y * up.x, 
			bas.y * up.y - bas.x * up.x
		);
	}
	
	public static float DeltaAngleSign (Vector2 vector1, Vector2 vector2) {
		return Vector2.Angle(vector1, vector2) * Mathf.Sign(vector2.y * vector1.x - vector2.x * vector1.y);
	}
	
	public static float DegreesFromVector2 (Vector2 vector) {
		return RadiansFromVector2(vector) * Mathf.Rad2Deg;
	}
	public static float RadiansFromVector2 (Vector2 vector) {
		return Mathf.Atan2(vector.x, vector.y);
	}

	


	public static float DampLerp (float delta) {
        if (delta <= 0) return 0f;
        if (delta >= 1) return 1f;

		return 1 - Mathf.Exp( -delta);
	}
	public static float DampLerp (float speed, float delta) {
		return DampLerp( speed * delta);
	}
	public static float DampLerp (float p1, float p2, float delta) {
		return Mathf.Lerp(p1, p2, DampLerp(delta));
	}
	public static float DampLerp (float p1, float p2, float speed, float delta) {
		return Mathf.Lerp(p1, p2, DampLerp(speed * delta));
	}


	public static float DistanceToStopped(Vector2 velocity, float acceleration){
		return (velocity.sqrMagnitude * 0.5f)  / acceleration;
	}

	public static float DistanceToStopped(float velocity, float acceleration){
		return (velocity * velocity * 0.5f) / acceleration;
	}

	public static float DistanceToVelocity(Vector2 currentVelocity, Vector2 wantedVelocity, float acceleration){

		var difVel = wantedVelocity - currentVelocity;
		return DistanceToStopped(difVel, acceleration);
	}

	public static float DistanceToVelocity(float currentVelocity, float wantedVelocity, float acceleration) {
		var difVel = wantedVelocity - currentVelocity;
		return DistanceToStopped(difVel, acceleration);
	}

	public static float TimeToVelocity(Vector2 currentVelocity, Vector2 wantedVelocity, float acceleration) {
		var difVel = wantedVelocity - currentVelocity;
		return (difVel.magnitude) / acceleration;
	}

	public static float TimeToVelocity(float currentVelocity, float wantedVelocity, float acceleration) {
		var difVel = wantedVelocity - currentVelocity;
		return (difVel) / acceleration;
	}


	public static Vector2 LinesCollideAt (Vector2 a1, Vector2 a2, Vector2 b1, Vector2 b2) {
	
		float denominator = ((a2.x - a1.x) * (b2.y - b1.y)) - ((a2.y - a1.y) * (b2.x - b1.x));
		float numerator1 = ((a1.y - b1.y) * (b2.x - b1.x)) - ((a1.x - b1.x) * (b2.y - b1.y));
		//float numerator2 = ((a1.y - b1.y) * (a2.x - a1.x)) - ((a1.x - b1.x) * (a2.y - a1.y));

		// Detect coincident lines (has a problem, read below)
		//if (denominator == 0) return numerator1 == 0 && numerator2 == 0;

		float r = numerator1 / denominator;
		//float s = numerator2 / denominator;

		return new Vector2 (a1.x + (a2.x - a1.x) * r, a1.y + (a2.y - a1.y) * r);
	}
}