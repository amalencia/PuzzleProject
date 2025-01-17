using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpObjects : MonoBehaviour
{
    [SerializeField] Portal blueCheck;
    [SerializeField] Portal pinkCheck;

    private GameObject cloneObject;

    private int inPortalCount = 0;

    private Portal inPortal;
    private Portal outPortal;

    protected new Rigidbody rigidbody;
    protected new Collider collider;

    private static readonly Quaternion halfTurn = Quaternion.Euler(0.0f, 180.0f, 0.0f);
    public virtual void Teleport()
    {

            Transform inTransform = inPortal.transform;
            Transform outTransform = outPortal.transform;

            // Update position of object.
            Vector3 relativePos = inTransform.InverseTransformPoint(transform.position);
            relativePos = halfTurn * relativePos;
            transform.position = outTransform.TransformPoint(relativePos);

            // Update rotation of object.
            Quaternion relativeRot = Quaternion.Inverse(inTransform.rotation) * transform.rotation;
            relativeRot = halfTurn * relativeRot;
            transform.rotation = outTransform.rotation * relativeRot;

            // Update velocity of rigidbody.
            Vector3 relativeVel = inTransform.InverseTransformDirection(rigidbody.velocity);
            relativeVel = halfTurn * relativeVel;
            rigidbody.velocity = outTransform.TransformDirection(relativeVel);

            // Swap portal references.
            Portal tmp = inPortal;
            inPortal = outPortal;
            outPortal = tmp;
    }

    protected virtual void Awake()
    {
        cloneObject = new GameObject();
        cloneObject.SetActive(false);
        MeshFilter meshFilter = cloneObject.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = cloneObject.AddComponent<MeshRenderer>();

        meshFilter.mesh = GetComponent<MeshFilter>().mesh;
        meshRenderer.materials = GetComponent<MeshRenderer>().materials;
        cloneObject.transform.localScale = transform.localScale;

        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
    }

    private void LateUpdate()
    {
        if (inPortal == null || outPortal == null)
        {
            return;
        }
        if(cloneObject != null)
        {
            if (cloneObject.activeSelf && inPortal.IsPlaced && outPortal.IsPlaced)
            {
                Transform inTransform = inPortal.transform;
                Transform outTransform = outPortal.transform;

                // Update position of clone.
                Vector3 relativePos = inTransform.InverseTransformPoint(transform.position);
                relativePos = halfTurn * relativePos;
                cloneObject.transform.position = outTransform.TransformPoint(relativePos);

                // Update rotation of clone.
                Quaternion relativeRot = Quaternion.Inverse(inTransform.rotation) * transform.rotation;
                relativeRot = halfTurn * relativeRot;
                cloneObject.transform.rotation = outTransform.rotation * relativeRot;
            }
            else
            {
                cloneObject.transform.position = new Vector3(-1000.0f, 1000.0f, -1000.0f);
            }
        }

    }

    public void SetIsInPortal(Portal inPortal, Portal outPortal, Collider wallCollider)
    {
        this.inPortal = inPortal;
        this.outPortal = outPortal;

        Physics.IgnoreCollision(collider, wallCollider);

        if(cloneObject != null)
        {
            cloneObject.SetActive(false);
        }
        

        ++inPortalCount;
    }

    public void ExitPortal(Collider wallCollider)
    {
        Physics.IgnoreCollision(collider, wallCollider, false);
        --inPortalCount;

        if (inPortalCount == 0 && cloneObject != null)
        {
            cloneObject.SetActive(false);
        }
    }
}
