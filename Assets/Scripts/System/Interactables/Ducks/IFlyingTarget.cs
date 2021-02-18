using UnityEngine;

public interface IFlyingTarget { 
    public enum State {
        FLYING,
        FLEEING,
        DEAD
    }
    
    public Vector3 SpawnSize { set; }
}