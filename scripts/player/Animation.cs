using Cirllet2;
using Godot;

public partial class Animation : Node2D
{
	[Export]
	private AnimatedSprite2D _right;
	[Export]
	private AnimatedSprite2D _up;
	[Export]
	private AnimatedSprite2D _down;
	// This will never be 0
	private int horizontalDirection = 1;
	private Vector2I _direction = new Vector2I(1, 0);
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	public override void _EnterTree()
	{
		var player = this.FindParent<Player>();
		player.OnDirectionChanged += OnDirectionChanged;
	}

	public override void _ExitTree()
	{
		var player = this.FindParent<Player>();
		player.OnDirectionChanged -= OnDirectionChanged;
	}

	void OnDirectionChanged(Vector2I direction)
	{
		_direction = direction;
		if(direction.X != 0) horizontalDirection = direction.X;
		_right.Visible = direction.Y == 0;
		_right.FlipH = horizontalDirection == -1;
		_up.Visible = direction.Y == -1;
		_up.FlipH = horizontalDirection == -1;
		_down.Visible = direction.Y == 1;
		_down.FlipH = horizontalDirection == -1;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}

	public override void _PhysicsProcess(double delta)
	{
	}

}
