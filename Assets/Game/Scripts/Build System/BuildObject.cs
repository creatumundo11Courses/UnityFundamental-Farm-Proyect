using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuildObject : MonoBehaviour
{
    public GameObject ObjectPrefab;
    public Transform ObjectT;
    public TextMeshPro PriceTMP;
    public int Price;

    private void Start()
    {
        PriceTMP.text = Price.ToString();
    }
    [ContextMenu("Build")]
    public void Build()
    {
        Instantiate(ObjectPrefab,ObjectT.position,ObjectT.rotation);
        gameObject.SetActive(false);
    }
}
