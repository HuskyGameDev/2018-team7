/// <summary>
/// Stores direction constants. The directions are represented as integers for use 
/// when indexing into arrays, for example.
/// </summary>
public static class Direction
{
	public const int Back = 0;
	public const int Front = 1;
	public const int Left = 2;
	public const int Right = 3;
	public const int FrontLeft = 4;
	public const int FrontRight = 5;
	public const int BackLeft = 6;
	public const int BackRight = 7;

	/// <summary>
	/// Stores a rotation value that can be applied to a sprite's z-axis 
	/// to create a rotation that matches up with the direction constants.
	/// </summary>
	public static readonly float[] Rotations =
	{
		180.0f, 0.0f, 90.0f, 270.0f,
		45.0f, 315.0f, 135.0f, 225.0f
	};
};
