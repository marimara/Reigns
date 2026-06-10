using UnityEngine;
using PixelCrushers.DialogueSystem;
using Sirenix.OdinInspector;

public class DialogueTester : MonoBehaviour
{
    [Button]
    public void StartLouiseConversation()
    {
        DialogueManager.StartConversation("Louise Greeting");
    }
}