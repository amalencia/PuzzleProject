using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CenterRoom
{
    public class Portal : MonoBehaviour
    {
        [SerializeField] private Material _disabledMaterial;
        [SerializeField] private PortalSO _portalSO;

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                DisablePortal();
                _portalSO.SetNotActive();
                _portalSO.RaiseEvent();
            }
        }

        private void DisablePortal()
        {
            GetComponent<Renderer>().material = _disabledMaterial;
            GetComponent<Collider>().isTrigger = false;
        }

        void Start()
        {
            if (!_portalSO.IsActive())
            {
                DisablePortal();
            }
        }
    }
}
