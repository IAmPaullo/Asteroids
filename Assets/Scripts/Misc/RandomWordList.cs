using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RandomWordList", menuName = "Scriptable Objects/RandomWordList")]
public class RandomWordList : ScriptableObject
{
    public List<string> words = new();
    public List<int> numbers = new(); //TODO switch to json?
    public List<char> signs = new();

    public List<string> abrev = new();



    private string GetRandomWord()
    {
        return words[Random.Range(0, words.Count)];
    }
    private string GetRandomNumber()
    {
        return words[Random.Range(0, numbers.Count)];
    }
    private string GetRandomAbrev()
    {
        return abrev[Random.Range(0, abrev.Count)];
    }
    private char GetRandomSign()
    {
        return signs[Random.Range(0, signs.Count)];
    }
    public string GenerateRandomSentence()
    {
        char sign = GetRandomSign();
        string sentence = $"{GetRandomNumber()} {sign} {GetRandomWord()} {GetRandomAbrev()} {sign}";
        return sentence;
    }

}
