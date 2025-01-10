using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResetButton : MonoBehaviour
{
    private void OnEnable() {
        hoop();
        GetComponent<Button>().onClick.AddListener(() => {transform.DOKill(); SceneManager.LoadScene(0);});
    }
    
    private void hoop() =>
        this.transform.DOScale(Vector3.one * 1.1f, .75f).OnComplete(()=>{
            this.transform.DOScale(Vector3.one, .75f).OnComplete(()=>{
                hoop();
            });
        });
    

}
