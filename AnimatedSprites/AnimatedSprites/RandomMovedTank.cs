﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnimatedSprites.GameSettings;
using AnimatedSprites.Utils;

namespace AnimatedSprites
{
    class RandomMovedTank : AITank
    {
        public RandomMovedTank(SpriteSettings settings, SpriteSettings missileSetting)
            : base(settings, missileSetting)
        {
            _ChangeDirectionTimeMax = 3000;
            _ChangeDirectionTimeMin = 100;
        }


        private Direction _MoveDirection = RandomUtils.GetRandomDirection();
        public override Direction CalculateMoveDirection()
        {

            if (_ChangeDirectionTime < 0 || !AllowedDirections.Contains(_MoveDirection))
            {
                if (AllowedDirections.Count > 0)
                    do
                    {
                        _MoveDirection = RandomUtils.GetRandomDirection();
                    } while (!AllowedDirections.Contains(_MoveDirection));
                else
                    _MoveDirection = Direction.None;
                SetChangeDirectionTime();
            }

            return _MoveDirection;
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Update(gameTime);
            _ChangeDirectionTime -= gameTime.ElapsedGameTime.Milliseconds;
            Fire();
        }


        int _ChangeDirectionTimeMax { get; set; }
        int _ChangeDirectionTimeMin { get; set; }
        int _ChangeDirectionTime { get; set; }
        private void SetChangeDirectionTime()
        {
            _ChangeDirectionTime = RandomUtils.GetRandomInt(_ChangeDirectionTimeMin, _ChangeDirectionTimeMax);
        }
    }
}
