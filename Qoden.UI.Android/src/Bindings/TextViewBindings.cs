﻿using System;
using Android.Text;
using Android.Widget;
using Qoden.Binding;
using Qoden.Reflection;

namespace Qoden.UI
{
    public static class TextViewBindings
    {
        static readonly RuntimeEvent _TextChangedEvent = new RuntimeEvent(typeof(TextView), "TextChanged");
        public static RuntimeEvent TextChangedEvent
        {
            get
            {
                if (LinkerTrick.False)
                {
                    new TextView(null).TextChanged += (sender, e) => {};
                }
                return _TextChangedEvent;
            }
        }

        public static readonly IPropertyBindingStrategy TextChangedBinding = new EventHandlerBindingStrategy<TextChangedEventArgs>(TextChangedEvent);

        public static IProperty<string> TextProperty(this IQView<TextView> view)
        {
            return view.PlatformView.TextProperty();
        }

        public static IProperty<string> TextProperty(this TextView view)
        {
            return view.GetProperty(_ => _.Text, TextChangedBinding);
        }

        static readonly RuntimeEvent _EditorActionEvent = new RuntimeEvent(typeof(TextView), "EditorAction");
        public static RuntimeEvent EditorActionEvent
        {
            get
            {
                if (LinkerTrick.False)
                {
                    new TextView(null).EditorAction += (sender, e) => {};
                }
                return _EditorActionEvent;
            }
        }

        public static EventHandlerSource<T> EditorActionTarget<T>(this IQView<T> view)
            where T : TextView
        {
            return view.PlatformView.EditorActionTarget();
        }

        public static EventHandlerSource<T> EditorActionTarget<T>(this T view)
            where T : TextView
        {
            return new EventHandlerSource<T>(EditorActionEvent, view)
            {
                ParameterExtractor = (sender, args) => { return ((TextView.EditorActionEventArgs)args).ActionId; } // TODO: pass both args
            };
        }
    }
}
