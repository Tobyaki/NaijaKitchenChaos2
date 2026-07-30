using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHasProgress
{
    // Remove the "public" keyword here (it is implicitly public in C# 7.3)
    event EventHandler<OnProgressChangedEventArgs> OnProgressChanged;

    void Interact(Player player);
}

// Move this class completely outside of the interface
public class OnProgressChangedEventArgs : EventArgs
{
    public float progressNormalized;
}

