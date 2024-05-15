using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FastDev
{
    public class ReferencePool
    {
        private static readonly Dictionary<Type, ReferenceCollection> referenceCollections = new Dictionary<Type, ReferenceCollection>();
        public static int Count { get { return referenceCollections.Count; } }

        public static void ClearAll()
        {
            lock (referenceCollections)
            {
                foreach (var item in referenceCollections.Values)
                {
                    item.RemoveAll();
                }

                referenceCollections.Clear();
            }
        }

        public static T Acquire<T>() where T : class, IReference, new()
        {
            return GetReferenceCollection(typeof(T)).Acquire<T>();
        }
        public static void Release(IReference reference)
        {
            GetReferenceCollection(reference.GetType()).Release(reference);
        }
        public static void Add<T>(int count) where T : class, IReference, new()
        {
            GetReferenceCollection(typeof(T)).Add<T>(count);
        }
        public static void Remove<T>(int count) where T : class, IReference, new()
        {
            GetReferenceCollection(typeof(T)).Remove(count);
        }
        public static void RemoveAll<T>() where T : class, IReference, new()
        {
            GetReferenceCollection(typeof(T)).RemoveAll();
        }

        public static ReferenceCollection GetReferenceCollection(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }
            ReferenceCollection referenceCollection = null;
            lock (referenceCollections)
            {
                if (!referenceCollections.TryGetValue(type, out referenceCollection))
                {
                    referenceCollection = new ReferenceCollection(type);
                    referenceCollections.Add(type, referenceCollection);
                }
            }
            return referenceCollection;
        }
    }
}
