using UnityEngine;
using UnityEngine.UI;

public class ScrollController : MonoBehaviour
{
    const float BUTTON_DELAY = 0.5f; //Time in seconds that the button has to be pressed to get down 
    const float SCROLL_SENSIBILITY = 100; // Scrolls's velocity when pressing the arrows

    private Scrollbar _scrollBar;
    private GameObject _buttonUp, _buttonUpFake, _buttonDownFake;
    private GameObject _button;
    public RectTransform container;

    private bool _buttonDownIsPressed = false;
    private bool _buttonDownIsEnter = false;
    private float _counter = 0;
    private float _scrollHeight;

    void Awake ()
    {
        _scrollBar = GetComponentInChildren<Scrollbar>();
        _buttonUp = transform.Find("ButtonUp").gameObject;
        _buttonUpFake = transform.Find("ButtonUpFake").gameObject;
        _buttonDownFake = transform.Find("ButtonDownFake").gameObject;
        _scrollHeight = GetComponent<RectTransform>().sizeDelta.y;

        ToggleFakeArrows(false);
    }
	
	void Update ()
    {
        if (_buttonDownIsPressed && _buttonDownIsEnter)
        {
            if (_counter == 0)
                ButtonDown();

            _counter += Time.deltaTime;

            if (_counter >= BUTTON_DELAY)
                ButtonDown();
        }
        else
            _counter = 0;
    }

    public void ButtonPressed(bool state)
    {
        _buttonDownIsPressed = state;
    }

    public void ButtonEnter(bool state)
    {
        _buttonDownIsEnter = state;
    }

    public void ReturnButton(GameObject button)
    {
        _button = button;
    }

    public void ButtonDown()
    {
        if (_button == _buttonUp)
            _scrollBar.value += GetArrowIncrement();
        else
            _scrollBar.value -= GetArrowIncrement();
    }

    public void ToggleFakeArrows(bool state)
    {
        _buttonUpFake.SetActive(state);
        _buttonDownFake.SetActive(state);
    }

    private float GetArrowIncrement()
    {
        float _heightContainer = container.sizeDelta.y;
        return 1 /(_heightContainer - _scrollHeight) * SCROLL_SENSIBILITY; 
    }
}
