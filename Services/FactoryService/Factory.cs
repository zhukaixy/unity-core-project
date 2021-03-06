﻿using UnityEngine;
using Zenject;

namespace Core.Factory
{
    public class Factory
    {
        private readonly DiContainer _diContainer;

        public Factory(DiContainer context)
        {
            _diContainer = context;
        }

        public T Instantiate<T>(T original) where T : Object
        {
            return _diContainer.InstantiatePrefabForComponent<T>(original);
        }

        public T Instantiate<T>(T original, Transform transform) where T : Object
        {
            return _diContainer.InstantiatePrefabForComponent<T>(original, transform);
        }

        public GameObject Instantiate(Object original, Transform transform)
        {
            return _diContainer.InstantiatePrefab(original, transform);
        }

        public ComponentPool<T> CreateComponentPool<T>(Component prefab, int amount, Transform poolTransform = null) where T : Component
        {
            return new ComponentPool<T>(prefab, amount, _diContainer, poolTransform);
        }

        public PooledCoreBehaviourPool<T> CreatePooledCoreBehaviourPool<T>(Component prefab, int amount, Transform poolTransform = null) where T : Component, IInitializablePoolElement
        {
            return new PooledCoreBehaviourPool<T>(prefab, amount, _diContainer, poolTransform);
        }
    }
}