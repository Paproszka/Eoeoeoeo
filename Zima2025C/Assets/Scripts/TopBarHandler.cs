using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TopBarHandler : MonoBehaviour
{
    private PlayerController _playerController;
    public Slider HPSlider;
    public Gradient HpGradient;
    public Image HpFillImage;
    public TMP_Text HpText;

    void Start()
    {
        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        HPSlider.value = _playerController.hp / 100;
        HpFillImage.color = HpGradient.Evaluate(_playerController.hp / 100);
        HpText.text = _playerController.hp.ToString("F1") + "%";
    }
}
