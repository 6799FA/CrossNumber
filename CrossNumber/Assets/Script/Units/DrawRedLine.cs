using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawRedLine : MonoBehaviour
{
    [SerializeField] Transform _coverImage = null;
    [SerializeField] float _playTimeSecond = .2f;

    // ��� ����� Ʋ���� ����� ���� ����
    public void DrawLine(Vector3 pos, Vector3 direct, float size) {

        pos += Vector3.forward;

        if ((transform.localScale - new Vector3(size, 1, 1)).magnitude < 0.1f) {
            return;
        }
        
        StopAllCoroutines();
        StartCoroutine(Wipe());
        UnitManager.instance._playErrorSound = true;

        transform.position = pos;
        transform.right = direct;
        transform.localScale = new Vector3(size, 1, 1);
    }

    public void EraseLine() {
        transform.localScale = Vector3.zero;
    }

    // ����� ���� �ߴ� �ִϸ��̼� �ڷ�ƾ
    IEnumerator Wipe()
    {
        float time = 0;
        float y = _coverImage.localScale.y;

        _coverImage.localScale = new Vector3(1, y);
        gameObject.layer = 9;

        while (time < _playTimeSecond) {
            yield return new WaitForEndOfFrame();
            _coverImage.localScale = new Vector3(1 - time / _playTimeSecond, y);
            time += Time.deltaTime;
        }
        _coverImage.localScale = new Vector3(0, y);
        gameObject.layer = 8;
    }
}
