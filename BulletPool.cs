using MyGame;
using System.Collections.Generic;
using System.Linq;

public class BulletPool
{
    private List<Bullet> availableBullets = new List<Bullet>();
    private List<Bullet> inUseBullets = new List<Bullet>();

    public Bullet GetBullet(Vector2 position, float speed)
    {
        Bullet bullet;
        if (availableBullets.Count > 0)
        {
            bullet = availableBullets[0];
            availableBullets.RemoveAt(0);
            bullet.ResetBullet(position, speed);
        }
        else
        {
            bullet = new Bullet(position.x, position.y, speed);
        }

        inUseBullets.Add(bullet);
        return bullet;
    }

    public void ReleaseBullet(Bullet bullet)
    {
        inUseBullets.Remove(bullet);
        availableBullets.Add(bullet);
    }

    public void Update()
    {
        foreach (var bullet in inUseBullets.ToList())
        {
            if (!bullet.IsActive)
            {
                ReleaseBullet(bullet);
            }
        }
    }
}
