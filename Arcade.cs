using BigBoyEngine;
using Dcrew;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace FruitMachine2;

internal class Arcade : Node {
    public static int Points = 600, FinishedSlots = 7;
    public static bool Began;
    public static Slot[] Slots = new Slot[7];

    static Random _random = new Random();

    float _slotTimer;
    static int _slotIndex = 0;

    public static void Begin() {
        Began = true;
        FinishedSlots = 0;
        Points -= 200;
    }

    public override void Setup() {
        ScaleBy(4, 4);
        Camera.Instance.Centered = false;
        Add(new Sprite(Content.Load<Texture2D>("machine"))).Centered = false;
        Add(new SpinButton(100, 160));

        Slots[0] = Add(new Slot());
        Slots[0].Translate(-8 + 15, 64);

        Slots[1] = Add(new Slot());
        Slots[1].Translate(20 + 15, 64);

        Slots[2] = Add(new Slot());
        Slots[2].Translate(48 + 15, 64);

        Slots[3] = Add(new Slot());
        Slots[3].Translate(76 + 15, 64);

        Slots[4] = Add(new Slot());
        Slots[4].Translate(20, 92);

        Slots[5] = Add(new Slot());
        Slots[5].Translate(48, 92);

        Slots[6] = Add(new Slot());
        Slots[6].Translate(76, 92);
    }

    public static void FinishSlot() {
        FinishedSlots++;
        if (FinishedSlots == Slots.Length) {
            var types = new Slot.Fruit[Slots.Length];
            for (int i = 0; i < Slots.Length; i++) {
                var fruit = Slots[i].GetAt<Sprite>(Slots[i].GlobalPosition.X + 25, Slots[i].GlobalPosition.Y + 25);
                types[i] = (Slot.Fruit)Convert.ToInt32(fruit.Name);
            }

            var howManyOfEach = new Dictionary<Slot.Fruit, int>();
            foreach (var type in types) {
                howManyOfEach.TryAdd(type, 0);
                howManyOfEach[type]++;
            }

            foreach (var value in howManyOfEach.Values)
                Points += (value - 1) * 50;
        }
    }

    public override void Update() {
        if (_slotTimer > 0)
            _slotTimer -= Time.Delta;

        if (FinishedSlots != Slots.Length) {
            var strength = (Slots.Length - FinishedSlots) * 3;
            Camera.Instance.Position = Vector2.Lerp(Camera.Instance.Position,
                new Vector2((_random.NextSingle() * strength) - 8, (_random.NextSingle() * strength) - 8), .5f);
        } else {
            Camera.Instance.Position = Vector2.Lerp(Camera.Instance.Position, Vector2.Zero, .1f);
        }

        if (_slotTimer <= 0 && Began) {
            _slotTimer += .25f;
            Slots[_slotIndex].StateMachine.Run(new SlotSpeedUpState());
            _slotIndex++;
            if (_slotIndex >= Slots.Length) {
                Began = false;
                _slotIndex = 0;
            }
        }
        base.Update();
    }

    public override void Draw() {
        Children[0].Draw();
        Children[1].Draw();
        Core.SpriteBatch.DrawString(Content.Load<SpriteFont>("score"), $"{Points}", new Vector2(32, 616), Color.White);
        Core.SpriteBatch.End();
        Core.SpriteBatch.Begin(SpriteSortMode.Immediate, rasterizerState: new RasterizerState() { ScissorTestEnable = true },
            transformMatrix: Camera.Instance.View);
        foreach (var slot in Slots)
            slot.Draw();
    }
}
