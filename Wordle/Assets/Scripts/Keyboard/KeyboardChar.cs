using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardChar : MonoBehaviour
{
    private string s = "";
    private void OnEnable() {
        s = transform.GetComponentInChildren<TMP_Text>().text;
        GetComponent<Button>().onClick.AddListener(()=>WordleManager.Instance.SetChar(s));
    }

    private void Start() => WordleManager.Instance.dic4Keyboard.Add(s, transform.GetComponent<Image>());
    
}
