using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Diagnostics;

namespace PopupManager.Popuppy
{
    public partial class Popuppy : UserControl
    {
        // OK/Cancel handlers
        public EventHandler OnAccept;
        public EventHandler OnCancel;

        public static readonly DependencyProperty ParamsProperty =
            DependencyProperty.Register("Params", typeof(TextParams), typeof(Popuppy), null);

        // Initializer
        public TextParams Params
        {
            get
            {
                return (TextParams) GetValue(ParamsProperty);
            }
            set
            {
                SetValue(ParamsProperty, value);
            }
        }
        public Popuppy() : base()
        {
            InitializeComponent();
        }

        // Animation controllers
        public void SpringAnimation(Double x)
        {
            SpringFrom.Value = Mover.X;
            SpringVelocity.Value = x;
            Spring.Begin();
        }
        public void RemoveAnimation(Panel par, int way)
        {
            Move(Mover.X, way, 0, 0);
            ManipulationDelta -= Delta;
            ManipulationStarted -= Started;
            ManipulationCompleted -= Completed;
        }
        public void Move(Double xFrom, Double xTo, Double yFrom, Double yTo)
        {
            XSlideAnim.From = xFrom;
            YSlideAnim.From = yFrom;
            XSlideAnim.To = xTo;
            YSlideAnim.To = yTo;
            Slide.Begin();
        }

        // Drag callbacks
        private void Started(object s, ManipulationStartedEventArgs e)
        {
            if (Mover.X != 0)
            {
                var span = TimeSpan.FromMilliseconds(15);
                Spring.SeekAlignedToLastTick(Spring.GetCurrentTime() + span);
                Slide.SeekAlignedToLastTick(Slide.GetCurrentTime() + span);
                Spring.Pause();
                Slide.Pause();
            }
        }
        private void Completed(object s, ManipulationCompletedEventArgs e)
        {
            var sDist = e.FinalVelocities.LinearVelocity.X / 12 + Mover.X;
            var noSpring = Mover.X < 0 && sDist < 0 && sDist > Mover.X || Mover.X > 0 && sDist > 0 && sDist < Mover.X;
            if (sDist > ActualWidth / 1.5) OnAccept(s, e); else if (sDist < -ActualWidth / 1.5) OnCancel(s, e); 
            else if (!e.IsInertial || noSpring) Move(Mover.X, 0, 0, 0); else SpringAnimation(sDist);
        }
        private void Delta(object s, ManipulationDeltaEventArgs e)
        {
            Mover.X += e.DeltaManipulation.Translation.X;
        }

        // Give user a cue on tap attempt
        private void TipAccept(object s, EventArgs e)
        {
            SpringAnimation(40);
        }
        private void TipCancel(object s, EventArgs e)
        {
            SpringAnimation(-40);
        }
    }
}
