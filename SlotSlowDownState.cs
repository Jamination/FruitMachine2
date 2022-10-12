using BigBoyEngine;
using Microsoft.Xna.Framework;
using System;

namespace FruitMachine2;

internal class SlotSlowDownState : State<Slot> {
    public override void Enter() {
    }

    public override void Exit() {
    }

    public override State<Slot> Process() {
        Controller.Speed = MathHelper.Lerp(Controller.Speed, 0, .025f);

        if (MathF.Floor(Controller.Speed) <= 0) {
            Controller.Speed = 0;
            Arcade.FinishSlot();
            return null;
        }
        return this;
    }
}