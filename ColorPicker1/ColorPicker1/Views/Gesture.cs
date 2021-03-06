﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Commands;
using Xamarin.Forms;
using System.Diagnostics;
using ColorPicker1.Models;

namespace ColorPicker1.Views
{
    public static class Gesture
    {
        public static readonly BindableProperty TappedProperty = BindableProperty.CreateAttached("Tapped", typeof(DelegateCommand<SimplePoint>), typeof(Gesture), null, propertyChanged: CommandChanged);

        public static DelegateCommand<SimplePoint> GetCommand(BindableObject view)
        {
            return (DelegateCommand<SimplePoint>)view.GetValue(TappedProperty);
        }

        public static void SetTapped(BindableObject view, DelegateCommand<SimplePoint> value)
        {
            view.SetValue(TappedProperty, value);
        }

        private static void CommandChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var view = bindable as View;
            if (view != null)
            {
                var effect = GetOrCreateEffect(view);
            }
        }

        private static GestureEffect GetOrCreateEffect(View view)
        {
            var effect = (GestureEffect)view.Effects.FirstOrDefault(e => e is GestureEffect);
            if (effect == null)
            {
                effect = new GestureEffect();
                view.Effects.Add(effect);
            }
            return effect;
        }

        class GestureEffect : RoutingEffect
        {
            public GestureEffect() : base("DigiPug.TapWithPositionGestureEffect")
            {
                Debug.WriteLine($"**** {this.GetType().Name}.{nameof(GestureEffect)}:  ctor");
            }
        }
    }
}
