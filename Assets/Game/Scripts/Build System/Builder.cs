using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Builder : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BuildObject"))
        {
            BuildObject buildObject = other.GetComponent<BuildObject>();
            if (buildObject.Price <= GameEconomy.Coins)
            {
                buildObject.Build();
                GameEconomy.Coins -= buildObject.Price;
            }
            
        }
    }
}
