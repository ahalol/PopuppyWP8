using System.Windows.Controls.Primitives;
using System.Collections.Generic;
using Microsoft.Phone.Controls;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows;
using System.Linq;
using System;

namespace PopupManager.Popuppy
{
    public class TextParams
    {
        public string Message { get; set; }
        public string Cancel { get; set; }
        public string Ok { get; set; }
    }
    public static class Manager
    {
        public static Popuppy ActiveDrag = null;
        public static Popup ActivePopup = new Popup { IsOpen = false };
        public static void Show(TextParams texts, EventHandler Ok, EventHandler Cancel)
        {
            // Init objects
            int w = (int) App.RootFrame.ActualWidth;
            ActiveDrag = new Popuppy { Params = texts, IsEnabled = true };
            PopupBase dragBase = new PopupBase { Width = w, Height = App.RootFrame.ActualHeight };
            if (!ActivePopup.IsOpen)
            { 
                // Set popup
                ActivePopup.Child = dragBase;
                ActivePopup.IsOpen = true;

                // Perform initial animations
                ActiveDrag.Loaded += (s, e) => ActiveDrag.Move(0, 0, -ActiveDrag.Holder.ActualHeight, 0);
                VisualStateManager.GoToState(control: dragBase, stateName: "Show", useTransitions: true);
                dragBase.Container.Children.Add(value: ActiveDrag);
                
                // Create remove handler and wire up events
                EventHandler PopupRemoveAnimation = delegate
                {
                    ActiveDrag.Slide.Completed += (s, e) => ActivePopup.IsOpen = false;
                    VisualStateManager.GoToState(dragBase, "Remove", true);
                    dragBase.IsHitTestVisible = false;
                };

                ActiveDrag.OnCancel += (s, e) => ActiveDrag.RemoveAnimation(dragBase.Container, -w);
                ActiveDrag.OnAccept += (s, e) => ActiveDrag.RemoveAnimation(dragBase.Container, w);
                ActiveDrag.OnCancel += PopupRemoveAnimation;
                ActiveDrag.OnAccept += PopupRemoveAnimation;
                ActiveDrag.OnCancel += Cancel;
                ActiveDrag.OnAccept += Ok;
            }
        }
    }
}
