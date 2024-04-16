using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shopper : MonoBehaviour
{
    public Transform[] CollectableTs;
    private int _currentCollectableTIndex;
    public bool IsReadyToPay;//Listo para pagar?
    public bool IsAlreadyBuy;//Ya pago?    
    public bool CanGoHome;//Ir a casa?
    public int AllowedId;
   
    private void OnTriggerStay(Collider other)
    {
        if (CanGoHome) return;

        if (other.CompareTag("FruitTable"))
        {
            if (_currentCollectableTIndex == CollectableTs.Length) return;

            ProductBaskets productBaskets = other.GetComponent<ProductBaskets>();
            Collectable collectable = productBaskets.GetAvailableCollectable();
            if (collectable == null)
            {
                if(HasProducts() == false) CanGoHome = true;

                return;
            }
            if (collectable.Id != AllowedId) return;
            if (collectable.IsReadyToBuy == false) return;
            if (collectable.HasOwner == true) return;
            
            collectable.SetLocalPositionToParent(CollectableTs[_currentCollectableTIndex]);
            collectable.HasOwner = true;
            collectable.IsReadyToBuy = true;
            IsReadyToPay = true;
            _currentCollectableTIndex++;
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

    public int GetAvailableCollectableCount()
    {
        int count = 0;
        for (int i = 0; i < CollectableTs.Length; i++)
        {
            if (CollectableTs[i].childCount > 0)
            {
                count++;
            }
        }

        return count;
    }
}
