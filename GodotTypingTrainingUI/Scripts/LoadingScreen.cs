using Godot;

public class LoadingScreen : Node
{
    public override void _Ready()
    {
        AnimationPlayer animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        animationPlayer.CurrentAnimation = "LoadingAnimation";
        animationPlayer.Play();
    }
}
