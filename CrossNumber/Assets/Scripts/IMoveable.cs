using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoveable { 
    
    public Vector2Int Pos { get; set; }
    
    public void SetPosition(int x, int y);
    public bool CanPlace(int x, int y);
}