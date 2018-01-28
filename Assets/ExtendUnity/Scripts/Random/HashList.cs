using UnityEngine;

public class HashList {

	int hashSize;

	string seed;
	int[] hash;

    int[] tempGetHash;

    public int HashSize     { get { return hashSize; } }
    public string Seed      { get { return seed; } }

	public HashList (int hashSize) {
        if (hashSize < 4) throw new UnityException ("Hash size is less than four");
        if (!Mathf.IsPowerOfTwo(hashSize)) throw new UnityException ("Hash size is not a power of two");

        this.hashSize = hashSize;

		SetSeed (string.Empty);
	}

	public void SetSeed (string seed) {
		this.seed = seed;

        var hashMask = hashSize - 1;
		var seedLength = Mathf.Max(seed.Length, 1);
		var shortSeed = new short[seedLength];

		for (int i = 0; i < seed.Length; ++i) shortSeed[i] = (short)seed[i];

		if (hash == null || hash.Length != hashSize)
			hash = new int [hashSize];

		for (int i = 0; i < hashSize; ++i) hash[i] = -1;

		for (int i = 0; i < hashSize; ++i) {
			var c = GetRandomValue (
				i, shortSeed[(i >= seedLength ? i ^ 29 : i) % seedLength]
			);
			var tmp = shortSeed[((c ^ i) % seedLength) + ((c ^ i) < 0 ? seedLength : 0)] + i;

			c = GetRandomValue (tmp, i) & hashMask;

			for(int u = 0; u < hashSize; ++u) {
				if (hash[(c + u) & hashMask] < 0) {
					hash[(c + u) & hashMask] = i;
					break;
				}
			}
		}
	}

    public static int GetRandomValue (int x, int y) {
        unchecked {
            return (x ^ 13)
                ^ ((y + 7) << 5 | (y + 7) >> 27)
                ^ ((x * -5 + y * 3) << 8 | (x * -5 + y * 3) >> 24);
        }
    }


    public int GetHash (int i1) {
        int hashMask = hashSize - 1;
        return hash[
            (i1) & hashMask
        ];
    }
    public int GetHash (int i1, int i2) {
        int hashMask = hashSize - 1;
        return hash[(hash[
            (i1) & hashMask
            ] + i2) & hashMask
        ];
    }
    public int GetHash (int i1, int i2, int i3) {
        int hashMask = hashSize - 1;
        return hash[(hash[(hash[
            (i1) & hashMask
            ] + i2) & hashMask
            ] + i3) & hashMask
        ];
    }
    public int GetHash (int i1, int i2, int i3, int i4) {
        int hashMask = hashSize - 1;
        return hash[(hash[(hash[(hash[
            (i1) & hashMask
            ] + i2) & hashMask
            ] + i3) & hashMask
            ] + i4) & hashMask
        ];
    }
    public int GetHash (int i1, int i2, int i3, int i4, int i5) {
        int hashMask = hashSize - 1;
        return hash[(hash[(hash[(hash[(hash[
            (i1) & hashMask
            ] + i2) & hashMask
            ] + i3) & hashMask
            ] + i4) & hashMask
            ] + i5) & hashMask
        ];
    }

    public int GetHash (int[] values, int count = -1) {

        if (values != null || values.Length == 0) throw new UnityException();
        if (count < 0) count = values.Length;

        int hashMask = hashSize - 1;
        int output = 0;

        for (int i = 0; i < count; ++i) {
            output = hash[(values[i] + output) & hashMask];
        }

        return output;
    }
}
