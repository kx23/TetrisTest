using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class GameOverView : MonoBehaviour
{
    public Image BGImage;
    public GameObject Panel;
    public Text Score;
    private void OnEnable()
    {
        BGImage.DOColor(new Color(0, 0, 0, 0.5f), 0.5f);
        Panel.transform.DOScale(new Vector3(1, 1, 1), 0.5f);
    }
}
