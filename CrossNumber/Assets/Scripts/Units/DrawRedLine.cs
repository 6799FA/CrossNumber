using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawRedLine : MonoBehaviour
{
    [SerializeField] Transform _coverImage = null;
    [SerializeField] float _playTimeSecond = .2f;

    public void EraseLine() {
        transform.localScale = Vector3.zero;

    }

    public void DrawLine(Vector3 pos, Vector3 direct, float size) {

        pos += Vector3.forward * 2;

        if ((transform.position - pos).magnitude < 0.1f &&
            (transform.localScale - new Vector3(size, 1, 1)).magnitude < 0.1f) {
            return;
        }

        // ������� ��ġ, ����, ũ�⸦ ������ �ش�.
        transform.position = pos;
        transform.right = direct;
        transform.localScale = new Vector3(size, 1, 1);

        // ������� �׷����� �־��ٸ� ���߰�, ���ο� �������� �׸��� �����Ѵ�.
        StopAllCoroutines();
        StartCoroutine(DrawLineAction());

        UnitManager.Instance.playErrorSound = true;

    }

    // ����� ���� �ߴ� �ִϸ��̼� �ڷ�ƾ
    IEnumerator DrawLineAction()
    {
        float time = 0;
        float y = _coverImage.localScale.y;

        _coverImage.localScale = new Vector3(1, y);

        while (time < _playTimeSecond) {
            yield return new WaitForEndOfFrame();
            _coverImage.localScale = new Vector3(1 - time / _playTimeSecond, y);
            time += Time.deltaTime;
        }
        _coverImage.localScale = new Vector3(0, y);

    }
}
