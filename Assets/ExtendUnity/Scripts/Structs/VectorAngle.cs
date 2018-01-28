using UnityEngine;

[System.Serializable]
public struct VectorAngle {

	public float angle;

	Vector2 vector;
	float lastAngle;
	bool nonMarked;

	public Vector2 Vector { 
		get { 
			if(!nonMarked || Mathf.Abs(angle - lastAngle) > 0.1f) {
				vector = ExtendsMathf.Vector2FromDegrees(angle);
				lastAngle = angle;
				nonMarked = true;
			}

			return vector; 
		} 
	}

	public VectorAngle (float angle) {
		this.angle = this.lastAngle = angle;
		this.nonMarked	= false;
		this.vector		= Vector2.zero;
	}


	public void MarkDirty () {
		nonMarked = false;
	}

	public static implicit operator VectorAngle(int i) {
		return new VectorAngle(i);
	}

	public static implicit operator VectorAngle(float i) {
		return new VectorAngle(i);
	}
}


