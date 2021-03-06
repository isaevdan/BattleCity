﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnimatedSprites.Utils
{
    static class Collisions
    {
        public static List<Sprite> Tanks;
        public static Rectangle GameWindow { get { return SpriteUtils.GameWindow; } }
        public static void ReleaseCollision(Sprite firstSprite, Sprite secondSprite)
        {
            if (firstSprite is Missile)
                ReleaseCollision(firstSprite as Missile, secondSprite);
            else if (secondSprite is Missile)
                ReleaseCollision(secondSprite as Missile, firstSprite);
        }

        public static void ReleaseCollision(Missile missile, Sprite sprite)
        {
            missile.Destroy();
            if (missile.TeamNumber != sprite.TeamNumber || sprite is Missile)
                sprite.Destroy();
        }

        public static bool IsOutOfBounds(Rectangle spritRectangle, Rectangle bounds)
        {
            return (spritRectangle.X < 0) ||
                (spritRectangle.Y < 0) ||
                (spritRectangle.X + spritRectangle.Width > bounds.Width) ||
                (spritRectangle.Y + spritRectangle.Height > bounds.Height);
        }

        public static List<Direction> GetAllowedDirections(Sprite spr, int speed)
        {
            Rectangle leftRect = spr.collisionRect;
            Rectangle upRect = spr.collisionRect;
            Rectangle downRect = spr.collisionRect;
            Rectangle rightRect = spr.collisionRect;
            upRect.Offset(0, -speed);
            downRect.Offset(0, speed);
            rightRect.Offset(speed, 0);
            leftRect.Offset(-speed, 0);

            List<Direction> direcs = new List<Direction>();
            direcs.Add(Direction.Down);
            direcs.Add(Direction.Left);
            direcs.Add(Direction.Right);
            direcs.Add(Direction.Up);

            if (IsOutOfBounds(upRect, GameWindow))
                direcs.Remove(Direction.Up);
            if (IsOutOfBounds(rightRect, GameWindow))
                direcs.Remove(Direction.Right);
            if (IsOutOfBounds(leftRect, GameWindow))
                direcs.Remove(Direction.Left);
            if (IsOutOfBounds(downRect, GameWindow))
                direcs.Remove(Direction.Down);

            foreach (Sprite w in Tanks)
            {
                if (direcs.Count == 0)
                    break;
                if (w is Missile || spr == w)
                    continue;

                if (direcs.Contains(Direction.Up) && upRect.Intersects(w.collisionRect))
                    direcs.Remove(Direction.Up);
                if (direcs.Contains(Direction.Right) && rightRect.Intersects(w.collisionRect))
                    direcs.Remove(Direction.Right);
                if (direcs.Contains(Direction.Left) && leftRect.Intersects(w.collisionRect))
                    direcs.Remove(Direction.Left);
                if (direcs.Contains(Direction.Down) && downRect.Intersects(w.collisionRect))
                    direcs.Remove(Direction.Down);
            }

            if (direcs.Contains(Direction.Up) && MapInfo.Intersects(upRect) != null)
                direcs.Remove(Direction.Up);
            if (direcs.Contains(Direction.Right) && MapInfo.Intersects(rightRect) != null)
                direcs.Remove(Direction.Right);
            if (direcs.Contains(Direction.Left) && MapInfo.Intersects(leftRect) != null)
                direcs.Remove(Direction.Left);
            if (direcs.Contains(Direction.Down) && MapInfo.Intersects(downRect) != null)
                direcs.Remove(Direction.Down);

            return direcs;
        }

        //public static Direction GetUserVisibleDirection(AITank tank, List<Direction> allowedDirections)
        //{
        //    if (Tanks != null)
        //    {
        //        foreach (var item in Tanks)
        //        {
        //            if (!(item is UserControlledTank))
        //                continue;

        //            if ((allowedDirections.Contains(Direction.Left)) && tank.LeftRectangle.Intersects(item.collisionRect))
        //                return Direction.Left;
        //            else if ((allowedDirections.Contains(Direction.Right)) && tank.RightRectangle.Intersects(item.collisionRect))
        //                return Direction.Right;
        //            else if ((allowedDirections.Contains(Direction.Up)) && tank.UpRectangle.Intersects(item.collisionRect))
        //                return Direction.Up;
        //            else if ((allowedDirections.Contains(Direction.Down)) && tank.DownRectangle.Intersects(item.collisionRect))
        //                return Direction.Down;
        //        }
        //    }

        //    return Direction.None;
        //}

        //public static Vector2 GetUserPosition(Vector2 aiPosition)
        //{
        //    Vector2 closest = aiPosition;
        //    float minDistance = float.MaxValue;
        //    float distance = 0.0f;

        //    foreach (Sprite sprite in Tanks)
        //    {
        //        if (sprite is UserControlledTank)
        //        {
        //            Vector2 currentSpritePosition = sprite.GetPosition;
        //            distance = (currentSpritePosition - aiPosition).Length();
        //            if (distance < minDistance)
        //            {
        //                minDistance = distance;
        //                closest = currentSpritePosition;
        //            }
        //        }
        //    }

        //    return closest;
        //}
    }
}
