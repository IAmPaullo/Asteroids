using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfig", menuName = "Game/PlayerConfig")]
public class PlayerConfig : ScriptableObject
{
    public bool isSoundActivated = true;


    public void SwitchAudio()
    {
        isSoundActivated = !isSoundActivated;
        Debug.Log("Sound Deactivated");
    }


}
