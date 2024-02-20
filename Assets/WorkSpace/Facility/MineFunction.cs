using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// �z�R(�{��)�̌���
/// </summary>
public class MineFunction : MonoBehaviour, IPointerClickHandler
{
    [SerializeField, Tooltip("���\�[�X��������Ԋu(s)")] float _span = 1f;
    [SerializeField, Tooltip("n�b�ő����郊�\�[�X��")] float _resourceIncreaseAmount = 10f;
    [SerializeField, Tooltip("�����ł��郊�\�[�X�̏��")] float _storageLimit = 1000f;

    ResourceManager _resourceManager;

    // �o�ߎ���(_span�Ń��Z�b�g)
    float _currentTime = 0;
    // ���݂̒�����
    float _currentResource = 0;

    void Start()
    {
        //_resourceManager = ResourceManager.Instance;
    }

    void Update()
    {
        _currentTime += Time.deltaTime;
        if (_currentTime > _span)
        {
            if (_currentResource < _storageLimit)
            {
                _currentResource += _resourceIncreaseAmount;
                _currentTime = 0;
            }
            else
            {
                _currentTime = 0;
            }
        }
    }

    /// <summary>
    /// �{�݂��N���b�N�����Ƃ��ɌĂ΂��
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        _resourceManager.AddResorce(_currentResource);
        _currentResource = 0;
    }
}
