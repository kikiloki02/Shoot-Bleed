using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodGenerator : MonoBehaviour
{
    public SpriteRenderer _spriteRenderer;

    public Sprite _blood1;
    public Sprite _blood2;
    public Sprite _blood3;
    public Sprite _blood4;

    // Start is called before the first frame update
    void Start()
    {
        int _randomValue = Random.Range(0, 4);

        switch (_randomValue)
        {
            case 0:
            {
                _spriteRenderer.sprite = _blood1;
                break;
            }
            case 1:
            {
                _spriteRenderer.sprite = _blood2;
                break;
            }
            case 2:
            {
                _spriteRenderer.sprite = _blood3;
                break;
            }
            case 3:
            {
                _spriteRenderer.sprite = _blood4;
                break;
            }
        }
    }
}
