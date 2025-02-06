using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;

public interface ICombinable
{
    GameObject gameObject { get; }
    Keychain keychain { get; }
    public void OnCombination();
}

public class Combinable : Keychain, ICombinable
{
    public Keychain keychain => this;
    GameObject ICombinable.gameObject { get => gameObject; }

    public void OnCombination()
    {
        gameObject.SetActive(false);
    }

    public Key GetRequiarKey()
    {
        return m_Keys[0];
    }
}