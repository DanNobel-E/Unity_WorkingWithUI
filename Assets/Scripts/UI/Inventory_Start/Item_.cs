using System;
[Serializable]
public class Item_ 
{
    public CellItem_ Owner { get; protected set; }

    public string PrefabPath;
    public string SpritePath;
    //  - 3D obj Resources path to load @ runtime
    //  - Sprite Resources path to load @ runtime

    public void SetOwner(CellItem_ cell)
    {
        Owner = cell;
    }
}
