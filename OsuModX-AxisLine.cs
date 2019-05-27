// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Collections.Generic;
using System.Linq;
using osu.Framework.Extensions.IEnumerableExtensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osu.Game.Rulesets.Mods;
using osu.Game.Rulesets.Objects;
using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Rulesets.Objects.Types;
using osu.Game.Rulesets.Osu.Objects;
using osu.Game.Rulesets.Osu.UI;
using osuTK;

namespace osu.Game.Rulesets.Osu.Mods
{
    internal class OsuModXLine : Mod, IApplicableToHitObject
    {
        public override string Name => "X-Axis Line";
        public override string Acronym => "XL";
        public override ModType Type => ModType.Fun;
        public override string Description => "A literal line on the X-Axis...";
        public override double ScoreMultiplier => 1;

        public void ApplyToHitObject(HitObject hitObject)
        {
            var osuObject = (OsuHitObject)hitObject;

            osuObject.Position = new Vector2(osuObject.Position.X,(osuObject.Y * 0) + 200);

            var slider = hitObject as Slider;

            if (slider == null)
                return;

            slider.NestedHitObjects.OfType<SliderTick>().ForEach(h => h.Position = new Vector2(h.Position.X,(h.Position.Y * 0) + 200));
            slider.NestedHitObjects.OfType<RepeatPoint>().ForEach(h => h.Position = new Vector2(h.Position.X,(h.Position.Y * 0) + 200));
        }
    }
}
