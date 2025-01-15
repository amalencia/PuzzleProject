using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MetalBox : MonoBehaviour
{
    [SerializeField] private Wire _wire1;
    [SerializeField] private Wire _wire2;

    private bool _isWire1Connected;
    private bool _isWire2Connected;

    private bool _isConnectionComplete;
    public UnityEvent EvtOnConnectionComplete;
    public UnityEvent EvtOnConnectionBroken;

    private void OnTriggerEnter(Collider other)
    {
        Wire wire = other.GetComponent<Wire>();
        if (wire != null)
        {
            if (wire == _wire1)
            {
                _isWire1Connected = true;
            }
            else if (wire == _wire2)
            {
                _isWire2Connected = true;
            }

            if (_isWire1Connected && _isWire2Connected)
            {
                _isConnectionComplete = true;
                EvtOnConnectionComplete.Invoke();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Wire wire = other.GetComponent<Wire>();
        if (wire != null)
        {
            if (wire == _wire1)
            {
                _isWire1Connected = false;
            }
            else if (wire == _wire2)
            {
                _isWire2Connected = false;
            }

            if (_isConnectionComplete && (_isWire1Connected == false || _isWire2Connected == false))
            {
                _isConnectionComplete = false;
                EvtOnConnectionBroken.Invoke();
            }
        }
    }
}
