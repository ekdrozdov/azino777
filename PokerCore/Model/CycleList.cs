using System;
using System.Collections.Generic;
using System.Text;

namespace PokerCore.Model
{
    public class CycleListNode
    {
        public int val { get; }
        public CycleListNode Next { get; set; }
        public CycleListNode(int _val, CycleListNode a)
        {
            val = _val;
            Next = a;
        }

        public CycleListNode()
        {

        }
    }

    public class CycleList
    {
        List<CycleListNode> _data;

        public CycleList()
        {
            _data = new List<CycleListNode>();
        }

        public void AddLast(int _val)
        {
            if (_data.Count != 0)
            {
                var a = new CycleListNode(_val, _data[0]);
                _data[_data.Count - 1].Next = a;
                _data.Add(a);
            }
            else
            {
                _data.Add(new CycleListNode(_val, new CycleListNode()));
            }
        }

        public void AddRange(List<int> a)
        {
            foreach (int el in a)
                AddLast(el);
        }

        public CycleListNode GetNode(int elem)
        {
            return _data.Find(x => x.val == elem);
        }
    }
}
