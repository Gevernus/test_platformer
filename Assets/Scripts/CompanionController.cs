using UnityEngine;

public class CompanionController : MonoBehaviour
{
    public PlayerController player;
    public MeshRenderer mesh;
    public Color32 idleColor = Color.green;
    public Color32 slideColor = Color.red;
    private PlayerState _state = PlayerState.Idle;

    private void Start()
    {
        mesh.sharedMaterial.color = idleColor;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.state != _state)
        {
            _state = player.state;
            mesh.sharedMaterial.color = _state == PlayerState.Slide ? slideColor : idleColor;
        }
    }
}
