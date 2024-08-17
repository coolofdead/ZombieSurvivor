using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private void Update()
    {
        var lookAtPos = Camera.main.transform.position;
        lookAtPos.y = transform.position.y;

        transform.LookAt(lookAtPos);    
    }

    // Pour avoir la meme logique dans l'éditeur
    private void OnDrawGizmos()
    {
        Update();
    }
}
