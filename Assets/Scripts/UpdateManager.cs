using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UpdateManager : MonoBehaviour
{
    public static UpdateManager Instance { get; private set; }

    private struct Sub<T> : IEquatable<Sub<T>>
    {
        public T Target;
        public int Priority;

        public Sub(T target)
        {
            Target = target;
            Priority = 1;
        }

        public Sub(T target, int priority)
        {
            Target = target;
            Priority = priority;
        }

        public bool Equals(Sub<T> other)
        {
            return EqualityComparer<T>.Default.Equals(Target, other.Target);
        }

        public override bool Equals(object obj)
        {
            return obj is Sub<T> other && Equals(other);
        }

        public override int GetHashCode()
        {
            return EqualityComparer<T>.Default.GetHashCode(Target);
        }
    }

    private static readonly List<Sub<IUpdatable>> Subscriptions = new();

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        var deltaTime = Time.deltaTime;
        foreach (var sub in Subscriptions) sub.Target.OnUpdate(deltaTime);
    }

    public static void Register(IUpdatable update)
    {
        Subscriptions.Add(new Sub<IUpdatable>(update));
        Sort();
    }
    
    public static void Register(IUpdatable update, int priority)
    {
        Subscriptions.Add(new Sub<IUpdatable>(update, priority));
        Sort();
    }

    public static void Unregister(IUpdatable update)
    {
        Subscriptions.Remove(new Sub<IUpdatable>(update));
    }

    private static void Sort()
    {
        // Lower priority first
        Subscriptions.Sort((a, b) => a.Priority.CompareTo(b.Priority));
    }
}

public interface IUpdatable { void OnUpdate(float deltaTime); }
