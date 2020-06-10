﻿using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TEDinc.LinesRunner
{
    public abstract class BaseWeightHolder : ScriptableObject, IWeightHolder
    {
        public virtual WeightHolder[] collection { get; protected set; }

        public virtual object GetNextByWeigth()
        {
            return collection[GetRandomIndex()];


            int GetRandomIndex()
            {
                float totalWeight = 0f, randomWeight;

                foreach (WeightHolder item in collection)
                    totalWeight += item.weight;

                randomWeight = Random.Range(0f, totalWeight);

                for (int i = 0; i < collection.Length; i++)
                    if (collection[i].weight > randomWeight)
                        return i;
                    else
                        randomWeight -= collection[i].weight;

                Debug.LogError($"[BWH] {nameof(GetRandomIndex)}: some logic error");
                return 0;
            }
        }
    }

    [Serializable]
    public abstract class WeightHolder
    {
        public virtual object obj { get; protected set; }
        public float weight { get { return _weight; } protected set { _weight = value; } }
        [SerializeField, Min(0.01f)]
        protected float _weight;
    }
}