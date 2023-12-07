using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePointer : MonoBehaviour
{
   
    public Transform[] positions;

    
    public float moveSpeed = 5f;

   
    private int currentPositionIndex = 0;

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.S))
        {
            
            MoveToNextPosition();
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
           
            MoveToPreviousPosition();
        }
    }

    void MoveToNextPosition()
    {
       
        currentPositionIndex = (currentPositionIndex + 1) % positions.Length;

        
        MoveToCurrentPosition();
    }

    void MoveToPreviousPosition()
    {
        
        currentPositionIndex = (currentPositionIndex - 1 + positions.Length) % positions.Length;

       
        MoveToCurrentPosition();
    }

    void MoveToCurrentPosition()
    {
        
        transform.position = positions[currentPositionIndex].position;
    }
}
