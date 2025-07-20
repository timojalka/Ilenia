using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealityManager : MonoBehaviour
{
    public enum EWorld
    {
        World1,
        World2
    }

    [SerializeField] CharacterController TrackedPlayer;
    [SerializeField] Camera TrackedPlayerCamera;

    [SerializeField] Transform ShadowPlayer;
    [SerializeField] Camera ShadowPlayerCamera;

    [SerializeField] Transform World1Anchor;
    [SerializeField] Transform World2Anchor;

    [field: SerializeField] public EWorld CurrentWorld { get; private set; } = EWorld.World1;

    public Vector3 CurrentAnchorPosition => CurrentWorld == EWorld.World1 ? World1Anchor.position : World2Anchor.position;
    public Vector3 OtherAnchorPosition => CurrentWorld == EWorld.World1 ? World2Anchor.position : World1Anchor.position;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        SynchroniseShadow();
    }

    void SynchroniseShadow()
    {
        // Update the Shadow Player location
        ShadowPlayer.position = TrackedPlayer.transform.position - CurrentAnchorPosition + OtherAnchorPosition;
        ShadowPlayer.rotation = TrackedPlayer.transform.rotation;

        // Update the Shadow Player Camera location
        ShadowPlayerCamera.transform.position = TrackedPlayerCamera.transform.position - CurrentAnchorPosition + OtherAnchorPosition;
        ShadowPlayerCamera.transform.rotation = TrackedPlayerCamera.transform.rotation;
    }

    public void FlipWorlds()
    {
        // TODO - Safety Check

        TrackedPlayer.transform.position = ShadowPlayer.transform.position;
        CurrentWorld = CurrentWorld == EWorld.World1 ? EWorld.World2 : EWorld.World1;
        SynchroniseShadow();
    }
}
