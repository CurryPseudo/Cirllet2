using Cirllet2;
using Godot;

public enum AnimationState
{
    Idle,
    Walk,
}

public partial class Animation : Node2D
{
    [Export] private AnimatedSprite2D _right;
    [Export] private AnimatedSprite2D _up;
    [Export] private AnimatedSprite2D _down;
    [Export] private AnimatedSprite2D _right_walk;
    [Export] private AnimatedSprite2D _up_walk;

    [Export] private AnimatedSprite2D _down_walk;

    // This will never be 0
    private int horizontalDirection = 1;
    private Vector2I _direction = new Vector2I(1, 0);

    private AnimationState _state = AnimationState.Idle;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
    }

    public override void _EnterTree()
    {
        var player = this.FindParent<Player>();
        player.OnDirectionChanged += OnDirectionChanged;
        player.OnMove += OnMove;
    }

    public override void _ExitTree()
    {
        var player = this.FindParent<Player>();
        player.OnDirectionChanged -= OnDirectionChanged;
        player.OnMove -= OnMove;
    }

    public void OnStateEnter()
    {
        OnDirectionChanged(_direction);
    }

    public void OnStateExit()
    {
        switch (_state)
        {
            case AnimationState.Idle:
            {
                _right.Visible = false;
                _up.Visible = false;
                _down.Visible = false;
                break;
            }
            case AnimationState.Walk:
            {
                _right_walk.Visible = false;
                _up_walk.Visible = false;
                _down_walk.Visible = false;
                break;
            }
        }
    }

    public void SwitchState(AnimationState state)
    {
        OnStateExit();
        _state = state;
        OnStateEnter();
    }

    void OnDirectionChanged(Vector2I direction)
    {
        _direction = direction;
        if (direction.X != 0) horizontalDirection = direction.X;
        switch (_state)
        {
            case AnimationState.Idle:
            {
                _right.Visible = _direction.Y == 0;
                _right.FlipH = horizontalDirection == -1;
                _up.Visible = _direction.Y == -1;
                _up.FlipH = horizontalDirection == -1;
                _down.Visible = _direction.Y == 1;
                _down.FlipH = horizontalDirection == -1;
                break;
            }
            case AnimationState.Walk:
            {
                _right_walk.Visible = _direction.Y == 0;
                _right_walk.FlipH = horizontalDirection == -1;
                _up_walk.Visible = _direction.Y == -1;
                _up_walk.FlipH = horizontalDirection == -1;
                _down_walk.Visible = _direction.Y == 1;
                _down_walk.FlipH = horizontalDirection == -1;
                break;
            }
        }
    }

    void OnMove(bool isMoving)
    {
        if (isMoving)
        {
            SwitchState(AnimationState.Walk);
        }
        else
        {
            SwitchState(AnimationState.Idle);
        }
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }

    public override void _PhysicsProcess(double delta)
    {
    }
}