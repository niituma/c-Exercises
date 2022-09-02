using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parabola : MonoBehaviour
{
    [SerializeField]
    Transform[] _points;
    float[,] _threepos = new float[3, 3];
    [SerializeField]
    float _addXPos = 0.1f;
    int positionCount;
    LineRenderer lineRenderer;
    float x, y;
    float a, b, c;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        x = _points[0].position.x;
        for (int i = 0; i < 3; i++)
        {
            _threepos[i, 0] = _points[i].position.y;
            _threepos[i, 1] = _points[i].position.x * _points[i].position.x;
            _threepos[i, 2] = _points[i].position.x;
        }
        Points();

        PointsSarch();
    }

    private void Points()
    {
        float y1, xx1, x1;
        float y2, xx2, x2;
        float y3, xx3, x3;
        (y1, xx1, x1)
            = (_threepos[0, 0] - _threepos[1, 0], _threepos[0, 1] - _threepos[1, 1], _threepos[0, 2] - _threepos[1, 2]);

        (y2, xx2, x2)
            = (_threepos[1, 0] - _threepos[2, 0], _threepos[1, 1] - _threepos[2, 1], _threepos[1, 2] - _threepos[2, 2]);

        var l = LCM(Mathf.Abs(xx1), Mathf.Abs(xx2));
        (y3, xx3, x3)
            = (y1 * (l / xx1) - y2 * (l / xx2), xx1 * (l / xx1) - xx2 * (l / xx2), x1 * (l / xx1) - x2 * (l / xx2));

        b = y3 / x3;
        a = (y1 - x1 * b) / xx1;
        c = _threepos[0, 0] - _threepos[0, 1] * a - _threepos[0, 2] * b;
    }

    void PointsSarch()
    {
        for (int i = 0; i < 3; i++)
        {
            positionCount++;
            lineRenderer.positionCount = positionCount;
            lineRenderer.SetPosition(positionCount - 1, _points[i].position);

            while (_points[i] != _points[2])
            {
                x += _addXPos;
                
                if (x >= _points[i + 1].position.x)
                {
                    break;
                }

                y = a * x * x + b * x + c;
                positionCount++;
                lineRenderer.positionCount = positionCount;
                lineRenderer.SetPosition(positionCount - 1, new Vector3(x, y));
            }
        }
    }
    public float LCM(float a, float b)
    {
        var x = a * b;

        var r = a % b;
        while (r != 0)
        {
            a = b;
            b = r;
            r = a % b;
        }

        return x / b;
    }
}
