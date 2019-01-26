using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Code", menuName = "ScriptableObjectData/Code", order = 2)]
public class Code : ScriptableObject
{
	public string text;
	public int scrollSpeed;
}
