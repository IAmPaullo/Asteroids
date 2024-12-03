using System;

[Serializable]
public class HighScoreEntry
{
    public char[] name = new char[3];
    public int score;

    public void Register(char[] name, int score)
    {
        this.name = name;
        this.score = score;
    }
}
