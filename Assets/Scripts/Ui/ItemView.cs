using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class ItemView : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _countText;
    [SerializeField] private Button _dropButton;
    [SerializeField] private Button _iconButton;

    private Cell _cell;

    public string Name => _cell.Name;
    private int _count => _cell.Count;

    public event UnityAction<ItemView> ItemDropped;

    private void OnEnable()
    {
        _dropButton.onClick.AddListener(OnDropButtonClick);
        _iconButton.onClick.AddListener(OnIconButtonClick);
    }

    private void OnDisable()
    {
        _dropButton.onClick.RemoveListener(OnDropButtonClick);
        _iconButton.onClick.RemoveListener(OnIconButtonClick);
        _cell.ItemsChanged -= OnItemsChanged;
    }

    public void Init(Cell cell)
    {
        _cell = cell;
        _cell.ItemsChanged += OnItemsChanged;
        MakeEmpty();
    }

    public void AddNewItem(Sprite sprite)
    {
        _countText.text = _count.ToString();
        _icon.sprite = sprite;
        _icon.enabled = true;
        _iconButton.enabled = true;
    }

    private void MakeEmpty()
    {
        _countText.enabled = false;
        _icon.enabled = false;
        _iconButton.enabled = false;
    }

    private void OnIconButtonClick()
    {
        _dropButton.gameObject.SetActive(!_dropButton.gameObject.activeInHierarchy);
    }

    private void OnDropButtonClick()
    {
        ItemDropped?.Invoke(this);
        _dropButton.gameObject.SetActive(false);
    }

    private void OnItemsChanged()
    {
        switch (_count)
        {
            case 1:
                {
                    _countText.enabled = false;
                    break;
                }
            case 0:
                {
                    MakeEmpty();
                    break;
                }
            default:
                {
                    _countText.enabled = true;
                    _countText.text = _count.ToString();
                    break;
                }
        }
    }
}
