// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Collections.Generic;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osu.Game.Rulesets.Mods;
using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Rulesets.Osu.Objects;
using osu.Game.Rulesets.Osu.Objects.Drawables;

namespace osu.Game.Rulesets.Osu.Mods
{
    internal class OsuModRandomCS : Mod, IApplicableToDrawableHitObjects
    {
        public override string Name => "Random circle size";

        public override string Acronym => "RC";

        public override ModType Type => ModType.Fun;

        public override string Description => "Some circles are eating too many mcdonalds happy meals.";

        public override double ScoreMultiplier => 1;

        public void ApplyToDrawableHitObjects(IEnumerable<DrawableHitObject> drawables)
        {
            foreach (var drawable in drawables) {
                switch (drawable) {
                    case DrawableSpinner _:
                        continue;

                    default:
                        drawable.ApplyCustomUpdateState += ApplyCustomState;
                        break;
                }
            }
        }

        protected virtual void ApplyCustomState(DrawableHitObject drawable,ArmedState state)
        {
            var h = (OsuHitObject)drawable.HitObject;

            // apply grow effect
            switch (drawable) {
                case DrawableSliderHead _:
                case DrawableSliderTail _:
                    // special cases we should *not* be scaling.
                    break;

                case DrawableSlider _:
                case DrawableHitCircle _: {
                    Random r = new Random();
                    double randomsize = r.NextDouble() * 2;

                    while (randomsize < .5 || randomsize > 1.5) {
                        randomsize = r.NextDouble();
                    }

                    using (drawable.BeginAbsoluteSequence(h.StartTime - h.TimePreempt,true))
                        drawable.ScaleTo(float.Parse(randomsize.ToString())).Then().ScaleTo(float.Parse(randomsize.ToString()),h.TimePreempt,Easing.OutSine);
                    break;
                }
            }

            // remove approach circles
            switch (drawable) {
                case DrawableHitCircle circle:
                    // we don't want to see the approach circle
                    using (circle.BeginAbsoluteSequence(h.StartTime - h.TimePreempt,true))
                        //circle.ApproachCircle.Hide();
                        break;
            }
        }
    }
}
