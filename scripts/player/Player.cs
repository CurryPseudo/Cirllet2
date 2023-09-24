using System;
using Cirllet2;
using Godot;

public partial class Player : Node2D
{
    [Export] private float _speed = 1;
    private Vector2I _direction = new Vector2I(1, 0);
    public event Action<Vector2I> OnDirectionChanged;
    public event Action<bool> OnMove;

    bool isMoving = false;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }

    public override void _PhysicsProcess(double delta)
    {
        bool currentIsMoving = false;
        foreach (var (action_prefix, direction) in new[]
                 {
                     ("left", new Vector2I(-1, 0)), ("right", new Vector2I(1, 0)), ("up", new Vector2I(0, -1)),
                     ("down", new Vector2I(0, 1))
                 })
        {
            var action = $"{action_prefix}_move";
            if (Input.IsActionPressed(action))
            {
                currentIsMoving = true;
                Position = Position + direction.ToVector2() * (float)delta * _speed;
                if (direction != _direction)
                {
                    _direction = direction;
                    OnDirectionChanged?.Invoke(direction);
                }

                break;
            }
        }

        if (currentIsMoving != isMoving)
        {
            isMoving = currentIsMoving;
            OnMove?.Invoke(isMoving);
        }
    }
}