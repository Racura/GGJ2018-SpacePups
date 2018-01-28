using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static partial class ExtendsUtil {
	
	public static string ReadFile(string path) {
        return File.ReadAllText (path);
	}
	
	public static void WriteValue(string value, string path) {	
        File.WriteAllText (path, value);
	}


	public static T BinaryDeserialize<T>(string path) {
		Environment.SetEnvironmentVariable ("MONO_REFLECTION_SERIALIZER", "yes");
		
		using (FileStream fs = new FileStream (path, FileMode.Open)) {
			
			BinaryFormatter formatter = new BinaryFormatter ();
			return (T)formatter.Deserialize (fs);
		}
	}
	
	public static void BinarySerialize<T>(T value, string path) {	
		Environment.SetEnvironmentVariable ("MONO_REFLECTION_SERIALIZER", "yes");

		using (FileStream fs = new FileStream (path, FileMode.Create)) {			
			BinaryFormatter formatter = new BinaryFormatter ();
			formatter.Serialize (fs, value);
		}
	}
	public static byte[] BinarySerialize<T>(T value) {	
		Environment.SetEnvironmentVariable ("MONO_REFLECTION_SERIALIZER", "yes");

		using (MemoryStream ms = new MemoryStream()) {			
			BinaryFormatter formatter = new BinaryFormatter ();
			formatter.Serialize (ms, value);

            return ms.ToArray ();
		}
	}
	public static T BinaryDeserialize<T>(byte[] data) {	
		Environment.SetEnvironmentVariable ("MONO_REFLECTION_SERIALIZER", "yes");

		using (MemoryStream ms = new MemoryStream(data)) {			
			BinaryFormatter formatter = new BinaryFormatter ();
			return (T)formatter.Deserialize (ms);
		}
	}
	public static void BinarySerializeCatch<T>(T value, string path){
		try {
			BinarySerialize<T> (value, path);
		} catch {
		}
	}
	
	public static void BinarySerializeAsync<T>(T value, string path){
		System.Threading.ThreadPool.QueueUserWorkItem(
			delegate {  BinarySerializeCatch(value, path); }, null
		);
	}
}