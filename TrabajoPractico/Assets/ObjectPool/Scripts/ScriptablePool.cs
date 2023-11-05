using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptablePool", menuName = "ObjectPool")]
public class ScriptablePool : ScriptableObject
{
    [SerializeField] private int s_amount;
    [SerializeField] private GameObject s_bulletPrefab;
    [SerializeField] private string s_name;
    public List<GameObject> s_pooledObjects;


    public int S_amount { get { return s_amount; } }
    public GameObject S_bulletPrefab { get { return s_bulletPrefab; } }
    public string S_name { get { return s_name; } }
    public List<GameObject> S_pooledObjects { get { return s_pooledObjects; } }
   
  
}
