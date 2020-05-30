using Godot;
using System;

public class SpritePPartDraw : Sprite // sprite player part draw
{

    private Root root;
    private Vector2 startPos;
    public Vector2 partSize = new Vector2(525, 700);

    public override void _Ready()
    {
        root = (Root)GetNode("/root/root/");
        RegionEnabled = true;
        startPos = this.Position;
    }

    public override void _Process(float delta)
    {
        if (root.activePlayer != null)
        {
            Rect2 r = new Rect2();
            r.Position = root.activePlayer.Position - startPos - Scale * partSize / 2;
            r.Position = new Vector2(Mathf.Max(r.Position.x, 0), Mathf.Max(r.Position.y, 0));
            r.Position /= Scale;
            this.Position = root.activePlayer.Position - Scale * partSize / 2;
            r.Size = partSize;
            RegionRect = r;
        }
    }

}
