using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class Mapper 
{  

	public static T MapCreate<T>(string data, T obj)
	{
	    T type;

		type = JsonConvert.DeserializeObject<T>(data);
		return type;
	}

	public static List<T> MapCreateList<T>(string data, List<T> objList)
	{
		List<T> type;

		type = JsonConvert.DeserializeObject<List<T>>(data);
		return type;
	}

}
