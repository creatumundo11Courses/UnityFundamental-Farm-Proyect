
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IAOrchestra : MonoBehaviour
{
    public GameObject IAPrefab;
    public Transform SpawnT;
    public float CreationRepeatRate;
    private float _creationTimer;
    public ProductBaskets[] ProductBaskets;

    private ProductBaskets _productBasketsAvailable;

    public List<CharacterIA> CharacterIAs;

    public CashRegister CashRegister;

    //Queue
    public Transform[] TargetTsQueue;
    public List<CharacterIA> CharacterIAsQueue;

    private void Start()
    {
        CharacterIAs = new();
    }
    private void Update()
    {
        _creationTimer += Time.deltaTime;
        if (_creationTimer > CreationRepeatRate)
        {
            if (IsAvailableProducts())
            {
                GameObject IAGO = Instantiate(IAPrefab, SpawnT.position, Quaternion.identity);
                CharacterIA characterIA = IAGO.GetComponentInChildren<CharacterIA>();
                CharacterIAs.Add(characterIA);
                ConfigureIA(characterIA);
                _creationTimer = 0;
            }
            
        }


        //Recorrer los estados de cada inteligencia artificial

        for (int i = 0; i < CharacterIAs.Count; i++)
        {
            if (CharacterIAs[i].Shopper.IsAlreadyBuy)
            {
                //Codigo para ir a casa
                RemoveElementQueue(CharacterIAs[i]);
                
            }
            else if (CharacterIAs[i].Shopper.IsReadyToPay)
            {
                //Codigo para ir a la caja registradora
                AddElementQueue(CharacterIAs[i]);
            }
            else if (CharacterIAs[i].Shopper.CanGoHome)
            {
                //Codigo para ir a casa
                GoHome(CharacterIAs[i]);
            }
        }
    }

    private void AddElementQueue(CharacterIA characterIA)
    {
        if (CharacterIAsQueue.Contains(characterIA) == true) return;

        CharacterIAsQueue.Add(characterIA);
        OrganizeQueue();
    }

    private void RemoveElementQueue(CharacterIA characterIA)
    {
        if (CharacterIAsQueue.Contains(characterIA) == false) return;
        CharacterIAsQueue.Remove(characterIA);
        GoHome(characterIA);
        OrganizeQueue();
    }

    private void OrganizeQueue()
    {
        for (int i = 0; i < CharacterIAsQueue.Count; i++)
        {
            CharacterIAsQueue[i].MoveToTransform(TargetTsQueue[i]);
        }
    }

    private void GoHome(CharacterIA characterIA)
    {
        if (CharacterIAs.Contains(characterIA) == false) return;
        CharacterIAs.Remove(characterIA);
        characterIA.MoveToTransform(SpawnT);
        Destroy(characterIA.transform.parent.gameObject,15);

    }

    private bool IsAvailableProducts()
    {
        for (int i = 0; i < ProductBaskets.Length; i++)
        {
            if (ProductBaskets[i].HasCollectables())
            {
                _productBasketsAvailable = ProductBaskets[i];
                return true;
            }
        }
        return false;
    }

    private void ConfigureIA(CharacterIA characterIA)
    {
        characterIA.MoveToTransform(_productBasketsAvailable.GoalT);
        characterIA.Shopper.AllowedId = _productBasketsAvailable.AllowedId;
    }
}
