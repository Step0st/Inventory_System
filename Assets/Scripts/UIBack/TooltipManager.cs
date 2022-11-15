using TMPro;
using UnityEngine;

public class TooltipManager : MonoBehaviour
{
    public static TooltipManager instance;
    [SerializeField] private TextMeshProUGUI _textComponent;
    
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        transform.position = new Vector3(Input.mousePosition.x + 5,Input.mousePosition.y + 5, Input.mousePosition.z);
    }

    public void SetAndShowTooltip(string message)
    {
        gameObject.SetActive(true);
        _textComponent.text = message;
    }
    
    public void HideTooltip()
    {
        gameObject.SetActive(false);
        _textComponent.text = string.Empty;
    }
}
