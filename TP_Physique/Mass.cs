using Microsoft.Xna.Framework;

namespace TP_Physique
{
    public struct Mass
    {
        public float mass;
        public Vector2 velocity;
        public Vector2 position;
        public Vector2 acceleration;

        public Mass(float mass, Vector2 position)
        {
            this.mass = mass;
            this.position = position;
        }

    }
}
