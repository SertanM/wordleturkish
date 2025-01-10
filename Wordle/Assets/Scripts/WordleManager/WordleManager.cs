using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WordleManager : MonoBehaviour
{
    public static WordleManager Instance { get; private set;}
    public Dictionary<string, Image> dic4Keyboard = new Dictionary<string, Image>();


    private int wordIndex = 0;
    private int charIndex = 0;
    private bool isAnimating = false;
    private string chosenWord = "ADANA";

    private void OnEnable() {
        Instance = this;
        System.Random random = new System.Random();
        int randomIndex = random.Next(DB.db.Length);
        chosenWord = DB.db[randomIndex];
    }

    public void SetChar(string s){
        if(isAnimating) return;
        if(s=="Enter"){
            EnterWorld();
            return;
        }
        if(s=="Back"){
            BackChar();
            return;
        }
        if(charIndex>=5) return;
        
        transform.GetChild(wordIndex).GetChild(charIndex).GetChild(0).GetComponentInChildren<TMP_Text>().text = s;
        charIndex++;
    }

    private void EnterWorld(){
        if(charIndex!=5) return;
        if(!DB.db.Contains(GetThatString())) {
            transform.GetChild(wordIndex).DOShakePosition(.2f, new Vector3(20f, 2f, 0), 10, 1000, false, true);
            return;
        }
        SpinWithDG();
        
    }

    private string GetThatString(){
        string s = "";

        for(int i = 0; i < 5; i++)
            s += this.transform.GetChild(wordIndex).GetChild(i).GetComponentInChildren<TMP_Text>().text;
        
        return s;
    }

    private void SpinWithDG(int i = 0, int greenCount = 0){
        Transform thatTransform = transform.GetChild(wordIndex).GetChild(i);
        string s = thatTransform.GetChild(0).GetChild(0).GetComponentInChildren<TMP_Text>().text;
        Image image = thatTransform.GetChild(0).GetComponentInChildren<Image>();

        thatTransform.DORotate(new Vector3(0, 90, 0), .2f).SetEase(Ease.Linear).OnComplete(()=> {

            if(chosenWord[i].ToString() == s) {
                greenCount+=1;
                image.color = new Color32(83, 141, 78, 85);
                dic4Keyboard[s].color = new Color32(83, 141, 78, 85);
            }
            else if(chosenWord.Contains(s)){
                image.color = new Color32(181, 159, 59, 85);
                if(dic4Keyboard[s].color != new Color32(83, 141, 78, 85)) dic4Keyboard[s].color = new Color32(181, 159, 59, 85);
            }
            else {
                image.color = new Color32(58, 58, 60, 85);
                dic4Keyboard[s].color = new Color32(58, 58, 60, 85);
            }

            thatTransform.DORotate(new Vector3(0, 0, 0), .2f).SetEase(Ease.Linear).OnComplete(()=> {
                if(i<4){
                    SpinWithDG(i+1, greenCount); 
                    return;
                }
                if(greenCount==5){
                    WinManager.Instance.Win();
                    transform.parent.gameObject.SetActive(false);
                    return;
                }
                isAnimating = false;
                wordIndex++;
                if(wordIndex==6){
                    WinManager.Instance.Lose(chosenWord);
                    transform.parent.gameObject.SetActive(false);
                    return;
                }

                charIndex = 0;
            });
        });
    }

    private void BackChar(){
        if(charIndex==0) return;
        charIndex--;
        transform.GetChild(wordIndex).GetChild(charIndex).GetChild(0).GetComponentInChildren<TMP_Text>().text = "";
    }

}
