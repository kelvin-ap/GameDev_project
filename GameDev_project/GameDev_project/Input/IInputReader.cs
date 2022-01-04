using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace GameDev_project.Input
{
    interface IInputReader
    {
        Vector2 ReadInput();
    }
}
