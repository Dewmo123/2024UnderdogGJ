using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class AstarAgent : MonoBehaviour
{
    public float moveSpeed;
    public float stoppingDistance = 0.1f;
    [SerializeField] private bool _cornerCheck = true;
    [SerializeField] private bool _lineDebug = true;

    private Vector3Int _currentPos, _destination;
    private Vector3 _worldDestination;
    [SerializeField] private MapInfoSO _map;
    public MapInfoSO Map => _map;

    private Rigidbody2D _rigid;

    #region Astar variables 
    private PriorityQueue<AstarNode> _openList;
    private List<AstarNode> _closeList;
    private List<Vector3> _routePath = new List<Vector3>();
    public List<Vector3> RoutePath => _routePath;

    private int _currentIndex;
    #endregion

    private PathLineDrawer _lineDrawer;

    public void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _lineDrawer = transform.Find("PathDrawer").GetComponent<PathLineDrawer>();
        _lineDrawer.SetActiveLine(_lineDebug);
    }

    public bool SetDestination(Vector3 destination)
    {
        _worldDestination = destination;
        _currentPos = Map.GetTilePosFromWorldPos(transform.position);
        _destination = Map.GetTilePosFromWorldPos(destination);

        _routePath = CalcRoute();
        _currentIndex = 0;

        if (_routePath.Count > 0 && _lineDebug)
        {
            _lineDrawer.DrawLine(_routePath.ToArray());
        }

        return _routePath.Count > 0;
    }

    private void FixedUpdate()
    {
        CheckToMove();
    }

    private void CheckToMove()
    {
        if (_currentIndex >= _routePath.Count) return;

        Vector3 pos = _routePath[_currentIndex];
        Vector3 direction = pos - transform.position;

        if (direction.magnitude < stoppingDistance)
        {
            _currentIndex++;
            if (_currentIndex >= _routePath.Count)
            {
                //�ӵ� 0
                _rigid.velocity = Vector2.zero;
                //_movement.SetVelocity(Vector2.zero); //����!
                return;
            }
        }
        //�ӵ� ����
        _rigid.velocity = direction.normalized * moveSpeed;
        //_movement.SetVelocity(direction.normalized * moveSpeed);
    }


    #region Astar Algorithm
    private List<Vector3> CalcRoute()
    {
        _openList = new PriorityQueue<AstarNode>();
        _closeList = new List<AstarNode>();

        _openList.Push(
            new AstarNode
            {
                pos = _currentPos,
                parent = null,
                G = 0,
                F = CalH(_currentPos)
            }
        );

        bool result = false; //��θ� ã�Ҵ°�?
        while (_openList.Count > 0)
        {
            AstarNode next = _openList.Pop(); //���� ����� ��带 ã�Ƽ� ������.
            FindOpenList(next); //�ش� ��忡�� �� �� �ִ� ��� ��θ� ���¸���Ʈ�� �߰�
            _closeList.Add(next);

            if (next.pos == _destination)
            {
                result = true;
                break;
            }
        }
        //while���� �����ٸ� closeList�� �湮�� ����� �� �����ְ� �ȴ�.
        List<Vector3> routePath = new List<Vector3>();
        if (result)
        {
            AstarNode lastNode = _closeList[^1]; //���� �������� �ִ� �༮
            while (lastNode.parent != null)
            {
                routePath.Add(Map.GetCellCenterToWorld(lastNode.pos));
                lastNode = lastNode.parent;
            }
            routePath.Reverse();
        }

        return routePath;
    }

    private void FindOpenList(AstarNode node)
    {
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (i == 0 && j == 0) continue;

                Vector3Int nextPos = node.pos + new Vector3Int(j, i);

                //�̹� �ش� ������ �湮�ߴ�.
                AstarNode nextNode = _closeList.Find(x => x.pos == nextPos);
                if (nextNode != null) continue;

                //���� �ڳ�üũ �˰��� ���� �Ѵ�.
                if(_cornerCheck ) 
                {
                    bool xEqual = !Map.CanMove(new Vector3Int(nextPos.x, node.pos.y));
                    bool yEqual = !Map.CanMove(new Vector3Int(node.pos.x, nextPos.y));
                    
                    if(xEqual || yEqual)
                        continue;
                }

                if (Map.CanMove(nextPos))
                {
                    float g = (nextPos - node.pos).magnitude + node.G;
                    nextNode = new AstarNode
                    {
                        pos = nextPos,
                        parent = node,
                        G = g,
                        F = g + CalH(nextPos)
                    };

                    AstarNode exist = _openList.Contains(nextNode);

                    if (exist != null) //�̹� ���¸���Ʈ�� �־�
                    {
                        if (nextNode.G < exist.G)
                        {
                            exist.G = nextNode.G;
                            exist.F = nextNode.F;
                            exist.parent = nextNode.parent;
                        }
                    }
                    else
                    {
                        _openList.Push(nextNode);
                    }
                }
            }
        }
    }

    private float CalH(Vector3Int currentPos)
    {
        Vector3Int distance = _destination - currentPos;
        return distance.magnitude;
    }

    #endregion
}
