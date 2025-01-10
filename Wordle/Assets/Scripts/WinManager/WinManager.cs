using TMPro;
using UnityEngine;

public class WinManager : MonoBehaviour
{
    public static WinManager Instance { get; private set; }

    private void OnEnable() => Instance = this;

    public void Win(){
        transform.GetChild(0).gameObject.SetActive(true);
    }

    public void Lose(string s){
        
        transform.GetChild(1).gameObject.SetActive(true);
        transform.GetChild(1).GetChild(1).GetComponent<TMP_Text>().text = "Kelime aslında \""+s + "\" idi.\nO kadar da zor değilmiş de' mi?";
    }
}
