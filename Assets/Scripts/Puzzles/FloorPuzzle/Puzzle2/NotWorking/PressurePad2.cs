using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePad2 : PressurePad
{
    protected override void OnTriggerEnter(Collider collider)
    {
        if (collider.GetComponent<CubePickable>() != null)
        {
            collider.gameObject.transform.SetLocalPositionAndRotation(transform.position, transform.rotation);
            transform.position = downPosition;
            mesh.materials = buttonPressed;
            state = true;
            ButtonPressed.Invoke(state, row, col);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<CubePickable>() != null)
        {
            ResetPad();
        }
    }
}
