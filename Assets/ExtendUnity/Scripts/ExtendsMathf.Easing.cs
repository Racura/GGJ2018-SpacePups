using UnityEngine;
using System.Collections;

public static partial class ExtendsMathf {

	public enum EasingType {
		None			= 0, 

		InOnly			= 0x00000001,
		OutOnly			= 0x00000002,

		QuadInOut		= 0x00000010,
		QuadIn			= QuadInOut | InOnly,
		QuadOut			= QuadInOut | OutOnly,

		CubicInOut		= 0x00000020,
		CubicIn			= CubicInOut | InOnly,
		CubicOut		= CubicInOut | OutOnly,

		QuartInOut		= 0x00000040,
		QuartIn			= QuartInOut | InOnly,
		QuartOut		= QuartInOut | OutOnly,

		QuintInOut		= 0x00000080,
		QuintIn			= QuintInOut | InOnly,
		QuintOut		= QuintInOut | OutOnly,

		SineInOut		= 0x00000100,
		SineIn			= SineInOut | InOnly,
		SineOut			= SineInOut | OutOnly,

		ExpoInOut		= 0x00000200,
		ExpoIn			= ExpoInOut | InOnly,
		ExpoOut			= ExpoInOut | OutOnly,

		CircInOut		= 0x00000400,
		CircIn			= CircInOut | InOnly,
		CircOut			= CircInOut | OutOnly,
	}

	public static float Ease (EasingType type, float a, float b, float t) {

		switch(type) {
		case EasingType.InOnly:			return a;
		case EasingType.OutOnly:		return b;

		case EasingType.QuadIn:			return Easing.InQuad	(a, b, t);
		case EasingType.QuadOut:		return Easing.OutQuad	(a, b, t);
		case EasingType.QuadInOut:		return Easing.InOutQuad(a, b, t);

		case EasingType.CubicIn:		return Easing.InCubic	(a, b, t);
		case EasingType.CubicOut:		return Easing.OutCubic(a, b, t);
		case EasingType.CubicInOut:		return Easing.InOutCubic(a, b, t);

		case EasingType.QuartIn:		return Easing.InQuart	(a, b, t); 
		case EasingType.QuartOut:		return Easing.OutQuart(a, b, t);
		case EasingType.QuartInOut:		return Easing.InOutQuart(a, b, t);

		case EasingType.QuintIn:		return Easing.InQuint	(a, b, t); 
		case EasingType.QuintOut:		return Easing.OutQuint(a, b, t);
		case EasingType.QuintInOut:		return Easing.InOutQuint(a, b, t);

		case EasingType.SineIn:			return Easing.InSine	(a, b, t); 
		case EasingType.SineOut:		return Easing.OutSine	(a, b, t); 
		case EasingType.SineInOut:		return Easing.InOutSine(a, b, t); 

		case EasingType.ExpoIn:			return Easing.InExpo	(a, b, t); 
		case EasingType.ExpoOut:		return Easing.OutExpo	(a, b, t); 
		case EasingType.ExpoInOut:		return Easing.InOutExpo(a, b, t); 

		case EasingType.CircIn:			return Easing.InCirc	(a, b, t); 
		case EasingType.CircOut:		return Easing.OutCirc	(a, b, t); 
		case EasingType.CircInOut:		return Easing.InOutCirc(a, b, t); 

		}

		return Mathf.Lerp(a, b, t);
	}


	public static class Easing {

		public static float InQuad (float a, float b, float t) {
			return (b - a) * t * t + a;
		}

		public static float OutQuad (float b, float c, float t) {
			return -(c - b) * t*(t-2) + b;
		}

		// quadratic easing in/out - acceleration until halfway, then deceleration
		public static float InOutQuad (float b, float c, float t) {

			t *= 2;
			if (t < 1) return InQuad(b, (b+c) * 0.5f, t);
			return OutQuad((b+c) * 0.5f, c, t - 1);
		}

		// cubic easing in - accelerating from zero velocity
		public static float InCubic (float b, float c, float t) {
			return (c - b)*t*t*t + b;
		}

		// cubic easing out - decelerating to zero velocity
		public static float OutCubic (float b, float c, float t) {
			t--;
			return (c - b)*(t*t*t + 1) + b;
		}

		// cubic easing in/out - acceleration until halfway, then deceleration
		public static float InOutCubic (float b, float c, float t) {

			t *= 2;
			if (t < 1) return InCubic(b, (b+c) * 0.5f, t);
			return OutCubic((b+c) * 0.5f, c, t - 1);
		}


		// quartic easing in - accelerating from zero velocity
		public static float InQuart (float b, float c, float t) {
			return (c - b)*t*t*t*t + b;
		}



		// quartic easing out - decelerating to zero velocity
		public static float OutQuart (float b, float c, float t) {
			t--;
			return -(c - b) * (t*t*t*t - 1) + b;
		}



		// quartic easing in/out - acceleration until halfway, then deceleration
		public static float InOutQuart (float b, float c, float t) {

			t *= 2;
			if (t < 1) return InQuart(b, (b+c) * 0.5f, t);
			return OutQuart((b+c) * 0.5f, c, t - 1);
		}


		// quintic easing in - accelerating from zero velocity
		public static float InQuint (float b, float c, float t) {
			return (c - b)*t*t*t*t*t + b;
		}



		// quintic easing out - decelerating to zero velocity
		public static float OutQuint (float b, float c, float t) {
			t--;
			return (c - b)*(t*t*t*t*t + 1) + b;
		}



		// quintic easing in/out - acceleration until halfway, then deceleration
		public static float InOutQuint (float b, float c, float t) {

			t *= 2;
			if (t < 1) return InQuint(b, (b+c) * 0.5f, t);
			return OutQuint((b+c) * 0.5f, c, t - 1);
		}


		// sinusoidal easing in - accelerating from zero velocity
		public static float InSine (float b, float c, float t) {
			return -(c - b) * Mathf.Cos(t * (Mathf.PI/2)) + c;
		}



		// sinusoidal easing out - decelerating to zero velocity
		public static float OutSine (float b, float c, float t) {
			return (c - b) * Mathf.Sin(t * (Mathf.PI/2)) + b;
		}



		// sinusoidal easing in/out - accelerating until halfway, then decelerating
		public static float InOutSine (float b, float c, float t) {

			t *= 2;
			if (t < 1) return InSine(b, (b+c) * 0.5f, t);
			return OutSine((b+c) * 0.5f, c, t - 1);
		}



		// exponential easing in - accelerating from zero velocity
		public static float InExpo (float b, float c, float t) {
			return (c - b) * Mathf.Pow( 2, 10 * (t - 1) ) + b;
		}



		// exponential easing out - decelerating to zero velocity
		public static float OutExpo (float b, float c, float t) {
			return (c - b) * ( -Mathf.Pow( 2, -10 * t ) + 1 ) + b;
		}



		// exponential easing in/out - accelerating until halfway, then decelerating
		public static float InOutExpo (float b, float c, float t) {

			t *= 2;
			if (t < 1) return InExpo(b, (b+c) * 0.5f, t);
			return OutExpo((b+c) * 0.5f, c, t - 1);
		}


		// circular easing in - accelerating from zero velocity
		public static float InCirc (float b, float c, float t) {
			return -(c - b) * (Mathf.Sqrt(1 - t*t) - 1) + b;
		}



		// circular easing out - decelerating to zero velocity
		public static float OutCirc (float b, float c, float t) {
			t--;
			return (c - b) * Mathf.Sqrt(1 - t*t) + b;
		}



		// circular easing in/out - acceleration until halfway, then deceleration

		public static float InOutCirc (float b, float c, float t) {

			t *= 2;
			if (t < 1) return InCirc(b, (b+c) * 0.5f, t);
			return OutCirc((b+c) * 0.5f, c, t - 1);
		}
	}
}