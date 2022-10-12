using BigBoyEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace FruitMachine2;

internal class Slot : Node {
    public const float MAX_SPEED = 6;

    static Random _random = new Random();

    public enum Fruit {
        Banana = 1,
        Cherries = 2,
        Lemon = 3,
        Watermelon = 4,
        Orange = 5,
        Grape = 6
    }

    public Fruit[] Fruities = new Fruit[6];

    public float Speed;

    public StateMachine<Slot> StateMachine;

    public Sprite Overlay;

    public override void Setup() {
        for (int i = 0; i < Fruities.Length; i++)
            Fruities[i] = (Fruit)_random.Next(Fruities.Length);

        for (int i = 0; i < Fruities.Length; i++) {
            var fruit = Add(new Sprite(Content.Load<Texture2D>($"fruits/{(int)Fruities[i] + 1}"), null, 0, (i * 30) - 150));
            fruit.Centered = false;
            fruit.Active = true;
            fruit.Name = $"{(int)Fruities[i] + 1}";
            fruit.ScaleBy(.15f, .15f);
        }
        Overlay = Add(new Sprite(Content.Load<Texture2D>("slotUI")));
        Overlay.Centered = false;

        StateMachine = new StateMachine<Slot>(this);
    }

    public override void Update() {
        for (int i = 1; i < Children.Length - 1; i++) {
            var child = Children[i];
            if (child == Overlay) continue;
            if (StateMachine.State != null)
                child.Translate(0, Speed);
            else
                child.Position = new Vector2(child.Position.X, MathHelper.Lerp(child.Position.Y, 
                    MathF.Floor(child.Position.Y / 30) * 30, .25f));

            if (child.Position.Y >= 30)
                child.Translate(0, -150);
        }

        StateMachine.Process();

        base.Update();
    }

    public override void Draw() {
        Core.Graphics.ScissorRectangle = new Rectangle((int)GlobalPosition.X + 2 - (int)Camera.Instance.Position.X, 
            (int)GlobalPosition.Y + 2 - (int)Camera.Instance.Position.Y, 116, 116);
        base.Draw();
    }
}
