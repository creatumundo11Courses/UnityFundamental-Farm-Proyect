using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Harvester : MonoBehaviour
{
    public Transform[] CollectableTs;
    private int _currentCollectableTIndex;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectable"))
        {
            if (_currentCollectableTIndex == CollectableTs.Length) return; 

            Collectable collectable = other.GetComponent<Collectable>();
            if (collectable.IsReadyToBuy == true) { return; }
            if (collectable.HasOwner == true) { return; }
            collectable.SetLocalPositionToParent(CollectableTs[_currentCollectableTIndex]);
            collectable.HasOwner = true;
            collectable.IsReadyToBuy = false;
            _currentCollectableTIndex++;
        }

       
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("FruitTable"))
        {
            ProductBaskets productBaskets = other.GetComponent<ProductBaskets>();
            Collectable collectable = GetAvailableCollectable();
            if (collectable == null) return;

            bool canPlace = productBaskets.PlaceProduct(collectable);
            if (canPlace)
            {
                _currentCollectableTIndex--;
            }
        }
    }

    private Collectable GetAvailableCollectable()
    {
        for (int i = CollectableTs.Length - 1; i >= 0; i--)
        {
            if (CollectableTs[i].childCount > 0)
            {
                 return CollectableTs[i].GetComponentInChildren<Collectable>();
            }
        }

        return null;
    }

    public bool HasProducts()
    {
        return GetAvailableCollectable() != null;
    }
}
