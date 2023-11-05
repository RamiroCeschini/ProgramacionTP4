using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour
{
    [SerializeField] private Decoration[] decorations;
    private Dictionary<string, Decoration> decosByName;

    private void Awake()
    {
        decosByName = new Dictionary<string, Decoration>();

        foreach(var deco in decorations)
        {
            decosByName.Add(deco.decoName, deco);
        }
    }

    public Decoration CreateDecoration(string decoName, Transform factoryTransform)
    {
        if(decosByName.TryGetValue(decoName, out Decoration decoPrefab))
        {
            Decoration decoInstance = Instantiate(decoPrefab, factoryTransform.position, Quaternion.identity);
            return decoInstance;
        }
        else
        {
            Debug.LogWarning($"La habilidad'{decoName}' no existe en la base de datos de Decoraciones.");
            return null;
        }
    }
}
