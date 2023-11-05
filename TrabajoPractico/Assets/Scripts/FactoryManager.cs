using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryManager : MonoBehaviour
{
    [SerializeField] private Transform factoryTransform;
    [SerializeField] private Factory decoFactory;

    public void CreateTreeButton()
    {
        decoFactory.CreateDecoration("DecoTree", factoryTransform);
    }

    public void CreateBushButton()
    {
        decoFactory.CreateDecoration("DecoBush", factoryTransform);
    }

    public void CreateStoneButton()
    {
        decoFactory.CreateDecoration("DecoStone", factoryTransform);
    }
}
