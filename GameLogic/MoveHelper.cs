using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml;

namespace Fia_med_krock.GameLogic
{
    /// <summary>
    /// MoveHelper class
    /// Takes care of moving the game pieces
    /// </summary>
    public static class MoveHelper
    {
        //Tar samma argument som 'movecar' funktionen
        public static void AnimateCarRight(Windows.UI.Xaml.Shapes.Rectangle carToMove, int columnNum)
        {
            //Animering för bilar
            //Skapar objekten som behövs för att göra animeringen
            carToMove.RenderTransform = new CompositeTransform();
            Storyboard oStoryboard = new Storyboard();
            DoubleAnimation oDoubleAnimation = new DoubleAnimation();

            //Värden som bestämmer vart elementet ska flyttas och hur snabbt.
            int startPosition = columnNum - 50;
            oDoubleAnimation.From = startPosition;
            oDoubleAnimation.To = startPosition + 50;
            oDoubleAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(100));
            //oDoubleAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(durationTime*200));

            //Applicerar animationen
            Storyboard.SetTarget(oDoubleAnimation, carToMove);
            //(CompositeTransform.TranslateY) flyttar vertikalt istället!
            Storyboard.SetTargetProperty(oDoubleAnimation, "(UIElement.RenderTransform).(CompositeTransform.TranslateX)");

            //Lägger till animationen i storyboard objektet
            oStoryboard.Children.Add(oDoubleAnimation);
            //Kör animering
            oStoryboard.Begin();
        }

        //Samma som de andra
        public static void AnimateCarLeft(Windows.UI.Xaml.Shapes.Rectangle carToMove, int columnNum)
        {
            carToMove.RenderTransform = new CompositeTransform();
            Storyboard oStoryboard = new Storyboard();
            DoubleAnimation oDoubleAnimation = new DoubleAnimation();
            int startPosition = columnNum + 50;
            oDoubleAnimation.From = startPosition;
            oDoubleAnimation.To = startPosition - 50;
            oDoubleAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(100));
            Storyboard.SetTarget(oDoubleAnimation, carToMove);
            Storyboard.SetTargetProperty(oDoubleAnimation, "(UIElement.RenderTransform).(CompositeTransform.TranslateX)");
            oStoryboard.Children.Add(oDoubleAnimation);
            oStoryboard.Begin();
        }

        //Samma som de andra
        public static void AnimateCarDown(Windows.UI.Xaml.Shapes.Rectangle carToMove, int columnNum)
        {
            carToMove.RenderTransform = new CompositeTransform();
            Storyboard oStoryboard = new Storyboard();
            DoubleAnimation oDoubleAnimation = new DoubleAnimation();
            int startPosition = columnNum - 50;
            oDoubleAnimation.From = startPosition;
            oDoubleAnimation.To = startPosition + 50;
            oDoubleAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(100));
            Storyboard.SetTarget(oDoubleAnimation, carToMove);
            Storyboard.SetTargetProperty(oDoubleAnimation, "(UIElement.RenderTransform).(CompositeTransform.TranslateY)");
            oStoryboard.Children.Add(oDoubleAnimation);
            oStoryboard.Begin();
        }

        //Samma som de andra
        public static void AnimateCarUp(Windows.UI.Xaml.Shapes.Rectangle carToMove, int columnNum)
        {
            carToMove.RenderTransform = new CompositeTransform();
            Storyboard oStoryboard = new Storyboard();
            DoubleAnimation oDoubleAnimation = new DoubleAnimation();
            int startPosition = columnNum + 50;
            oDoubleAnimation.From = startPosition;
            oDoubleAnimation.To = startPosition - 50;
            oDoubleAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(100));
            Storyboard.SetTarget(oDoubleAnimation, carToMove);
            Storyboard.SetTargetProperty(oDoubleAnimation, "(UIElement.RenderTransform).(CompositeTransform.TranslateY)");
            oStoryboard.Children.Add(oDoubleAnimation);
            oStoryboard.Begin();
        }

        public static void MoveCar(Windows.UI.Xaml.Shapes.Rectangle carToMove, Grid playBoard, int columnNum, int rowNum)
        {
            var parent = (Panel)carToMove.Parent;

            if (parent != null)
            {
                parent.Children.Remove(carToMove);
            }
            playBoard.Children.Add(carToMove);
            Grid.SetRow(carToMove, rowNum);
            Grid.SetColumn(carToMove, columnNum);
        }
    }
}
