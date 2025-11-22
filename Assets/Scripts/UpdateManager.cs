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

    private static readonly List<Sub<IUpdatable>> UpdateSubscriptions = new();
    private static readonly List<Sub<IFixedUpdatable>> FixedUpdateSubscriptions = new();

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        var deltaTime = Time.deltaTime;
        foreach (var sub in UpdateSubscriptions) sub.Target.OnUpdate(deltaTime);
    }

    private void FixedUpdate()
    {
        var deltaTime = Time.fixedDeltaTime;
        foreach (var sub in FixedUpdateSubscriptions) sub.Target.OnFixedUpdate(deltaTime);
    }

    public static void Register(IUpdatable update)
    {
        UpdateSubscriptions.Add(new Sub<IUpdatable>(update));
        Sort(UpdateSubscriptions);
    }
    
    public static void Register(IUpdatable update, int priority)
    {
        UpdateSubscriptions.Add(new Sub<IUpdatable>(update, priority));
        Sort(UpdateSubscriptions);
    }

    public static void Register(IFixedUpdatable update)
    {
        FixedUpdateSubscriptions.Add(new Sub<IFixedUpdatable>(update));
        Sort(FixedUpdateSubscriptions);
    }

    public static void Unregister(IUpdatable update)
    {
        UpdateSubscriptions.Remove(new Sub<IUpdatable>(update));
    }
    
    public static void Unregister(IFixedUpdatable update)
    {
        FixedUpdateSubscriptions.Remove(new Sub<IFixedUpdatable>(update));
    }

    private static void Sort<T>(List<Sub<T>> list)
    {
        // Lower priority first
        list.Sort((a, b) => a.Priority.CompareTo(b.Priority));
    }
}

public interface IUpdatable { void OnUpdate(float deltaTime); }
public interface IFixedUpdatable { void OnFixedUpdate(float deltaTime); }
