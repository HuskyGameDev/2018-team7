using UnityEngine;

public class Sniper : Gun
{

    // tracks bullet count
    //public int bulletsRemaining = 0;

	protected override void Start()
	{
		fireRate = 1.5f;
		speed = 18.0f;
        bulletsRemaining = 12;
		damage = 10;
	}

	public override void Activate(PlayerController pc)
	{
		audioSource.clip = Resources.Load<AudioClip>("Sounds/Guns/Sniper Rifle");
	}

	private void DoFire(Facing facing)
	{
        // If out of bullets, don't fire
        if (bulletsRemaining <= 0)
            return;
		pc.ChangeFacing(facing);
		audioSource.Play();
		Bullet go = CreateBullet(pc.transform);
		go.SetSpeed(speed);
		ResetTimeToFire();
		bulletsRemaining--;
	}

	public override void Fire(PlayerController pc)
	{
		timeBeforeFire -= Time.deltaTime;

		if (timeBeforeFire < 0.0f && bulletsRemaining > 0)
		{
			if (Input.GetKey(KeyCode.RightArrow))
				DoFire(Facing.Right);
			else if (Input.GetKey(KeyCode.LeftArrow))
				DoFire(Facing.Left);
			else if (Input.GetKey(KeyCode.UpArrow))
				DoFire(Facing.Back);
			else if (Input.GetKey(KeyCode.DownArrow))
				DoFire(Facing.Front);
		}
	}
}
