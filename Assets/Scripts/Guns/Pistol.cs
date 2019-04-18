using UnityEngine;

public class Pistol : Gun
{
	private void DoFire(Facing facing)
	{
		pc.ChangeFacing(facing);
		audioSource.Play();
		Bullet go = CreateBullet(pc.transform);
		go.SetSpeed(speed);
		ResetTimeToFire();
	}

    private void DoFire(Vector3 aimPos) 
    {
        aimPos = aimPos - pc.transform.position;
        audioSource.Play();
        Bullet go = CreateBullet(pc.transform);
        go.ChangeFacing(aimPos);
        go.SetSpeed(speed);
        ResetTimeToFire();
    }

	public override void Fire(PlayerController pc)
	{
		timeBeforeFire -= Time.deltaTime;

		if (timeBeforeFire < 0.0f)
		{
            if (Input.GetKey(KeyCode.Mouse0))
                DoFire(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            else if (Input.GetKey(KeyCode.RightArrow))
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
