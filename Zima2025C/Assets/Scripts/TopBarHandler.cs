using UnityEngine;
using UnityEngine.UI;

public class TopBarHandler : MonoBehaviour
{
    private PlayerController _playerController;
    public Slider HPSlider;

    void Start()
    {
        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        HPSlider.value = _playerController.hp / 100;
    }
}
