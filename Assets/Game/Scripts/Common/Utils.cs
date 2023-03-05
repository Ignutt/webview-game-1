using System;
using UnityEngine;
using RandomUnity = UnityEngine.Random;

namespace Common
{
    [Serializable]
    public struct Range
    {
        [SerializeField] private float max;
        [SerializeField] private float min;

        public float Random()
        {
            return RandomUnity.Range(min, max);
        }
    }
    
    public static class Utils
    {
        // COMMON LOGIC
    }
}
