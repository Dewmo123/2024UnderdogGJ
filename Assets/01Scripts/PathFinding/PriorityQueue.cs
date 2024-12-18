using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriorityQueue<T> where T : IComparable<T>
{
    public List<T> _heap = new List<T>();
    public int Count => _heap.Count;

    public T Contains(T item)
    {
        int idx = _heap.IndexOf(item);
        if (idx < 0) return default;
        return _heap[idx];
    }

    public void Push(T newItem)
    {
        _heap.Add(newItem); //���� �������� �־��ְ�
        int now = _heap.Count - 1; //���� �ε��� ����
        while(now > 0)
        {
            int next = (now - 1) / 2;
            if (_heap[now].CompareTo(_heap[next]) < 0 )
            {
                break;
            }
            (_heap[now], _heap[next]) = (_heap[next], _heap[now]); //��ȯ

            now = next;
        }
    }

    public T Pop()
    {
        T ret = _heap[0];

        //�ٽ� ���� �����ؾ���.
        int lastIndex = _heap.Count - 1;
        _heap[0] = _heap[lastIndex];
        _heap.RemoveAt(lastIndex); //������ �༮�� �� ������ �����´�.
        lastIndex--;

        int now = 0;
        while(true)
        {
            int left = 2 * now + 1;
            int right = 2 * now + 2;

            int next = now; //������ ����
            //���� �ڽİ� ���ؼ� ���� �ڽ��� �� �۴ٸ� now�� ���� �ڽ��� �ȴ�.
            if (left <= lastIndex && _heap[next].CompareTo(_heap[left]) < 0)
                next = left;

            //����� ������ �� �����ɷ� �Ǹ��� �Ͱ� ������ �ڽ��̶� ���Ѵ�.
            if (right <= lastIndex && _heap[next].CompareTo(_heap[right]) < 0)
                next = right;

            //�� �۾��� ��� ������ next���� ���� ���� ������ �ڽ��� ���� �����༮�� ��.
            if (next == now)
                break;

            (_heap[now], _heap[next]) = (_heap[next], _heap[now]); //��ȯ

            now = next;
        }

        return ret;
    }

    public T Peek()
    {
        return _heap.Count == 0 ? default : _heap[0];
    }
}