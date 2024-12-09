using UnityEngine;
using UnityEngine.UIElements;

public class HoverRotateLetters : MonoBehaviour
{
    private Label rotatingLabel;

    void OnEnable()
    {
        // Obt�m a refer�ncia do Label
        var root = GetComponent<UIDocument>().rootVisualElement;
        rotatingLabel = root.Q<Label>("rotatingLabel");

        // Divide o texto em letras
        WrapTextWithSpans(rotatingLabel);

        // Adiciona eventos de hover
        rotatingLabel.RegisterCallback<MouseEnterEvent>(evt => RotateLetters(true));
        rotatingLabel.RegisterCallback<MouseLeaveEvent>(evt => RotateLetters(false));
    }

    void WrapTextWithSpans(Label label)
    {
        string text = label.text;
        label.text = ""; // Limpa o texto original
        foreach (char c in text)
        {
            // Cria elementos para cada letra
            var letterElement = new Label(c.ToString());
            letterElement.AddToClassList("letter");
            label.Add(letterElement);
        }
    }

    void RotateLetters(bool isHovering)
    {
        foreach (var child in rotatingLabel.Children())
        {
            if (child is Label letter)
            {
                if (isHovering)
                {
                    // Define uma rota��o aleat�ria
                    letter.style.rotate = new StyleRotate(new Rotate(new Angle(Random.Range(0, 360), AngleUnit.Degree)));
                }
                else
                {
                    // Reseta a rota��o
                    letter.style.rotate = new StyleRotate(new Rotate(0));
                }
            }
        }
    }
}
