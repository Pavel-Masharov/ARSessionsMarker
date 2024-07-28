using UnityEngine;
using System;
using System.Collections.Generic;

namespace AR.MarkersSession
{
    [CreateAssetMenu(fileName = "SettingsARSession", menuName = "ScriptableObjects/SettingsARSession", order = 2)]
    public class SettingsARSessionSO : ScriptableObject
    {
        [SerializeField] [Header("Скорость вращения")] [Range(1f, 360f)] private float _rotationSpeed = 180f;
        [SerializeField] [Header("Минимальный скейл")] private float _minScale = 0.05f;
        [SerializeField] [Header("Максимальный скейл")] private float _maxScale = 0.2f;
        [SerializeField] [Header("Скорость изменения скейла")] private float _speedScale = 0.05f;
        [SerializeField] private List<ARSessionItem> _sessionsAR = new();
        public float RotationSpeed => _rotationSpeed;
        public float MinScale => _minScale;
        public float MaxScale => _maxScale;
        public float SpeedScale => _speedScale;

        public ARSessionItem GetSessionAtIndex(int index)
        {
            int id = index < _sessionsAR.Count ? index : 0;
            return _sessionsAR[id];
        }
    }

    [Serializable]
    public class ARSessionItem
    {
        [Header ("Ссылка на маркер")] public string pathToMarker;
        [Header("Ссылка на модель")] public string pathToModel;
    }
}
