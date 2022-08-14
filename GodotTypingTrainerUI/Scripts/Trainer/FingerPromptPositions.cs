using Godot;
using System;
using System.Collections.Generic;
using TypingTraining.FingerPointer;

namespace GodotTypingTrainerUI.Scripts.Trainer
{
    public class FingerPromptPositions : Control
    {
        private Dictionary<Fingers, Vector2> _fingersPositions;

        public override void _Ready()
        {
            _fingersPositions = new();
            FillPositionsDictionary(_fingersPositions);
        }

        public Vector2 GetFingerPromptPosition(Fingers finger)
        {
            Vector2 position = new();
            _fingersPositions.TryGetValue(finger, out position);

            return position;
        }

        private void FillPositionsDictionary(Dictionary<Fingers, Vector2> fingersPositions)
        {
            fingersPositions.Add(Fingers.LeftThumb, GetNode<Position2D>("LeftThumbFinger").Position);
            fingersPositions.Add(Fingers.LeftIndex, GetNode<Position2D>("LeftIndexFinger").Position);
            fingersPositions.Add(Fingers.LeftMiddle, GetNode<Position2D>("LeftMiddleFinger").Position);
            fingersPositions.Add(Fingers.LeftRing, GetNode<Position2D>("LeftRingFinger").Position);
            fingersPositions.Add(Fingers.LeftLittle, GetNode<Position2D>("LeftLittleFinger").Position);

            fingersPositions.Add(Fingers.RightThumb, GetNode<Position2D>("RightThumbFinger").Position);
            fingersPositions.Add(Fingers.RightIndex, GetNode<Position2D>("RightIndexFinger").Position);
            fingersPositions.Add(Fingers.RightMiddle, GetNode<Position2D>("RightMiddleFinger").Position);
            fingersPositions.Add(Fingers.RightRing, GetNode<Position2D>("RightRingFinger").Position);
            fingersPositions.Add(Fingers.RightLittle, GetNode<Position2D>("RightLittleFinger").Position);
        }
    }
}