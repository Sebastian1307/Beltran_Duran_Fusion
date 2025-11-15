using Fusion;
using UnityEngine;

public class Player : NetworkBehaviour
{
    private NetworkCharacterController _cc;

    [SerializeField] private Renderer bodyRenderer;

    [Networked] public Color PlayerColor { get; set; }
    [Networked] public int Score { get; set; }

    public override void Spawned()
    {
        if (Object.HasStateAuthority)
            PlayerColor = UnityEngine.Random.ColorHSV();

        bodyRenderer.material.color = PlayerColor;

        if (Object.HasInputAuthority)
        {
            var cam = Camera.main;
            cam.transform.SetParent(transform);

            cam.transform.localPosition = new Vector3(0, 1.6f, -3f);
            cam.transform.localRotation = Quaternion.Euler(10, 0, 0);
        }
    }

    public void AddPoint()
    {
        if (Object.HasStateAuthority == false) return;

        Score++;

        Debug.Log($"Player {Object.InputAuthority} score = {Score}");

        if (Score >= 3)
        {
            FindObjectOfType<MatchManager>().EndMatch(Object.InputAuthority);
        }
    }

    private void Awake()
    {
        _cc = GetComponent<NetworkCharacterController>();
    }

    public override void FixedUpdateNetwork()
    {
        if (GetInput(out NetworkInputData data))
        {
            data.direction.Normalize();
            _cc.Move(5 * data.direction * Runner.DeltaTime);
        }
    }
}
