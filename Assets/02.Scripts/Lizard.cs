using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lizard : MonoBehaviour
{
    //https://www.youtube.com/watch?v=NE5j8YmJ5Ds&list=PLO-mt5Iu5TeZF8xMHqtT_DhAPKmjF6i3x&index=15
    //ui를 누르면 새새끼가 캐릭터 뒤에 뿅 나와야댐
    //그럼 새새끼 위치랑 새새끼 이미지를 정해야되고
    //캐릭터가 왼족 오른족 움직일때 새새끼가 캐릭터 기준 뒤에잇어야대
    //새새끼를 프리팹으로


    public GameObject prefab;
    public Transform tagetObject;


    public SpriteRenderer sr;
    public SpriteRenderer player;
    
    //캐릭터 오른쪽봤을때 새위치
    Vector3 rightPos = new Vector3(-0.5f, 0.5f, 0);
    //캐릭터 왼쪽봤을때 새위치
    Vector3 leftPos = new Vector3(-0.5f, 0.5f, 0);

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        player = GetComponentInParent<SpriteRenderer>();

    }
    //프리팹이 rightpos에 뿅 나와야댐

    //캐릭터가 왼쪽보면 프리팹이 leftpos에 가야되고 filp도 해야댐


    //내가 왼쪽을 보고있을때 lizard도 왼쪽을 봐야댐 위치는 flip기준


}
