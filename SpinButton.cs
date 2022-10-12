using BigBoyEngine;
using Dcrew;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace FruitMachine2;

internal class SpinButton : Sprite {
    Vector2 _startPos;

    public SpinButton(int x, int y) : base(Content.Load<Texture2D>("spin_button")) {
        Translate(x, y);
        Tint = Color.DarkGray;
        _startPos = new Vector2(x, y);
    }

    public override void Update() {
        if (MouseHovering() && Arcade.Points > 0 && !Arcade.Began && Arcade.FinishedSlots == Arcade.Slots.Length) {
            Tint = Color.Lerp(Tint, Color.White, .25f);
            Scale = new Vector2(MathHelper.Lerp(Scale.X, 1.25f, .25f), MathHelper.Lerp(Scale.Y, 1.25f, .25f));
            Translate(0, MathF.Sin(Time.Total * 5) * .125f);
            Texture = Content.Load<Texture2D>("spin_button_pressed");
            if (Input.LMBPressed) {
                Arcade.Begin();
                Scale = new Vector2(.75f);
            }
        } else {
            Tint = Color.Lerp(Tint, Color.DarkGray, .25f);
            Scale = new Vector2(MathHelper.Lerp(Scale.X, 1, .1f), MathHelper.Lerp(Scale.Y, 1, .1f));
            Position = Vector2.Lerp(Position, _startPos, .25f);
            Texture = Content.Load<Texture2D>("spin_button");
        }
        base.Update();
    }
}