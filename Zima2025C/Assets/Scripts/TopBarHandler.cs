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

    public Slider SpeedBoostSlider;
    public Gradient SpeedBoostGradient;
    public Image SpeedBoostFillImage;
    public TMP_Text SpeedBoostText;

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

        SpeedBoostSlider.value = _playerController.SpeedBoostTime / 5;
        SpeedBoostFillImage.color = SpeedBoostGradient.Evaluate(_playerController.SpeedBoostTime / 5);
        SpeedBoostText.text = _playerController.SpeedBoostTime.ToString("F1") + "s";
    }
}
