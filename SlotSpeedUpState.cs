using BigBoyEngine;
using Dcrew;
using Microsoft.Xna.Framework;

namespace FruitMachine2;

internal class SlotSpeedUpState : State<Slot> {
    float timer = 4;

    public override void Enter() {
    }

    public override void Exit() {
    }

    public override State<Slot> Process() {
        Controller.Speed = MathHelper.Lerp(Controller.Speed, Slot.MAX_SPEED, .1f);

        if (timer <= 0)
            return new SlotSlowDownState();

        timer -= Time.Delta;
        return this;
    }
}