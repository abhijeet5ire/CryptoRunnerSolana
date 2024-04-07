using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PocketDroidsSceneManager : MonoBehaviour
{
    public abstract void PlayerTapped();
    public abstract void droidTapped(GameObject droid);

    public virtual void DroidCollision(GameObject droid, Collision other) { }
}
