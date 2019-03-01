using UnityEngine;
using System.Collections.Generic;
#if antiCheat
//anticheat tool kit allow to protect your variable from being changed, as all data is cached here its only require one take one sec to enable it
using CodeStage.AntiCheat.ObscuredTypes;
#endif

namespace GSFramework
{
    public class DataManager : MonoBehaviour 
{
	
	#region Variables
	public bool ClearPlayerPrefOnAwake;

	//User Data [Player saved]
	// TYPE INT
	private Dictionary<string, int> values_INT = new Dictionary<string, int>();
	public Dictionary<string, int> DictionaryInt
	{
		get { return values_INT; }
	}
	//TYPE STRING
	private Dictionary<string, string> values_STRING = new Dictionary<string, string>();
	public Dictionary<string, string> DictionaryString
	{
		get { return values_STRING; }
	}
	//TYPE BOOL
	private Dictionary<string, bool> values_BOOL = new Dictionary<string, bool>();
	public Dictionary<string, bool> DictionaryBool
	{
		get { return values_BOOL; }
	}
	//TYPE Object
	private Dictionary<string, object> values_Object = new Dictionary<string, object>();
	public Dictionary<string, object> DictionaryObject
	{
		get { return values_Object; }
	}

	#endregion
	#region SINGLETON

	public static DataManager Instance { get; private set; }

	private void Awake() 
	{
		if (ClearPlayerPrefOnAwake)
			PlayerPrefs.DeleteAll ();
		
		Instance = this;
		LocalCache.LoadCachedKeys ();
	}

	#endregion


	#region Player Cached DATA
	#region TYPE INT
	public void Add_INT( string key, int value ) {
		if (values_INT.ContainsKey( key )) {
			values_INT.Remove( key );
		}
		values_INT.Add( key, value );
	}

	public int Get_INT( string key ) {
		int obj = 0;
		if ( values_INT.ContainsKey( key ) ) {
			values_INT.TryGetValue( key, out obj );
		}  
		return obj;
	}

	public bool HasKey_INT( string key ) {
		return values_INT.ContainsKey( key );
	}

	public void DeleteKey_INT( string key ) {
		values_INT.Remove( key );
	}
	#endregion

	#region TYPE STRING
	public void Add_STRING( string key, string value ) {
		if (values_STRING.ContainsKey( key )) {
			values_STRING.Remove( key );
		}
		values_STRING.Add( key, value );
	}

	public string Get_string( string key ) {
		string obj = "";
		if ( values_STRING.ContainsKey( key ) ) {
			values_STRING.TryGetValue( key, out obj );
		}  
		return obj;
	}

	public bool HasKey_STRING( string key ) {
		return values_STRING.ContainsKey( key );
	}

	public void DeleteKey_STRING( string key ) {
		values_STRING.Remove( key );
	}
	#endregion

	#region TYPE BOOL
	public void Add_BOOL( string key, bool value ) {
		if (values_BOOL.ContainsKey( key )) {
			values_BOOL.Remove( key );
		}
		values_BOOL.Add( key, value );
	}

	public bool Get_BOOL( string key ) {
		bool obj = false;
		if ( values_BOOL.ContainsKey( key ) ) {
			values_BOOL.TryGetValue( key, out obj );
		}  
		return obj;
	}

	public bool HasKey_BOOL( string key ) {
		return values_BOOL.ContainsKey( key );
	}

	public void DeleteKey_BOOL( string key ) {
		values_BOOL.Remove( key );
	}
	#endregion
	
	#region TYPE object
	public void Add_Object( string key, object value ) {
		if (values_Object.ContainsKey( key )) {
			values_Object.Remove( key );
		}
		values_Object.Add( key, value );
	}

	public object Get_Object( string key ) {
		object obj = null;
		if ( values_Object.ContainsKey( key ) ) {
			values_Object.TryGetValue( key, out obj );
		}  
		return obj;
	}

	public bool HasKey_Object( string key ) {
		return values_Object.ContainsKey( key );
	}

	public void DeleteKey_Object( string key ) {
		values_Object.Remove( key );
	}
	#endregion

	#endregion


}
}


