using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashRegister : MonoBehaviour
{
    public int HereIsPlayer; //0 No, 1 Yes
    public int HereIsShopper;//0 No, 1 Yes
    public Shopper Shopper;//0 No, 1 Yes
    public Transform TargetT;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HereIsPlayer = 1;
        }
        if (other.CompareTag("IA"))
        {
            Shopper = other.GetComponent<Shopper>();
            if (Shopper.IsReadyToPay == false)
            {
                Shopper = null;
                return;
            }

            HereIsShopper = 1;

        }
    }

    private void Update()
    {
        if (HereIsPlayer == 1 && HereIsShopper == 1)
        {
            if (Shopper == null) return;
            if (Shopper.IsAlreadyBuy == true) return;
            if (Shopper.IsReadyToPay == false) return;
            int purchaseValue = Shopper.GetAvailableCollectableCount() * 20;
            Shopper.IsAlreadyBuy = true;
            Shopper.CanGoHome = true;
            GameEconomy.Coins += purchaseValue;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HereIsPlayer = 0;
        }
        if (other.CompareTag("IA"))
        {
            HereIsShopper = 0;
            Shopper = null;
        }
    }
}
