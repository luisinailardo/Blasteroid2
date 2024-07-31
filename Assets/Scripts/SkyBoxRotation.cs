using UnityEngine;

public class SkyBoxRotation : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    private static readonly int Rotation = Shader.PropertyToID("_Rotation");

    void Update()
    {
        RenderSettings.skybox.SetFloat(Rotation, Time.time * rotationSpeed);
    }
}
