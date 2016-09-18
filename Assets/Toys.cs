using UnityEngine;

public abstract class Toy
{
    public abstract Vector3 GetPosition();

    private readonly string toyName;
    public string ToyName { get { return this.toyName; } }

    protected Toy(string toyName)
    {
        this.toyName = toyName;
    }
}

public class NamedObjToy : Toy
{
    public override Vector3 GetPosition()
    {
        var lpObj = GameObject.Find(this.ToyName);
        return lpObj.transform.position;
    }

    public NamedObjToy(string name) : base(name)
    {
    }
}

public class PathObjToy : Toy
{
    private string rootObjName;
    private string[] objPath;

    public override Vector3 GetPosition()
    {
        var lpObj = GameObject.Find(rootObjName);
        var currTransform = lpObj.transform;
        foreach (var pathComponent in objPath)
        {
            currTransform = currTransform.FindChild(pathComponent);
        }
        return currTransform.position;
    }

    public PathObjToy(string toyName, string rootObjName, string[] objPath) : base(toyName)
    {
        this.rootObjName = rootObjName; this.objPath = objPath;
    }
}

