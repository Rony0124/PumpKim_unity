using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAtkTarget 
{

    void AtkTarget();
    void AtkTarget(Vector3 targetPos, Vector3 pos, int speed);
}
