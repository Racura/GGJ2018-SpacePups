using System;
using UnityEngine;

public class RepeatableRandom {

	public float Value { get { return (float)NextDouble(); } }

	System.Random randomSequence;

	int seed, steps, hashSeed;


	public RepeatableRandom (int seed) { Reset(seed); }
	public RepeatableRandom (string seed) { Reset(seed); }

	public void Reset(string seed) {

		Reset(GetHashSeed(seed.Length, ToInt(seed)));
	}

	public void Reset(int seed) {
		this.seed = seed;
		SetHash();
	}

	public void SetHash(string hash) {

		SetHash(ToInt(hash));
	}

	public void SetHash(params int[] hash) {

		this.hashSeed = GetHashSeed(seed, hash);

		SetSteps(0);
	}

	public void SetSteps(int steps){

		if(this.steps > steps || steps == 0) {
			randomSequence = new System.Random(hashSeed);
			this.steps = 0;
		}

		while(this.steps < steps) {
			Next();
		}
	}

	public int Next() {
		++steps;
		return randomSequence.Next();
	}

	public int Next(int max) {
		++steps;
		return randomSequence.Next(max);
	}

	public int Next(int min, int max) {
		++steps;
		return randomSequence.Next(min, max);
	}

	public double NextDouble() {
		++steps;
		return randomSequence.NextDouble();
	}

	public double NextDouble(float min, float max) {
		return min + NextDouble() * (max - min);
	}

	public Vector2 NextVector2(float minX, float maxX, float minY, float maxY) {

		return new Vector2((float)NextDouble(minX, maxX), (float)NextDouble(minY, maxY));
	}






	private static int[] ToInt(string str) {

		int[] output = new int[str.Length];

		for(int i = output.Length - 1; i >= 0; --i) {
			output[i] = char.ConvertToUtf32(str, i);
		}

		return output;
	}



	private static int GetHashSeed(int seed, params int[] hash) {
		
		int hashSeed = CalcSubHash(seed);

		for(int i = hash.Length - 1; i >= 0; --i) {
			hashSeed = CalcSubHash(hash[i]) ^ RotateLeft(hashSeed, 3);
		}

		return hashSeed;
	}

	private static int CalcSubHash(int value) {

		var v = value.GetHashCode();

		for(int i = 0; i < 8; ++i) {
			v = v ^ RotateLeft(v + 17442, 13); 		
		}

		return v;
	}

	private static int RotateLeft(int value, int count)
	{
		while(count > 32)
			count = (count % 32);

		return (value << count) | (value >> (32 - count));
	}
}

