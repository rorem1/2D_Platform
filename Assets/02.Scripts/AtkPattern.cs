using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtkPattern : MonoBehaviour
{
    /*
    맵 모서리나 가장자리에 생성기 만들고 걔내들이 1~3개으ㅣ 패턴으로 프리팹을 쏨
    
    직선발사
    추적발사
    샷건
     */
    [SerializeField] private GameObject bulletPrefab;

    private Transform target; // 플레이어로 설정해야댐
    [SerializeField] private float spawnRateMin = 0.5f;
    [SerializeField] private float spawnRateMax = 2.0f;
    private float spawnRate;

    [SerializeField] private int spreadCount = 4;
    [SerializeField] private float spreadDeg = 30.0f;

    public enum ShotPattern { targeting , Spread , Right, Left, Thunder}
    [SerializeField] private ShotPattern pattern = ShotPattern.targeting;

    private void Awake()
    {
        spawnRate = Random.Range(spawnRateMin, spawnRateMax);

        target = FindObjectOfType<PlayerController>()?.transform;

        StartCoroutine(AtkPatternCo());
    }
    private IEnumerator AtkPatternCo()
    {
        while (true)
        {
            pattern = (ShotPattern)Random.Range(0f, 4f);

            float atkDuration = Random.Range(1f, 6f);
            float duration = 0f;

            while(duration < atkDuration)
            {
                spawnRate = Random.Range(spawnRateMin, spawnRateMax);
                yield return new WaitForSeconds(spawnRate);

                Vector2 dir = (target.position - transform.position).normalized;
                switch (pattern)
                {
                    case ShotPattern.targeting:
                        Tageting(transform.position, dir, 5f);
                        break;
                    case ShotPattern.Spread:
                        SpreadShot(transform.position, dir, 5f);
                        break;
                    case ShotPattern.Right:
                        RightShot(transform.position, Vector2.right, 5f);
                        break;
                    case ShotPattern.Left:
                        LeftShot(transform.position, Vector2.left, 5f);
                        break;
                }
                duration += spawnRate;
            }
            float restTime = Random.Range(1f, 3f);
            yield return new WaitForSeconds(restTime);
        }
        
    }
    
    //private IEnumerator AtkPatternCo()
    //{
    //    while (true)
    //    {
    //        yield return new WaitForSeconds(spawnRate);

    //        Vector2 dir = (target.position - transform.position).normalized;
            
    //        switch (pattern)
    //        {
    //            case ShotPattern.targeting:
    //                Tageting(transform.position, dir, 5f);
    //                break;
    //            case ShotPattern.Spread:
    //                SpreadShot(transform.position, dir, 5f);
    //                break;
    //            case ShotPattern.Right:
    //                RightShot(transform.position, Vector2.right, 5f);
    //                break;
    //            case ShotPattern.Left:
    //                LeftShot(transform.position, Vector2.left, 5f);
    //                break;
    //        }
    //        spawnRate = Random.Range(spawnRateMin, spawnRateMax);
    //    }
    //}

    private void Tageting(Vector2 pos, Vector2 dir, float speed)
    {
        GameObject go = Instantiate(bulletPrefab, pos, Quaternion.identity);
        if(go.TryGetComponent<EnemyBullet>(out EnemyBullet bullet))
        {
            bullet.Shot(dir, speed);
        }
    }
    private void SpreadShot(Vector2 pos,Vector2 dir, float speed)
    {
        int half = spreadCount / 2;

        for(int i = -half; i<= half; i++)
        {
            float angle = i * spreadDeg;
            Vector2 newDir = Quaternion.Euler(0, 0, angle)*dir;
            Tageting(pos, newDir, speed);
        }
    }
    private void RightShot(Vector2 pos, Vector2 dir, float speed)
    {
        GameObject go = Instantiate(bulletPrefab, pos, Quaternion.identity);
        if (go.TryGetComponent<EnemyBullet>(out EnemyBullet bullet))
        {
            bullet.RightShot(dir, speed);
        }
    }
    private void LeftShot(Vector2 pos, Vector2 dir, float speed)
    {
        GameObject go = Instantiate(bulletPrefab, pos, Quaternion.identity);
        if (go.TryGetComponent<EnemyBullet>(out EnemyBullet bullet))
        {
            bullet.LeftShot(dir, speed);
        }
    }

}
