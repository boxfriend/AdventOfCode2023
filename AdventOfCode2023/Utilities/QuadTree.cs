namespace AdventOfCode2023.Utilities;

internal class QuadTree
{
    private readonly int _capacity;
    private readonly int _maxDepth;

    private readonly int _currentDepth;

    private readonly QuadTree? _parent;

    private readonly QuadTree[] _children = new QuadTree[4];

    private readonly List<ISpatialObject> _objects = new();

    public Bounds Bounds { get; init; }

    private QuadTree (Bounds bounds, int capacity, int maxDepth, int currentDepth, QuadTree? parent)
    {
        Bounds = bounds;
        _capacity = capacity;
        _maxDepth = maxDepth;
        _currentDepth = currentDepth;
        _parent = parent;
    }

    public QuadTree (Bounds bounds, int capacity, int maxDepth) : this(bounds, capacity, maxDepth, 0, null) { }

    public bool TryInsert (ISpatialObject obj)
    {
        if (!Bounds.AreIntersecting(Bounds, obj.Bounds))
            return false;

        var node = GetFittingNode(obj.Bounds);

        if (node is null)
            return false;

        node.Insert(obj);

        return true;
    }

    private void Insert (ISpatialObject obj)
    {
        _objects.Add(obj);

        if (_objects.Count >= _capacity && _currentDepth < _maxDepth)
            Redistribute();
    }

    public bool TryRemove (ISpatialObject obj)
    {
        if (!Bounds.AreIntersecting(Bounds, obj.Bounds))
            return false;

        var node = GetFittingNode(obj.Bounds);
        return node!.Remove(obj);
    }

    private bool Remove (ISpatialObject obj) => _objects.Remove(obj);

    private QuadTree? GetFittingNode (Bounds bounds)
    {
        if (!Bounds.FullyContained(bounds))
            return null;

        foreach (var child in _children)
        {
            if (child is null)
                continue;
            var node = child.GetFittingNode(bounds);
            if (node is not null)
                return node;
        }

        return this;
    }

    public QuadTree? GetFittingNode (Point point)
    {
        if (!Bounds.PointInside(point))
            return null;

        foreach (var child in _children)
        {
            if (child is null)
                continue;
            var node = child.GetFittingNode(point);
            if (node is not null)
                return node;
        }

        return this;
    }

    private void Redistribute ()
    {
        if (_children.Any(x => x is not null))
            return;

        for (var i = 0; i < _children.Length; i++)
        {
            var halfExtents = Bounds.HalfExtents / 2;
            var center = (Directions[i] * halfExtents) + Bounds.Center;
            _children[i] = new QuadTree(new Bounds(center, halfExtents), _capacity, _maxDepth, _currentDepth + 1, this);
        }

        for (var i = _objects.Count - 1; i >= 0; i--)
        {
            foreach (var child in _children)
            {
                if (child.TryInsert(_objects[i]))
                {
                    _objects.RemoveAt(i);
                    break;
                }
            }
        }
    }

    public int Count ()
    {
        var count = 0;
        foreach (var child in _children)
        {
            if (child is not null)
                count += child.Count();
        }

        return count + _objects.Count;
    }

    public HashSet<ISpatialObject> GetCollisions(Point obj, HashSet<ISpatialObject> set)
    {
        //var set = new HashSet<ISpatialObject>();
        CheckCollisions(obj, set);
        CheckChildNodeCollisions(obj, set);
        return set;
    }

    public HashSet<ISpatialObject> GetCollisions (ISpatialObject obj)
    {
        var set = new HashSet<ISpatialObject>();
        CheckCollisions(obj, set);
        CheckChildNodeCollisions(obj, set);
        return set;
    }

    private void CheckCollisions (Point obj, HashSet<ISpatialObject> collisions)
    {
        foreach (var col in _objects)
        {
            if (col.Bounds.PointInside(obj))
                collisions.Add(col);
        }
    }

    private void CheckCollisions (ISpatialObject obj, HashSet<ISpatialObject> collisions)
    {
        foreach (var col in _objects)
        {
            if (col.Bounds.Intersects(obj.Bounds))
                collisions.Add(col);
        }
    }

    private void CheckParentNodeCollisions (Point obj, HashSet<ISpatialObject> collisions)
    {
        var node = _parent;
        while (node != null)
        {
            node.CheckCollisions(obj, collisions);
            node = node._parent;
        }
    }

    private void CheckParentNodeCollisions (ISpatialObject obj, HashSet<ISpatialObject> collisions)
    {
        var node = _parent;
        while (node != null)
        {
            node.CheckCollisions(obj, collisions);
            node = node._parent;
        }
    }

    private void CheckChildNodeCollisions (Point obj, HashSet<ISpatialObject> collisions)
    {
        foreach (var c in _children)
        {
            if (c is null || !c.Bounds.PointInside(obj))
                continue;

            c.CheckCollisions(obj, collisions);
            c.CheckChildNodeCollisions(obj, collisions);
        }
    }

    private void CheckChildNodeCollisions (ISpatialObject obj, HashSet<ISpatialObject> collisions)
    {
        foreach (var c in _children)
        {
            if (c is null || !c.Bounds.Intersects(obj.Bounds))
                continue;

            c.CheckCollisions(obj, collisions);
            c.CheckChildNodeCollisions(obj, collisions);
        }
    }

    public static Point[] Directions = [ new Point(-1, 1), new Point(1, 1), new Point(1, -1), new Point(-1, -1) ];
}
