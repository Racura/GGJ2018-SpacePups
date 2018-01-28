using UnityEngine;
using System.Collections;

public static partial class ExtendsUtil {

	static bool[] helperBool;


	public static T[] Subtraction<T> (T[] a, T[] b) {

		if(helperBool == null || helperBool.Length < a.Length) {
			helperBool = new bool[Mathf.NextPowerOfTwo(a.Length)];
		}

		int count = 0;

		for (int i = 0; i < a.Length; ++i) {
			bool contains = false;
			for (int j = 0; j < b.Length; ++j) {
				if (a[i].Equals(b[j])) {
					contains = true;
					break;
				}
			}

			if(!contains) {
				++count;
			}

			helperBool[i] = !contains;
		}

		var output = new T[count];
		count = 0;

		for (int i = 0; i < a.Length; ++i) {
			if (helperBool[i]) {
				output[count] = a[i];
				++count;
			}
		}

		return output;
	}

	public static T[] Intersect<T> (T[] a, T[] b) {

		if(helperBool == null || helperBool.Length < a.Length) {
			helperBool = new bool[Mathf.NextPowerOfTwo(a.Length)];
		}

		int count = 0;

		for (int i = 0; i < a.Length; ++i) {
			bool contains = false;
			for (int j = 0; j < b.Length; ++j) {
				if (a[i].Equals(b[j])) {
					contains = true;
					++count;
					break;
				}
			}

			helperBool[i] = contains;
		}

		var output = new T[count];
		count = 0;

		for (int i = 0; i < a.Length; ++i) {
			if (helperBool[i]) {
				output[count] = a[i];
				++count;
			}
		}

		return output;
	}


}