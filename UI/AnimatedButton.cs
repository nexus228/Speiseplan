using CommunityToolkit.Maui.Behaviors;
using Speiseplan.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Speiseplan.UI
{
    public class AnimatedButton : Button
    {
        public AnimatedButton()
        {
           
            Behaviors.Add(new TouchBehavior
            {
                PressedScale = 0.95,
                PressedOpacity = 0.7
            });
            Logger.Info("AnimatedButton created");
        }
    }
}
