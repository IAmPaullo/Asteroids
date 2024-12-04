using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfig", menuName = "Game/PlayerConfig")]
public class PlayerConfig : ScriptableObject
{
    public bool isSoundActivated = true;

    public string MainMenuScene;
    public string GameScene;

    [Range(0f, 1f)]
    public float gameAudioVolume = .5f;


    public void SwitchAudio()
    {
        isSoundActivated = !isSoundActivated;
        Debug.Log("Sound Deactivated");
    }
    public void SetAudioVolume(float volume)
    {
        gameAudioVolume = volume;
    }
}
