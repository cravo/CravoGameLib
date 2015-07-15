using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CravoGameLib.TileMap
{
    public class Camera
    {
        public Vector2 Position;

        public Camera()
        {
            Position = Vector2.Zero;
        }

        public Camera(Vector2 position)
        {
            Position = position;
        }
    }
}
