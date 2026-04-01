using System;
using Unity.Cinemachine;
using UnityEngine;

public class IntroCamController : MonoBehaviour
{
    private CinemachineCamera vcam;

    [Range(0.001f, 0.1f)]
    public float speed;

    private void Awake()
    {
        vcam = GetComponent<CinemachineCamera>();
    }
    
    void Update()
    {
        if (IsVCamActive())
        {
            vcam.Lens.OrthographicSize -= speed * Time.fixedDeltaTime;
        }
    }

    private bool IsVCamActive()
    {
        return vcam.Priority>= 10;
    }
}
