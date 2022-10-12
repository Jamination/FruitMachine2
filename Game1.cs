using BigBoyEngine;
using Microsoft.Xna.Framework;

namespace FruitMachine2;

public class Game1 : Core {

    protected override void Initialize() {
        Config("Fruit Machine", 512, 768, false);
        ClearColor = new Color(85, 0, 0);
        Scene = new Arcade();
        base.Initialize();
    }
}
