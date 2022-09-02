using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchLine : MonoBehaviour
{
    [SerializeField]
    int count = 0;
    int _rcount = 0;
    int _lcount = 0;


    public void LineSarch()
    {
        Ray2D rightray = new Ray2D(transform.position, new Vector2(1, 0));
        Ray2D leftray = new Ray2D(transform.position, new Vector2(-1, 0));
        //ヒット判定にPysics2D.Raycastを使用
        RaycastHit2D righthit = Physics2D.Raycast(rightray.origin, rightray.direction);
        RaycastHit2D lefthit = Physics2D.Raycast(leftray.origin, leftray.direction);

        if (righthit.collider)
        {
            _rcount++;
            while (true)
            {
                rightray.origin = righthit.point + new Vector2(0.1f, 0);
                righthit = Physics2D.Raycast(rightray.origin, rightray.direction);
                if (righthit.collider) { _rcount++; }
                else { break; }
            }
        }

        if (lefthit.collider)
        {
            _lcount++;
            while (true)
            {
                leftray.origin = lefthit.point + new Vector2(-0.1f, 0);
                lefthit = Physics2D.Raycast(leftray.origin, leftray.direction);
                if (lefthit.collider) { _lcount++; }
                else { break; }
            }
        }
        count = _lcount + _rcount;
        if (count % 2 == 1 || count == 0 || count % 4 == 0 || (count == 2 && (_rcount == 2 || _lcount == 2)))
        {
            GetComponent<SpriteRenderer>().color = Color.white;
        }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.red;
        }
    }

    public void CountReset()
    {
        count = 0;
        _lcount = 0;
        _rcount = 0;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, new Vector2(1, 0));
        Gizmos.DrawRay(transform.position, new Vector2(-1, 0));
    }
}
