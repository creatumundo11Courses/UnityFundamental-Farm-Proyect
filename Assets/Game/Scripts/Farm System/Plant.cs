using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    public GameObject CollectablePrefab;
    public Transform[] CollectableTs;
    public GameObject[] GrowthStagesGOs;
    public float MaxGrowthTime = 5f;
    private float _maxTimePerStage;
    private float _timerPerStage;
    private int _currentGrowthStageIndex;
    private bool _isAlreadyGrowth;

    private void Start()
    {
        ExtractPlant();
    }

    private void Update()
    {
        if (_isAlreadyGrowth)
        {
            CheckCollectableAvailable();
            return;
        }


        _timerPerStage += Time.deltaTime;
        if (_timerPerStage > _maxTimePerStage)
        {
            UpdateStages();
            _timerPerStage = 0;
            _currentGrowthStageIndex++;
        }
    }

    private void CheckCollectableAvailable()
    {
        bool hasCollectable = false;

        for (int i = 0; i < CollectableTs.Length; i++)
        {
            hasCollectable = CollectableTs[i].childCount > 0;
            if (hasCollectable)
            {
                return;
            }
        }

        ExtractPlant();
    }

    private void ExtractPlant()
    {
        for (int i = 0; i < GrowthStagesGOs.Length; i++)
        {
            GrowthStagesGOs[i].gameObject.SetActive(false);
        }
        _currentGrowthStageIndex = 0;
        _timerPerStage = 0;
        _isAlreadyGrowth = false;
        _maxTimePerStage = MaxGrowthTime / GrowthStagesGOs.Length;
    }

    private void UpdateStages()
    {
        if (_currentGrowthStageIndex == GrowthStagesGOs.Length)
        {
            CreateCollectables();
            return;
        }

        if (_currentGrowthStageIndex > 0)
        {
            GrowthStagesGOs[_currentGrowthStageIndex - 1].SetActive(false);
        }
        GrowthStagesGOs[_currentGrowthStageIndex].SetActive(true);
    }

    private void CreateCollectables()
    {
        for (int i = 0; i < CollectableTs.Length; i++)
        {
            GameObject collectableGO =  Instantiate(CollectablePrefab);
            Collectable collectable = collectableGO.GetComponent<Collectable>();
            collectable.SetLocalPositionToParent(CollectableTs[i]);

        }

        _isAlreadyGrowth = true;
    }
}
